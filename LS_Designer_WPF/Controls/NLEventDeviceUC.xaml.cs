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
    /// Логика взаимодействия для NLEventDevice.xaml
    /// </summary>
    public partial class NLEventDeviceUC : UserControl
    {
        public NLEventDeviceUC()
        {
            InitializeComponent();
        }

        public int ChCount
        {
            get { return (int)GetValue(ChCountProperty); }
            set { SetValue(ChCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ChCount.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ChCountProperty =
            DependencyProperty.Register("ChCount", typeof(int), typeof(NLEventDeviceUC), new PropertyMetadata(0, ChCountChanged));

        private static void ChCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            NLEventDeviceUC uc;
            if (d != null)
            {
                uc = (NLEventDeviceUC)d;
                uc.channel2.Visibility = Visibility.Visible;
                uc.channel3.Visibility = Visibility.Visible;
                uc.channel4.Visibility = Visibility.Visible;
                if (uc.ChCount == 1)
                {
                    uc.channel2.Visibility = Visibility.Hidden;
                    uc.channel3.Visibility = Visibility.Collapsed;
                    uc.channel4.Visibility = Visibility.Collapsed;
                    return;
                }
                if (uc.ChCount == 2)
                {
                    uc.channel3.Visibility = Visibility.Collapsed;
                    uc.channel4.Visibility = Visibility.Collapsed;
                    return;
                }
                if (uc.ChCount == 3)
                {
                    uc.channel4.Visibility = Visibility.Hidden;
                    return;
                }
            }
        }
    }
}
