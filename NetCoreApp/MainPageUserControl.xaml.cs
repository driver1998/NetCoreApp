using System;
using System.Runtime.InteropServices;
using System.Text;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;

namespace NetCoreApp
{
    internal static partial class NativeMethods
    {

        [LibraryImport("advapi32.dll", SetLastError = true)]
        private static partial int OpenProcessToken(IntPtr processHandle, uint desiredAccess, out IntPtr tokenHandle);

        [LibraryImport("advapi32.dll", SetLastError = true)]
        private static partial int GetTokenInformation(IntPtr tokenHandle, int tokenInformationClass, IntPtr tokenInformation, int tokenInformationLength, out int returnLength);

        [LibraryImport("kernel32.dll")]
        private static partial IntPtr GetCurrentProcess();

        [LibraryImport("kernel32.dll", SetLastError = true)]
        private static partial int CloseHandle(IntPtr handle);

        private static readonly uint TOKEN_QUERY = 8;
        private static readonly int TokenIsAppContainer = 29;

        public static bool IsAppContainer()
        {
            IntPtr hToken = 0;
            try
            {
                unsafe
                {
                    IntPtr hProcess = GetCurrentProcess();
                    OpenProcessToken(hProcess, TOKEN_QUERY, out hToken);

                    GetTokenInformation(hToken, TokenIsAppContainer, 0, 0, out int infoLength);

                    var info = stackalloc byte[infoLength];
                    GetTokenInformation(hToken, TokenIsAppContainer, (IntPtr)info, infoLength, out _);

                    bool isAppContainer = *(uint*)info > 0;
                    return isAppContainer;
                }
                
            }
            finally
            {
                CloseHandle(hToken);
            }
        }

    }

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
                bool isAppContainer = NativeMethods.IsAppContainer();
                StringBuilder builder = new();
                builder.AppendLine($"IsCoreWindow: {isCoreWindow}");
                builder.AppendLine($"IsAppContainer: {isAppContainer}");
                builder.AppendLine($"Runtime: {RuntimeInformation.FrameworkDescription}");
                builder.AppendLine($"OS: {RuntimeInformation.OSDescription}");
                builder.AppendLine($"ProcessArchitecture: {RuntimeInformation.ProcessArchitecture.ToString()}");
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

