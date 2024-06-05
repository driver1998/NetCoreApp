using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace NetCoreApp
{
    public partial class App : Application
    {
        protected override void OnWindowCreated(WindowCreatedEventArgs args)
        {
            var window = args.Window;
            window.Content = new NavigationView
            {
                PaneDisplayMode = NavigationViewPaneDisplayMode.Left,
                Content = new StackPanel
                {
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Children =
                    {
                        new TextBlock {
                            Text = "UWP ❤️ NativeAOT",
                            FontSize = 48
                        },
                        new TextBlock
                        {
                            Text = RuntimeInformation.FrameworkDescription
                        },
                        new TextBlock
                        {
                            Text = "IsDynamicCodeCompiled: " + RuntimeFeature.IsDynamicCodeCompiled
                        }
                    }
                }
            };
            
            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
            var titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.ButtonBackgroundColor = Color.FromArgb(0, 0, 0, 0);
            titleBar.ButtonInactiveBackgroundColor = Color.FromArgb(0, 0, 0, 0);
            window.Activate();
        }

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {

        }

        public static void Main()
        {
            Application.Start(_ => new App());
        }
    }
}
