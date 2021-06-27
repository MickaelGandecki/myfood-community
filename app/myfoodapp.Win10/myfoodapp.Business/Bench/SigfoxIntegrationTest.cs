using myfoodapp.Business.Clock;
using myfoodapp.Business.Sensor;
using myfoodapp.Business.Sensor.HumidityTemperature;
using myfoodapp.Model;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace myfoodapp.Business.Bench
{
    public class SigfoxIntegrationTest
    {
        private UserSettingsModel userSettingsModel = UserSettingsModel.GetInstance;
        private LogModel logModel = LogModel.GetInstance;
        private DatabaseModel databaseModel = DatabaseModel.GetInstance;

        private AtlasSensorManager sensorManager;
        private SigfoxInterfaceManager sigfoxManager;       

        public SigfoxIntegrationTest()
        {
        }

        public void Run()
        {
            var watch = Stopwatch.StartNew();
            var captureDateTime = DateTime.Now;

            var clockManager = ClockManager.GetInstance;

            logModel.AppendLog(Log.CreateLog("[UTEST00] Sigfox Integration Test start", Log.LogType.Information));
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

            sigfoxManager = SigfoxInterfaceManager.GetInstance;

            logModel.AppendLog(Log.CreateLog("[UTEST01] Sigfox Init started", Log.LogType.Information));

            var taskSigfox = Task.Run(async () => { await sigfoxManager.InitSensors(); });
            taskSigfox.Wait();

            sensorManager = AtlasSensorManager.GetInstance;

            logModel.AppendLog(Log.CreateLog("[UTEST03] Atlas Sensors init", Log.LogType.Information));
            var taskSensor = Task.Run(async () => { await sensorManager.InitSensors(false); });
            taskSensor.Wait();

            logModel.AppendLog(Log.CreateLog("[UTEST04] Air Temp/Hum Sensor init", Log.LogType.Information));
            var humTempManager = HumidityTemperatureManager.GetInstance;

            var taskHumManager = Task.Run(async () =>
            {
                await humTempManager.Connect();
            });
            taskHumManager.Wait();

            try
            {

            var elapsedMs = watch.ElapsedMilliseconds;

            TimeSpan t = TimeSpan.FromMilliseconds(elapsedMs);

            var watchMesures = Stopwatch.StartNew();

            if (sensorManager.isSensorOnline(SensorTypeEnum.waterTemperature))
            {
                logModel.AppendLog(Log.CreateLog("[UTEST10] Water Temperature capturing", Log.LogType.Information));

                decimal capturedValue = 0;
                capturedValue = sensorManager.RecordSensorsMeasure(SensorTypeEnum.waterTemperature, false);

                if (capturedValue > -20 && capturedValue < 80)
                {
                    sensorManager.SetWaterTemperatureForPHSensor(capturedValue);

                    var task = Task.Run(async () =>
                    {
                        await databaseModel.AddMesure(captureDateTime, capturedValue, SensorTypeEnum.waterTemperature);
                    });
                    task.Wait();

                    logModel.AppendLog(Log.CreateLog(String.Format("[UTEST11] Water Temperature captured : {0}", capturedValue), Log.LogType.Information));
                    var status = sensorManager.GetSensorStatus(SensorTypeEnum.waterTemperature, false);
                    logModel.AppendLog(Log.CreateLog(String.Format("[UTEST12] Water Temperature status : {0}", status), Log.LogType.System));
                }
                else
                    logModel.AppendLog(Log.CreateLog(String.Format("[UTEST13] Water Temperature value out of range - {0}", capturedValue), Log.LogType.Warning));
            }

            if (sensorManager.isSensorOnline(SensorTypeEnum.ph))
            {
                logModel.AppendLog(Log.CreateLog("[UTEST13] PH capturing", Log.LogType.Information));

                decimal capturedValue = 0;
                capturedValue = sensorManager.RecordPhMeasure(false);

                if (capturedValue > 1 && capturedValue < 12)
                {
                    var task = Task.Run(async () =>
                    {
                        await databaseModel.AddMesure(captureDateTime, capturedValue, SensorTypeEnum.ph);
                    });
                    task.Wait();

                    logModel.AppendLog(Log.CreateLog(String.Format("[UTEST14] PH captured : {0}", capturedValue), Log.LogType.Information));
                    var status = sensorManager.GetSensorStatus(SensorTypeEnum.ph, false);
                    logModel.AppendLog(Log.CreateLog(String.Format("[UTEST15] PH status : {0}", status), Log.LogType.System));
                }
                else
                    logModel.AppendLog(Log.CreateLog(String.Format("PH value out of range - {0}", capturedValue), Log.LogType.Warning));
            }

            if (sensorManager.isSensorOnline(SensorTypeEnum.orp))
            {
                logModel.AppendLog(Log.CreateLog("[UTEST16] ORP capturing", Log.LogType.Information));

                decimal capturedValue = 0;
                capturedValue = sensorManager.RecordSensorsMeasure(SensorTypeEnum.orp, false);

                if (capturedValue > 0 && capturedValue < 1500)
                {
                    var task = Task.Run(async () =>
                    {
                        await databaseModel.AddMesure(captureDateTime, capturedValue, SensorTypeEnum.orp);
                    });
                    task.Wait();

                    logModel.AppendLog(Log.CreateLog(String.Format("[UTEST17] ORP captured : {0}", capturedValue), Log.LogType.Information));
                    var status = sensorManager.GetSensorStatus(SensorTypeEnum.orp, false);
                    logModel.AppendLog(Log.CreateLog(String.Format("[UTEST18] ORP status : {0}", status), Log.LogType.System));
                }
                else
                    logModel.AppendLog(Log.CreateLog(String.Format("[UTEST19] ORP value out of range - {0}", capturedValue), Log.LogType.Warning));
            }

            if (sensorManager.isSensorOnline(SensorTypeEnum.dissolvedOxygen))
            {
                logModel.AppendLog(Log.CreateLog("[UTEST20] DO capturing", Log.LogType.Information));

                decimal capturedValue = 0;
                capturedValue = sensorManager.RecordSensorsMeasure(SensorTypeEnum.dissolvedOxygen, false);

                if (capturedValue > 0 && capturedValue < 100)
                {
                    var task = Task.Run(async () =>
                    {
                        await databaseModel.AddMesure(captureDateTime, capturedValue, SensorTypeEnum.dissolvedOxygen);
                    });
                    task.Wait();

                    logModel.AppendLog(Log.CreateLog(String.Format("[UTEST21] DO captured : {0}", capturedValue), Log.LogType.Information));
                    var status = sensorManager.GetSensorStatus(SensorTypeEnum.dissolvedOxygen, false);
                    logModel.AppendLog(Log.CreateLog(String.Format("[UTEST22] DO status : {0}", status), Log.LogType.System));
                }
                else
                    logModel.AppendLog(Log.CreateLog(String.Format("[UTEST23] DO value out of range - {0}", capturedValue), Log.LogType.Warning));
            }

            try
            {
                logModel.AppendLog(Log.CreateLog("[UTEST24] Air Temperature capturing", Log.LogType.Information));

                decimal capturedValue = (decimal)humTempManager.Temperature;

                if (capturedValue > 0 && capturedValue < 100)
                {
                    var taskTemp = Task.Run(async () =>
                    {
                        await databaseModel.AddMesure(captureDateTime, capturedValue, SensorTypeEnum.airTemperature);
                    });
                    taskTemp.Wait();

                    logModel.AppendLog(Log.CreateLog(String.Format("[UTEST25] Air Temperature captured : {0}", capturedValue), Log.LogType.Information));
                }
                else
                    logModel.AppendLog(Log.CreateLog(String.Format("[UTEST26] Air Temperature out of range - {0}", capturedValue), Log.LogType.Warning));
            }
            catch (Exception ex)
            {
                logModel.AppendLog(Log.CreateErrorLog("Exception on Air Temperature Sensor", ex));
            }

            try
            {
                logModel.AppendLog(Log.CreateLog("[UTEST27] Humidity capturing", Log.LogType.Information));

                decimal capturedValue = (decimal)humTempManager.Humidity;

                if (capturedValue > 0 && capturedValue < 100)
                {
                    var taskHum = Task.Run(async () =>
                    {
                        await databaseModel.AddMesure(captureDateTime, capturedValue, SensorTypeEnum.humidity);
                    });
                    taskHum.Wait();

                    logModel.AppendLog(Log.CreateLog(String.Format("[UTEST28] Air Humidity captured : {0}", capturedValue), Log.LogType.Information));
                }
                else
                    logModel.AppendLog(Log.CreateLog(String.Format("[UTEST29] Air Humidity out of range - {0}", capturedValue), Log.LogType.Warning));
            }
            catch (Exception ex)
            {
                logModel.AppendLog(Log.CreateErrorLog("Exception on Air Humidity Sensor", ex));
            }

            logModel.AppendLog(Log.CreateLog(String.Format("[UTEST30] Measures captured in {0} sec.", watchMesures.ElapsedMilliseconds / 1000), Log.LogType.System));

            watchMesures.Restart();

            string sigFoxSignature = String.Empty;

            var taskSig = Task.Run(async () =>
                {
                    sigFoxSignature = await databaseModel.GetLastMesureSignature();
                });
                taskSig.Wait();

                sigfoxManager.SendMessage(sigFoxSignature);

                logModel.AppendLog(Log.CreateLog(String.Format("[UTEST31] Data sent to Azure via Sigfox in {0} sec.", watchMesures.ElapsedMilliseconds / 1000), Log.LogType.System));
                }

        catch (Exception ex)
        {
            logModel.AppendLog(Log.CreateErrorLog("Exception on Sigfox Integration Test", ex));
        }

        watch.Stop();
        }
    }
}
