using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Windows.ApplicationModel.Activation;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace NetCoreApp
{
    public partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();
        }

        protected override void OnWindowCreated(WindowCreatedEventArgs args)
        {
            args.Window.Content = new MainPageUserControl();
            args.Window.Activate();
        }
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
        }
    }
}
