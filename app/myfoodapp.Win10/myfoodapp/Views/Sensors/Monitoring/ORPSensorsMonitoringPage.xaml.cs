using GalaSoft.MvvmLight.Messaging;
using myfoodapp.Business;
using myfoodapp.Common;
using myfoodapp.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace myfoodapp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ORPSensorsMonitoringPage : Page
    {
        public ORPSensorsMonitoringPage()
        {
            this.ViewModel = new ORPSensorsMonitoringViewModel();
            this.InitializeComponent();
            this.Loaded += SensorsMonitoringPage_Loaded;
        }

        private void SensorsMonitoringPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = ViewModel;
        }

        public ORPSensorsMonitoringViewModel ViewModel { get; set; }
    }
}
