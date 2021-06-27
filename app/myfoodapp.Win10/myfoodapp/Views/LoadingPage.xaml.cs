using Microsoft.EntityFrameworkCore;
using myfoodapp.Business;
using myfoodapp.Model;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace myfoodapp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoadingPage : Page
    {
        public LoadingPage()
        {
            this.InitializeComponent();
            this.Loaded += LoadingPage_Loaded;
        }

        private void LoadingPage_Loaded(object sender, RoutedEventArgs e)
        {
            var logModel = LogModel.GetInstance;
      
            logModel.AppendLog(Log.CreateLog("Database starting...", Log.LogType.System));
            using (var db = new LocalDataContext())
            {
                db.Database.Migrate();
                LocalDataContextExtension.EnsureSeedData(db);
            }
            logModel.AppendLog(Log.CreateLog("Database initialized", Log.LogType.System));

            logModel.AppendLog(Log.CreateLog("User Settings Init", Log.LogType.System));

            var taskUserFile = Task.Run(async () => { await UserSettingsModel.GetInstance.InitFileFolder(); });
            taskUserFile.Wait();

            logModel.AppendLog(Log.CreateLog("Background Service Init", Log.LogType.System));

            var mesureBackgroundTask = MeasureBackgroundTask.GetInstance;
            mesureBackgroundTask.Run();

            App.TryShowNewWindow<MainPage>();
        }

    }
}
