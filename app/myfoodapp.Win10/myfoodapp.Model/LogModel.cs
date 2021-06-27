using MetroLog;
using MetroLog.Targets;
using myfoodapp.Business;
using myfoodapp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;

namespace myfoodapp.Model
{
    public class LogModel
    {
        private static readonly AsyncLock asyncLock = new AsyncLock();
        private StorageFolder folder = ApplicationData.Current.LocalFolder;

        private ILogger Logger;

        private static LogModel instance;

        public static LogModel GetInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LogModel();
                }
                return instance;
            }
        }

        private LogModel()
        {
            var streamFile = new StreamingFileTarget();
            streamFile.KeepLogFilesOpenForWrite = false;
            LogManagerFactory.DefaultConfiguration.AddTarget(LogLevel.Trace, LogLevel.Fatal, streamFile);
            Logger = LogManagerFactory.DefaultLogManager.GetLogger<LogModel>();
        }

        public async Task<List<Log>> GetLogsAsync()
        {          
            using (await asyncLock.LockAsync())
            {
                var metrologFolder = await folder.GetFolderAsync("MetroLogs");

                var files = await metrologFolder.GetFilesAsync();
                var lastLogFile = files.OrderByDescending(f => f.DateCreated).FirstOrDefault();

                if (lastLogFile != null)
                {
                    var readLines = await FileIO.ReadLinesAsync(lastLogFile);
                    List<Log> logs = new List<Log>();

                    //22 | 2016 - 10 - 21T12: 13:56.7589699 + 00:00 | INFO | 24 | LogModel | ORP online

                    try
                    {
                        readLines.ToList().ForEach(l =>
                        {
                            if(l.Split('|').Count() > 5)
                            {
                                var newLog = new Log();

                                if (l.Split('|')[2].Contains("INFO"))
                                    newLog.type = Log.LogType.Information;

                                if (l.Split('|')[2].Contains("TRACE"))
                                    newLog.type = Log.LogType.System;

                                if (l.Split('|')[2].Contains("WARN"))
                                    newLog.type = Log.LogType.Warning;

                                if (l.Split('|')[2].Contains("ERROR"))
                                    newLog.type = Log.LogType.Error;

                                newLog.Id = int.Parse(l.Split('|')[0]);
                                newLog.date = DateTime.Parse(l.Split('|')[1]);
                                newLog.description = l.Split('|')[5];

                                logs.Add(newLog);
                            }                         
                        });
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex.InnerException);
                    }

                    return logs.OrderByDescending(l => l.date).ToList();
                }
                return null;
            }
        }

        public void AppendLog(Log newLog)
        {
            try
            {
                switch (newLog.type)
                {
                    case Log.LogType.Information:
                        Logger.Info(newLog.description);
                        break;
                    case Log.LogType.Warning:
                        Logger.Warn(newLog.description);
                        break;
                    case Log.LogType.System:
                        Logger.Trace(newLog.description);
                        break;
                    case Log.LogType.Error:
                        Logger.Error(newLog.description, newLog.stackCall);
                        break;
                    default:
                        break;
                }              
            }
            catch (Exception ex)
            {
                Console.Write(ex.InnerException);
            }
        }

        public async Task ClearLog()
        {
            try
            {
                using (await asyncLock.LockAsync())
                {
                    var metrologFolder = await folder.GetFolderAsync("MetroLogs");

                    var files = await metrologFolder.GetFilesAsync();

                    files.ToList().ForEach(async (f) => await f.DeleteAsync());
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.InnerException);
            }
        }
    }
}
