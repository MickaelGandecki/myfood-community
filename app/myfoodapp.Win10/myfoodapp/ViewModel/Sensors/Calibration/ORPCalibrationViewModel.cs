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
    public class ORPCalibrationViewModel : BindableBase
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

        public ORPCalibrationViewModel()
        {
            LastCalibrationDate = new NotifyTaskCompletion<string>(databaseModel.GetLastCalibrationDate(SensorTypeEnum.orp));
        }

        public void OnBackClicked(object sender, RoutedEventArgs args)
        {
            App.TryShowNewWindow<MainPage>();
        }

        public void OnORPCalibrationat225Clicked(object sender, RoutedEventArgs args)
        {
            IsBusy = true;
            Messenger.Default.Send(new CloseFlyoutMessage());

            logModel.AppendLog(Log.CreateLog("Set ORP at 225 starting", LogType.Information));

            var mesureBackgroundTask = MeasureBackgroundTask.GetInstance;
            mesureBackgroundTask.Completed += ORP225BackgroundTask_Completed;
            mesureBackgroundTask.Stop();
        }

        public void OnResetCalibrationClicked(object sender, RoutedEventArgs args)
        {
            IsBusy = true;
            Messenger.Default.Send(new CloseFlyoutMessage());

            logModel.AppendLog(Log.CreateLog("Reset ORP to Default Calibration starting", LogType.Information));

            var mesureBackgroundTask = MeasureBackgroundTask.GetInstance;
            mesureBackgroundTask.Completed += factoryBackgroundTask_Completed;
            mesureBackgroundTask.Stop();
        }

        public void CancelClicked(object sender, RoutedEventArgs args)
        {
            Messenger.Default.Send(new CloseFlyoutMessage());
        }

        private void ORP225BackgroundTask_Completed(object sender, EventArgs e)
        {
            var mesureBackgroundTask = MeasureBackgroundTask.GetInstance;
            mesureBackgroundTask.Completed -= ORP225BackgroundTask_Completed;

            try
            {
                DateTime currentDate = GetClockDateTime();

                sensorManager.SetCalibration(SensorTypeEnum.orp, 225);

                var taskUser = Task.Run(async () => { await databaseModel.SetLastCalibrationDate(SensorTypeEnum.orp, currentDate); });
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
                logModel.AppendLog(Log.CreateLog("Set ORP at 225 ended", LogType.Information));
                App.TryShowNewWindow<ORPCalibrationPage>();
            }
        }

        private void factoryBackgroundTask_Completed(object sender, EventArgs e)
        {
            var mesureBackgroundTask = MeasureBackgroundTask.GetInstance;
            mesureBackgroundTask.Completed -= factoryBackgroundTask_Completed;

            try
            {
                DateTime currentDate = GetClockDateTime();

                sensorManager.ResetCalibration(SensorTypeEnum.orp);

                var taskUser = Task.Run(async () => { await databaseModel.SetLastCalibrationDate(SensorTypeEnum.orp, currentDate); });
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
                App.TryShowNewWindow<ORPCalibrationPage>();
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
