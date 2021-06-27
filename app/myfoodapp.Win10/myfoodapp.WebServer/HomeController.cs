using myfoodapp.Business;
using myfoodapp.Business.Bench;
using myfoodapp.Business.Clock;
using myfoodapp.Business.Sensor;
using myfoodapp.Model;
using Restup.Webserver.Attributes;
using Restup.Webserver.Models.Contracts;
using Restup.Webserver.Models.Schemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel;

namespace myfoodapp.WebServer
{
    [RestController(InstanceCreationType.Singleton)]
    public class HomeController
    {
        [UriFormat("/measuresFile")]
        public IGetResponse GetMeasuresFile()
        {
            var databaseModel = DatabaseModel.GetInstance;
            var measures = new List<Measure>();

            var task = Task.Run(async () =>
            {
                measures = await databaseModel.GetLastWeeksMesures();
            });
            task.Wait();

            if (measures == null)
                return new GetResponse(
                  GetResponse.ResponseStatus.NotFound);

            var formatJson = measures.Select(f => new {
                date = String.Format("{0} {1}", f.captureDate.ToString("yyyy-MM-dd"), f.captureDate.ToString("HH:mm:ss")),
                sensor = f.sensor.name,
                value = f.value
            });

            return new GetResponse(
              GetResponse.ResponseStatus.OK, formatJson);
        }

        [UriFormat("/logsFile")]
        public IGetResponse GetLogsFile()
        {
            var databaseModel = LogModel.GetInstance;
            var logs = new List<Log>();

            var task = Task.Run(async () =>
            {
                logs = await databaseModel.GetLogsAsync();
            });
            task.Wait();

            if(logs == null)
                return new GetResponse(
                  GetResponse.ResponseStatus.NotFound);

            var formatJson = logs.Select(f => new {
                date = String.Format("{0} {1}", f.date.ToString("yyyy-MM-dd"), f.date.ToString("HH:mm:ss")),
                type = Enum.GetName(typeof(Log.LogType), f.type),
                description = f.description,
                stackCall = f.stackCall
            });

            return new GetResponse(
              GetResponse.ResponseStatus.OK, formatJson);
        }

        [UriFormat("/setPHTo7")]
        public IGetResponse SetPHTo7()
        {
            var logModel = LogModel.GetInstance;
            logModel.AppendLog(Log.CreateLog("Set pH to 7 started", Log.LogType.Information));

            var mesureBackgroundTask = MeasureBackgroundTask.GetInstance;
            mesureBackgroundTask.Completed += SetPhAtSevenBackgroundTask_Completed;
            mesureBackgroundTask.Stop();

            return new GetResponse(
              GetResponse.ResponseStatus.OK);
        }

        [UriFormat("/resetToFactory")]
        public IGetResponse ResetToFactory()
        {
            var logModel = LogModel.GetInstance;
            logModel.AppendLog(Log.CreateLog("Reset to factory started", Log.LogType.Information));

            var mesureBackgroundTask = MeasureBackgroundTask.GetInstance;
            mesureBackgroundTask.Completed += ResetHardwareBackgroundTask_Completed;
            mesureBackgroundTask.Stop();

            return new GetResponse(
              GetResponse.ResponseStatus.OK);
        }

        [UriFormat("/restartApp")]
        public IGetResponse RestartApp()
        {
            var logModel = LogModel.GetInstance;
            logModel.AppendLog(Log.CreateLog("Restart app started", Log.LogType.Information));

            var mesureBackgroundTask = MeasureBackgroundTask.GetInstance;
            mesureBackgroundTask.Completed += RestartAppBackgroundTask_Completed;
            mesureBackgroundTask.Stop();

            return new GetResponse(
              GetResponse.ResponseStatus.OK);
        }

        [UriFormat("/eraseLogs")]
        public IGetResponse EraseLogs()
        {
            var logModel = LogModel.GetInstance;
            logModel.AppendLog(Log.CreateLog("Erase logs started", Log.LogType.Information));

            var task = Task.Run(async () => { await logModel.ClearLog(); });
            task.Wait();

            logModel.AppendLog(Log.CreateLog("Erase logs ended", Log.LogType.Information));

            return new GetResponse(
              GetResponse.ResponseStatus.OK);
        }

        [UriFormat("/eraseMeasures")]
        public IGetResponse EraseMeasures()
        {
            var logModel = LogModel.GetInstance;
            var databaseModel = DatabaseModel.GetInstance;

            logModel.AppendLog(Log.CreateLog("Erase measures started", Log.LogType.Information));

            var task = Task.Run(async () => { await databaseModel.DeleteAllMesures(); });
            task.Wait();

            logModel.AppendLog(Log.CreateLog("Erase measures ended", Log.LogType.Information));

            return new GetResponse(
              GetResponse.ResponseStatus.OK);
        }

