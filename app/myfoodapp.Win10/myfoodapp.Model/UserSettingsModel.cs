using myfoodapp.Business;
using myfoodapp.Common;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;

namespace myfoodapp.Model
{
    public class UserSettingsModel
    {
        private static readonly AsyncLock asyncLock = new AsyncLock();
        private string FILE_NAME = "user.json";
        private StorageFolder folder = ApplicationData.Current.LocalFolder;

        private static UserSettingsModel instance;
        private LogModel logModel = LogModel.GetInstance;
        public static UserSettings CurrentUserSettings = new UserSettings();

        public static UserSettingsModel GetInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UserSettingsModel();
                }
                return instance;
            }
        }

        private UserSettingsModel()
        {
        }

        public async Task InitFileFolder()
        {
            try
            {
                if (await folder.TryGetItemAsync(FILE_NAME) == null)
                {
#if DEBUG
                    var defaultUserSettings = new UserSettings()
                    {
                        isDebugLedEnable = true,
                        isScreenSaverEnable = false,
                        isSigFoxComEnable = false,
                        isSleepModeEnable = false,
                        isTempHumiditySensorEnable = true,
                        isDiagnosticModeEnable = false,
                        measureFrequency = 600000,
                        productionSiteId = "XXXXX",
                        hubMessageAPI = "https://hub.myfood.eu/api/Messages",
                        SSID = "MYFOODPI_AP",
                        ACCESS_POINT_PWD = "myfoodpi"
                    };
#endif

#if !DEBUG
                    var defaultUserSettings = new UserSettings()
                    {
                        isDebugLedEnable = false,
                        isScreenSaverEnable = false,
                        isSigFoxComEnable = false,
                        isSleepModeEnable = false,
                        isTempHumiditySensorEnable = false,
                        isDiagnosticModeEnable = false,
                        measureFrequency = 1800000,
                        productionSiteId = "XXXXX",
                        hubMessageAPI = "https://hub.myfood.eu/api/Messages",
                        SSID = "MYFOODPI_AP",
                        ACCESS_POINT_PWD = "myfoodpi"
                    };
#endif

                    var str = JsonConvert.SerializeObject(defaultUserSettings);

                    var newFile = await folder.CreateFileAsync(FILE_NAME, CreationCollisionOption.FailIfExists);

                    await FileIO.WriteTextAsync(newFile, str);
                }
            }
            catch (FileNotFoundException ex)
            {
                var newFile = await folder.CreateFileAsync(FILE_NAME, CreationCollisionOption.FailIfExists);
                logModel.AppendLog(Log.CreateErrorLog("Configuration file created", ex));
            }
        }

        public async Task<UserSettings> GetUserSettingsAsync()
        {
            using (await asyncLock.LockAsync())
            {
                var file = await folder.GetFileAsync(FILE_NAME);

                if (file != null)
                {
                    var read = await FileIO.ReadTextAsync(file);
                    UserSettings userSettings = JsonConvert.DeserializeObject<UserSettings>(read);

                    CurrentUserSettings = userSettings;
                    return userSettings;
                }

                return null;
            }
        }

        public async Task SyncUserSettings(UserSettings userSettings)
        {
            using (await asyncLock.LockAsync())
            {
                var task = Task.Run(async () => {

                    var file = await folder.GetFileAsync(FILE_NAME);

                    if (file != null)
                    {
                        var str = JsonConvert.SerializeObject(userSettings);

                        var newFile = await folder.CreateFileAsync(FILE_NAME, CreationCollisionOption.ReplaceExisting);
                        await FileIO.WriteTextAsync(newFile, str);
                    }
                });

                task.Wait();
                CurrentUserSettings = userSettings;
            }
        }

    }
}
