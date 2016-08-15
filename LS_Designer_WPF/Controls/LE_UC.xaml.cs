using LS_Designer_WPF.Model;
using LS_Library;
using System.Windows;
using System.Windows.Controls;

namespace LS_Designer_WPF.Controls
{
    public partial class LE_UC : UserControl
    {
        public LE_UC()
        {
            InitializeComponent();
            partition.Visibility = Visibility.Collapsed;
            rgbProps.Visibility = Visibility.Collapsed;
            colorSeq.Visibility = Visibility.Collapsed;
        }

        void SetNameFocus()
        {
            nameTb.Focus();
            nameTb.CaretIndex = nameTb.Text.Length;
        }

        #region IsEditModeDP

        public bool IsEditMode
        {
            get { return (bool)GetValue(IsEditModeProperty); }
            set { SetValue(IsEditModeProperty, value); }
        }

        public static readonly DependencyProperty IsEditModeProperty =
            DependencyProperty.Register("IsEditMode", typeof(bool), typeof(LE_UC), new PropertyMetadata(false, IsEditModeChanged));

        private static void IsEditModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d != null)
            {
                LE_UC uc = (LE_UC)d;
                if ((bool)e.NewValue)
                {
                    uc.partition.Visibility = Visibility.Visible;
                    //uc.SetNameFocus();
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
            DependencyProperty.Register("IsAddMode", typeof(bool), typeof(LE_UC), new PropertyMetadata(false, IsAddModeChanged));

        private static void IsAddModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d != null)
            {
                LE_UC uc = (LE_UC)d;
                if ((bool)e.NewValue)
                {
                    uc.partition.Visibility = Visibility.Collapsed;
                    uc.SetNameFocus();
                }
            }
        }

        #endregion

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsAddMode)
                SetNameFocus();
        }

        #region PointTypeDP

        public PointTypeEnum PointType
        {
            get { return (PointTypeEnum)GetValue(PointTypeProperty); }
            set { SetValue(PointTypeProperty, value); }
        }

        public static readonly DependencyProperty PointTypeProperty =
            DependencyProperty.Register("PointType", typeof(PointTypeEnum), typeof(LE_UC), new PropertyMetadata(PointTypeEnum.W, PointTypeChanged));

        private static void PointTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d != null)
            {
                LE_UC uc = (LE_UC)d;
                PointTypeEnum pt = (PointTypeEnum)e.NewValue;
                string prefix = uc.CSprefix;
                if (pt != PointTypeEnum.W && pt != PointTypeEnum.WT && pt != PointTypeEnum.CW)
                {
                    uc.rgbProps.Visibility = Visibility.Visible;
                    if (prefix == "AN" || prefix == "DX")
                    {
                        uc.colorSeq.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    uc.rgbProps.Visibility = Visibility.Collapsed;
                    uc.colorSeq.Visibility = Visibility.Collapsed;
                }
            }
        }

        #endregion

        #region CSprefixDP

        public string CSprefix
        {
            get { return (string)GetValue(CSprefixProperty); }
            set { SetValue(CSprefixProperty, value); }
        }

        public static readonly DependencyProperty CSprefixProperty =
            DependencyProperty.Register("CSprefix", typeof(string), typeof(LE_UC), new PropertyMetadata(null, CSprefixChanged));

        private static void CSprefixChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d != null)
            {
                LE_UC uc = (LE_UC)d;
                string prefix = (string)e.NewValue;
                PointTypeEnum pt = uc.PointType;
                if (prefix == "AN" || prefix == "DX")
                {

                    uc.lsProps.Visibility = Visibility.Visible;
                    if (pt == PointTypeEnum.RGB || pt == PointTypeEnum.RGBW )
                        uc.colorSeq.Visibility = Visibility.Visible;

                }
                else
                    uc.lsProps.Visibility = Visibility.Collapsed;
                
            }
        }

        #endregion
    }
}
