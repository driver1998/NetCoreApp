using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls;
using Windows.UI.Xaml;
using System.Diagnostics.CodeAnalysis;

namespace NetCoreApp
{
    public partial class MainPage : Page
    {
        public int TestProperty { get; set; }
        public MainPage()
        {
            Content = new StackPanel
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Children =
                {
                    new TextBlock
                    {
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
                            var teachingTip = FindName("teachingTip") as TeachingTip;
                            if (teachingTip != null)
                            {
                                teachingTip.Target = sender as Button;
                                teachingTip.IsOpen = true;
                            }
                        };
                    }),
                }
            };
        }
    }
}
