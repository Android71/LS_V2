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


//MultiSlider.OnTrackClick
//MultiSlider.OnKeyDown
//MultiSlider.OnApplyTemplate
//MultiSlider.RearrangeSliderItems
//MultiSlider.SliderItemGotMouseCapture
//MultiSlider.OnSliderItemValueChanged

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
            //if (SliderList != null)
            //    ReArrangeSliderItems();
        }

        void ReArrangeSliderItems()
        {
            foreach (SliderItem si in SliderList)   // ??????????
            {
                si.GotMouseCapture -= new MouseEventHandler(SliderItemGotMouseCapture);
                si.ValueChanged -= new RoutedPropertyChangedEventHandler<double>(OnSliderPositionChanged);
                si.UpdatePatternCommand = UpdatePatternCommand;
            }
            DownSliders.Children.Clear();
            UpSliders.Children.Clear();
            
            int k = 0;
            foreach (SliderItem si in SliderList)
            {
                si.Ix = k;
                si.GotMouseCapture += new MouseEventHandler(SliderItemGotMouseCapture);
                si.ValueChanged += new RoutedPropertyChangedEventHandler<double>(OnSliderPositionChanged);
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

        public List<SliderItem> SliderList
        {
            get { return (List<SliderItem>)GetValue(SliderListProperty); }
            set { SetValue(SliderListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SliderListProperty =
            DependencyProperty.Register("SliderList", typeof(List<SliderItem>), typeof(MultiSlider), 
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

        public int Position
        {
            get { return (int)GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }

        public static readonly DependencyProperty PositionProperty =
            DependencyProperty.Register("Position", typeof(int), typeof(MultiSlider), new PropertyMetadata(0));

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
            DependencyProperty.Register("UpdatePatternCommand", typeof(ICommand), typeof(MultiSlider), new PropertyMetadata(null, OnUpdatePatternCommandChanged));

        private static void OnUpdatePatternCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MultiSlider ms = (MultiSlider)d;
            ms.ReArrangeSliderItems();
        }

        public SliderItem SelectedSlider
        {
            get { return (SliderItem)GetValue(SelectedSliderProperty); }
            set { SetValue(SelectedSliderProperty, value); }
        }

        public static readonly DependencyProperty SelectedSliderProperty =
            DependencyProperty.Register("SelectedSlider", typeof(SliderItem), typeof(MultiSlider), new PropertyMetadata(null));


        #endregion

        #region SliderItem Handlers

        //bool startMove = false;

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

            Position = SelectedSlider.Pos;

            // firtst slider in list
            if (sliderIx == 0)
            {
                si.SelectionStart = 1;
                if (SelectedSlider.Variant == PointVariant.RangeLeft)
                    si.SelectionEnd = SliderList[sliderIx + 1].Value;
                else
                    si.SelectionEnd = SliderList[sliderIx + 1].Value - 1;
                return;
            }

            //last slider in list
            if (sliderIx == SliderList.Count - 1)
            {
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

        //int currentIx;
        private void OnSliderPositionChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SliderItem si = (SliderItem)sender;
            Position = si.Pos;
            int newValue = Convert.ToInt32(e.NewValue);

            PatternPoint pp = Pattern[newValue - 1];

            if (si.Variant == PointVariant.Lightness)
            {
                switch (si.SliderType)
                {
                    case SliderTypeEnum.RGB:
                        pp.L = si.PatternPoint.L;
                        pp.SaveLightness();
                        pp.UpdatePoint_RGB();
                        break;

                    case SliderTypeEnum.W:
                    case SliderTypeEnum.WT:
                        pp.WhiteD = si.PatternPoint.WhiteD;
                        pp.InitialWhiteD = pp.WhiteD;
                        break;
                    case SliderTypeEnum.Warm:
                        pp.WarmD = si.PatternPoint.WarmD;
                        pp.InitialWarmD = pp.WarmD;
                        break;
                    case SliderTypeEnum.Cold:
                        pp.ColdD = si.PatternPoint.ColdD;
                        pp.InitialColdD = pp.ColdD;
                        break;
                }

                //Console.WriteLine($"DeltaT: {pp.Temp - si.PatternPoint.Temp}");

                si.PatternPoint = pp;
                UpdatePatternCommand.Execute(si);

                // для обновления ColorPanel
                SelectedSlider = null;
                SelectedSlider = si;

                return;
            }
            else
            {
                switch (si.SliderType)
                {
                    case SliderTypeEnum.RGB:
                        si.PatternPoint.CopyTo(pp, si.SliderType);
                        break;
                    case SliderTypeEnum.W:
                        si.PatternPoint.CopyTo(pp, si.SliderType);
                        break;
                    case SliderTypeEnum.WT:
                        si.PatternPoint.CopyTo(pp, si.SliderType);
                        break;
                    case SliderTypeEnum.Warm:
                        si.PatternPoint.CopyTo(pp, si.SliderType);
                        break;
                    case SliderTypeEnum.Cold:
                        si.PatternPoint.CopyTo(pp, si.SliderType);
                        break;
                }
                
            }

            si.PatternPoint = pp;
            UpdatePatternCommand.Execute(si);
        }

        #endregion

        #region Mouse Handlers

        private void OnTrackClick(object sender, MouseButtonEventArgs e)
        {
            SliderItem newSlider = null;
            if (e.ClickCount == 2)      // DoubleClick
            {
                if (e.Source is Border && (e.Source as Border).Name == "PART_Track")
                {
                    System.Windows.Point pt = e.GetPosition((UIElement)sender);
                    double halfStep = (slidersArea.ActualWidth + 2 * Margin.Left) / this.Maxlimit;
                    int sliderPos = Convert.ToInt32(pt.X / halfStep) + 1;
                    //Console.WriteLine($"ActualWidth + Margin {slidersArea.ActualWidth + 2 * this.Margin.Left}");
                    //Console.WriteLine($"ptX {pt.X}");
                    //Console.WriteLine($"LedPos: {sliderPos}");
                    //Console.WriteLine($"CurrentMode: {AddMode}");

                    // AddMode = 0 Gradient
                    // AddMode = 1 Range
                    // AddMode = 2 Lightness

                    PatternPoint pp = Pattern[sliderPos - 1];

                    // нельзя добавить слайдер в существующую позицию
                    if (SliderList.FirstOrDefault(p => p.Value == (double)sliderPos) != null)
                        return;

                    // если первая точка в Pattern
                    if (sliderPos < SliderList[0].Value)
                    {
                        SliderItem tmpSi = SliderList[0];
                        switch (AddMode)
                        {
                            case 1:   //Range
                                SliderList[0].PatternPoint.CopyTo(pp, SliderList[0].SliderType);
                                SliderItem rangeLeft = CreateSlider(sliderPos, Maxlimit, pp);
                                rangeLeft.Variant = PointVariant.RangeLeft;
                                newSlider = rangeLeft;
                                SliderItem rangeRight = CreateSlider(sliderPos, Maxlimit, pp);
                                rangeRight.Variant = PointVariant.RangeRight;
                                SliderList.Insert(0, rangeRight);
                                SliderList.Insert(0, rangeLeft);
                                ReArrangeSliderItems();
                                UpdatePatternCommand.Execute(rangeRight);
                                break;

                            case 2:     //Lightness
                                break;

                            default:    //Gradient        
                                SliderList[0].PatternPoint.CopyTo(pp, SliderList[0].SliderType);
                                SliderItem gradient = CreateSlider(sliderPos, Maxlimit, pp);
                                newSlider = gradient;
                                SliderList.Insert(0, gradient);
                                ReArrangeSliderItems();
                                UpdatePatternCommand.Execute(gradient);
                                break;
                        }
                        if (newSlider != null)
                        {
                            if (SelectedSlider != null)
                                SelectedSlider.IsSelected = false;
                            newSlider.IsSelected = true;
                            SelectedSlider = newSlider;
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
                                SliderList[SliderList.Count - 1].PatternPoint.CopyTo(pp, SliderList[0].SliderType);
                                SliderItem rangeLeft = CreateSlider(sliderPos, Maxlimit, pp);
                                rangeLeft.Variant = PointVariant.RangeLeft;
                                SliderItem rangeRight = CreateSlider(sliderPos, Maxlimit, pp);
                                rangeRight.Variant = PointVariant.RangeRight;
                                SliderList.Add(rangeLeft);
                                SliderList.Add(rangeRight);
                                ReArrangeSliderItems();
                                UpdatePatternCommand.Execute(rangeLeft);
                                break;
                            case 2:     //Lightness
                                break;
                            default:    //Gradient
                                SliderList[SliderList.Count - 1].PatternPoint.CopyTo(pp, SliderList[0].SliderType);
                                SliderItem gradient = CreateSlider(sliderPos, Maxlimit, pp);
                                SliderList.Add(gradient);
                                ReArrangeSliderItems();

                                UpdatePatternCommand.Execute(gradient);

                                break;
                        }
                        return;
                    }

                    // точка между PatternPoint
                    SliderItem prevSi = null;
                    SliderItem nextSi = null;
                    int minLeft = Maxlimit;
                    int minRight = Maxlimit;
                    foreach (SliderItem si in SliderList)
                    {
                        if (si.Value > sliderPos)
                        {
                            if ((si.Value - sliderPos) < minRight)
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
                    // нельзя добавить точку внутри Range
                    if (nextSi.Variant == PointVariant.RangeRight)
                        return;

                    newSlider = CreateSlider(sliderPos, Maxlimit, Pattern[sliderPos - 1]);
                    int ix = SliderList.IndexOf(nextSi);
                    if (AddMode == 1)
                    {
                        newSlider.Variant = PointVariant.RangeLeft;
                        SliderItem rangeRight = CreateSlider(sliderPos, Maxlimit, Pattern[sliderPos - 1]);
                        rangeRight.Variant = PointVariant.RangeRight;
                        SliderList.Insert(ix, rangeRight);
                        SliderList.Insert(ix, newSlider);
                        ReArrangeSliderItems();
                        if (newSlider != null)
                        {
                            if (SelectedSlider != null)
                                SelectedSlider.IsSelected = false;
                            newSlider.IsSelected = true;
                            SelectedSlider = newSlider;
                        }
                        return;
                    }
                    if (AddMode == 2)
                    {
                        newSlider.Variant = PointVariant.Lightness;
                        newSlider.PatternPoint.SaveLightness();
                    }
                    SliderList.Insert(ix, newSlider);
                    ReArrangeSliderItems();
                    if (newSlider != null)
                    {
                        if (SelectedSlider != null)
                            SelectedSlider.IsSelected = false;
                        newSlider.IsSelected = true;
                        SelectedSlider = newSlider;
                    }
                }
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
            //object newValue = null;
            double target = 0.0;

            if (SelectedSlider != null)
            {
                switch (SelectedSlider.SliderType)
                {
                    case SliderTypeEnum.RGB:
                        target = SelectedSlider.PatternPoint.L;
                        break;
                    case SliderTypeEnum.W:
                    case SliderTypeEnum.WT:
                        target = SelectedSlider.PatternPoint.WhiteD;
                        break;
                    case SliderTypeEnum.Warm:
                        target = SelectedSlider.PatternPoint.WarmD;
                        break;
                    case SliderTypeEnum.Cold:
                        target = SelectedSlider.PatternPoint.ColdD;
                        break;
                }

                if (e.Delta > 0)
                {
                    target += 0.02;
                    if (target > 1.0)
                        target = 1.0;
                }
                else
                {
                    target -= 0.02;
                    if (target < 0.0)
                        target = 0.0;
                }


                switch (SelectedSlider.SliderType)
                {
                    case SliderTypeEnum.RGB:
                        
                        SelectedSlider.PatternPoint.L = target;
                        SelectedSlider.PatternPoint.UpdatePoint_RGB();
                        SelectedSlider.PatternPoint.SaveLightness();
                        if (SelectedSlider.Variant == PointVariant.RangeLeft)
                            SelectedSlider.PatternPoint.CopyTo(SelectedSlider.Owner[SelectedSlider.Ix + 1].PatternPoint, SelectedSlider.SliderType);
                        if (SelectedSlider.Variant == PointVariant.RangeRight)
                            SelectedSlider.PatternPoint.CopyTo(SelectedSlider.Owner[SelectedSlider.Ix - 1].PatternPoint, SelectedSlider.SliderType);
                        
                        break;

                    case SliderTypeEnum.WT:
                        SelectedSlider.PatternPoint.WhiteD = target;
                        SelectedSlider.PatternPoint.InitialWhiteD = target;
                        //SelectedSlider.PatternPoint.SaveWhiteD();
                        if (SelectedSlider.Variant == PointVariant.RangeLeft)
                            SelectedSlider.PatternPoint.CopyTo(SelectedSlider.Owner[SelectedSlider.Ix + 1].PatternPoint, SelectedSlider.SliderType);
                        if (SelectedSlider.Variant == PointVariant.RangeRight)
                            SelectedSlider.PatternPoint.CopyTo(SelectedSlider.Owner[SelectedSlider.Ix - 1].PatternPoint, SelectedSlider.SliderType);
                        break;


                    case SliderTypeEnum.W:
                        
                        SelectedSlider.PatternPoint.WhiteD = target;
                        SelectedSlider.PatternPoint.InitialWhiteD = target;
                        //SelectedSlider.PatternPoint.SaveWhiteD();
                        if (SelectedSlider.Variant == PointVariant.RangeLeft)
                            SelectedSlider.PatternPoint.CopyTo(SelectedSlider.Owner[SelectedSlider.Ix + 1].PatternPoint, SelectedSlider.SliderType);
                        if (SelectedSlider.Variant == PointVariant.RangeRight)
                            SelectedSlider.PatternPoint.CopyTo(SelectedSlider.Owner[SelectedSlider.Ix - 1].PatternPoint, SelectedSlider.SliderType);
                        break;

                    case SliderTypeEnum.Warm:

                        SelectedSlider.PatternPoint.WarmD = target;
                        SelectedSlider.PatternPoint.InitialWarmD = target;
                        if (SelectedSlider.Variant == PointVariant.RangeLeft)
                            SelectedSlider.PatternPoint.CopyTo(SelectedSlider.Owner[SelectedSlider.Ix + 1].PatternPoint, SelectedSlider.SliderType);
                        if (SelectedSlider.Variant == PointVariant.RangeRight)
                            SelectedSlider.PatternPoint.CopyTo(SelectedSlider.Owner[SelectedSlider.Ix - 1].PatternPoint, SelectedSlider.SliderType);
                        break;

                    case SliderTypeEnum.Cold:

                        SelectedSlider.PatternPoint.ColdD = target;
                        SelectedSlider.PatternPoint.InitialColdD = target;
                        if (SelectedSlider.Variant == PointVariant.RangeLeft)
                            SelectedSlider.PatternPoint.CopyTo(SelectedSlider.Owner[SelectedSlider.Ix + 1].PatternPoint, SelectedSlider.SliderType);
                        if (SelectedSlider.Variant == PointVariant.RangeRight)
                            SelectedSlider.PatternPoint.CopyTo(SelectedSlider.Owner[SelectedSlider.Ix - 1].PatternPoint, SelectedSlider.SliderType);
                        break;

                }
                UpdatePatternCommand.Execute(SelectedSlider);
                SelectedSlider.RaiseWheelVariableChanged(target);
            }
        }


        protected override void OnKeyDown(KeyEventArgs e)
        {
            List<SliderItem> lightSliders = new List<SliderItem>();
            int prevIx = -1;
            int nextIx = -1;
            SliderItem activeSlider = null;
            SliderItem secondRange = null;
            int ix = -1;

            base.OnKeyDown(e);

            if (e.Source is MultiSlider)
            {
                if (SelectedSlider != null)
                {
                    if (e.Key == Key.Delete)
                    {
                        ix = SliderList.IndexOf(SelectedSlider);

                        #region Delete Gradient

                        if (SelectedSlider.Variant == PointVariant.Gradient)
                        {
                            if (SliderList.Count != 1)
                            {
                                if (ix == 0)
                                {
                                    // первый слайдер в списке
                                    nextIx = 0;
                                    do // необходимо удалить промежуточные яркостные точки справа
                                    {
                                        nextIx++;
                                        if (SliderList[nextIx].Variant == PointVariant.Lightness)
                                            lightSliders.Add(SliderList[nextIx]);
                                        else
                                            break;
                                    } while (nextIx != SliderList.Count - 1);
                                    activeSlider = SliderList[nextIx];
                                    foreach (SliderItem si in lightSliders)
                                        SliderList.Remove(si);
                                    goto M1;
                                }

                                if (ix == SliderList.Count - 1)
                                {
                                    // последний слайдер в списке
                                    prevIx = SliderList.Count - 1;
                                    do  // удаляем промежуточные яркостные точки слева
                                    {
                                        prevIx--;
                                        if (SliderList[prevIx].Variant == PointVariant.Lightness)
                                            SliderList.Remove(SliderList[prevIx]);
                                        else
                                            break;
                                    } while (prevIx != 0);
                                    activeSlider = SliderList[prevIx];
                                    goto M1;
                                }

                                nextIx = ix;
                                do // пропускаем промежуточные яркостные точки справа
                                {
                                    nextIx++;
                                    if (SliderList[nextIx].Variant != PointVariant.Lightness)
                                        break;
                                } while (nextIx != SliderList.Count - 1);
                                activeSlider = SliderList[nextIx];

                                M1:

                                SliderList.Remove(SelectedSlider);
                                SelectedSlider.IsSelected = false;
                                ReArrangeSliderItems();

                                if (activeSlider != null)
                                {
                                    activeSlider.IsSelected = true;
                                    SelectedSlider = activeSlider;
                                    UpdatePatternCommand.Execute(activeSlider);
                                }
                            }
                            return;
                        }

                        #endregion

                        #region Delete Range

                        if (SelectedSlider.Variant == PointVariant.RangeLeft || SelectedSlider.Variant == PointVariant.RangeRight)
                        {
                            if (SliderList.Count != 2)
                            {
                                // первый диапазон в списке
                                if ((SelectedSlider.Variant == PointVariant.RangeLeft && ix == 0) ||
                                    (SelectedSlider.Variant == PointVariant.RangeRight && ix == 1))
                                {
                                    if (SelectedSlider.Variant == PointVariant.RangeLeft)
                                    {
                                        nextIx = 1;
                                        secondRange = SliderList[nextIx]; // rangeRight
                                    }
                                    else
                                    {
                                        nextIx = ix;
                                        secondRange = SliderList[nextIx - 1]; // rangeLeft
                                    }
                                    
                                    do // необходимо удалить яркостные точки справа
                                    {
                                        nextIx++;
                                        if (SliderList[nextIx].Variant == PointVariant.Lightness)
                                            lightSliders.Add(SliderList[nextIx]);
                                        else
                                            break;
                                    } while (nextIx != SliderList.Count - 1);

                                    activeSlider = SliderList[nextIx];

                                    foreach (SliderItem si in lightSliders)
                                        SliderList.Remove(si);
                                    goto M2;
                                }

                                // последний диапазон в списке
                                if ((SelectedSlider.Variant == PointVariant.RangeRight && ix == SliderList.Count - 1) ||
                                    (SelectedSlider.Variant == PointVariant.RangeLeft && ix == SliderList.Count - 2))
                                {
                                    if (SelectedSlider.Variant == PointVariant.RangeRight)
                                    {
                                        prevIx = SliderList.Count - 1 - 1;  // rangeLeftIx
                                        secondRange = SliderList[prevIx];
                                    }
                                    else
                                    {
                                        prevIx = ix;
                                        secondRange = SliderList[prevIx + 1];
                                    }

                                    do  // удаляем промежуточные яркостные точки слева
                                    {
                                        prevIx--;
                                        if (SliderList[prevIx].Variant == PointVariant.Lightness)
                                            SliderList.Remove(SliderList[prevIx]);
                                        else
                                            break;
                                    } while (prevIx != 0);
                                    activeSlider = SliderList[prevIx];
                                    goto M2;
                                }

                                // диапазон внутри списка

                                if (SelectedSlider.Variant == PointVariant.RangeLeft)
                                {
                                    secondRange = SliderList[ix + 1];
                                    nextIx = ix + 1;
                                }
                                else
                                {
                                    secondRange = SliderList[ix - 1];
                                    nextIx = ix;
                                }

                                do // пропускаем промежуточные яркостные точки справа
                                {
                                    nextIx++;
                                    if (SliderList[nextIx].Variant != PointVariant.Lightness)
                                        break;
                                } while (nextIx != SliderList.Count - 1);
                                activeSlider = SliderList[nextIx];


                                M2:
                                SliderList.Remove(SelectedSlider);
                                SliderList.Remove(secondRange);
                                ReArrangeSliderItems();
                                if (activeSlider != null)
                                {
                                    activeSlider.IsSelected = true;
                                    SelectedSlider = activeSlider;
                                    UpdatePatternCommand.Execute(activeSlider);
                                }
                            }
                            return;
                        }

                        #endregion

                        #region Delete Lightness

                        if (SelectedSlider.Variant == PointVariant.Lightness)
                        {
                            activeSlider = SliderList[ix + 1];
                            SliderList.Remove(SelectedSlider);
                            //SelectedSlider.IsSelected = false;
                            ReArrangeSliderItems();
                            activeSlider.IsSelected = true;
                            SelectedSlider = activeSlider;
                            UpdatePatternCommand.Execute(activeSlider);
                        }

                        #endregion
                    }
                }
            }
        }

        #endregion
    }
}