        [UriFormat("/performTest")]
        public IGetResponse PerformTest()
        {
            var logModel = LogModel.GetInstance;
            logModel.AppendLog(Log.CreateLog("Sigfox Test started", Log.LogType.Information));

            var mesureBackgroundTask = MeasureBackgroundTask.GetInstance;
            mesureBackgroundTask.Completed += PerformSigfoxTestBackgroundTask_Completed;
            mesureBackgroundTask.Stop();

            return new GetResponse(
              GetResponse.ResponseStatus.OK);
        }

        [UriFormat("/getusersettings")]
        public IGetResponse GetUserSettings()
        {
            var userSettingsModel = UserSettingsModel.GetInstance;
            UserSettings currentUserSettings = new UserSettings();

            var taskFile = Task.Run(async () => { currentUserSettings = await userSettingsModel.GetUserSettingsAsync(); });
            taskFile.Wait();

            var strVersion = GetAppVersion();

            currentUserSettings.PackageVersion = String.Format("v.{0}", strVersion);

            return new GetResponse(
              GetResponse.ResponseStatus.OK, currentUserSettings);
        }

        [UriFormat("/getclockdatetime")]
        public IGetResponse GetClockDateTime()
        {
            var clockManager = ClockManager.GetInstance;
            var captureDateTime = DateTime.Now;

            if (clockManager != null)
            {
                var taskClock = Task.Run(async () =>
                {
                    await clockManager.Connect();
                });
                taskClock.Wait();

                captureDateTime = clockManager.ReadDate();

                clockManager.Dispose();
            }

            return new GetResponse(
              GetResponse.ResponseStatus.OK, captureDateTime.ToString("g"));
        }

        [UriFormat("/setclock/{strDate}")]
        public IPutResponse SetClockDateTime(string strDate)
        {
            var mesureBackgroundTask = MeasureBackgroundTask.GetInstance;
            var logModel = LogModel.GetInstance;

            DateTime date = DateTime.Now; 

            try
            {
                date = DateTime.Parse(strDate);
            }
            catch (Exception ex)
            {
                logModel.AppendLog(Log.CreateErrorLog("Exception on Date Format", ex));
            }
             
            EventHandler handler = null;

            handler = (sender, eventArgs) =>
            {
                mesureBackgroundTask.Completed -= handler;
                try
                {
                    var clockManager = ClockManager.GetInstance;

                    if (clockManager != null)
                    {
                        var taskClock = Task.Run(async () =>
                        {
                            await clockManager.Connect();
                        });
                        taskClock.Wait();

                        clockManager.SetDate(date);

                        clockManager.Dispose();
                    }

                    mesureBackgroundTask.Run();

                }
                catch (Exception ex)
                {
                    logModel.AppendLog(Log.CreateErrorLog("Exception on Clock Settings", ex));
                }
                finally
                {
                    logModel.AppendLog(Log.CreateLog("Clock setting ended", Log.LogType.Information));
                }
            };

            mesureBackgroundTask.Completed += handler;
            mesureBackgroundTask.Stop();

            return new PutResponse(
              PutResponse.ResponseStatus.OK);
        }

        [UriFormat("/putusersettings?args={settings}&id={productionSiteId}")]
        public IPutResponse PutSettings(IEnumerable<bool> settings,string productionSiteId)
        {
            var mesureBackgroundTask = MeasureBackgroundTask.GetInstance;
            var userSettingsModel = UserSettingsModel.GetInstance;
            var logModel = LogModel.GetInstance;

            EventHandler handler = null;

            handler = (sender, eventArgs) =>
            {
                mesureBackgroundTask.Completed -= handler;
                try
                {
                    UserSettings currentUserSettings = new UserSettings();

                    var taskFile = Task.Run(async () => { currentUserSettings = await userSettingsModel.GetUserSettingsAsync(); });
                    taskFile.Wait();

                    var newUserSettings = new UserSettings();
                    var args = settings.ToList();

                    newUserSettings.isDebugLedEnable = args[0];
                    newUserSettings.isDiagnosticModeEnable = args[1];
                    newUserSettings.isSigFoxComEnable = args[2];
                    newUserSettings.isTempHumiditySensorEnable = args[3];
                    
                    newUserSettings.productionSiteId = productionSiteId;

                    newUserSettings.ACCESS_POINT_PWD = currentUserSettings.ACCESS_POINT_PWD;
                    newUserSettings.SSID = currentUserSettings.SSID;
                    newUserSettings.hubMessageAPI = currentUserSettings.hubMessageAPI;
                    newUserSettings.isScreenSaverEnable = currentUserSettings.isScreenSaverEnable;
                    newUserSettings.isSleepModeEnable = currentUserSettings.isSleepModeEnable;
                    newUserSettings.measureFrequency = currentUserSettings.measureFrequency;

                    var taskUserSync = Task.Run(async () =>
                    {
                        await userSettingsModel.SyncUserSettings(newUserSettings);
                    });
                    taskUserSync.Wait();

                    mesureBackgroundTask.Run();
                }
                catch (Exception ex)
                {
                    logModel.AppendLog(Log.CreateErrorLog("Exception on Save Settings", ex));
                }
                finally
                {
                    logModel.AppendLog(Log.CreateLog("Settings saved", Log.LogType.Information));
                }
            };

            mesureBackgroundTask.Completed += handler;
            mesureBackgroundTask.Stop();

            return new PutResponse(
              PutResponse.ResponseStatus.OK);
        }

