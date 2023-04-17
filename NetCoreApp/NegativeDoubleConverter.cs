using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace NetCoreApp
{
    public class NegativeDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return -1.0 * (double)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return -1.0 * (double)value;
        }
    }
}
