using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LS_Designer_WPF.WPF_Extras
{
    public sealed class WaterMarkExtentions
    {
        public static string GetWaterMark(DependencyObject obj)
        {
            return (string)obj.GetValue(WaterMarkProperty);
        }

        public static void SetWaterMark(DependencyObject obj, string value)
        {
            obj.SetValue(WaterMarkProperty, value);
        }

        public static readonly DependencyProperty WaterMarkProperty =

           DependencyProperty.RegisterAttached("WaterMark"
                                       , typeof(string)
                                       , typeof(WaterMarkExtentions)
                                       , new PropertyMetadata(""));

    }

}
