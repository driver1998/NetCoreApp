using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Windows.UI.Popups;
using CSharpMarkup.SystemXaml;
using static CSharpMarkup.SystemXaml.Helpers;
using WUXC = Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI;

namespace NetCoreApp
{
    public class MainViewUserControl : WUXC.UserControl
    {
        public MainViewModel ViewModel { get; set; }

        public void BuildUI() =>
            Content = VStack(
                TextBox()
                    .Bind(ViewModel.UserName, BindingMode.TwoWay),

                TextBlock()
                    .Bind(ViewModel.Message)
                    .FontSize().Bind(ViewModel.FontSize, fallbackValue: 1.0)
                    .Foreground().Bind(ViewModel.Color, convert: (Color color) => SolidColorBrush(color)),

                Slider()
                    .Orientation().Horizontal()
                    .Minimum(1)
                    .Maximum(100)
                    .Bind(ViewModel.FontSize, BindingMode.TwoWay),

                HStack(
                    Button("Say Hello")
                        .Invoke((WUXC.Button btn) =>
                        {
                            btn.Click += (s, e) => ViewModel.SayHello();
                        }),

                    Button("About")
                        .Invoke((WUXC.Button btn) =>
                        {
                            btn.Click += async (s, e) =>
                            {
                                StringBuilder builder = new();
                                builder.AppendLine(RuntimeInformation.FrameworkDescription);
                                builder.AppendLine(RuntimeInformation.OSDescription);
                                builder.Append($"ProcessArchitecture: {RuntimeInformation.ProcessArchitecture.ToString()}");
                                MessageDialog dialog = new(builder.ToString(), "About");
                                await dialog.ShowAsync();
                            };
                        })
                ),

                ColorPicker()
                    .Color().Bind(ViewModel.Color, BindingMode.TwoWay)
            );

        public MainViewUserControl()
        {

            DataContext = ViewModel = new MainViewModel()
            {
                UserName = "UWP",
                FontSize = 24,
                Color = Colors.Blue
            };            
            BuildUI();
            ViewModel.SayHello();
        }
    }
}
