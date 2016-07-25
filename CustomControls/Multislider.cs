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
        Grid slidersArea;
        Border track;

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
            slidersArea = Template.FindName("PART_Sliders", this) as Grid;
            track = Template.FindName("PART_Track", this) as Border;
            UpSliders = Template.FindName("PART_UpSliders", this) as Grid;
            DownSliders = Template.FindName("PART_DownSliders", this) as Grid;
            slidersArea.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(OnTrackClick);
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



        public int SelectedMode
        {
            get { return (int)GetValue(SelectedModeProperty); }
            set { SetValue(SelectedModeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedMode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedModeProperty =
            DependencyProperty.Register("SelectedMode", typeof(int), typeof(MultiSlider), new PropertyMetadata(0));



        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(int), typeof(MultiSlider), new PropertyMetadata(0));



        public PatternPoint[] Pattern
        {
            get { return (PatternPoint[])GetValue(PatternProperty); }
            set { SetValue(PatternProperty, value); }
        }

        public static readonly DependencyProperty PatternProperty =
            DependencyProperty.Register("Pattern", typeof(PatternPoint[]), typeof(MultiSlider), new PropertyMetadata(null));

        public int Maxlimit // PointCount
        {
            get { return (int)GetValue(MaxlimitProperty); }
            set { SetValue(MaxlimitProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Maxlimit.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxlimitProperty =
            DependencyProperty.Register("Maxlimit", typeof(int), typeof(MultiSlider), new PropertyMetadata(0));



        public ICommand UpdatePattern
        {
            get { return (ICommand)GetValue(UpdatePatternProperty); }
            set { SetValue(UpdatePatternProperty, value); }
        }

        public static readonly DependencyProperty UpdatePatternProperty =
            DependencyProperty.Register("UpdatePattern", typeof(ICommand), typeof(MultiSlider), new PropertyMetadata(null));

        #endregion

        #region SliderItem Handlers

        bool startMove = false;

        private void SliderItemGotMouseCapture(object sender, MouseEventArgs e)
        {
            if (selectedSlider != null)
            {
                selectedSlider.IsSelected = false;
                
            }
            SliderItem si = sender as SliderItem;
            //startMove = true;
            //currentIx = 
            sliderIx = SliderList.IndexOf(si);
            selectedSlider = si;
            selectedSlider.IsSelected = true;
            Value = (int)selectedSlider.Value;
            if (sliderIx == 0)
            {
                // firtst slider in list
                si.SelectionStart = 1;
                if (selectedSlider.Variant == PointVariant.RangeLeft)
                    si.SelectionEnd = SliderList[sliderIx + 1].Value;
                else
                    si.SelectionEnd = SliderList[sliderIx + 1].Value - 1;
                return;
            }

            if (sliderIx == SliderList.Count - 1)
            {
                //last slider in list
                si.SelectionEnd = Maxlimit;
                if (selectedSlider.Variant == PointVariant.RangeRight)
                    si.SelectionStart = SliderList[sliderIx - 1].Value;
                else
                    si.SelectionStart = SliderList[sliderIx - 1].Value + 1;
                return;
            }

            // internal slider
            if (selectedSlider.Variant == PointVariant.RangeRight)
            {
                si.SelectionStart = SliderList[sliderIx - 1].Value;
                si.SelectionEnd = SliderList[sliderIx + 1].Value - 1;
                return;
            }

            if (selectedSlider.Variant == PointVariant.RangeLeft)
            {
                si.SelectionStart = SliderList[sliderIx - 1].Value + 1;
                si.SelectionEnd = SliderList[sliderIx + 1].Value;
                return;
            }

            if (selectedSlider.Variant != PointVariant.RangeLeft && selectedSlider.Variant != PointVariant.RangeRight)
            {
                si.SelectionStart = SliderList[sliderIx - 1].Value + 1;
                si.SelectionEnd = SliderList[sliderIx + 1].Value - 1;
                return;
            }

        }

        int currentIx;
        private void OnSliderItemValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SliderItem si = (SliderItem)sender;
            Value = (int)si.Value;
            //si.Busy = true;
            int oldIx = Convert.ToInt32(e.OldValue);
            int newIx = Convert.ToInt32(e.NewValue);

            PatternPoint pp = Pattern[newIx - 1];
            PatternPoint sliderPoint = si.PatternPoint;
            if (si.Variant == PointVariant.Lightness)
            {
                pp.L = si.PatternPoint.L;
                pp.UpdateColor();
                si.PatternPoint = pp;
                UpdatePattern.Execute(new SliderDuplet(SliderList[sliderIx - 1], si, true));
                UpdatePattern.Execute(new SliderDuplet(si, SliderList[sliderIx + 1], true));
                return;
            }
            else
            {
                si.PatternPoint.CopyTo(pp);
            }
            si.PatternPoint = pp;
            if (sliderIx == 0)
            {
                // First slider movement
                UpdatePattern.Execute(new SliderDuplet(null, si));
                UpdatePattern.Execute(new SliderDuplet(si, SliderList[sliderIx + 1]));
                return;
            }
            if (sliderIx == SliderList.Count - 1)
            {
                // Last slider movement
                UpdatePattern.Execute(new SliderDuplet(si, null));
                UpdatePattern.Execute(new SliderDuplet(SliderList[sliderIx - 1], si));
                return;
            }
            //if (si.Variant == PointVariant.RangeLeft)
            //{
            //    UpdatePattern.Execute(new SliderDuplet(SliderList[sliderIx - 1], si));
            //    return;
            //}
            //if (si.Variant == PointVariant.RangeRight)
            //{
            //    UpdatePattern.Execute(new SliderDuplet(si, SliderList[sliderIx + 1]));
            //    return;
            //}

            UpdatePattern.Execute(new SliderDuplet(SliderList[sliderIx - 1], si));
            UpdatePattern.Execute(new SliderDuplet(si, SliderList[sliderIx + 1]));

            //si.Busy = false;


            //if (newIx > oldIx && sliderIx == 0)
            //// first Slider move right
            //{
            //    si.PatternPoint.Clear();
            //    si.PatternPoint = pp;
            //}




            //if (startMove)
            //{
            //    currentIx = newIx;
            //    startMove = false;
            //    Console.WriteLine("Start");
            //Console.WriteLine($"OldIx: {oldIx}");
            //Console.WriteLine($"NewIx: {newIx}");
            //Console.WriteLine();
            //    return;
            //}

            //if (currentIx != newIx)
            //{
            //    currentIx = newIx;
            //    Console.WriteLine($"OldIx: {oldIx}");
            //    Console.WriteLine($"NewIx: {newIx}");
            //    Console.WriteLine();
            //}

        }

        #endregion

        #region Mouse Handlers

        private void OnTrackClick(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Point pt = e.GetPosition((UIElement)sender);
            double halfStep = (slidersArea.ActualWidth + 2 * this.Margin.Left) / this.Maxlimit;
            int siderPos = Convert.ToInt32(pt.X / halfStep);
            Console.WriteLine($"ActualWidth + Margin {slidersArea.ActualWidth + 2 * this.Margin.Left}");
            Console.WriteLine($"ptX {pt.X}");
            Console.WriteLine($"LedPos: {siderPos}");
            Console.WriteLine($"CurrentMode: {SelectedMode}");
            if (siderPos < 1)
                siderPos = 1;
        }

        #endregion
    }
}
