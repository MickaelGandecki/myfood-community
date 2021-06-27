using GalaSoft.MvvmLight.Messaging;
using myfoodapp.Business;
using myfoodapp.Business.Clock;
using myfoodapp.Business.Sensor;
using myfoodapp.Common;
using myfoodapp.Model;
using myfoodapp.Views;
using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using static myfoodapp.Business.Log;

namespace myfoodapp.ViewModel
{
    public class TempCalibrationViewModel : BindableBase
    {
        private LogModel logModel = LogModel.GetInstance;
        private DatabaseModel databaseModel = DatabaseModel.GetInstance;
        private AtlasSensorManager sensorManager = AtlasSensorManager.GetInstance;

        private bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                OnPropertyChanged("IsBusy");
            }
        }

        public NotifyTaskCompletion<string> LastCalibrationDate { get; private set; }

        public TempCalibrationViewModel()
        {
            LastCalibrationDate = new NotifyTaskCompletion<string>(databaseModel.GetLastCalibrationDate(SensorTypeEnum.waterTemperature));
        }

        public void OnBackClicked(object sender, RoutedEventArgs args)
        {
            App.TryShowNewWindow<MainPage>();
        }

        public void OnTempCalibrationat0Clicked(object sender, RoutedEventArgs args)
        {
            IsBusy = true;
            Messenger.Default.Send(new CloseFlyoutMessage());

            logModel.AppendLog(Log.CreateLog("Set Water Temp at 0°C starting", LogType.Information));

            var mesureBackgroundTask = MeasureBackgroundTask.GetInstance;
            mesureBackgroundTask.Completed += Temp0BackgroundTask_Completed;
            mesureBackgroundTask.Stop();
        }

        public void OnTempCalibrationat100Clicked(object sender, RoutedEventArgs args)
        {
            IsBusy = true;
            Messenger.Default.Send(new CloseFlyoutMessage());

            logModel.AppendLog(Log.CreateLog("Set Water Temp at 100°C starting", LogType.Information));

            var mesureBackgroundTask = MeasureBackgroundTask.GetInstance;
            mesureBackgroundTask.Completed += Temp100BackgroundTask_Completed;
            mesureBackgroundTask.Stop();
        }

        public void OnResetCalibrationClicked(object sender, RoutedEventArgs args)
        {
            IsBusy = true;
            Messenger.Default.Send(new CloseFlyoutMessage());

            logModel.AppendLog(Log.CreateLog("Reset Water Temp to Default Calibration starting", LogType.Information));

            var mesureBackgroundTask = MeasureBackgroundTask.GetInstance;
            mesureBackgroundTask.Completed += factoryBackgroundTask_Completed;
            mesureBackgroundTask.Stop();
        }

        public void CancelClicked(object sender, RoutedEventArgs args)
        {
            Messenger.Default.Send(new CloseFlyoutMessage());
        }

        private void Temp0BackgroundTask_Completed(object sender, EventArgs e)
        {
            var mesureBackgroundTask = MeasureBackgroundTask.GetInstance;
            mesureBackgroundTask.Completed -= Temp0BackgroundTask_Completed;

            try
            {
                DateTime currentDate = GetClockDateTime();

                sensorManager.SetCalibration(SensorTypeEnum.waterTemperature, 0);

                var taskUser = Task.Run(async () => { await databaseModel.SetLastCalibrationDate(SensorTypeEnum.waterTemperature, currentDate); });
                taskUser.Wait();

                mesureBackgroundTask.Run();
            }
            catch (Exception ex)
            {
                logModel.AppendLog(Log.CreateErrorLog("Exception on Set Calibration", ex));
            }
            finally
            {              
                IsBusy = false;
                logModel.AppendLog(Log.CreateLog("Set Water Temp at 0°C ended", LogType.Information));
                App.TryShowNewWindow<TempCalibrationPage>();
            }
        }

        private void Temp100BackgroundTask_Completed(object sender, EventArgs e)
        {
            var mesureBackgroundTask = MeasureBackgroundTask.GetInstance;
            mesureBackgroundTask.Completed -= Temp100BackgroundTask_Completed;

            try
            {
                DateTime currentDate = GetClockDateTime();

                sensorManager.SetCalibration(SensorTypeEnum.waterTemperature, 100);

                var taskUser = Task.Run(async () => { await databaseModel.SetLastCalibrationDate(SensorTypeEnum.waterTemperature, currentDate); });
                taskUser.Wait();

                mesureBackgroundTask.Run();
            }
            catch (Exception ex)
            {
                logModel.AppendLog(Log.CreateErrorLog("Exception on Set Calibration", ex));
            }
            finally
            {
                IsBusy = false;
                logModel.AppendLog(Log.CreateLog("Set Water Temp at 100°C ended", LogType.Information));
                App.TryShowNewWindow<TempCalibrationPage>();
            }
        }

        private void factoryBackgroundTask_Completed(object sender, EventArgs e)
        {
            var mesureBackgroundTask = MeasureBackgroundTask.GetInstance;
            mesureBackgroundTask.Completed -= factoryBackgroundTask_Completed;

            try
            {
                DateTime currentDate = GetClockDateTime();

                sensorManager.ResetCalibration(SensorTypeEnum.waterTemperature);

                var taskUser = Task.Run(async () => { await databaseModel.SetLastCalibrationDate(SensorTypeEnum.waterTemperature, currentDate); });
                taskUser.Wait();

                mesureBackgroundTask.Run();
            }
            catch (Exception ex)
            {
                logModel.AppendLog(Log.CreateErrorLog("Exception on Reset Calibration", ex));
            }
            finally
            {
                IsBusy = false;
                logModel.AppendLog(Log.CreateLog("Reset ended", LogType.Information));
                App.TryShowNewWindow<TempCalibrationPage>();
            }
        }

        private DateTime GetClockDateTime()
        {
            DateTime clockDate = new DateTime();

            var clockManager = ClockManager.GetInstance;

            if (clockManager != null)
            {
                var taskClock = Task.Run(async () =>
                {
                    await clockManager.Connect();
                });
                taskClock.Wait();

                clockDate = clockManager.ReadDate();

                clockManager.Dispose();
            }

            return clockDate;
        }
    }
}
