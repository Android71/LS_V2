using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace LS_Designer_WPF.WPF_Extras
{
    /*  http://www.codeproject.com/Tips/285358/All-purpose-Boolean-to-Visibility-Converter    
    
        
        Usage

               <!--Hides control if boolean value is true-->
               <local:BooleanToVisibilityConverter x:Key="HiddenIfTrue" TriggerValue="True" IsHidden="True"/>

               <!--Hides control if boolean value is false-->
               <local:BooleanToVisibilityConverter x:Key="HiddenIfFalse" TriggerValue="False" IsHidden="True"/>

               <!--Collapses control if boolean value is true-->
               <local:BooleanToVisibilityConverter x:Key="CollapsedIfTrue" TriggerValue="True" IsHidden="False"/>

               <!--Collapses control if boolean value is false-->
               <local:BooleanToVisibilityConverter x:Key="CollapsedIfFalse" TriggerValue="False" IsHidden="False"/>

    */

    /// <summary>
    /// Converts Boolean Values to Control.Visibility values
    /// </summary>
    public class BooleanToVisibilityConverter : IValueConverter
    {
        //Set to true if you want to show control when boolean value is true
        //Set to false if you want to hide/collapse control when value is true
        private bool triggerValue = false;
        public bool TriggerValue
        {
            get { return triggerValue; }
            set { triggerValue = value; }
        }
        //Set to true if you just want to hide the control
        //else set to false if you want to collapse the control
        private bool isHidden;
        public bool IsHidden
        {
            get { return isHidden; }
            set { isHidden = value; }
        }

        private object GetVisibility(object value)
        {
            if (!(value is bool))
                return DependencyProperty.UnsetValue;
            bool objValue = (bool)value;
            if ((objValue && TriggerValue && IsHidden) || (!objValue && !TriggerValue && IsHidden))
            {
                return Visibility.Hidden;
            }
            if ((objValue && TriggerValue && !IsHidden) || (!objValue && !TriggerValue && !IsHidden))
            {
                return Visibility.Collapsed;
            }
            return Visibility.Visible;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return GetVisibility(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
