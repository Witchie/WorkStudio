using System;
using System.Globalization;
using System.Windows.Data;

namespace GreenWhale.Extensions.TestTools2.Models
{
    public class ToggleConventer:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool res)
            {
                return !res;
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool res)
            {
                return !res;
            }
            else
            {
                return null;
            }
        }
    }
}
