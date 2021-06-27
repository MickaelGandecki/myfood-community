using GalaSoft.MvvmLight.Messaging;
using myfoodapp.Business;
using myfoodapp.Business.Bench;
using myfoodapp.Business.Sensor;
using myfoodapp.Common;
using myfoodapp.Model;
using System;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;
using Windows.UI.Xaml;
using static myfoodapp.Business.Log;

namespace myfoodapp.ViewModel
{
    public class AdvancedSettingsViewModel : BindableBase
    {
        private LogModel logModel = LogModel.GetInstance;
        private UserSettingsModel userSettingsModel = UserSettingsModel.GetInstance;
        private DatabaseModel databaseModel = DatabaseModel.GetInstance;

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

        private string packageVersion;
        public string PackageVersion
        {
            get { return packageVersion; }
            set
            {
                packageVersion = value;
                OnPropertyChanged("PackageVersion");
            }
        }

        private string freeDiskSpace;
        public string FreeDiskSpace
        {
            get { return freeDiskSpace; }
            set
            {
                freeDiskSpace = value;
                OnPropertyChanged("FreeDiskSpace");
            }
        }

        private bool isDebugLedEnable = false;
        public bool IsDebugLedEnable
        {
            get { return isDebugLedEnable; }
            set
            {
                isDebugLedEnable = value;
                OnPropertyChanged("IsDebugLedEnable");
            }
        }

        private bool isScreenSaverEnable = false;
        public bool IsScreenSaverEnable
        {
            get { return isScreenSaverEnable; }
            set
            {
                isScreenSaverEnable = value;
                OnPropertyChanged("IsScreenSaverEnable");
            }
        }

        private bool isSleepModeEnable = false;
        public bool IsSleepModeEnable
        {
            get { return isSleepModeEnable; }
            set
            {
                isSleepModeEnable = value;
                OnPropertyChanged("IsSleepModeEnable");
            }
        }

        private bool isSigFoxComEnable = false;
        public bool IsSigFoxComEnable
        {
            get { return isSigFoxComEnable; }
            set
            {
                isSigFoxComEnable = value;
                OnPropertyChanged("IsSigFoxComEnable");
            }
        }

        private bool isDiagnosticModeEnable = false;
        public bool IsDiagnosticModeEnable
        {
            get { return isDiagnosticModeEnable; }
            set
            {
                isDiagnosticModeEnable = value;
                OnPropertyChanged("IsDiagnosticModeEnable");
            }
        }

        private bool isTempHumiditySensorEnable = false;
        public bool IsTempHumiditySensorEnable
        {
            get { return isTempHumiditySensorEnable; }
            set
            {
                isTempHumiditySensorEnable = value;
                OnPropertyChanged("IsTempHumiditySensorEnable");
            }
        }

        private int MeasureFrequency;
        private string ProductionSiteId;
        private string HubMessageAPI;

        public AdvancedSettingsViewModel()
        {
            Package package = Package.Current;
            PackageId packageId = package.Id;
            PackageVersion version = packageId.Version;

            PackageVersion = String.Format("Release v.{0}.{1}.{2}.{3}", version.Major, version.Minor, version.Build, version.Revision);

            UInt64 intfreeDisk = 0;

            var taskInfo = Task.Run(async () => { intfreeDisk = await GetFreeSpace(); });
            taskInfo.Wait();

            FreeDiskSpace = String.Format("Free Disk Space: {0}Mo", intfreeDisk / 10000000);

            UserSettings currentUserSettings = new UserSettings();

            var taskFile = Task.Run(async () => { currentUserSettings = await userSettingsModel.GetUserSettingsAsync(); });
            taskFile.Wait();

            IsScreenSaverEnable = currentUserSettings.isScreenSaverEnable;
            IsSigFoxComEnable = currentUserSettings.isSigFoxComEnable;
            IsSleepModeEnable = currentUserSettings.isSleepModeEnable;
            IsTempHumiditySensorEnable = currentUserSettings.isTempHumiditySensorEnable;
            IsDiagnosticModeEnable = currentUserSettings.isDiagnosticModeEnable;
            IsDebugLedEnable = currentUserSettings.isDebugLedEnable;
            MeasureFrequency = currentUserSettings.measureFrequency;
            ProductionSiteId = currentUserSettings.productionSiteId;
            HubMessageAPI = currentUserSettings.hubMessageAPI;
        }

        public async Task<UInt64> GetFreeSpace()
        {
            StorageFolder local = ApplicationData.Current.LocalFolder;
            var retrivedProperties = await local.Properties.RetrievePropertiesAsync(new string[] { "System.FreeSpace" });
            return (UInt64)retrivedProperties["System.FreeSpace"];
        }

