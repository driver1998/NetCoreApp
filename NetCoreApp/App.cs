using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.XamlTypeInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.System;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using MUXC = Microsoft.UI.Xaml.Controls;

namespace NetCoreApp
{
    public static class UIElementExtensions
    {
        public static UIElement With<T>(this T self, Action<T> action) where T : UIElement
        {
            action(self);
            return self;
        }
    }
    public partial class App : Application, IXamlMetadataProvider
    {
        private readonly List<IXamlMetadataProvider> _providers = new() {
            new XamlControlsXamlMetaDataProvider()
        };

        protected override void OnWindowCreated(WindowCreatedEventArgs args)
        {
            this.Resources = new XamlControlsResources();

            var window = args.Window;
            var page = new Page();
            var content = new MUXC.NavigationView
            {
                PaneDisplayMode = MUXC.NavigationViewPaneDisplayMode.Left,
                Margin = new Thickness(0, 48, 0, 0),
                IsTitleBarAutoPaddingEnabled = false,
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
                        },
                        new TeachingTip
                        {
                            Name = "teachingTip",
                            Title = "WinUI 🎉",
                            Content = "This is TeachingTip from WinUI 2.",
                            PreferredPlacement = TeachingTipPlacementMode.Bottom
                        },
                        new Button
                        {                           
                            Margin = new Thickness(0, 8, 0, 0),
                            Content = "Show TeachingTip",
                        }
                        .With(btn =>
                        {
                            btn.Click += (sender, e) =>
                            {
                                var teachingTip = page.FindName("teachingTip") as TeachingTip;
                                if (teachingTip != null)
                                {
                                    teachingTip.Target = sender as Button;
                                    teachingTip.IsOpen = true;
                                }
                            };
                        }),
                    }
                }
            };
            page.Content = content;
            window.Content = page;
            BackdropMaterial.SetApplyToRootOrPageBackground(content, true);

            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
            var titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.ButtonBackgroundColor = Color.FromArgb(0, 0, 0, 0);
            titleBar.ButtonInactiveBackgroundColor = Color.FromArgb(0, 0, 0, 0);
            window.Activate();
        }

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
        }

        public IXamlType? GetXamlType(Type type)
        {
            foreach (var provider in _providers)
            {
                var xamlType = provider.GetXamlType(type);
                if (xamlType != null)
                {
                    return xamlType;
                }
            }
            return null;
        }

        public IXamlType? GetXamlType(string fullName)
        {
            foreach (var provider in _providers)
            {
                var xamlType = provider.GetXamlType(fullName);
                if (xamlType != null)
                {
                    return xamlType;
                }
            }
            return null;
        }

        public XmlnsDefinition[] GetXmlnsDefinitions()
        {
            return _providers.SelectMany(p => p.GetXmlnsDefinitions()).ToArray();            
        }

        public static void Main()
        {
            Application.Start(_ => new App());
        }

    }
}
