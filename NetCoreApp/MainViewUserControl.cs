using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI;

namespace NetCoreApp
{
    public class MainViewUserControl : UserControl
    {
        public MainViewModel ViewModel { get; set; }

        public void BuildUI()
        {

        }
            
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
