using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
//using System.Windows.Media;
using Media = System.Windows.Media;
using LS_Library;
using System.Drawing;

namespace LS_Designer_WPF.Controls
{

    public class HslSlider : Slider
    {
        static HslSlider()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HslSlider), new FrameworkPropertyMetadata(typeof(HslSlider)));
        }

        public enum ColorScaleEnum { H, S, L};

        #region Private Fields

        Border scale;

        #endregion

        /*****************************************************************************/

        #region Dependency Properties



        public double HueValue
        {
            get { return (double)GetValue(HueValueProperty); }
            set { SetValue(HueValueProperty, value); }
        }

        public static readonly DependencyProperty HueValueProperty =
            DependencyProperty.Register("HueValue", typeof(double), typeof(HslSlider), new PropertyMetadata(0.0, OnHueValueChanged));

        private static void OnHueValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            HslSlider slider = d as HslSlider;
            slider.MakeGradient();
        }

        public ColorScaleEnum ColorScale
        {
            get { return (ColorScaleEnum)GetValue(ColorScaleProperty); }
            set { SetValue(ColorScaleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ColorScale.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColorScaleProperty =
            DependencyProperty.Register("ColorScale", typeof(ColorScaleEnum), typeof(HslSlider), new PropertyMetadata(ColorScaleEnum.H, OnColorScaleChanged));

        private static void OnColorScaleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //HslSlider slider = d as HslSlider;
            //slider.MakeGradient();
        }

        void MakeGradient_1(ColorRange cr)
        {
            Media.LinearGradientBrush lgb = new Media.LinearGradientBrush();
            lgb.StartPoint = new System.Windows.Point(0.5, 1.0);
            lgb.EndPoint = new System.Windows.Point(0.5, 0.0);
            if (ColorScale == ColorScaleEnum.H)
            {
                lgb.GradientStops.Add(new Media.GradientStop(cr.FromColor, 0.0));
                lgb.GradientStops.Add(new Media.GradientStop(cr.ToColor, 1.0));
                if (scale != null)
                    scale.Background = lgb;
                Minimum = cr.HueMinimum;
                Maximum = cr.HueMaximum;
                SmallChange = (Maximum - Minimum) / 100.0;
                LargeChange = SmallChange * 10;
            }
        }

        private void MakeGradient()
        {
            //    Media.Color color, color1;
            //    Media.LinearGradientBrush lgb = new Media.LinearGradientBrush();
            //    lgb.StartPoint = new System.Windows.Point(0.5, 1.0);
            //    lgb.EndPoint = new System.Windows.Point(0.5, 0.0);


            //    if (ColorScale == ColorScaleEnum.H)
            //    {
            //        //color = new HslColor(ScaleRange.X, 1.0, 0.5).ToRGB().MediaColor();
            //        //color1 = new HslColor(ScaleRange.Y, 1.0, 0.5).ToRGB().MediaColor();

            //        //lgb.GradientStops.Add(new Media.GradientStop(color, 0.0));
            //        //lgb.GradientStops.Add(new Media.GradientStop(color1, 1.0));

            //        if (scale != null)
            //            scale.Background = lgb;
            //    }

            //    if (ColorScale == ColorScaleEnum.S)
            //    {
            //        //color = new HslColor(HueValue, 0.0, 0.5).ToRGB().MediaColor();
            //        //color1 = new HslColor(HueValue, 1.0, 0.5).ToRGB().MediaColor();

            //        //lgb.GradientStops.Add(new Media.GradientStop(color, 0.0));
            //        //lgb.GradientStops.Add(new Media.GradientStop(color1, 1.0));

            //        if (scale != null)
            //            scale.Background = lgb;
            //    }

            //    if (ColorScale == ColorScaleEnum.L)
            //    {
            //        lgb.GradientStops.Add(new Media.GradientStop(Media.Colors.Black, 0.0));
            //        lgb.GradientStops.Add(new Media.GradientStop(Media.Colors.LightGray, 1.0));

            //        if (scale != null)
            //            scale.Background = lgb;
            //    }
        }

        public System.Windows.Point ScaleRange
        {
            get { return (System.Windows.Point)GetValue(ScaleRangeProperty); }
            set { SetValue(ScaleRangeProperty, value); }
        }

        public static readonly DependencyProperty ScaleRangeProperty =
            DependencyProperty.Register("ScaleRange", typeof(System.Windows.Point), typeof(HslSlider), 
                                        new PropertyMetadata(new System.Windows.Point(0,30), OnScaleRangeChanged));

        private static void OnScaleRangeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //HslSlider slider = d as HslSlider;
            //var x = slider.ScaleRange;
            //slider.MakeGradient();
        }

        public ColorRange ColorRange
        {
            get { return (ColorRange)GetValue(ColorRangeProperty); }
            set { SetValue(ColorRangeProperty, value); }
        }

        public static readonly DependencyProperty ColorRangeProperty =
            DependencyProperty.Register("ColorRange", typeof(ColorRange), typeof(HslSlider), new PropertyMetadata(ColorRange.BlackRange, OnColorRangeChanged));

        private static void OnColorRangeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d != null)
            {
                HslSlider slider = (HslSlider)d;
                slider.MakeGradient_1(e.NewValue as ColorRange);
            }
             
            //throw new NotImplementedException();
        }



        public SliderItem SelectedSlider
        {
            get { return (SliderItem)GetValue(SelectedSliderProperty); }
            set { SetValue(SelectedSliderProperty, value); }
        }

        public static readonly DependencyProperty SelectedSliderProperty =
            DependencyProperty.Register("SelectedSlider", typeof(SliderItem), typeof(HslSlider), new PropertyMetadata(null, OnSelectedSliderChanged));

        private static void OnSelectedSliderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }





        #endregion

        /*****************************************************************************/

        #region Public Properties

        public bool IsActive { get; set; }

        #endregion

        /*****************************************************************************/

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            scale = Template.FindName("PART_Scale", this) as Border;
            //var x = IsActive;
            //MakeGradient();
        }

        protected override void OnGotMouseCapture(MouseEventArgs e)
        {
            //base.OnGotMouseCapture(e);
            //IsActive = true;
            ////Console.WriteLine("Start changing");
        }

        protected override void OnThumbDragCompleted(DragCompletedEventArgs e)
        {
            //base.OnThumbDragCompleted(e);
            //IsActive = false;
            ////Console.WriteLine("End changing");
        }

    }
}
