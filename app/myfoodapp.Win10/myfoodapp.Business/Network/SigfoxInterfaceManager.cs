using myfoodapp.Model;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.SerialCommunication;
using Windows.Storage.Streams;

namespace myfoodapp.Business.Sensor
{
    public sealed class SigfoxInterfaceManager
    {
        private LogModel logModel = LogModel.GetInstance;
        public bool isInitialized = false;
        private SerialDevice serialPort;

        private CancellationTokenSource ReadCancellationTokenSource;

        private static SigfoxInterfaceManager instance;

        public static SigfoxInterfaceManager GetInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SigfoxInterfaceManager();
                }
                return instance;
            }
        }

        private DataWriter dataWriteObject;
        private DataReader dataReaderObject;

        private string sendMessageAcCommand = "AT$SF={0},1\r";

        private SigfoxInterfaceManager()
        {         
        }

        public async Task InitSensors()
        {
            if (isInitialized)
                return;

            var watch = Stopwatch.StartNew();

            try
            {
                string aqs = SerialDevice.GetDeviceSelector();
                var dis = await DeviceInformation.FindAllAsync(aqs);

                var sigfoxUART = dis.ToList().Where(d => d.Id.Contains(@"\\?\ACPI#BCM2")).FirstOrDefault();

                if (sigfoxUART == null)
                    logModel.AppendLog(Log.CreateLog("Sigfox device not found", Log.LogType.System));

                    try
                    {
                            var task = Task.Run(async () =>
                            {
                                try
                                {
                                    DeviceInformation entry = (DeviceInformation)sigfoxUART;

                                    logModel.AppendLog(Log.CreateLog("Associating Sigfox device", Log.LogType.System));

                                    serialPort = await SerialDevice.FromIdAsync(entry.Id);

                                    // Configure serial settings
                                    serialPort.WriteTimeout = TimeSpan.FromMilliseconds(1000);
                                    serialPort.ReadTimeout = TimeSpan.FromMilliseconds(1000);
                                    serialPort.BaudRate = 9600;
                                    serialPort.Parity = SerialParity.None;
                                    serialPort.StopBits = SerialStopBitCount.One;
                                    serialPort.DataBits = 8;
                                    serialPort.Handshake = SerialHandshake.None;

                                    // Create cancellation token object to close I/O operations when closing the device
                                    ReadCancellationTokenSource = new CancellationTokenSource();

                                    dataWriteObject = new DataWriter(serialPort.OutputStream);
                                    dataReaderObject = new DataReader(serialPort.InputStream);                               
                                }

                                catch (AggregateException ex)
                                {
                                    logModel.AppendLog(Log.CreateErrorLog("Exception on Sigfox Init", ex));
                                }
                            });

                            task.Wait();
                    }
                    catch (Exception ex)
                    {
                        logModel.AppendLog(Log.CreateErrorLog("Exception on Sigfox Init", ex));
                    }
               
            }
            catch (Exception ex)
            {
                logModel.AppendLog(Log.CreateErrorLog("Exception on Sigfox Init", ex));
            }
            finally
            {
                isInitialized = true;

                logModel.AppendLog(Log.CreateLog(String.Format("Sigfox Interface online in {0} sec.", watch.ElapsedMilliseconds / 1000), Log.LogType.System));
                watch.Stop();
            }
        }


        public void SendMessage(string message)
        {
            if (!isInitialized)
                return;

                string strResult = String.Empty;

                var taskReset = Task.Run(async () => {
                    await WriteAsync(String.Format(sendMessageAcCommand, message));
                    await Task.Delay(5000);
                });

                taskReset.Wait();
        }

        private async Task WriteAsync(string command)
        {
            Task<UInt32> storeAsyncTask;

            dataWriteObject.WriteString(command);

            storeAsyncTask = dataWriteObject.StoreAsync().AsTask();

                UInt32 bytesWritten = await storeAsyncTask;
                if (bytesWritten > 0)
                {
                logModel.AppendLog(Log.CreateLog(String.Format("Mess. sent : {0}", command), Log.LogType.Information));
            }
        }

        public void Listen()
        {
            try
            {
                if (serialPort != null)
                {
                    //dataReaderObject = new DataReader(serialPort.InputStream);

                        var taskListen = Task.Run(async () => {
                        // keep reading the serial input
                        await ReadAsync(ReadCancellationTokenSource.Token);                      
                        });

                       taskListen.Wait();
                }
            }
            catch (Exception ex)
            {
                logModel.AppendLog(Log.CreateErrorLog("Exception on Sigfox Mess.", ex));
            }
            finally
            {
                //// Cleanup once complete
                //if (dataReaderObject != null)
                //{
                //    dataReaderObject.DetachStream();
                //    dataReaderObject = null;
                //}
            }
        }

        private async Task ReadAsync(CancellationToken cancellationToken)
        {
            Task<UInt32> loadAsyncTask;

            uint ReadBufferLength = 1024;

            // If task cancellation was requested, comply
            cancellationToken.ThrowIfCancellationRequested();

            // Set InputStreamOptions to complete the asynchronous read operation when one or more bytes is available
            dataReaderObject.InputStreamOptions = InputStreamOptions.Partial;

            // Create a task object to wait for data on the serialPort.InputStream
            loadAsyncTask = dataReaderObject.LoadAsync(ReadBufferLength).AsTask(cancellationToken);

            // Launch the task and wait
            UInt32 bytesRead = await loadAsyncTask;
            if (bytesRead > 0)
            {
                logModel.AppendLog(Log.CreateLog(String.Format("Mess. received : {0}", dataReaderObject.ReadString(bytesRead)), Log.LogType.Information));
            }
        }

    }
}