        [UriFormat("data/type/{sensorType}")]
        public IGetResponse GetMeasures(string sensorType)
        {
            SensorTypeEnum? currentSensorType = null;

            switch (sensorType)
            {
                case "waterTemp" :
                    currentSensorType = SensorTypeEnum.waterTemperature;
                    break;
                case "pH" :
                    currentSensorType = SensorTypeEnum.ph;
                    break;
                case "airTemp":
                    currentSensorType = SensorTypeEnum.airTemperature;
                    break;
                case "airHum":
                    currentSensorType = SensorTypeEnum.humidity;
                    break;
                default:
                    break;
            }

            var databaseModel = DatabaseModel.GetInstance;
            string response = String.Empty;

            var listMes = new List<Measure>();

            var task= Task.Run(async () =>
            {
                if(currentSensorType != null)
                 listMes = await databaseModel.GetLastWeeksMesures(currentSensorType.Value);
            });
            task.Wait();

            listMes.OrderBy(m => m.captureDate).ToList().ForEach(m => {
                response += (String.Format("{0}T{1},{2}\n", m.captureDate.ToString("yyyy-MM-dd"), m.captureDate.ToString("HH:mm:ss"), m.value));
                                 });

            return new GetResponse(
              GetResponse.ResponseStatus.OK, response);
        }

        private void ResetHardwareBackgroundTask_Completed(object sender, EventArgs e)
        {
            var logModel = LogModel.GetInstance;
            var mesureBackgroundTask = MeasureBackgroundTask.GetInstance;
            mesureBackgroundTask.Completed -= ResetHardwareBackgroundTask_Completed;

            try
            {
                AtlasSensorManager.GetInstance.ResetSensorsToFactory();
                mesureBackgroundTask.Run();
            }
            catch (Exception ex)
            {
                logModel.AppendLog(Log.CreateErrorLog("Exception on Reset Hardware", ex));
            }
            finally
            {
                logModel.AppendLog(Log.CreateLog("Hardware reset ended", Log.LogType.Information));
            }
        }

        private void SetPhAtSevenBackgroundTask_Completed(object sender, EventArgs e)
        {
            var logModel = LogModel.GetInstance;
            var mesureBackgroundTask = MeasureBackgroundTask.GetInstance;
            mesureBackgroundTask.Completed -= SetPhAtSevenBackgroundTask_Completed;

            try
            {
                AtlasSensorManager.GetInstance.SetCalibration(SensorTypeEnum.ph, AtlasSensorManager.CalibrationType.Mid);
                mesureBackgroundTask.Run();
            }
            catch (Exception ex)
            {
                logModel.AppendLog(Log.CreateErrorLog("Exception on Set pH to 7", ex));
            }
            finally
            {
                logModel.AppendLog(Log.CreateLog("Set pH to 7 ended", Log.LogType.Information));
            }
        }

        private void RestartAppBackgroundTask_Completed(object sender, EventArgs e)
        {
            var logModel = LogModel.GetInstance;
            var mesureBackgroundTask = MeasureBackgroundTask.GetInstance;
            mesureBackgroundTask.Completed -= RestartAppBackgroundTask_Completed;

            try
            {
                Windows.ApplicationModel.Core.CoreApplication.Exit();
            }
            catch (Exception ex)
            {
                logModel.AppendLog(Log.CreateErrorLog("Exception on Restart App", ex));
            }
            finally
            {
                logModel.AppendLog(Log.CreateLog("App restart ended", Log.LogType.Information));
            }
        }

        private void PerformSigfoxTestBackgroundTask_Completed(object sender, EventArgs e)
        {
            var logModel = LogModel.GetInstance;
            var mesureBackgroundTask = MeasureBackgroundTask.GetInstance;
            mesureBackgroundTask.Completed -= PerformSigfoxTestBackgroundTask_Completed;

            try
            {
                var sigfoxIntegrationTest = new SigfoxIntegrationTest();
                sigfoxIntegrationTest.Run();

                mesureBackgroundTask.Run();
            }
            catch (Exception ex)
            {
                logModel.AppendLog(Log.CreateErrorLog("Exception on Sigfox Integration Test", ex));
            }
            finally
            {
                logModel.AppendLog(Log.CreateLog("[UTest Done] Sigfox Integration Test", Log.LogType.Information));
            }
        }

        public static string GetAppVersion()
        {
            Package package = Package.Current;
            PackageId packageId = package.Id;
            PackageVersion version = packageId.Version;

            return string.Format("{0}.{1}.{2}.{3}", version.Major, version.Minor, version.Build, version.Revision);

        }

    }
}
