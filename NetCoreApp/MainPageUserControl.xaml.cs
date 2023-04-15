using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace NetCoreApp
{
    public partial class MainPageUserControl : UserControl
    {
        public MainPageUserControl()
        {
            this.InitializeComponent();
            this.DataContext = new MainViewModel();
        }


    }
}

