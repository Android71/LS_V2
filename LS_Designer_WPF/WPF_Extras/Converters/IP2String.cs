using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace LS_Designer_WPF.WPF_Extras
{
    public class IP2StringConverter : IValueConverter
    //public class IP2String : ConverterBase<IP2String>
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            IPAddress ip = (IPAddress)value;
            if (ip != null)
                return ip.ToString();
            else
                return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            IPAddress ip = IPAddress.Parse("9.9.9.9");
            if (value != null)
            {
                try
                {
                    ip = IPAddress.Parse((string)value);
                }
                catch (Exception)
                {

                }
            }
            return ip;
        }
    }

}
