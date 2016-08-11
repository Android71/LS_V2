using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Media = System.Windows.Media;
using LS_Library;

namespace LS_Designer_WPF.Controls
{
    public partial class ColorPanel : UserControl
    {
        bool blockChangePoint = false;

        public ColorPanel()
        {
            InitializeComponent();
            (Content as FrameworkElement).DataContext = this;
        }

        ColorRange SelectedRange { get; set; }

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
            ColorRange rangeToSelect = null;
            ColorPanel panel = null;
            if (d != null)
            {
                panel = (ColorPanel)d;
                SliderItem si = panel.SelectedSlider;
                
                if (si != null)
                {
                    if (si.SliderType == SliderTypeEnum.RGB)
                    {   // Установка указателя на цветовой диапазоно
                        foreach (ColorRange cr in panel.ColorRangeList)
                        {
                            cr.IsSelected = false;
                            if (cr.HueMinimum <= si.PatternPoint.H)
                            {
                                if (rangeToSelect == null)
                                {
                                    rangeToSelect = cr;
                                    rangeToSelect.IsSelected = true;
                                }
                            }
                        }
                        panel.SelectedRange = rangeToSelect;
                        panel.hueSlider.UpdateScaleGradient(panel.SelectedRange);
                    }

                    panel.blockChangePoint = true;

                    panel.SetSlidersValues(si);
                    panel.PrepareSliderBehaviors(si);

                    panel.blockChangePoint = false;
                }
                else
                {
                    panel.hueSlider.IsEnabled = false;
                    panel.satSlider.IsEnabled = false;
                    panel.lightSlider.IsEnabled = false;
                    panel.whiteSlider.IsEnabled = false;
                    panel.tempSlider.IsEnabled = false;
                    panel.warmSlider.IsEnabled = false;
                    panel.coldSlider.IsEnabled = false;
                }
            }
        }

        public List<ColorRange> ColorRangeList
        {
            get { return (List<ColorRange>)GetValue(ColorRangeListProperty); }
            set { SetValue(ColorRangeListProperty, value); }
        }

        public static readonly DependencyProperty ColorRangeListProperty =
            DependencyProperty.Register("ColorRangeList", typeof(List<ColorRange>), typeof(ColorPanel), new PropertyMetadata(null));




        #endregion

        /****************************************************************************/

        #region Mouse Input

