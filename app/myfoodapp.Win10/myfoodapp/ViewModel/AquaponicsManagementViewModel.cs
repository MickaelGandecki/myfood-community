using myfoodapp.Business.Clock;
using myfoodapp.Common;
using myfoodapp.Model;
using myfoodapp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace myfoodapp.ViewModel
{
    public class AquaponicsManagementViewModel : BindableBase
    {
        private DatabaseModel databaseModel = DatabaseModel.GetInstance;

        public NotifyTaskCompletion<Decimal> CurrentPhValue { get; private set; }
        public NotifyTaskCompletion<Decimal> LastDayPhValue { get; private set; }

        public NotifyTaskCompletion<Decimal> CurrentTempValue { get; private set; }
        public NotifyTaskCompletion<Decimal> CurrentExtTemp { get; private set; }
        public NotifyTaskCompletion<Decimal> CurrentHumidity { get; private set; }

        public NotifyTaskCompletion<Decimal> CurrentORPValue { get; private set; }
        public NotifyTaskCompletion<Decimal> LastDayORPValue { get; private set; }

        public NotifyTaskCompletion<Decimal> CurrentDOValue { get; private set; }
        public NotifyTaskCompletion<Decimal> LastDayDOValue { get; private set; }

        public AquaponicsManagementViewModel()
        {
            GetMesures();
        }

        private void GetMesures()
        {
            CurrentPhValue = new NotifyTaskCompletion<Decimal>(databaseModel.GetLastMesure(SensorTypeEnum.ph));
            LastDayPhValue = new NotifyTaskCompletion<Decimal>(databaseModel.GetYesterdayMesure(SensorTypeEnum.ph));

            CurrentTempValue = new NotifyTaskCompletion<Decimal>(databaseModel.GetLastMesure(SensorTypeEnum.waterTemperature));

            CurrentExtTemp = new NotifyTaskCompletion<Decimal>(databaseModel.GetLastMesure(SensorTypeEnum.airTemperature));
            CurrentHumidity = new NotifyTaskCompletion<Decimal>(databaseModel.GetLastMesure(SensorTypeEnum.humidity));

            CurrentORPValue = new NotifyTaskCompletion<Decimal>(databaseModel.GetLastMesure(SensorTypeEnum.orp));
            LastDayORPValue = new NotifyTaskCompletion<Decimal>(databaseModel.GetYesterdayMesure(SensorTypeEnum.orp));

            CurrentDOValue = new NotifyTaskCompletion<Decimal>(databaseModel.GetLastMesure(SensorTypeEnum.dissolvedOxygen));
            LastDayDOValue = new NotifyTaskCompletion<Decimal>(databaseModel.GetYesterdayMesure(SensorTypeEnum.dissolvedOxygen));
        }

        public void OnPHSensorClicked(object sender, RoutedEventArgs args)
        {
            App.TryShowNewWindow<PhSensorsMonitoringPage>();
        }

        public void OnTempSensorClicked(object sender, RoutedEventArgs args)
        {
            App.TryShowNewWindow<TempSensorsMonitoringPage>();
        }

        public void OnORPSensorClicked(object sender, RoutedEventArgs args)
        {
            App.TryShowNewWindow<ORPSensorsMonitoringPage>();
        }

        public void OnDOSensorClicked(object sender, RoutedEventArgs args)
        {
            App.TryShowNewWindow<DOSensorsMonitoringPage>();
        }

        public void OnHealthClicked(object sender, RoutedEventArgs args)
        {
            App.TryShowNewWindow<HealthMonitoringPage>();
        }

        public void OnNetworkClicked(object sender, RoutedEventArgs args)
        {
            App.TryShowNewWindow<NetworkPage>();
        }

    }
}
