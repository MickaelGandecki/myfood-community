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
using Windows.UI.Xaml.Controls;

namespace myfoodapp.ViewModel
{
    public class TempSensorsMonitoringViewModel : BindableBase
    {
        private DatabaseModel databaseModel = DatabaseModel.GetInstance;
        private PivotItem currentPivotItem;

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

        public NotifyTaskCompletion<List<Measure>> Measures { get; private set; }
        public NotifyTaskCompletion<Decimal> CurrentValue { get; private set; }
        public NotifyTaskCompletion<Decimal> LastDayValue { get; private set; }
        public NotifyTaskCompletion<Decimal> LastDayMinValue { get; private set; }
        public NotifyTaskCompletion<Decimal> LastDayMaxValue { get; private set; }
        public NotifyTaskCompletion<Decimal> LastDayAverageValue { get; private set; }

        public NotifyTaskCompletion<Decimal> CurrentExtTemp { get; private set; }
        public NotifyTaskCompletion<Decimal> CurrentHumidity { get; private set; }

        public TempSensorsMonitoringViewModel()
        {            
        }

        private void Measures_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Measures.PropertyChanged -= Measures_PropertyChanged;
            IsBusy = false;
        }

        public void OnBackClicked(object sender, RoutedEventArgs args)
        {
            App.TryShowNewWindow<MainPage>();
        }

        public void OnSelectionChanged(object sender, RoutedEventArgs args)
        {
            var currentPivot = (Pivot)sender;
            currentPivotItem = (PivotItem)currentPivot.SelectedItem;

            GetMesures();
        }

        private void GetMesures()
        {
            IsBusy = true;

            if (currentPivotItem.Name == "lastDay")
                Measures = new NotifyTaskCompletion<List<Measure>>(databaseModel.GetLastDayMesures(SensorTypeEnum.waterTemperature));

            if (currentPivotItem.Name == "lastWeek")
                Measures = new NotifyTaskCompletion<List<Measure>>(databaseModel.GetLastWeeksMesures(SensorTypeEnum.waterTemperature));

            CurrentValue = new NotifyTaskCompletion<Decimal>(databaseModel.GetLastMesure(SensorTypeEnum.waterTemperature));
            LastDayValue = new NotifyTaskCompletion<Decimal>(databaseModel.GetYesterdayMesure(SensorTypeEnum.waterTemperature));

            LastDayMinValue = new NotifyTaskCompletion<Decimal>(databaseModel.GetLastDayMinMesure(SensorTypeEnum.waterTemperature));
            LastDayMaxValue = new NotifyTaskCompletion<Decimal>(databaseModel.GetLastDayMaxMesure(SensorTypeEnum.waterTemperature));
            LastDayAverageValue = new NotifyTaskCompletion<Decimal>(databaseModel.GetLastDayAverageMesure(SensorTypeEnum.waterTemperature));

            CurrentExtTemp = new NotifyTaskCompletion<Decimal>(databaseModel.GetLastMesure(SensorTypeEnum.airTemperature));
            CurrentHumidity = new NotifyTaskCompletion<Decimal>(databaseModel.GetLastMesure(SensorTypeEnum.humidity));

            Measures.PropertyChanged += Measures_PropertyChanged;
        }

        public void OnRefreshClicked(object sender, RoutedEventArgs args)
        {
            App.TryShowNewWindow<TempSensorsMonitoringPage>();
        }
    }
}
