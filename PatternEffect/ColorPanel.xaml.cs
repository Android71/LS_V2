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
using Media = System.Windows.Media;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LS_Designer_WPF.Controls
{
    
    public class ColorRange
    {
        public ColorRange(Media.Color from, Media.Color to)
        {
            FromColor = from;
            ToColor = to;
            if (from == Media.Colors.Black)
            {
                HueMinimum = 0.0;
                HueMaximum = 0.0;
                HueMiddle = 0.0;
            }
            else
            {
                Color tmp = Color.FromArgb(from.R, from.G, from.B);
                Color tmp1 = Color.FromArgb(to.R, to.G, to.B);
                HueMinimum = Math.Round(tmp.GetHue());
                HueMaximum = Math.Round(tmp1.GetHue());               //Math.Round(tmp1.GetHue());
                if (HueMaximum == 0.0)
                    HueMaximum = 360.0;
                HueMiddle = HueMinimum + (HueMaximum - HueMinimum) / 2;
            }
        }
        public double HueMinimum { get; set; }
        public double HueMaximum { get; set; }
        public double HueMiddle { get; set; }
        public Media.Color FromColor { get; set; }
        public Media.Color ToColor { get; set; }

    }

    public partial class ColorPanel : UserControl
    {
        public ColorPanel()
        {
            InitializeComponent();
            (Content as FrameworkElement).DataContext = this;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //new Media.SolidColorBrush(Media.Color.FromRgb(255, 0, 128)),    /* Red */
            //new Media.SolidColorBrush(Media.Color.FromRgb(255, 0, 255)),    /* Magenta */
            //new Media.SolidColorBrush(Media.Color.FromRgb(128, 0, 255)),    /* Red */
            //new Media.SolidColorBrush(Media.Color.FromRgb(0, 0, 255)),      /* Blue*/
            //new Media.SolidColorBrush(Media.Color.FromRgb(0, 128, 255)),    /* Red */
            //new Media.SolidColorBrush(Media.Color.FromRgb(0, 255, 255)),    /* Cyan */
            //new Media.SolidColorBrush(Media.Color.FromRgb(0, 255, 0)),      /* Green */
            //new Media.SolidColorBrush(Media.Color.FromRgb(255, 255, 0)),    /* Yellow */
            //new Media.SolidColorBrush(Media.Color.FromRgb(255, 128, 0)),    /* Orange */
            //new Media.SolidColorBrush(Media.Color.FromRgb(255, 0, 0)),      /* Red */
            List<ColorRange> list = new List<ColorRange>();
            list.Add(new ColorRange(Media.Color.FromRgb(255,0,127), Media.Color.FromRgb(255,0,0)));         // 330-360
            list.Add(new ColorRange(Media.Color.FromRgb(255, 0, 255), Media.Color.FromRgb(255, 0, 128)));   // 300-330
            list.Add(new ColorRange(Media.Color.FromRgb(128, 0, 255), Media.Color.FromRgb(255, 0, 255)));   // 270-300
            list.Add(new ColorRange(Media.Color.FromRgb(0, 0, 255), Media.Color.FromRgb(128, 0, 255)));     // 240-270 
            list.Add(new ColorRange(Media.Color.FromRgb(0, 128, 255), Media.Color.FromRgb(0, 0, 255)));     // 210-240
            list.Add(new ColorRange(Media.Color.FromRgb(0, 255, 255), Media.Color.FromRgb(0, 128, 255)));   // 180-210
            list.Add(new ColorRange(Media.Color.FromRgb(0, 255, 128), Media.Color.FromRgb(0, 255, 255)));   // 150-180
            //list.Add(new ColorRange(Media.Color.FromRgb(0, 255, 0), Media.Color.FromRgb(0, 255, 128)));     // 120-150
            //list.Add(new ColorRange(Media.Color.FromRgb(128, 255, 0), Media.Color.FromRgb(0, 255, 0)));     // 90-120
            list.Add(new ColorRange(Media.Color.FromRgb(128, 255, 0), Media.Color.FromRgb(0, 255, 128)));   // 90-150
            list.Add(new ColorRange(Media.Color.FromRgb(255, 255, 0), Media.Color.FromRgb(128, 255, 0)));   // 60-90
            list.Add(new ColorRange(Media.Color.FromRgb(255, 170, 0), Media.Color.FromRgb(255, 255, 0)));   // 40-60
            list.Add(new ColorRange(Media.Color.FromRgb(255, 85, 0), Media.Color.FromRgb(255, 170, 0)));    // 20-40
            list.Add(new ColorRange(Media.Color.FromRgb(255, 0, 0), Media.Color.FromRgb(255, 85, 0)));      // 0-20
            list.Add(new ColorRange(Media.Colors.Black, Media.Colors.Black));                                
            
            ColorRangeList = list;
            SelectedColorRange = ColorRangeList[ColorRangeList.Count - 1];
        }

        public SliderItem SelectedSlider
        {
            get { return (SliderItem)GetValue(SelectedSliderProperty); }
            set { SetValue(SelectedSliderProperty, value); }
        }

        public static readonly DependencyProperty SelectedSliderProperty =
            DependencyProperty.Register("SelectedSlider", typeof(SliderItem), typeof(ColorPanel), new PropertyMetadata(null, SelectedSliderChanged));

        private static void SelectedSliderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        public List<ColorRange> ColorRangeList
        {
            get { return (List<ColorRange>)GetValue(ColorRangeListProperty); }
            set { SetValue(ColorRangeListProperty, value); }
        }

        public static readonly DependencyProperty ColorRangeListProperty =
            DependencyProperty.Register("ColorRangeList", typeof(List<ColorRange>), typeof(ColorPanel), new PropertyMetadata(null));


        public ColorRange SelectedColorRange
        {
            get { return (ColorRange)GetValue(SelectedColorRangeProperty); }
            set { SetValue(SelectedColorRangeProperty, value); }
        }

        public static readonly DependencyProperty SelectedColorRangeProperty =
            DependencyProperty.Register("SelectedColorRange", typeof(ColorRange), typeof(ColorPanel), new PropertyMetadata(null));

        private void colorSelector_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SelectedColorRange = (ColorRange)((e.OriginalSource as System.Windows.Shapes.Rectangle).DataContext);
            Console.WriteLine($"HueFrom {SelectedColorRange.HueMinimum}  HueTo {SelectedColorRange.HueMaximum}");
        }
    }
}
