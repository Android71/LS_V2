using LS_Library;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LS_Designer_WPF.Controls
{
    public class MultiSlider : Control
    {
        Grid UpSliders;
        Grid DownSliders;

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
                UpSliders.Children.Add(SliderList[0]);
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
            //ms.ApplyTemplate();
            //ms.UpSliders = ms.Template.FindName("PART_UpSliders", ms) as Grid;
            //ms.UpSliders.Children.Add(ms.SliderList[0]);
        }



        public ObservableCollection<PatternPoint> Pattern
        {
            get { return (ObservableCollection<PatternPoint>)GetValue(PatternProperty); }
            set { SetValue(PatternProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Pattern.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PatternProperty =
            DependencyProperty.Register("Pattern", typeof(ObservableCollection<PatternPoint>), typeof(MultiSlider), new PropertyMetadata(null));


        #endregion
    }
}
