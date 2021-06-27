using myfoodapp.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.SerialCommunication;
using Windows.Storage.Streams;

namespace myfoodapp.Business.Sensor
{
    public sealed class AtlasSensorManager
    {
        private LogModel logModel = LogModel.GetInstance;
        private List<AtlasSensor> sensorsList = new List<AtlasSensor>();

        public bool isInitialized = false;

        public enum CalibrationType
        {
            Mid,
            Low,
            High
        }

        private CancellationTokenSource ReadCancellationTokenSource;

        private static AtlasSensorManager instance;

        public static AtlasSensorManager GetInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AtlasSensorManager();
                }
                return instance;
            }
        }

        //pH Sensors Commands
        //private string queryCalibrationCommand = "Cal,?\r";
        private string clearCalibrationCommand = "Cal,clear\r";
        private string midCalibrationCommand = "Cal,mid,7.00\r";
        private string lowCalibrationCommand = "Cal,low,4.00\r";
        private string highCalibrationCommand = "Cal,high,10.00\r";
        private string setCalibrationCommand = "Cal,{0}\r";

        //Water Temperature Sensors commands
        //private string setCelsiusTemperatureCommand = "S,C\r";
        //private string setFahrenheitTemperatureCommand = "S,F\r";
        //private string setKelvinTemperatureCommand = "S,K\r";

        private string informationCommand = "I\r";
        private string resetFactoryCommand = "Factory\r";
        private string ledDebugCommand = "L,{0}\r";
        private string getStatusCommand = "Status\r";
        //private string sleepModeCommand = "Sleep\r";
        private string readValueCommand = "R\r";
        private string setWaterTemperatureCommand = "T,{0}\r";
        //private string wakeupCommand = "W\r";
        private string disableContinuousModeCommand = "C,0\r";
        private string disableAutomaticAnswerCommand = "RESPONSE,0\r";

        //private string answersWrongCommand = "*ER";
        //private string answersOverVoltedCircuit = "*OV";
        //private string answersUnderVoltedCircuit = "*UV";
        //private string answersResetCircuit = "*RS";
        //private string answersBootUpCircuit = "*RE";
        private string answersSleepMode = "*SL";
        private string answersWakeUpMode = "*WA";

        private AtlasSensorManager()
        {
        }

        public async Task InitSensors(bool isSleepModeActivated)
        {
            if (isInitialized)
                return;

            var watch = Stopwatch.StartNew();

            try
            {
                string aqs = SerialDevice.GetDeviceSelector();
                var dis = await DeviceInformation.FindAllAsync(aqs);

                logModel.AppendLog(Log.CreateLog(String.Format("Sensors found in {0} sec.", watch.ElapsedMilliseconds / 1000), Log.LogType.System));

                for (int i = 0; i < dis.Count; i++)
                {
                    try
                    {
                        if (dis[i].Id.Contains(@"\\?\FTDIBUS"))
                        {
                            var task = Task.Run(async () =>
                            {
                                try
                                {
                                    DeviceInformation entry = (DeviceInformation)dis[i];

                                    logModel.AppendLog(Log.CreateLog(String.Format("Associating device ID - {0}", entry.Id), Log.LogType.System));

                                    var serialPort = await SerialDevice.FromIdAsync(entry.Id);

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

                                    var newSensor = new AtlasSensor() { serialDevice = serialPort };

                                    newSensor.dataWriteObject = new DataWriter(serialPort.OutputStream);
                                    newSensor.dataReaderObject = new DataReader(serialPort.InputStream);

                                    string s = String.Empty;
                                    string r = String.Empty;
                                    string strResult = String.Empty;

                                    //var taskWakeUp = Task.Run(async () =>
                                    //{
                                    //    await WriteAsync(wakeupCommand, newSensor)
                                    //        .ContinueWith((a) => strResult = ReadAsync(ReadCancellationTokenSource.Token, newSensor).Result);

                                    //    await Task.Delay(1000);
                                    //});

                                    //taskWakeUp.Wait(20000);

                                    var taskContinuous = Task.Run(async () =>
                                    {
                                        await WriteAsync(disableContinuousModeCommand, newSensor);
                                        await Task.Delay(5000);
                                    });
                                    taskContinuous.Wait();

                                    var taskStatus = Task.Run(async () =>
                                    {
                                        await WriteAsync(getStatusCommand, newSensor)
                                             .ContinueWith((are) => s = ReadAsync(ReadCancellationTokenSource.Token, newSensor).Result);
                                    });
                                    taskStatus.Wait();

                                    var taskInformation = Task.Run(async () =>
                                    {
                                        await WriteAsync(informationCommand, newSensor)
                                             .ContinueWith((are) => r = ReadAsync(ReadCancellationTokenSource.Token, newSensor).Result);
                                    });
                                    taskInformation.Wait();

                                    if (r.Contains("*OK"))
                                    {
                                        var taskAuto = Task.Run(async () =>
                                        {
                                            await WriteAsync(disableAutomaticAnswerCommand, newSensor);
                                            await Task.Delay(5000);
                                        });
                                        taskAuto.Wait();
                                    }

                                    //if (isSleepModeActivated)
                                    //{
                                    //    var taskSleep = Task.Run(async () =>
                                    //    {
                                    //        await WriteAsync(sleepModeCommand, newSensor);
                                    //    });

                                    //    taskSleep.Wait();
                                    //}

                                    logModel.AppendLog(Log.CreateLog(String.Format("Sensor Information - {0}", r), Log.LogType.System));

                                    if (r.ToUpper().Contains("RTD"))
                                    {
                                        newSensor.sensorType = SensorTypeEnum.waterTemperature;
                                        logModel.AppendLog(Log.CreateLog("Water Temperature online", Log.LogType.Information));
                                        logModel.AppendLog(Log.CreateLog(String.Format("Water Temperature status - {0}", s), Log.LogType.System));
                                    }

                                    if (r.ToUpper().Contains("PH"))
                                    {
                                        newSensor.sensorType = SensorTypeEnum.ph;
                                        logModel.AppendLog(Log.CreateLog("PH online", Log.LogType.Information));
                                        logModel.AppendLog(Log.CreateLog(String.Format("PH status - {0}", s), Log.LogType.System));
                                    }

                                    if (r.ToUpper().Contains("ORP"))
                                    {
                                        newSensor.sensorType = SensorTypeEnum.orp;
                                        logModel.AppendLog(Log.CreateLog("ORP online", Log.LogType.Information));
                                        logModel.AppendLog(Log.CreateLog(String.Format("ORP status - {0}", s), Log.LogType.System));
                                    }

                                    if (r.ToUpper().Contains("DO"))
                                    {
                                        newSensor.sensorType = SensorTypeEnum.dissolvedOxygen;
                                        logModel.AppendLog(Log.CreateLog("Dissolved Oxygen online", Log.LogType.Information));
                                        logModel.AppendLog(Log.CreateLog(String.Format("Dissolved Oxygen status - {0}", s), Log.LogType.System));
                                    }

                                    if (r.ToUpper().Contains("EC"))
                                    {
                                        newSensor.sensorType = SensorTypeEnum.EC;
                                        logModel.AppendLog(Log.CreateLog("Electro-Conductivity online", Log.LogType.Information));
                                        logModel.AppendLog(Log.CreateLog(String.Format("Electro-Conductivity - {0}", s), Log.LogType.System));
                                    }

                                    sensorsList.Add(newSensor);
                                }

                                catch (AggregateException ex)
                                {
                                    logModel.AppendLog(Log.CreateErrorLog("Exception on Sensors Init", ex));
                                }
                            });

                            task.Wait();
                        }
                    }
                    catch (Exception ex)
                    {
                        logModel.AppendLog(Log.CreateErrorLog("Exception on Sensors Init", ex));
                    }
                }
            }
            catch (Exception ex)
            {
                logModel.AppendLog(Log.CreateErrorLog("Exception on Sensors Init", ex));
            }
            finally
            {
                isInitialized = true;

                logModel.AppendLog(Log.CreateLog(String.Format("Atlas Sensors online in {0} sec.", watch.ElapsedMilliseconds / 1000), Log.LogType.System));
                watch.Stop();
            }
        }

        public bool isSensorOnline(SensorTypeEnum currentSensorType)
        {
            return (sensorsList.Where(s => s.sensorType == currentSensorType).FirstOrDefault() != null) ? true : false;
        }

        public AtlasSensor GetSensor(SensorTypeEnum currentSensorType)
        {
            return sensorsList.Where(s => s.sensorType == currentSensorType).FirstOrDefault();
        }

        public void SetCalibration(SensorTypeEnum sensorType, CalibrationType calibrationType)
        {
            if (!isInitialized)
                return;

            var calibrationCommand = String.Empty;

            switch (calibrationType)
            {
                case CalibrationType.Mid:
                    calibrationCommand = midCalibrationCommand;
                    break;
                case CalibrationType.Low:
                    calibrationCommand = lowCalibrationCommand;
                    break;
                case CalibrationType.High:
                    calibrationCommand = highCalibrationCommand;
                    break;
                default:
                    return;
            }

            var currentSensor = this.GetSensor(sensorType);

            if (currentSensor != null)
            {
                string strResult = String.Empty;

                var taskCal = Task.Run(async () => {
                    await WriteAsync(calibrationCommand, currentSensor);
                });

                taskCal.Wait();
            }
        }

        public void SetCalibration(SensorTypeEnum sensorType, int calibrationValue)
        {
            if (!isInitialized)
                return;

            var currentSensor = this.GetSensor(sensorType);

            if (currentSensor != null)
            {
                string strQuery = String.Format(setCalibrationCommand, calibrationValue);

                var taskCal = Task.Run(async () => {
                    await WriteAsync(strQuery, currentSensor);
                });

                taskCal.Wait();
            }
        }

        public void ResetCalibration(SensorTypeEnum sensorType)
        {
            if (!isInitialized)
                return;

            var currentSensor = this.GetSensor(sensorType);

            if (currentSensor != null)
            {
                string strResult = String.Empty;

                var taskCal = Task.Run(async () => {
                    await WriteAsync(clearCalibrationCommand, currentSensor);
                });

                taskCal.Wait();
            }
        }

        public void SetDebugLedMode(bool isEnable)
        {
            if (!isInitialized)
                return;

            var strIsEnable = isEnable ? "1" : "0";

            foreach (AtlasSensor currentSensor in sensorsList)
            {

                string strResult = String.Empty;

                var taskDebugMode = Task.Run(async () => {
                    await WriteAsync(String.Format(ledDebugCommand, strIsEnable), currentSensor);
                });

                taskDebugMode.Wait();
            }
        }

        public void ResetSensorsToFactory()
        {
            if (!isInitialized)
                return;

            foreach(AtlasSensor currentSensor in sensorsList)
            {
                string strResult = String.Empty;

                var taskReset = Task.Run(async () => {
                    await WriteAsync(String.Format(resetFactoryCommand), currentSensor);
                    await Task.Delay(5000);
                });

                taskReset.Wait();

                var taskAuto = Task.Run(async () =>
                {
                    await WriteAsync(disableAutomaticAnswerCommand, currentSensor);
                    await Task.Delay(5000);
                });
                taskAuto.Wait();

                var taskContinuous = Task.Run(async () =>
                {
                    await WriteAsync(disableContinuousModeCommand, currentSensor);
                    await Task.Delay(5000);
                });
                taskContinuous.Wait();

            }
        }

        public bool SetWaterTemperatureForPHSensor(decimal waterTemperature)
        {
            var phSensor = this.GetSensor(SensorTypeEnum.ph);

            if (phSensor != null)
            {
                //var taskWakeUp = Task.Run(async () =>
                //{
                //    await WriteAsync(wakeupCommand, phSensor);
                //});

                //taskWakeUp.Wait();

                var taskSetTemp = Task.Run(async () =>
                {
                    await WriteAsync(String.Format(setWaterTemperatureCommand, waterTemperature), phSensor);

                });

                taskSetTemp.Wait();

                return true;
            }

            return false;
        }

        public decimal RecordSensorsMeasure(SensorTypeEnum sensorType, bool isSleepModeActivated)
        {
            var currentSensor = this.GetSensor(sensorType);

            if (currentSensor != null)
            {
                decimal capturedMesure = 0;
                string strResult = String.Empty;

                //if (isSleepModeActivated)
                //{
                //    var taskWakeUp = Task.Run(async () =>
                //    {
                //        await WriteAsync(wakeupCommand, currentSensor);

                //        await Task.Delay(1000);
                //    });

                //    taskWakeUp.Wait();
                //}

                var taskMeasure = Task.Run(async () => {
                    await WriteAsync(readValueCommand, currentSensor)
                         .ContinueWith((a) => strResult = ReadAsync(ReadCancellationTokenSource.Token, currentSensor).Result);

                    var boolMeasure = Decimal.TryParse(strResult.Replace("\r", "")
                                                                .Replace(answersSleepMode, "")
                                                                .Replace(answersWakeUpMode, "")
                                                                , out capturedMesure);
                });

                taskMeasure.Wait();

                //if (isSleepModeActivated)
                //{
                //    var taskSleep = Task.Run(async () =>
                //    {
                //        await WriteAsync(sleepModeCommand, currentSensor);
                //    });

                //    taskSleep.Wait();
                //}

                return capturedMesure;
            }

            return 0;
        }

        public decimal RecordPhMeasure(bool isSleepModeActivated)
        {
            var phSensor = this.GetSensor(SensorTypeEnum.ph);

            if (phSensor != null)
            {
                StringBuilder strResult = new StringBuilder();
                decimal sumCapturedMesure = 0;

                //if (isSleepModeActivated)
                //{
                //    var taskWakeUp = Task.Run(async () =>
                //    {
                //        await WriteAsync(wakeupCommand, phSensor);
                //        await Task.Delay(1000);
                //    });

                //    taskWakeUp.Wait();
                //}

                for (int i = 0; i < 4; i++)
                {
                    var taskSumMeasure = Task.Run(async () => {
                        await WriteAsync(readValueCommand, phSensor)
                        .ContinueWith((a) => strResult.Append(ReadAsync(ReadCancellationTokenSource.Token, phSensor).Result));
                        decimal capturedMesure = 0;
                        var boolMeasure_1 = Decimal.TryParse(strResult.ToString().Replace("\r", "")
                                                                                 .Replace(answersSleepMode, "")
                                                                                 .Replace(answersWakeUpMode, ""), out capturedMesure);
                        sumCapturedMesure += capturedMesure;
                        strResult.Clear();
                    });

                    taskSumMeasure.Wait();
                }

                //if (isSleepModeActivated)
                //{
                //    var taskSleep = Task.Run(async () =>
                //    {
                //        await WriteAsync(sleepModeCommand, phSensor);
                //    });

                //    taskSleep.Wait();
                //}

                return sumCapturedMesure / 4;
            }

            return 0;
        }

        public string GetSensorStatus(SensorTypeEnum sensorType, bool isSleepModeActivated)
        {
            var currentSensor = this.GetSensor(sensorType);

            if (currentSensor != null)
            {
                string strResult = String.Empty;

                //if (isSleepModeActivated)
                //{
                //    var taskWakeUp = Task.Run(async () =>
                //    {
                //        await WriteAsync(wakeupCommand, currentSensor);

                //        await Task.Delay(1000);
                //    });

                //    taskWakeUp.Wait();
                //}

                var taskStatus = Task.Run(async () => {
                    await WriteAsync(getStatusCommand, currentSensor)
                         .ContinueWith((a) => strResult = ReadAsync(ReadCancellationTokenSource.Token, currentSensor).Result);

                });

                taskStatus.Wait();

                //if (isSleepModeActivated)
                //{
                //    var taskSleep = Task.Run(async () =>
                //    {
                //        await WriteAsync(sleepModeCommand, currentSensor);
                //    });

                //    taskSleep.Wait();
                //}

                return strResult;
            }

            return string.Empty;
        }

        private async Task<string> WriteAsync(string command, AtlasSensor currentSensor)
        {
            Task<UInt32> storeAsyncTask;

            currentSensor.dataWriteObject.WriteString(command);

            storeAsyncTask = currentSensor.dataWriteObject.StoreAsync().AsTask();

            UInt32 bytesWritten = await storeAsyncTask;
            if (bytesWritten > 0)
            {
                return command;
            }
            return String.Empty;
        }

        private async Task<string> ReadAsync(CancellationToken cancellationToken, AtlasSensor currentSensor)
        {
            Task<UInt32> loadAsyncTask;

            uint ReadBufferLength = 1024;

            // If task cancellation was requested, comply
            cancellationToken.ThrowIfCancellationRequested();

            // Set InputStreamOptions to complete the asynchronous read operation when one or more bytes is available
            currentSensor.dataReaderObject.InputStreamOptions = InputStreamOptions.Partial;

            // Create a task object to wait for data on the serialPort.InputStream
            loadAsyncTask = currentSensor.dataReaderObject.LoadAsync(ReadBufferLength).AsTask(cancellationToken);

            // Launch the task and wait
            UInt32 bytesRead = await loadAsyncTask;
            if (bytesRead > 0)
            {
                return currentSensor.dataReaderObject.ReadString(bytesRead);
            }

            return String.Empty;
        }

        private void CloseDevice(AtlasSensor sensor)
        {
            if (sensor.serialDevice != null)
            {
                sensor.serialDevice.Dispose();
            }
            sensorsList = null;
        }
    }
}
