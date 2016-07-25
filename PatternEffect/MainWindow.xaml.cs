using System.Windows;
using PatternEffect.ViewModel;
using LS_Designer_WPF.Controls;
using System.Windows.Controls;
using System;

namespace PatternEffect
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Closing += (s, e) => ViewModelLocator.Cleanup();
            //regulator.Value = 1080;
        }

        private void patternView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //Control c = sender as Control;
            //Console.WriteLine($"Width {c.ActualWidth}");
            UpdateMargin();
        }

        private void patternView_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateMargin();
        }

        void UpdateMargin()
        {
            double width = patternView.ActualWidth;
            int pointCount = multiSlider.Maxlimit;
            double halfPointWidth = (width / pointCount) / 2;
            double margin = -9.5 + halfPointWidth;
            multiSlider.Margin = new Thickness(margin, 0, margin, 0);
        }
    }
}