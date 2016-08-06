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

        public void SetPanel(SliderTypeEnum variant)
        {
            switch (variant)
            {
                case SliderTypeEnum.RGB:
                    rangePointer.Visibility = Visibility.Visible;
                    colorSelector.Visibility = Visibility.Visible;
                    huePart.Visibility = Visibility.Visible;
                    satPart.Visibility = Visibility.Visible;
                    lightPart.Visibility = Visibility.Visible;
                    whitePart.Visibility = Visibility.Collapsed;
                    tempPart.Visibility = Visibility.Collapsed;
                    coldPart.Visibility = Visibility.Collapsed;
                    warmPart.Visibility = Visibility.Collapsed;
                    break;

                case SliderTypeEnum.W:
                    rangePointer.Visibility = Visibility.Collapsed;
                    colorSelector.Visibility = Visibility.Collapsed;
                    huePart.Visibility = Visibility.Collapsed;
                    satPart.Visibility = Visibility.Collapsed;
                    lightPart.Visibility = Visibility.Collapsed;
                    whitePart.Visibility = Visibility.Visible;
                    tempPart.Visibility = Visibility.Collapsed;
                    coldPart.Visibility = Visibility.Collapsed;
                    warmPart.Visibility = Visibility.Collapsed;
                    break;
            }
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
        }

        /****************************************************************************/

        #region DP

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
                SliderItem si = (SliderItem)e.NewValue;
                //panel.SetVisualContent(si);
                if (si != null)
                {
                    PatternPoint pp = si.PatternPoint;
                    panel.SetSlidersValues(si);
                    panel.TuneHandlersAndSliders(si, pp);
                }
                //else
                //{
                //    panel.hueSlider.IsEnabled = false;
                //    panel.satSlider.IsEnabled = false;
                //    panel.lightSlider.IsEnabled = false;
                //}
            }
        }

        public List<ColorRange> ColorRangeList
        {
            get { return (List<ColorRange>)GetValue(ColorRangeListProperty); }
            set { SetValue(ColorRangeListProperty, value); }
        }

        public static readonly DependencyProperty ColorRangeListProperty =
            DependencyProperty.Register("ColorRangeList", typeof(List<ColorRange>), typeof(ColorPanel), new PropertyMetadata(null));


        #region Generic methods

        void SetVisualContent(SliderItem si)
        {
            // меняем набор слайдеров соотвествующий si.SliderType
            if (si != null)
            {
                switch (si.SliderType)
                {
                    case SliderTypeEnum.RGB:
                        rangePointer.Visibility = Visibility.Visible;
                        colorSelector.Visibility = Visibility.Visible;
                        huePart.Visibility = Visibility.Visible;
                        satPart.Visibility = Visibility.Visible;
                        lightPart.Visibility = Visibility.Visible;
                        whitePart.Visibility = Visibility.Collapsed;
                        tempPart.Visibility = Visibility.Collapsed;
                        coldPart.Visibility = Visibility.Collapsed;
                        warmPart.Visibility = Visibility.Collapsed;
                        break;
                }
            }
            else
            {
                rangePointer.Visibility = Visibility.Collapsed;
                colorSelector.Visibility = Visibility.Collapsed;
                huePart.Visibility = Visibility.Collapsed;
                satPart.Visibility = Visibility.Collapsed;
                lightPart.Visibility = Visibility.Collapsed;
                whitePart.Visibility = Visibility.Collapsed;
                tempPart.Visibility = Visibility.Collapsed;
                coldPart.Visibility = Visibility.Collapsed;
                warmPart.Visibility = Visibility.Collapsed;
            }
        }


        void SetSlidersValues(SliderItem si)
        {
            switch (si.SliderType)
            {
                case SliderTypeEnum.RGB:
                    SetHSLSlidersValue(si.PatternPoint);
                    break;
            }
        }

        void TuneHandlersAndSliders(SliderItem si, PatternPoint pp)
        {
            switch (si.SliderType)
            {
                case SliderTypeEnum.RGB:
                    si.LightnessChanged += UpdateLightness;
                    foreach (ColorRange cr in ColorRangeList)
                    {
                        if (cr.HueMinimum <= pp.H)
                        {
                            //Console.WriteLine($"HueRange: {cr.HueMinimum} - {cr.HueMaximum}");
                            hueSlider.UpdateScaleGradient(cr);
                            break;
                        }
                    }

                    if (si.Variant != PointVariant.Lightness)
                    {
                        hueSlider.IsEnabled = true;
                        satSlider.IsEnabled = true;
                        lightSlider.IsEnabled = true;
                    }
                    else
                    {
                        hueSlider.IsEnabled = false;
                        satSlider.IsEnabled = false;
                        lightSlider.IsEnabled = true;
                    }
                    break;
            }
        }

        #endregion


        #endregion

        /****************************************************************************/

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
                    SetHSLSlidersValue(cr.HueMiddle, 1.0, 0.5);
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

        /****************************************************************************/

        #region SliderType RGB

        private void hslSliders_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            HslSlider slider = (HslSlider)sender;
            if (slider.ColorScale == SliderScaleEnum.H)
                SelectedSlider.PatternPoint.H = e.NewValue;
            if (slider.ColorScale == SliderScaleEnum.S)
                SelectedSlider.PatternPoint.S = e.NewValue;
            if (slider.ColorScale == SliderScaleEnum.L)
            {
                SelectedSlider.PatternPoint.L = e.NewValue;
                SelectedSlider.PatternPoint.SaveLightness();
            }
            SelectedSlider.PatternPoint.UpdateColor();
            SelectedSlider.UpdatePattern();
            UpdateHSLColorInfo(SelectedSlider.PatternPoint.PointColor);
        }

        void UpdateHSLColorInfo(Media.Color c)
        {
            hueValue.Content = Math.Round(hueSlider.Value).ToString();
            rValue.Content = c.R.ToString();
            satValue.Content = Math.Round(satSlider.Value * 100).ToString();
            gValue.Content = c.G.ToString();
            lightValue.Content = Math.Round(lightSlider.Value * 100).ToString();
            bValue.Content = c.B.ToString();
        }

        public void SetHSLSlidersValue(PatternPoint pp)
        {
            hueSlider.ExternalCall = true;
            satSlider.ExternalCall = true;
            lightSlider.ExternalCall = true;

            hueSlider.Value = pp.H;
            satSlider.Value = pp.S;
            lightSlider.Value = pp.L;

            UpdateHSLColorInfo(pp.PointColor);

            hueSlider.ExternalCall = false;
            satSlider.ExternalCall = false;
            lightSlider.ExternalCall = false;
        }

        void SetHSLSlidersValue(double H, double S, double L)
        {
            Media.Color c;

            hueSlider.ExternalCall = true;
            satSlider.ExternalCall = true;
            lightSlider.ExternalCall = true;

            hueSlider.Value = H;
            satSlider.Value = S;
            lightSlider.Value = L;

            c = ColorUtilities.Hsl2MediaColor(H, S, L);
            UpdateHSLColorInfo(c);

            hueSlider.ExternalCall = false;
            satSlider.ExternalCall = false;
            lightSlider.ExternalCall = false;
        }

        void UpdateLightness(object sender, EventArgs e)
        {
            lightSlider.Value = SelectedSlider.PatternPoint.L;
        }


        private void WT_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void CW_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        #endregion

        /****************************************************************************/

    }
}
