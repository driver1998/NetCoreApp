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
    public class App : Application
    {
        public App()
        {
        }

        protected override void OnWindowCreated(WindowCreatedEventArgs args)
        {
            args.Window.Content = new MainViewUserControl();
            args.Window.Activate();
        }
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
        }
    }
}
