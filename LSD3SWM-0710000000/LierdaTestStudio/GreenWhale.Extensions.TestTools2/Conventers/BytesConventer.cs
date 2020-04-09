using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Data;
using GreenWhale.Extensions.TestTools2.Extensions;
namespace GreenWhale.Extensions.TestTools2.Conventers
{
    /// <summary>
    /// 字节转换器
    /// </summary>
    public class BytesConventer:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is byte[] data)
            {
                return data.ToHex();
            }
            else
            {
                return string.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string content)
            {
                return content.ToHex();
            }
            else
            {
              return  Array.Empty<byte>();
            }
        }
    }
}
