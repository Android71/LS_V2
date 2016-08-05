using LS_Library;
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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LS_Designer_WPF.Controls
{
    /// <summary>
    /// Interaction logic for PatternUC.xaml
    /// </summary>
    public partial class PatternUC : UserControl
    {
        public PatternUC()
        {
            InitializeComponent();
        }

        void TuneControl()
        {
            if (PointType == PointTypeEnum.RGB)
            {
                ReBinding();
                SetVisibility();
                
            }
        }

        void SetVisibility()
        {
            switch (PointType)
            {
                case PointTypeEnum.RGB:
                    // UpScreen
                    rgbScreen.Visibility = Visibility.Visible;
                    whiteUpScreen.Visibility = Visibility.Hidden;
                    warmScreen.Visibility = Visibility.Hidden;
                    wtUpScreen.Visibility = Visibility.Hidden;

                    // DownScreen
                    whiteScreen.Visibility = Visibility.Hidden;
                    wtScreen.Visibility = Visibility.Hidden;
                    coldScreen.Visibility = Visibility.Hidden;

                    //MultiSliders
                    upMultiSlider.Visibility = Visibility.Visible;
                    downMultiSlider.Visibility = Visibility.Hidden;

                    scaleTb.Visibility = Visibility.Collapsed;
                    scaleSelector.Visibility = Visibility.Collapsed;

                    //ActiveSliderList = UpSliderList;
                    SetActiveList.Execute(UpSliderList);

                    break;
            }
        }

        void ReBinding()
        {
            switch (PointType)
            {
                case PointTypeEnum.RGB:
                    //BindingOperations.ClearAllBindings(whiteUpScreen);
                    //BindingOperations.ClearAllBindings(warmScreen);
                    //BindingOperations.ClearAllBindings(wtUpScreen);
                    //BindingOperations.ClearAllBindings(whiteScreen);
                    //BindingOperations.ClearAllBindings(wtScreen);
                    //BindingOperations.ClearAllBindings(coldScreen);
                    //upMultiSlider.SetBinding(MultiSlider.SliderListProperty, new Binding("UpSliderList") { Source = this });
                    BindingOperations.SetBinding(upMultiSlider, MultiSlider.SliderListProperty, new Binding("UpSliderList") { Source = this });
                    BindingOperations.SetBinding(upMultiSlider, MultiSlider.PatternProperty, new Binding("Pattern") { Source = this });
                    BindingOperations.SetBinding(rgbScreen, ItemsControl.ItemsSourceProperty, new Binding("Pattern") { Source = this });
                    BindingOperations.SetBinding(addModeSelector, ComboBox.SelectedIndexProperty, new Binding("AddMode") { Source = upMultiSlider });
                    BindingOperations.ClearBinding(downMultiSlider, MultiSlider.SelectedSliderProperty);
                    BindingOperations.SetBinding(upMultiSlider, MultiSlider.SelectedSliderProperty, new Binding("SelectedSlider") { Source = this, Mode = BindingMode.TwoWay});
                    break;
            }
        }

        #region DP

        //******* PointType ********

        public PointTypeEnum PointType
        {
            get { return (PointTypeEnum)GetValue(PointTypeProperty); }
            set { SetValue(PointTypeProperty, value); }
        }

        public static readonly DependencyProperty PointTypeProperty =
            DependencyProperty.Register("PointType", typeof(PointTypeEnum), typeof(PatternUC), new PropertyMetadata(PointTypeEnum.RGB/*, OnPointTypeChanged*/));

        //private static void OnPointTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
            
        //}

        //******* UpSliderList ********

        public List<SliderItem> UpSliderList
        {
            get { return (List<SliderItem>)GetValue(UpSliderListProperty); }
            set { SetValue(UpSliderListProperty, value); }
        }

        public static readonly DependencyProperty UpSliderListProperty =
            DependencyProperty.Register("UpSliderList", typeof(List<SliderItem>), typeof(PatternUC), new PropertyMetadata(null, OnUpSliderListChanged));

        private static void OnUpSliderListChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PatternUC uc = (PatternUC)d;
            //uc.rgbScreen.SetValue(ItemsControl.InputScopeProperty, uc.Pattern);
        }

        //******* DownSliderList ********

        public List<SliderItem> DownSliderList
        {
            get { return (List<SliderItem>)GetValue(DownSliderListProperty); }
            set { SetValue(DownSliderListProperty, value); }
        }

        public static readonly DependencyProperty DownSliderListProperty =
            DependencyProperty.Register("DownSliderList", typeof(List<SliderItem>), typeof(PatternUC), new PropertyMetadata(null, OnDownSliderListChanged));

        private static void OnDownSliderListChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        //******* Pattern ********

        public PatternPoint[] Pattern
        {
            get { return (PatternPoint[])GetValue(PatternProperty); }
            set { SetValue(PatternProperty, value); }
        }

        public static readonly DependencyProperty PatternProperty =
            DependencyProperty.Register("Pattern", typeof(PatternPoint[]), typeof(PatternUC), new PropertyMetadata(null, OnPatternChanged));

        private static void OnPatternChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PatternUC uc = (PatternUC)d;
            if (uc.Pattern != null)
                uc.TuneControl(); 
            
            
        }


        //******* PointCount ********

        public int PointCount
        {
            get { return (int)GetValue(PointCountProperty); }
            set { SetValue(PointCountProperty, value); }
        }

        public static readonly DependencyProperty PointCountProperty =
            DependencyProperty.Register("PointCount", typeof(int), typeof(PatternUC), new PropertyMetadata(0));

        //******* ActiveSliderList ********

        //public List<SliderItem> ActiveSliderList
        //{
        //    get { return (List<SliderItem>)GetValue(ActiveSliderListProperty); }
        //    set { SetValue(ActiveSliderListProperty, value); }
        //}

        //public static readonly DependencyProperty ActiveSliderListProperty =
        //    DependencyProperty.Register("ActiveSliderList", typeof(List<SliderItem>), typeof(PatternUC), new PropertyMetadata(null));



        public ICommand SetActiveList
        {
            get { return (ICommand)GetValue(SetActiveListProperty); }
            set { SetValue(SetActiveListProperty, value); }
        }

        public static readonly DependencyProperty SetActiveListProperty =
            DependencyProperty.Register("SetActiveList", typeof(ICommand), typeof(PatternUC), new PropertyMetadata(null));


        public SliderItem SelectedSlider
        {
            get { return (SliderItem)GetValue(SelectedSliderProperty); }
            set { SetValue(SelectedSliderProperty, value); }
        }

        public static readonly DependencyProperty SelectedSliderProperty =
            DependencyProperty.Register("SelectedSlider", typeof(SliderItem), typeof(PatternUC), new PropertyMetadata(null, OnSelectedSliderChanged));

        private static void OnSelectedSliderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        #endregion



        private void effectUC_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateMargin();
        }

        private void patternView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateMargin();
        }

        void UpdateMargin()
        {
            double width = patternView.ActualWidth;
            int pointCount = upMultiSlider.Maxlimit;
            if (pointCount != 0)
            {
                double halfPointWidth = (width / pointCount) / 2;
                double margin = -9.5 + halfPointWidth;
                upMultiSlider.Margin = new Thickness(margin, 0, margin, 0);
            }
        }
    }
}
