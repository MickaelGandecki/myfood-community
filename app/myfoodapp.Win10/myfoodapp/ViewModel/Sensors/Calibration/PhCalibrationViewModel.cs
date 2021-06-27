using GalaSoft.MvvmLight.Messaging;
using myfoodapp.Business;
using myfoodapp.Business.Clock;
using myfoodapp.Business.Sensor;
using myfoodapp.Common;
using myfoodapp.Model;
using myfoodapp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Data.Json;
using Windows.Data.Xml.Dom;
using Windows.Storage;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using static myfoodapp.Business.Log;

namespace myfoodapp.ViewModel
{
    public class PhCalibrationViewModel : BindableBase
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

        public PhCalibrationViewModel()
        {
            LastCalibrationDate = new NotifyTaskCompletion<string>(databaseModel.GetLastCalibrationDate(SensorTypeEnum.ph));
        }

        public void OnBackClicked(object sender, RoutedEventArgs args)
        {
            App.TryShowNewWindow<MainPage>();
        }

        public void OnPhCalibrationat7Clicked(object sender, RoutedEventArgs args)
        {
            IsBusy = true;
            Messenger.Default.Send(new CloseFlyoutMessage());

            logModel.AppendLog(Log.CreateLog("Set pH at 7 starting", LogType.Information));

            var mesureBackgroundTask = MeasureBackgroundTask.GetInstance;
            mesureBackgroundTask.Completed += ph7BackgroundTask_Completed;
            mesureBackgroundTask.Stop();
        }

        public void OnPhCalibrationat4Clicked(object sender, RoutedEventArgs args)
        {
            IsBusy = true;
            Messenger.Default.Send(new CloseFlyoutMessage());

            logModel.AppendLog(Log.CreateLog("Set pH at 4 starting", LogType.Information));

            var mesureBackgroundTask = MeasureBackgroundTask.GetInstance;
            mesureBackgroundTask.Completed += ph4BackgroundTask_Completed;
            mesureBackgroundTask.Stop();
        }

        public void OnPhCalibrationat10Clicked(object sender, RoutedEventArgs args)
        {
            IsBusy = true;
            Messenger.Default.Send(new CloseFlyoutMessage());

            logModel.AppendLog(Log.CreateLog("Set pH at 10 starting", LogType.Information));

            var mesureBackgroundTask = MeasureBackgroundTask.GetInstance;
            mesureBackgroundTask.Completed += ph10BackgroundTask_Completed;
            mesureBackgroundTask.Stop();
        }

        public void OnResetCalibrationClicked(object sender, RoutedEventArgs args)
        {
            IsBusy = true;
            Messenger.Default.Send(new CloseFlyoutMessage());

            logModel.AppendLog(Log.CreateLog("Reset pH to Default Calibration starting", LogType.Information));

            var mesureBackgroundTask = MeasureBackgroundTask.GetInstance;
            mesureBackgroundTask.Completed += factoryBackgroundTask_Completed;
            mesureBackgroundTask.Stop();
        }

        public void CancelClicked(object sender, RoutedEventArgs args)
        {
            Messenger.Default.Send(new CloseFlyoutMessage());
        }

        private void ph7BackgroundTask_Completed(object sender, EventArgs e)
        {
            var mesureBackgroundTask = MeasureBackgroundTask.GetInstance;
            mesureBackgroundTask.Completed -= ph7BackgroundTask_Completed;

            try
            {
                DateTime currentDate = GetClockDateTime();

                sensorManager.SetCalibration(SensorTypeEnum.ph, AtlasSensorManager.CalibrationType.Mid);

                var taskUser = Task.Run(async () => { await databaseModel.SetLastCalibrationDate(SensorTypeEnum.ph, currentDate); });
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
                logModel.AppendLog(Log.CreateLog("Set pH at 7 ended", LogType.Information));
                App.TryShowNewWindow<PhCalibrationPage>();
            }
        }

        private void ph4BackgroundTask_Completed(object sender, EventArgs e)
        {
            var mesureBackgroundTask = MeasureBackgroundTask.GetInstance;
            mesureBackgroundTask.Completed -= ph4BackgroundTask_Completed;

            try
            {
                DateTime currentDate = GetClockDateTime();

                sensorManager.SetCalibration(SensorTypeEnum.ph, AtlasSensorManager.CalibrationType.Low);

                var taskUser = Task.Run(async () => { await databaseModel.SetLastCalibrationDate(SensorTypeEnum.ph, currentDate); });
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
                logModel.AppendLog(Log.CreateLog("Set pH at 4 ended", LogType.Information));
                App.TryShowNewWindow<PhCalibrationPage>();
            }
        }

        private void ph10BackgroundTask_Completed(object sender, EventArgs e)
        {
            var mesureBackgroundTask = MeasureBackgroundTask.GetInstance;
            mesureBackgroundTask.Completed -= ph10BackgroundTask_Completed;

            try
            {
                DateTime currentDate = GetClockDateTime();

                sensorManager.SetCalibration(SensorTypeEnum.ph, AtlasSensorManager.CalibrationType.High);

                var taskUser = Task.Run(async () => { await databaseModel.SetLastCalibrationDate(SensorTypeEnum.ph, currentDate); });
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
                logModel.AppendLog(Log.CreateLog("Set pH at 10 ended", LogType.Information));
                App.TryShowNewWindow<PhCalibrationPage>();
            }
        }

        private void factoryBackgroundTask_Completed(object sender, EventArgs e)
        {
            var mesureBackgroundTask = MeasureBackgroundTask.GetInstance;
            mesureBackgroundTask.Completed -= factoryBackgroundTask_Completed;

            try
            {
                DateTime currentDate = GetClockDateTime();

                sensorManager.ResetCalibration(SensorTypeEnum.ph);

                var taskUser = Task.Run(async () => { await databaseModel.SetLastCalibrationDate(SensorTypeEnum.ph, currentDate); });
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
                App.TryShowNewWindow<PhCalibrationPage>();
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
