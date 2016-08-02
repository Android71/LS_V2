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
using LS_Library;
using static LS_Designer_WPF.Controls.HslSlider;
using System.ComponentModel;

namespace LS_Designer_WPF.Controls
{
    

    public partial class ColorPanel : UserControl
    {
        public ColorPanel()
        {
            InitializeComponent();
            (Content as FrameworkElement).DataContext = this;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
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
            list.Add(new ColorRange(Media.Color.FromRgb(255, 213, 0), Media.Color.FromRgb(255, 255, 0)));   // 50-60
            list.Add(new ColorRange(Media.Color.FromRgb(255, 170, 0), Media.Color.FromRgb(255, 213, 0)));   // 40-50
            list.Add(new ColorRange(Media.Color.FromRgb(255, 85, 0), Media.Color.FromRgb(255, 170, 0)));    // 20-40
            list.Add(new ColorRange(Media.Color.FromRgb(255, 0, 0), Media.Color.FromRgb(255, 85, 0)));      // 0-20
            list.Add(new ColorRange(Media.Colors.Black, Media.Colors.Black));                                
            
            ColorRangeList = list;
            //SelectedColorRange = ColorRangeList[ColorRangeList.Count - 1];
            //hueSlider.ValueChanged += Slier
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
            ColorPanel panel = null;
            if (d != null)
            {
                panel = (ColorPanel)d;
                if (e.NewValue != null)
                {
                    SliderItem si = (SliderItem)e.NewValue;

                    si.LightnessChanged += panel.UpdateLightness;

                    PatternPoint pp = si.PatternPoint;
                    
                    foreach (ColorRange cr in panel.ColorRangeList)
                    {
                        if (cr.HueMinimum <= pp.H)
                        {
                            //Console.WriteLine($"HueRange: {cr.HueMinimum} - {cr.HueMaximum}");
                            panel.hueSlider.UpdateScaleGradient(cr);
                            break;
                        }
                    }
                    panel.SetSlidersValue(si.PatternPoint);
                    if (si.Variant != PointVariant.Lightness)
                    {
                        panel.hueSlider.IsEnabled = true;
                        panel.satSlider.IsEnabled = true;
                        panel.lightSlider.IsEnabled = true;
                    }
                    else
                    {
                        panel.hueSlider.IsEnabled = false;
                        panel.satSlider.IsEnabled = false;
                        panel.lightSlider.IsEnabled = true;
                    }
                }
                else
                {
                    panel.hueSlider.IsEnabled = false;
                    panel.satSlider.IsEnabled = false;
                    panel.lightSlider.IsEnabled = false;
                }
            }
        }

        void UpdateLightness(object sender, EventArgs e)
        {
            lightSlider.Value = SelectedSlider.PatternPoint.L;
        }

        public void SetSlidersValue(PatternPoint pp)
        {
            hueSlider.ExternalCall = true;
            satSlider.ExternalCall = true;
            lightSlider.ExternalCall = true;

            hueSlider.Value = pp.H;
            satSlider.Value = pp.S;
            lightSlider.Value = pp.L;

            UpdateColorInfo(pp.PointColor);

            hueSlider.ExternalCall = false;
            satSlider.ExternalCall = false;
            lightSlider.ExternalCall = false;
        }

        void SetSlidersValue(double H, double S, double L)
        {
            Media.Color c;

            hueSlider.ExternalCall = true;
            satSlider.ExternalCall = true;
            lightSlider.ExternalCall = true;

            hueSlider.Value = H;
            satSlider.Value = S;
            lightSlider.Value = L;

            c = ColorUtilities.Hsl2MediaColor(H, S, L);
            UpdateColorInfo(c);

            hueSlider.ExternalCall = false;
            satSlider.ExternalCall = false;
            lightSlider.ExternalCall = false;
        }

        void UpdateColorInfo(Media.Color c)
        {
            hueValue.Content = Math.Round(hueSlider.Value).ToString();
            rValue.Content = c.R.ToString();
            satValue.Content = Math.Round(satSlider.Value * 100).ToString();
            gValue.Content = c.G.ToString();
            lightValue.Content = Math.Round(lightSlider.Value * 100).ToString();
            bValue.Content = c.B.ToString();
        }

        public List<ColorRange> ColorRangeList
        {
            get { return (List<ColorRange>)GetValue(ColorRangeListProperty); }
            set { SetValue(ColorRangeListProperty, value); }
        }

        public static readonly DependencyProperty ColorRangeListProperty =
            DependencyProperty.Register("ColorRangeList", typeof(List<ColorRange>), typeof(ColorPanel), new PropertyMetadata(null));

        #region Mouse Input

        private void colorSelector_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //Media.Color color;
            ColorRange cr = (ColorRange)((e.OriginalSource as System.Windows.Shapes.Rectangle).DataContext);
            Console.WriteLine($"HueFrom {cr.HueMinimum}  HueTo {cr.HueMaximum}");
            if (SelectedSlider != null)
            {
                if (SelectedSlider.Variant != PointVariant.Lightness)
                {
                    hueSlider.UpdateScaleGradient(cr);
                    SetSlidersValue(cr.HueMiddle, 1.0, 0.5);
                    SelectedSlider.PatternPoint.H = cr.HueMiddle;
                    SelectedSlider.PatternPoint.S = 1.0;
                    SelectedSlider.PatternPoint.L = 0.5;
                    SelectedSlider.PatternPoint.SaveLightness();
                    SelectedSlider.PatternPoint.UpdateColor();
                    SelectedSlider.UpdatePattern();
                }
            }
        }

        #endregion

        private void hslSliders_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            HslSlider slider = (HslSlider)sender;
            if (slider.ColorScale == ColorScaleEnum.H)
                SelectedSlider.PatternPoint.H = e.NewValue;
            if (slider.ColorScale == ColorScaleEnum.S)
                SelectedSlider.PatternPoint.S = e.NewValue;
            if (slider.ColorScale == ColorScaleEnum.L)
            {
                SelectedSlider.PatternPoint.L = e.NewValue;
                SelectedSlider.PatternPoint.SaveLightness();
                //SelectedSlider.PatternPoint.InitialL = SelectedSlider.PatternPoint.L;
            }
            SelectedSlider.PatternPoint.UpdateColor();
            SelectedSlider.UpdatePattern();
            UpdateColorInfo(SelectedSlider.PatternPoint.PointColor);
        }

    }
}
