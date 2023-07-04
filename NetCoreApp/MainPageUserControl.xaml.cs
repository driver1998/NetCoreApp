using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace NetCoreApp
{
    public partial class MainPageUserControl : UserControl
    {
        public MainViewModel ViewModel { get; set; }

        public MainPageUserControl()
        {
            this.InitializeComponent();            

            DataContext = ViewModel = new MainViewModel()
            {
                UserName = "UWP"
            };
            ViewModel.SayHello();

            this.btn1.Command = ViewModel.SayHelloCommand;
            this.btn2.Click += async (s, e) =>
            {
                bool isCoreWindow = CoreWindow.GetForCurrentThread() != null;                
                StringBuilder builder = new();
                builder.AppendLine($"IsCoreWindow: {isCoreWindow}");
                builder.AppendLine($"Runtime: {RuntimeInformation.FrameworkDescription}");
                builder.AppendLine($"OS: {RuntimeInformation.OSDescription}");
                builder.Append($"ProcessArchitecture: {RuntimeInformation.ProcessArchitecture.ToString()}");
                var contentDialog = new ContentDialog
                {
                    Title = "About",
                    Content = builder.ToString(),
                    CloseButtonText = "OK"
                };
                await contentDialog.ShowAsync();
            };
        }


    }
}

