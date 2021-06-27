using myfoodapp.Common;
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
  
    public class GreenhouseConfigurationViewModel : BindableBase
    {
        public GreenhouseConfigurationViewModel()
        {
            DefaultState = true;
            GHSizeState = false;
            TankNumberState = false;
            ZipgrowNumberState = false;
        }      

        private bool defaultState = false;
        public bool DefaultState
        {
            get { return defaultState; }
            set
            {
                defaultState = value;
                OnPropertyChanged("DefaultState");
            }
        }

        private bool ghSizeState = false;
        public bool GHSizeState
        {
            get { return ghSizeState; }
            set
            {
                ghSizeState = value;
                OnPropertyChanged("GHSizeState");
            }
        }

        private bool tankNumberState = false;
        public bool TankNumberState
        {
            get { return tankNumberState; }
            set
            {
                tankNumberState = value;
                OnPropertyChanged("TankNumberState");
            }
        }

        private bool zipgrowNumberState = false;
        public bool ZipgrowNumberState
        {
            get { return zipgrowNumberState; }
            set
            {
                zipgrowNumberState = value;
                OnPropertyChanged("ZipgrowNumberState");
            }
        }

        public void OnBackClicked(object sender, RoutedEventArgs args)
        {
            App.TryShowNewWindow<MainPage>();
        }

        public void OnStartClicked(object sender, RoutedEventArgs args)
        {
            DefaultState = false;
            GHSizeState = true;
            TankNumberState = false;
            ZipgrowNumberState = false;
        }

        public void OnGHSizeSelectionClicked(object sender, RoutedEventArgs args)
        {
            DefaultState = false;
            GHSizeState = false;
            TankNumberState = true;
            ZipgrowNumberState = false;
        }

        public void OnTankSelectionClicked(object sender, RoutedEventArgs args)
        {
            DefaultState = false;
            GHSizeState = false;
            TankNumberState = false;
            ZipgrowNumberState = true;
        }

        public void OnTowerSelectionClicked(object sender, RoutedEventArgs args)
        {
            DefaultState = true;
            GHSizeState = false;
            TankNumberState = false;
            ZipgrowNumberState = false;
        }
        
    }
}
