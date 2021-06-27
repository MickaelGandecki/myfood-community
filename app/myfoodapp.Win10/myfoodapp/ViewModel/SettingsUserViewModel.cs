using myfoodapp.Common;
using myfoodapp.Views;
using Windows.UI.Xaml;

namespace myfoodapp.ViewModel
{
    public class SettingsViewModel : BindableBase
    {
        public SettingsViewModel()
        {
        }

        public void OnPHSensorSettingsClicked(object sender, RoutedEventArgs args)
        {
            App.TryShowNewWindow<PhCalibrationPage>();
        }

        public void OnTempSensorSettingsClicked(object sender, RoutedEventArgs args)
        {
            App.TryShowNewWindow<TempCalibrationPage>();
        }

        public void OnORPSensorSettingsClicked(object sender, RoutedEventArgs args)
        {
            App.TryShowNewWindow<ORPCalibrationPage>();
        }

        public void OnDOSensorSettingsClicked(object sender, RoutedEventArgs args)
        {
            App.TryShowNewWindow<DOCalibrationPage>();
        }

        public void OnClockSettingsClicked(object sender, RoutedEventArgs args)
        {
            App.TryShowNewWindow<ClockManagementPage>();
        }

        public void OnLogManagementClicked(object sender, RoutedEventArgs args)
        {
            App.TryShowNewWindow<LogManagementPage>();
        }
        
        public void OnAdvancedSettingsClicked(object sender, RoutedEventArgs args)
        {
            App.TryShowNewWindow<AdvancedSettingsPage>();
        }
        public void OnAboutClicked(object sender, RoutedEventArgs args)
        {
            App.TryShowNewWindow<AboutPage>();
        }
    }
}
