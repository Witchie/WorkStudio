using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;
namespace GreenWhale.Extensions.TestTools2.Conventers
{
    /// <summary>
    /// 字节转换器
    /// </summary>
    public class ByteConventer:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is byte data)
            {
                return System.Convert.ToString(data, 16).ToUpper() ;
            }
            else
            {
                return string.Empty;
            }
        }
        private const string expression = "[0-9a-fA-F]{0,2}";
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value is string content)
                {
                    return System.Convert.ToByte(content, 16);
                }
                else
                {
                    return Array.Empty<byte>();
                }

            }
            catch (Exception)
            {
                return value;
            }

        }
    }
}