        public void OnBackClicked(object sender, RoutedEventArgs args)
        {
            App.TryShowNewWindow<MainPage>();
        }

        public void OnEraseLogsClicked(object sender, RoutedEventArgs args)
        {
            IsBusy = true;
            Messenger.Default.Send(new CloseFlyoutMessage());

            var task = Task.Run(async () => { await logModel.ClearLog(); });
            task.Wait();

            IsBusy = false;         
        }

        public void OnEraseMeasuresClicked(object sender, RoutedEventArgs args)
        {
            IsBusy = true;
            Messenger.Default.Send(new CloseFlyoutMessage());

            var task = Task.Run(async () => { await databaseModel.DeleteAllMesures(); });
            task.Wait();

            logModel.AppendLog(Log.CreateLog("Measures erased", LogType.Information));

            IsBusy = false;
        }

        public void OnRestartAppClicked(object sender, RoutedEventArgs args)
        {
            IsBusy = true;
            Messenger.Default.Send(new CloseFlyoutMessage());

            logModel.AppendLog(Log.CreateLog("Restart from user", LogType.Information));

            var mesureBackgroundTask = MeasureBackgroundTask.GetInstance;
            mesureBackgroundTask.Completed += RestartBackgroundTask_Completed;
            mesureBackgroundTask.Stop();            
        }

        public void OnResetHardwareClicked(object sender, RoutedEventArgs args)
        {
            IsBusy = true;
            Messenger.Default.Send(new CloseFlyoutMessage());

            logModel.AppendLog(Log.CreateLog("Hardware reset started", LogType.Information));

            var mesureBackgroundTask = MeasureBackgroundTask.GetInstance;
            mesureBackgroundTask.Completed += ResetHardwareBackgroundTask_Completed;
            mesureBackgroundTask.Stop();
        }

        public void OnPerformSigfoxTestClicked(object sender, RoutedEventArgs args)
        {
            IsBusy = true;
            Messenger.Default.Send(new CloseFlyoutMessage());

            logModel.AppendLog(Log.CreateLog("Sigfox Test started", LogType.Information));

            var mesureBackgroundTask = MeasureBackgroundTask.GetInstance;
            mesureBackgroundTask.Completed += PerformSigfoxTestBackgroundTask_Completed;
            mesureBackgroundTask.Stop();
        }

        public void OnSaveClicked(object sender, RoutedEventArgs args)
        {
            IsBusy = true;
            Messenger.Default.Send(new CloseFlyoutMessage());

            var mesureBackgroundTask = MeasureBackgroundTask.GetInstance;
            mesureBackgroundTask.Completed += SaveSettingMesureBackgroundTask_Completed;
            mesureBackgroundTask.Stop();
        }

        public void CancelClicked(object sender, RoutedEventArgs args)
        {
            Messenger.Default.Send(new CloseFlyoutMessage());
        }

        private void RestartBackgroundTask_Completed(object sender, EventArgs e)
        {
            var mesureBackgroundTask = MeasureBackgroundTask.GetInstance;
            mesureBackgroundTask.Completed -= RestartBackgroundTask_Completed;

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
                Messenger.Default.Send(new CloseFlyoutMessage());
                IsBusy = false;
            }
        }

        private void ResetHardwareBackgroundTask_Completed(object sender, EventArgs e)
        {
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
                logModel.AppendLog(Log.CreateLog("Hardware reset ended", LogType.Information));
                IsBusy = false;
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
                IsBusy = false;
            }
        }

        private void SaveSettingMesureBackgroundTask_Completed(object sender, EventArgs e)
        {
            var mesureBackgroundTask = MeasureBackgroundTask.GetInstance;
            mesureBackgroundTask.Completed -= SaveSettingMesureBackgroundTask_Completed;

            try
            {
                var newUserSettings = new UserSettings();

                newUserSettings.isScreenSaverEnable = IsScreenSaverEnable;
                newUserSettings.isSigFoxComEnable = IsSigFoxComEnable;
                newUserSettings.isSleepModeEnable = IsSleepModeEnable;
                newUserSettings.isTempHumiditySensorEnable = IsTempHumiditySensorEnable;
                newUserSettings.isDiagnosticModeEnable = IsDiagnosticModeEnable;
                newUserSettings.isDebugLedEnable = IsDebugLedEnable;

                newUserSettings.measureFrequency = MeasureFrequency;
                newUserSettings.productionSiteId = ProductionSiteId;
                newUserSettings.hubMessageAPI = HubMessageAPI;

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
                logModel.AppendLog(Log.CreateLog("Settings saved", LogType.Information));
                IsBusy = false;
            }
        }
    }
}
