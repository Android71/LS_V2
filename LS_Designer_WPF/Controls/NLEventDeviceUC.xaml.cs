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
            }
        }
    }
}
