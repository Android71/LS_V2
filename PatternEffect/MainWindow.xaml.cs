using System.Windows;
using PatternEffect.ViewModel;
using LS_Designer_WPF.Controls;

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
            regulator.Value = 1080;
        }
    }
}