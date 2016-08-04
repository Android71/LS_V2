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
    /// Interaction logic for PatternUC.xaml
    /// </summary>
    public partial class PatternUC : UserControl
    {
        public PatternUC()
        {
            InitializeComponent();
        }

        private void effectUC_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateMargin();
        }

        private void patternView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateMargin();
        }

        void UpdateMargin()
        {
            double width = patternView.ActualWidth;
            int pointCount = upMultiSlider.Maxlimit;
            if (pointCount != 0)
            {
                double halfPointWidth = (width / pointCount) / 2;
                double margin = -9.5 + halfPointWidth;
                upMultiSlider.Margin = new Thickness(margin, 0, margin, 0);
            }
        }
    }
}
