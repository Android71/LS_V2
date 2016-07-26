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

        #region SliderList

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

        #endregion

        public int AddMode
        {
            get { return (int)GetValue(AddModeProperty); }
            set { SetValue(AddModeProperty, value); }
        }

        public static readonly DependencyProperty AddModeProperty =
            DependencyProperty.Register("AddMode", typeof(int), typeof(MultiSlider), new PropertyMetadata(0));

        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

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

        public static readonly DependencyProperty MaxlimitProperty =
            DependencyProperty.Register("Maxlimit", typeof(int), typeof(MultiSlider), new PropertyMetadata(0));

        public ICommand UpdatePatternCommand
        {
            get { return (ICommand)GetValue(UpdatePatternCommandProperty); }
            set { SetValue(UpdatePatternCommandProperty, value); }
        }

        public static readonly DependencyProperty UpdatePatternCommandProperty =
            DependencyProperty.Register("UpdatePatternCommand", typeof(ICommand), typeof(MultiSlider), new PropertyMetadata(null));



        public SliderItem SelectedSlider
        {
            get { return (SliderItem)GetValue(SelectedSliderProperty); }
            set { SetValue(SelectedSliderProperty, value); }
        }

        public static readonly DependencyProperty SelectedSliderProperty =
            DependencyProperty.Register("SelectedSlider", typeof(SliderItem), typeof(MultiSlider), new PropertyMetadata(null));


        #endregion

        #region SliderItem Handlers

        bool startMove = false;

        private void SliderItemGotMouseCapture(object sender, MouseEventArgs e)
        {
            if (SelectedSlider != null)
            {
                SelectedSlider.IsSelected = false;
                
            }
            SliderItem si = sender as SliderItem;
            sliderIx = SliderList.IndexOf(si);
            SelectedSlider = si;
            SelectedSlider.IsSelected = true;
            Value = (int)SelectedSlider.Value;
            if (sliderIx == 0)
            {
                // firtst slider in list
                si.SelectionStart = 1;
                if (SelectedSlider.Variant == PointVariant.RangeLeft)
                    si.SelectionEnd = SliderList[sliderIx + 1].Value;
                else
                    si.SelectionEnd = SliderList[sliderIx + 1].Value - 1;
                return;
            }

            if (sliderIx == SliderList.Count - 1)
            {
                //last slider in list
                si.SelectionEnd = Maxlimit;
                if (SelectedSlider.Variant == PointVariant.RangeRight)
                    si.SelectionStart = SliderList[sliderIx - 1].Value;
                else
                    si.SelectionStart = SliderList[sliderIx - 1].Value + 1;
                return;
            }

            // internal slider
            if (SelectedSlider.Variant == PointVariant.RangeRight)
            {
                si.SelectionStart = SliderList[sliderIx - 1].Value;
                si.SelectionEnd = SliderList[sliderIx + 1].Value - 1;
                return;
            }

            if (SelectedSlider.Variant == PointVariant.RangeLeft)
            {
                si.SelectionStart = SliderList[sliderIx - 1].Value + 1;
                si.SelectionEnd = SliderList[sliderIx + 1].Value;
                return;
            }

            if (SelectedSlider.Variant != PointVariant.RangeLeft && SelectedSlider.Variant != PointVariant.RangeRight)
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
                UpdatePatternCommand.Execute(new SliderDuplet(SliderList[sliderIx - 1], si, true));
                UpdatePatternCommand.Execute(new SliderDuplet(si, SliderList[sliderIx + 1], true));
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
                UpdatePatternCommand.Execute(new SliderDuplet(null, si));
                UpdatePatternCommand.Execute(new SliderDuplet(si, SliderList[sliderIx + 1]));
                return;
            }
            if (sliderIx == SliderList.Count - 1)
            {
                // Last slider movement
                UpdatePatternCommand.Execute(new SliderDuplet(si, null));
                UpdatePatternCommand.Execute(new SliderDuplet(SliderList[sliderIx - 1], si));
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

            UpdatePatternCommand.Execute(new SliderDuplet(SliderList[sliderIx - 1], si));
            UpdatePatternCommand.Execute(new SliderDuplet(si, SliderList[sliderIx + 1]));

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
            if (e.ClickCount == 2)      // DoubleClick
            {
                System.Windows.Point pt = e.GetPosition((UIElement)sender);
                double halfStep = (slidersArea.ActualWidth + 2 * Margin.Left) / this.Maxlimit;
                int sliderPos = Convert.ToInt32(pt.X / halfStep) + 1;
                Console.WriteLine($"ActualWidth + Margin {slidersArea.ActualWidth + 2 * this.Margin.Left}");
                Console.WriteLine($"ptX {pt.X}");
                Console.WriteLine($"LedPos: {sliderPos}");
                Console.WriteLine($"CurrentMode: {AddMode}");

                // AddMode = 0 Gradient
                // AddMode = 1 Range
                // AddMode = 2 Lightness

                PatternPoint pp = Pattern[sliderPos - 1];

                // если первая точка в Pattern
                if (sliderPos < SliderList[0].Value)
                {
                    SliderItem tmpSi = SliderList[0];
                    switch (AddMode)
                    {
                        case 1:   //Range
                            SliderList[0].PatternPoint.CopyTo(pp);
                            SliderItem rangeLeft = CreateSlider(sliderPos, Maxlimit, pp);
                            rangeLeft.Variant = PointVariant.RangeLeft;
                            SliderItem rangeRight = CreateSlider(sliderPos, Maxlimit, pp);
                            rangeRight.Variant = PointVariant.RangeRight;
                            SliderList.Insert(0, rangeRight);
                            SliderList.Insert(0, rangeLeft);
                            ReArrangeSliderItems();

                            //UpdatePatternCommand.Execute(new SliderDuplet(rangeLeft, rangeRight));
                            UpdatePatternCommand.Execute(new SliderDuplet(rangeRight, tmpSi));
                            break;
                        case 2:     //Lightness
                            break;
                        default:    //Gradient        
                            SliderList[0].PatternPoint.CopyTo(pp);
                            SliderItem gradient = CreateSlider(sliderPos, Maxlimit, pp);
                            SliderList.Insert(0, gradient);
                            ReArrangeSliderItems();

                            UpdatePatternCommand.Execute(new SliderDuplet(gradient, tmpSi));
                            break;
                    }
                    return;
                }

                // если последняя точка в Pattern
                if (sliderPos > SliderList[SliderList.Count - 1].Value)
                {
                    SliderItem lastSi = SliderList[SliderList.Count - 1];
                    switch (AddMode)
                    {
                        case 1:     // Range
                            SliderList[SliderList.Count - 1].PatternPoint.CopyTo(pp);
                            SliderItem rangeLeft = CreateSlider(sliderPos, Maxlimit, pp);
                            rangeLeft.Variant = PointVariant.RangeLeft;
                            SliderItem rangeRight = CreateSlider(sliderPos, Maxlimit, pp);
                            rangeRight.Variant = PointVariant.RangeRight;
                            SliderList.Add(rangeLeft);
                            SliderList.Add(rangeRight);
                            ReArrangeSliderItems();

                            UpdatePatternCommand.Execute(new SliderDuplet(lastSi, rangeLeft));
                            //UpdatePatternCommand.Execute(new SliderDuplet(rangeLeft, rangeRight));
                            break;
                        case 2:     //Lightness
                            break;
                        default:    //Gradient
                            SliderList[SliderList.Count - 1].PatternPoint.CopyTo(pp);
                            SliderItem gradient = CreateSlider(sliderPos, Maxlimit, pp);
                            SliderList.Add(gradient);
                            ReArrangeSliderItems();

                            UpdatePatternCommand.Execute(new SliderDuplet(lastSi, gradient));

                            break;
                    }
                    return;
                }

                // между PatternPoint
                SliderItem prevSi = null;
                SliderItem nextSi = null;
                int minLeft = Maxlimit;
                int minRight = Maxlimit;
                foreach(SliderItem si in SliderList)
                {
                    if (si.Value > sliderPos)
                    {
                        if ((si.Value - sliderPos)< minRight)
                        {
                            minRight = (int)si.Value - sliderPos;
                            nextSi = si;
                        }
                    }

                    if (si.Value < sliderPos)
                    {
                        if ((sliderPos - si.Value) < minLeft)
                        {
                            minLeft = sliderPos - (int)si.Value;
                            prevSi = si;
                        }
                    }
                }
                if (nextSi.Variant == PointVariant.RangeRight)
                    // нельзя добавить точку внутри Range
                    return;
                SliderItem newSlider = CreateSlider(sliderPos, Maxlimit, Pattern[sliderPos - 1]);
                int ix = SliderList.IndexOf(nextSi);
                if (AddMode == 1)
                {
                    newSlider.Variant = PointVariant.RangeLeft;
                    SliderItem rangeRight = CreateSlider(sliderPos, Maxlimit, Pattern[sliderPos - 1]);
                    rangeRight.Variant = PointVariant.RangeRight;
                    SliderList.Insert(ix, rangeRight);
                    SliderList.Insert(ix, newSlider);
                    ReArrangeSliderItems();
                    return;
                }
                if (AddMode == 2)
                    newSlider.Variant = PointVariant.Lightness;
                SliderList.Insert(ix, newSlider);
                ReArrangeSliderItems();
            }
        }

        SliderItem CreateSlider(int value, int max, PatternPoint point)
        {
            SliderItem si = new SliderItem();
            si.PatternPoint = point;
            si.Minimum = 1;
            si.Maximum = max;
            si.SelectionStart = 1;
            si.SelectionEnd = max;
            si.Value = value;
            return si;
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);
            if (SelectedSlider != null)
            {
                //Console.WriteLine($"Delta: {e.Delta}");
                if (e.Delta > 0)
                {
                    SelectedSlider.PatternPoint.L += 0.02;
                    if (SelectedSlider.PatternPoint.L > 1.0)
                        SelectedSlider.PatternPoint.L = 1.0;
                }
                else
                {
                    SelectedSlider.PatternPoint.L -= 0.02;
                    if (SelectedSlider.PatternPoint.L < 0.0)
                        SelectedSlider.PatternPoint.L = 0.0;
                }
            }
            SelectedSlider.PatternPoint.UpdateColor();
            UpdatePattern(SelectedSlider);
        }

        void UpdatePattern(SliderItem currentSlider)
        {
            SliderItem prevSlider = null;
            SliderItem nextSlider = null;
            SliderItem afterRangeRight = null;
            SliderItem beforeRangeLeft = null;

            if (currentSlider != null)
            {
                int ix = SliderList.IndexOf(currentSlider);
                if (ix != 0)
                    prevSlider = SliderList[ix - 1];
                if (ix != SliderList.Count - 1)
                    nextSlider = SliderList[ix + 1];

                if (currentSlider.Variant == PointVariant.RangeLeft)
                {
                    currentSlider.PatternPoint.CopyTo(nextSlider.PatternPoint);
                    if ((ix + 1) != (SliderList.Count -1))
                    {
                        // Slider RangeRight не последний Slider в SliderList
                        afterRangeRight = SliderList[ix + 2];
                        UpdatePatternCommand.Execute(new SliderDuplet(nextSlider, afterRangeRight));
                    }
                }
                if (currentSlider.Variant == PointVariant.RangeRight)
                {
                    currentSlider.PatternPoint.CopyTo(prevSlider.PatternPoint);
                    if ((ix - 1)!= 0)
                    {
                        // Slider RangeLeft не первый Slider в SliderList
                        beforeRangeLeft = SliderList[ix - 2];
                        UpdatePatternCommand.Execute(new SliderDuplet(beforeRangeLeft, prevSlider));
                    }
                }

                UpdatePatternCommand.Execute(new SliderDuplet(prevSlider, currentSlider));
                UpdatePatternCommand.Execute(new SliderDuplet(currentSlider, nextSlider));
            }
        }

        #endregion
    }
}
