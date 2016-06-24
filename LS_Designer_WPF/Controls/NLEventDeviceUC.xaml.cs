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
            partition.Visibility = Visibility.Collapsed;
        }

        void SetNameFocus()
        {
            nameTb.Focus();
            nameTb.CaretIndex = nameTb.Text.Length;
        }

        #region ChCountDP

        public int ChCount
        {
            get { return (int)GetValue(ChCountProperty); }
            set { SetValue(ChCountProperty, value); }
        }

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

        #endregion

        #region IsEditModeDP

        public bool IsEditMode
        {
            get { return (bool)GetValue(IsEditModeProperty); }
            set { SetValue(IsEditModeProperty, value); }
        }

        public static readonly DependencyProperty IsEditModeProperty =
            DependencyProperty.Register("IsEditMode", typeof(bool), typeof(NLEventDeviceUC), new PropertyMetadata(false, IsEditModeChanged));

        private static void IsEditModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d != null)
            {
                NLEventDeviceUC uc = (NLEventDeviceUC)d;
                if ((bool)e.NewValue)
                {
                    uc.partition.Visibility = Visibility.Visible;
                    uc.SetNameFocus();
                }
                else
                    uc.partition.Visibility = Visibility.Collapsed;
            }
        }

        #endregion

        #region IsAddModeDP

        public bool IsAddMode
        {
            get { return (bool)GetValue(IsAddModeProperty); }
            set { SetValue(IsAddModeProperty, value); }
        }

        public static readonly DependencyProperty IsAddModeProperty =
            DependencyProperty.Register("IsAddMode", typeof(bool), typeof(NLEventDeviceUC), new PropertyMetadata(false, IsAddModeChanged));

        private static void IsAddModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d != null)
            {
                NLEventDeviceUC uc = (NLEventDeviceUC)d;
                if ((bool)e.NewValue)
                {
                    uc.partition.Visibility = Visibility.Collapsed;
                }
            }
        }

        #endregion

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsAddMode)
                SetNameFocus();
        }
    }
}
