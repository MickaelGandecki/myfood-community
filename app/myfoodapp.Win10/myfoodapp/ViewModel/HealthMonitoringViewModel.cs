using GalaSoft.MvvmLight.Messaging;
using myfoodapp.Business;
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
    public class HealthMonitoringViewModel : BindableBase
    {
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

        public NotifyTaskCompletion<List<Measure>> PHMeasures { get; private set; }

        public HealthMonitoringViewModel()
        {
            
        }

        private void PHMeasures_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PHMeasures.PropertyChanged -= PHMeasures_PropertyChanged;
            IsBusy = false;
        }

        public void OnBackClicked(object sender, RoutedEventArgs args)
        {
            App.TryShowNewWindow<MainPage>();
        }

        public void OnSelectionChanged(object sender, RoutedEventArgs args)
        {

        }

        public void OnRefreshClicked(object sender, RoutedEventArgs args)
        {

        }
    }
}
