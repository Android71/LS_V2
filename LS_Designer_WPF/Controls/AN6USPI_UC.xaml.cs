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
    public partial class AN6USPI_UC : UserControl
    {
        public AN6USPI_UC()
        {
            InitializeComponent();
            //Messenger.Default.Register<string>(this, "DeviceControlFocus", SetNameFocus);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            //Messenger.Default.Unregister<string>(this);
        }

        void SetNameFocus(string msg)
        {
            if (msg == "focus")
            {
                nameTb.Focus();
                nameTb.CaretIndex = nameTb.Text.Length;
            }
        }
    }

    
}
