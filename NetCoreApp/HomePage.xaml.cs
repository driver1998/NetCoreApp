using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls;
using Windows.UI.Xaml;
using System.Diagnostics.CodeAnalysis;

namespace NetCoreApp
{
    public partial class HomePage : Page
    {
        public int TestProperty { get; set; }
        public string FrameworkDescription => RuntimeInformation.FrameworkDescription;
        public bool IsDynamicCodeCompiled
        {
            get
            {
#if COMPILING_XAML
                return false;
#else
                return RuntimeFeature.IsDynamicCodeCompiled;
#endif
            }
        }
        public HomePage()
        {
            this.InitializeComponent();
        }

        private void TipBtn_Click(object sender, RoutedEventArgs e)
        {
            Tip.IsOpen = true;
        }
    }
}
