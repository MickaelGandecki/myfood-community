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
    public class DOCalibrationViewModel : BindableBase
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

        public DOCalibrationViewModel()
        {
            LastCalibrationDate = new NotifyTaskCompletion<string>(databaseModel.GetLastCalibrationDate(SensorTypeEnum.dissolvedOxygen));
        }

        public void OnBackClicked(object sender, RoutedEventArgs args)
        {
            App.TryShowNewWindow<MainPage>();
        }

        public void OnDOCalibrationat0Clicked(object sender, RoutedEventArgs args)
        {
            IsBusy = true;
            Messenger.Default.Send(new CloseFlyoutMessage());

            logModel.AppendLog(Log.CreateLog("Set DO at 0 starting", LogType.Information));

            var mesureBackgroundTask = MeasureBackgroundTask.GetInstance;
            mesureBackgroundTask.Completed += DO0BackgroundTask_Completed;
            mesureBackgroundTask.Stop();
        }

        public void OnResetCalibrationClicked(object sender, RoutedEventArgs args)
        {
            IsBusy = true;
            Messenger.Default.Send(new CloseFlyoutMessage());

            logModel.AppendLog(Log.CreateLog("Reset DO to Default Calibration starting", LogType.Information));

            var mesureBackgroundTask = MeasureBackgroundTask.GetInstance;
            mesureBackgroundTask.Completed += factoryBackgroundTask_Completed;
            mesureBackgroundTask.Stop();
        }

        public void CancelClicked(object sender, RoutedEventArgs args)
        {
            Messenger.Default.Send(new CloseFlyoutMessage());
        }

        private void DO0BackgroundTask_Completed(object sender, EventArgs e)
        {
            var mesureBackgroundTask = MeasureBackgroundTask.GetInstance;
            mesureBackgroundTask.Completed -= DO0BackgroundTask_Completed;

            try
            {
                DateTime currentDate = GetClockDateTime();

                sensorManager.SetCalibration(SensorTypeEnum.dissolvedOxygen, 0);

                var taskUser = Task.Run(async () => { await databaseModel.SetLastCalibrationDate(SensorTypeEnum.dissolvedOxygen, currentDate); });
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
                logModel.AppendLog(Log.CreateLog("Set DO at 0 ended", LogType.Information));
                App.TryShowNewWindow<DOCalibrationPage>();
            }
        }

        private void factoryBackgroundTask_Completed(object sender, EventArgs e)
        {
            var mesureBackgroundTask = MeasureBackgroundTask.GetInstance;
            mesureBackgroundTask.Completed -= factoryBackgroundTask_Completed;

            try
            {
                DateTime currentDate = GetClockDateTime();

                sensorManager.ResetCalibration(SensorTypeEnum.dissolvedOxygen);

                var taskUser = Task.Run(async () => { await databaseModel.SetLastCalibrationDate(SensorTypeEnum.dissolvedOxygen, currentDate); });
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
                App.TryShowNewWindow<DOCalibrationPage>();
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
