using LS_Library;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LS_Designer_WPF.Controls
{
    public class MultiSlider : Control
    {
        #region Private Fields

        Grid UpSliders;
        Grid DownSliders;
        SliderItem selectedSlider = null;
        int sliderIx = -1;

        #endregion

        static MultiSlider()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MultiSlider), new FrameworkPropertyMetadata(typeof(MultiSlider)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            UpSliders = Template.FindName("PART_UpSliders", this) as Grid;
            DownSliders = Template.FindName("PART_DownSliders", this) as Grid;
            if (SliderList != null)
                //UpSliders.Children.Add(SliderList[0]);
                ReArrangeSliderItems();
        }

        void ReArrangeSliderItems()
        {
            foreach (SliderItem si in SliderList)   // ??????????
            {
                si.GotMouseCapture -= new MouseEventHandler(SliderItemGotMouseCapture);
                si.ValueChanged -= new RoutedPropertyChangedEventHandler<double>(OnSliderItemValueChanged);
            }
            DownSliders.Children.Clear();
            UpSliders.Children.Clear();
            
            int k = 0;
            foreach (SliderItem si in SliderList)
            {
                si.GotMouseCapture += new MouseEventHandler(SliderItemGotMouseCapture);
                si.ValueChanged += new RoutedPropertyChangedEventHandler<double>(OnSliderItemValueChanged);
                if (k % 2 == 0)
                    // чётный 
                    UpSliders.Children.Add(si);
                else
                    // нечётный
                    DownSliders.Children.Add(si);
                k++;
            }
        }

        #region DP

        private void SliderItemGotMouseCapture(object sender, MouseEventArgs e)
        {
            if (selectedSlider != null)
                selectedSlider.IsSelected = false;
            SliderItem si = sender as SliderItem;
            sliderIx = SliderList.IndexOf(si);
            //clickedIx = si.Position;
            selectedSlider = si;
            selectedSlider.IsSelected = true;
            //SelectedPoint = Pattern[si.PatternIx];
            //valueLabel.Text = ((int)selectedSliderItem.Value).ToString();
        }

        private void OnSliderItemValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //SliderItem s = sender as SliderItem;
            //int ix = sliders.IndexOf(s);
            //if (e.NewValue > e.OldValue)
            //{
            //    if (ix != sliders.Count - 1)
            //    {
            //        if (e.NewValue >= sliders[ix + 1].Value - 1)
            //            s.Value = sliders[ix + 1].Value - 1;
            //    }
            //}
            //else
            //{
            //    if (ix != 0)
            //    {
            //        if (e.NewValue <= sliders[ix - 1].Value + 1)
            //            s.Value = sliders[ix - 1].Value + 1;
            //    }
            //}
            //valueLabel.Text = ((int)s.Value).ToString();
            //if ((s.Variant == SliderVariant.RangeLeftLimit) || (s.Variant == SliderVariant.Gradient) || (s.Variant == SliderVariant.Lightness))
            //    SelectedPoint.LedPos = (int)s.Value;
            //if (s.Variant == SliderVariant.RangeLeftLimit)
            //    SelectedPoint.LedCount = (int)(sliders[ix + 1].Value - s.Value + 1);
            //if (s.Variant == SliderVariant.RangeRightLimit)
            //    SelectedPoint.LedCount = (int)s.Value - (int)sliders[ix - 1].Value + 1;
            ////Console.WriteLine("LedPos: {0}   LedCount:  {1}", SelectedPoint.LedPos, SelectedPoint.LedCount);
            //UpdateModel();

        }

        public ObservableCollection<SliderItem> SliderList
        {
            get { return (ObservableCollection<SliderItem>)GetValue(SliderListProperty); }
            set { SetValue(SliderListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SliderListProperty =
            DependencyProperty.Register("SliderList", typeof(ObservableCollection<SliderItem>), typeof(MultiSlider), 
                                new PropertyMetadata(null, SliderListChanged));

        private static void SliderListChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MultiSlider ms = (MultiSlider)d;
            if (ms.UpSliders == null || ms.DownSliders == null) return;
               
            ms.ReArrangeSliderItems();
            //ms.ApplyTemplate();
            //ms.UpSliders = ms.Template.FindName("PART_UpSliders", ms) as Grid;
            //ms.UpSliders.Children.Add(ms.SliderList[0]);
        }



        public PatternPoint[] Pattern
        {
            get { return (PatternPoint[])GetValue(PatternProperty); }
            set { SetValue(PatternProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Pattern.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PatternProperty =
            DependencyProperty.Register("Pattern", typeof(PatternPoint[]), typeof(MultiSlider), new PropertyMetadata(null));


        #endregion
    }
}
