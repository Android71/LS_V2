using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LS_Designer_WPF.Controls
{
    /// <summary>
    /// Логика взаимодействия для AN6USPI_UC.xaml
    /// </summary>
    public partial class EventChannelUC : UserControl
    {
        public EventChannelUC()
        {
            InitializeComponent();
            //partition.Visibility = Visibility.Collapsed;
            //Messenger.Default.Register<string>(this, "DeviceControlFocus", SetNameFocus);
        }

        //private void UserControl_Loaded(object sender, RoutedEventArgs e)
        //{

        //}

        //private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        //{
        //    //Messenger.Default.Unregister<string>(this);
        //}

        void SetNameFocus()
        {
            //if (msg == "focus")
            //{
                //nameTb.Focus();
                //nameTb.CaretIndex = nameTb.Text.Length;
            //}
        }
        //public bool IsEditMode
        //{
        //    get { return (bool)GetValue(IsEditModeProperty); }
        //    set { SetValue(IsEditModeProperty, value); }
        //}

        //public static readonly DependencyProperty IsEditModeProperty =
        //    DependencyProperty.Register("IsEditMode", typeof(bool), typeof(AN6USPI_UC), new PropertyMetadata(false, IsEditModeChanged));

        //private static void IsEditModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    if (d != null)
        //    {
        //        EventChannelUC uc = (EventChannelUC)d;
        //        if ((bool)e.NewValue)
        //        {
        //            uc.partition.Visibility = Visibility.Visible;
        //            uc.SetNameFocus();
        //        }
        //        else
        //            uc.partition.Visibility = Visibility.Collapsed;
        //    }
        //}
    }

    
}
