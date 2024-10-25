using Windows.UI.Xaml.Controls;
using MUXC = Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;
using System;

namespace NetCoreApp
{
    public partial class MainPage : Page
    {
        private static readonly Dictionary<string, Type> Pages = new()
        {
            { "home", typeof(HomePage) },
            { "todo", typeof(TodoPage) },
            { "color", typeof(ColorPage) },
            { "webView", typeof(WebViewPage) }
        };

        public MainPage()
        {
            this.InitializeComponent();
            NavView.SelectedItem = NavView.MenuItems[0];
        }

        private void NavView_SelectionChanged(MUXC.NavigationView sender, MUXC.NavigationViewSelectionChangedEventArgs args)
        {
            string tag = args.SelectedItemContainer.Tag.ToString() ?? "";
            if (Pages.TryGetValue(tag, out var targetPage))
            {
                var currPage = rootFrame.CurrentSourcePageType;
                if (currPage != targetPage)
                {
                    rootFrame.Navigate(targetPage, null, args.RecommendedNavigationTransitionInfo);
                }
            }

        }
    }
}
