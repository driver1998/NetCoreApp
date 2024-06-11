using Microsoft.UI.Xaml.XamlTypeInfo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
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
            new XamlControlsXamlMetaDataProvider(),
            new NetCoreAppXamlMetadataProvider()
        };

        protected override void OnWindowCreated(WindowCreatedEventArgs args)
        {
            this.Resources = new MUXC.XamlControlsResources();

            var window = args.Window;
            var rootFrame = new Frame();
            var navigationView = new MUXC.NavigationView
            {
                PaneDisplayMode = MUXC.NavigationViewPaneDisplayMode.Left,
                Margin = new Thickness(0, 48, 0, 0),
                IsTitleBarAutoPaddingEnabled = false,
                Content = rootFrame,
                MenuItems =
                {
                    new MUXC.NavigationViewItem
                    {
                        Content = "Home",
                        Icon = new SymbolIcon { Symbol = Symbol.Home },
                        Tag = "home"
                    },
                    new MUXC.NavigationViewItem
                    {
                        Content = "Todo",
                        Icon = new FontIcon { Glyph = "\uE9D5" },
                        Tag = "todo"
                    }
                }
            };

            var pages = new Dictionary<string, Type>
            { 
                { "home", typeof(MainPage) }, 
                { "todo", typeof(TodoPage) }
            };

            navigationView.SelectionChanged += (s, e) =>
            {
                string tag = e.SelectedItemContainer.Tag.ToString() ?? "";
                if (e.IsSettingsSelected)
                {
                    tag = "settings";
                }
                if (pages.TryGetValue(tag, out var targetPage))
                {
                    var currPage = rootFrame.CurrentSourcePageType;
                    if (currPage != targetPage)
                    {
                        rootFrame.Navigate(targetPage, null, e.RecommendedNavigationTransitionInfo);
                    }
                }
            };

            window.Content = navigationView;
            MUXC.BackdropMaterial.SetApplyToRootOrPageBackground(navigationView, true);
            navigationView.SelectedItem = navigationView.MenuItems[0];            

            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
            var titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.ButtonBackgroundColor = Colors.Transparent;
            titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
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
