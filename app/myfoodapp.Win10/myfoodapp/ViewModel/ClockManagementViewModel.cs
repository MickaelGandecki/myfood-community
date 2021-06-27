using GalaSoft.MvvmLight.Messaging;
using myfoodapp.Business;
using myfoodapp.Business.Clock;
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
using Windows.Data.Json;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using static myfoodapp.Business.Log;

namespace myfoodapp.ViewModel
{
    public class ClockManagementViewModel : BindableBase
    {
        private LogModel logModel = LogModel.GetInstance;
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

        private bool isConnected = false;
        public bool IsConnected
        {
            get { return isConnected; }
            set
            {
                isConnected = value;
                OnPropertyChanged("IsConnected");
            }
        }

        private string currentDate = string.Empty;
        public string CurrentDate
        {
            get { return currentDate; }
            set
            {
                currentDate = value;
                OnPropertyChanged("CurrentDate");
            }
        }

        private DateTime? setDate;
        public DateTime? SetDate
        {
            get { return setDate; }
            set
            {
                setDate = value;
                OnPropertyChanged("SetDate");
            }
        }

        private DateTime? setTime;
        public DateTime? SetTime
        {
            get { return setTime; }
            set
            {
                setTime = value;
                OnPropertyChanged("SetTime");
            }
        }

        public ClockManagementViewModel()
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
                CurrentDate = clockDate.ToString();

                clockManager.Dispose();
            }

            SetDate = clockDate;
            SetTime = clockDate;
        }

        public void OnBackClicked(object sender, RoutedEventArgs args)
        {
            App.TryShowNewWindow<MainPage>();
        }

        public void OnSetDateClicked(object sender, RoutedEventArgs args)
        {
            IsBusy = true;

            try
            {
                 var mesureBackgroundTask = MeasureBackgroundTask.GetInstance;
                 mesureBackgroundTask.Completed += MesureBackgroundTask_Completed;
                 mesureBackgroundTask.Stop();       
            }
            catch (Exception ex)
            {
                logModel.AppendLog(Log.CreateErrorLog("Exception on Stopping Measure Service", ex));
            }
        }

        private void MesureBackgroundTask_Completed(object sender, EventArgs e)
        {
            try
            {
                var clockManager = ClockManager.GetInstance;
                var mesureBackgroundTask = MeasureBackgroundTask.GetInstance;
                mesureBackgroundTask.Completed -= MesureBackgroundTask_Completed;

                if (clockManager != null)
                {
                    var taskClock = Task.Run(async () =>
                    {
                        await clockManager.Connect();
                    });
                    taskClock.Wait();

                    var newDateTime = new DateTime(SetDate.Value.Year, SetDate.Value.Month, SetDate.Value.Day, SetTime.Value.Hour, SetTime.Value.Minute, SetTime.Value.Second);
                    clockManager.SetDate(newDateTime);
                    CurrentDate = clockManager.ReadDate().ToString();

                    clockManager.Dispose();

                    var taskDb = Task.Run(async () =>
                    {
                        await databaseModel.DeleteAllMesures();
                    });
                    taskDb.Wait();

                    mesureBackgroundTask.Run();
                }
            }
            catch (Exception ex)
            {
                logModel.AppendLog(Log.CreateErrorLog("Exception on Set Date", ex));
            }
            finally
            {
                logModel.AppendLog(Log.CreateLog("Date settings changed", LogType.Information));
                IsBusy = false;
            }
        }

    }
}