        private void colorSelector_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ColorRange cr = (ColorRange)((e.OriginalSource as System.Windows.Shapes.Rectangle).DataContext);
            Console.WriteLine($"HueFrom {cr.HueMinimum}  HueTo {cr.HueMaximum}");
            if (SelectedSlider != null)
            {
                if (SelectedSlider.Variant != PointVariant.Lightness)
                {
                    SliderItem si = SelectedSlider;

                    hueSlider.UpdateScaleGradient(cr);

                    si.PatternPoint.H = cr.HueMiddle;
                    si.PatternPoint.S = si.PatternPoint.S;//= 1.0;
                    si.PatternPoint.L = si.PatternPoint.L; //0.5;
                    si.PatternPoint.SaveLightness();
                    si.PatternPoint.UpdatePoint_RGB();

                    blockChangePoint = true;
                    SetSlidersValues(si);
                    blockChangePoint = false;

                    if (si.Variant == PointVariant.RangeLeft)
                        si.PatternPoint.CopyTo_RGB(si.Owner[si.Ix + 1].PatternPoint);
                    if (si.Variant == PointVariant.RangeRight)
                        si.PatternPoint.CopyTo_RGB(si.Owner[si.Ix - 1].PatternPoint);

                    si.PatternPoint.SaveLightness();
                    si.PatternPoint.UpdatePoint_RGB();
                    si.UpdatePattern();
                }
                if (SelectedRange != null)
                    SelectedRange.IsSelected = false;
                cr.IsSelected = true;
                SelectedRange = cr;
            }
        }

        #endregion

        /****************************************************************************/

        #region Base methods


        // Метод вызывает EffectUC
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

                case SliderTypeEnum.WT:
                    rangePointer.Visibility = Visibility.Collapsed;
                    colorSelector.Visibility = Visibility.Collapsed;
                    huePart.Visibility = Visibility.Collapsed;
                    satPart.Visibility = Visibility.Collapsed;
                    lightPart.Visibility = Visibility.Collapsed;
                    whitePart.Visibility = Visibility.Visible;
                    tempPart.Visibility = Visibility.Visible;
                    coldPart.Visibility = Visibility.Collapsed;
                    warmPart.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        void UpdateSlidersFromWheel(object sender, WheelEventArgs e)
        {
            blockChangePoint = true;
            switch (SelectedSlider.SliderType)
            {
                case SliderTypeEnum.RGB:
                    //SelectedSlider.PatternPoint.L = (double)e.NewValue;
                    SelectedSlider.PatternPoint.SaveLightness();
                    lightSlider.Value = SelectedSlider.PatternPoint.L;
                    break;
                case SliderTypeEnum.W:
                    //SelectedSlider.PatternPoint.WhiteD = (double)e.NewValue;
                    whiteSlider.Value = SelectedSlider.PatternPoint.WhiteD;
                    break;
            }
            blockChangePoint = false;
        }

        void SetSlidersValues(SliderItem si)
        {
            
            switch (si.SliderType)
            {
                case SliderTypeEnum.RGB:
                    hueSlider.Value = si.PatternPoint.H;
                    satSlider.Value = si.PatternPoint.S;
                    lightSlider.Value = si.PatternPoint.L;
                    UpdateSlidersInfo(si.PatternPoint);
                    break;

                case SliderTypeEnum.W:
                    whiteSlider.Value = si.PatternPoint.WhiteD;
                    UpdateSlidersInfo(si.PatternPoint);
                    break;
                case SliderTypeEnum.WT:
                    whiteSlider.Value = si.PatternPoint.WhiteD;
                    tempSlider.Value = si.PatternPoint.Temp;
                    UpdateSlidersInfo(si.PatternPoint);
                    break;
            }
        }

        void UpdateSlidersInfo(PatternPoint pp)
        {
            switch (SelectedSlider.SliderType)
            {
                case SliderTypeEnum.RGB:
                    hueValue.Content = Math.Round(hueSlider.Value).ToString();
                    rValue.Content = pp.PointColor.R.ToString();
                    satValue.Content = Math.Round(satSlider.Value * 100).ToString();
                    gValue.Content = pp.PointColor.G.ToString();
                    lightValue.Content = Math.Round(lightSlider.Value * 100).ToString();
                    bValue.Content = pp.PointColor.B.ToString();
                    break;
                case SliderTypeEnum.W:
                    whiteValue.Content = Convert.ToInt32(pp.WhiteD * 255.0).ToString();
                    break;
                case SliderTypeEnum.WT:
                    whiteValue.Content = Convert.ToInt32(pp.WhiteD * 255.0).ToString();
                    tempValue.Content = Convert.ToInt32(pp.Temp * 9000.0 + 1000.0).ToString();
                    break;
            }
        }

        void PrepareSliderBehaviors(SliderItem si/*, PatternPoint pp*/)
        {
            si.WheelVariableChanged += UpdateSlidersFromWheel;

            switch (si.SliderType)
            {
                case SliderTypeEnum.RGB:
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
                case SliderTypeEnum.W:
                    whiteSlider.IsEnabled = true;
                    break;
                case SliderTypeEnum.WT:
                    if (si.Variant != PointVariant.Lightness)
                    {
                        whiteSlider.IsEnabled = true;
                        tempSlider.IsEnabled = true;
                    }
                    else
                    {
                        whiteSlider.IsEnabled = true;
                        tempSlider.IsEnabled = false;
                    }
                    break;
            }
        }

        #endregion

        /****************************************************************************/

        #region ValueChanged Handlers

        private void hslSliders_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SliderItem si = SelectedSlider;
            if (!blockChangePoint)
            {
                HslSlider slider = (HslSlider)sender;
                if (slider.ColorScale == SliderScaleEnum.H)
                    si.PatternPoint.H = e.NewValue;
                if (slider.ColorScale == SliderScaleEnum.S)
                    si.PatternPoint.S = e.NewValue;
                if (slider.ColorScale == SliderScaleEnum.L)
                {
                    si.PatternPoint.L = e.NewValue;
                    si.PatternPoint.SaveLightness();
                }

                si.PatternPoint.UpdatePoint_RGB();

                if (si.Variant == PointVariant.RangeLeft)
                    si.PatternPoint.CopyTo_RGB(si.Owner[si.Ix + 1].PatternPoint);
                if (si.Variant == PointVariant.RangeRight)
                    si.PatternPoint.CopyTo_RGB(si.Owner[si.Ix - 1].PatternPoint);

                si.UpdatePattern();
            }
            UpdateSlidersInfo(si.PatternPoint);
        }

        private void WT_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SliderItem si = SelectedSlider;
            if (!blockChangePoint)
            {
                if (si != null)
                {
                    HslSlider slider = (HslSlider)sender;
                    switch (slider.ColorScale)
                    {
                        case SliderScaleEnum.W:
                            si.PatternPoint.WhiteD = e.NewValue;
                            si.PatternPoint.InitialWhiteD = e.NewValue;

                            if (si.Variant == PointVariant.RangeLeft)
                                si.PatternPoint.CopyTo_White(si.Owner[si.Ix + 1].PatternPoint);
                            if (si.Variant == PointVariant.RangeRight)
                                si.PatternPoint.CopyTo_White(si.Owner[si.Ix - 1].PatternPoint);
                            break;

                        case SliderScaleEnum.T:
                            si.PatternPoint.Temp = e.NewValue;
                            if (si.Variant == PointVariant.RangeLeft)
                                si.PatternPoint.CopyTo_WT(si.Owner[si.Ix + 1].PatternPoint);
                            if (si.Variant == PointVariant.RangeRight)
                                si.PatternPoint.CopyTo_WT(si.Owner[si.Ix - 1].PatternPoint);
                            break;
                    }

                    si.UpdatePattern();
                }
            }
            UpdateSlidersInfo(si.PatternPoint);
        }

        private void CW_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        #endregion

        /****************************************************************************/

    }
}
