﻿using LS_Library;
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
    /// Interaction logic for EffectUC.xaml
    /// </summary>
    public partial class EffectUC : UserControl
    {
        public EffectUC()
        {
            InitializeComponent();
        }

        void TuneControl()
        {
            //if (PointType == PointTypeEnum.RGB)
            //{
                SetBinding();
                SetVisuals();
            //}
        }

        void SetVisuals()
        {
            List<string> screenList = null;
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

                    screenTb.Visibility = Visibility.Collapsed;
                    screenSelector.Visibility = Visibility.Collapsed;

                    //ActiveSliderList = UpSliderList;
                    SetActiveList.Execute(UpSliderList);

                    break;

                case PointTypeEnum.RGBW:
                    // UpScreen
                    rgbScreen.Visibility = Visibility.Visible;
                    whiteUpScreen.Visibility = Visibility.Hidden;
                    warmScreen.Visibility = Visibility.Hidden;
                    wtUpScreen.Visibility = Visibility.Hidden;

                    // DownScreen
                    whiteScreen.Visibility = Visibility.Visible;
                    wtScreen.Visibility = Visibility.Hidden;
                    coldScreen.Visibility = Visibility.Hidden;

                    //MultiSliders
                    upMultiSlider.Visibility = Visibility.Visible;
                    downMultiSlider.Visibility = Visibility.Hidden;

                    screenList = new List<string>() { "RGB", "White" };
                    screenTb.Visibility = Visibility.Visible;
                    screenSelector.Visibility = Visibility.Visible;
                    ScreenList = screenList;

                    //ActiveSliderList = UpSliderList;
                    SetActiveList.Execute(UpSliderList);

                    break;

                case PointTypeEnum.RGBWT:
                    // UpScreen
                    rgbScreen.Visibility = Visibility.Visible;
                    whiteUpScreen.Visibility = Visibility.Hidden;
                    warmScreen.Visibility = Visibility.Hidden;
                    wtUpScreen.Visibility = Visibility.Hidden;

                    // DownScreen
                    whiteScreen.Visibility = Visibility.Hidden;
                    wtScreen.Visibility = Visibility.Visible;
                    coldScreen.Visibility = Visibility.Hidden;

                    //MultiSliders
                    upMultiSlider.Visibility = Visibility.Visible;
                    downMultiSlider.Visibility = Visibility.Hidden;

                    screenList = new List<string>() { "RGB", "WT" };
                    screenTb.Visibility = Visibility.Visible;
                    screenSelector.Visibility = Visibility.Visible;
                    ScreenList = screenList;

                    //ActiveSliderList = UpSliderList;
                    SetActiveList.Execute(UpSliderList);

                    break;

                case PointTypeEnum.CW:
                    // UpScreen
                    rgbScreen.Visibility = Visibility.Hidden;
                    whiteUpScreen.Visibility = Visibility.Hidden;
                    warmScreen.Visibility = Visibility.Visible;
                    wtUpScreen.Visibility = Visibility.Hidden;

                    // DownScreen
                    whiteScreen.Visibility = Visibility.Hidden;
                    wtScreen.Visibility = Visibility.Hidden;
                    coldScreen.Visibility = Visibility.Visible;

                    //MultiSliders
                    upMultiSlider.Visibility = Visibility.Visible;
                    downMultiSlider.Visibility = Visibility.Hidden;

                    screenList = new List<string>() { "Warm", "Cold" };
                    screenTb.Visibility = Visibility.Visible;
                    screenSelector.Visibility = Visibility.Visible;
                    ScreenList = screenList;

                    //ActiveSliderList = UpSliderList;
                    SetActiveList.Execute(UpSliderList);

                    break;
                case PointTypeEnum.WT:
                    // UpScreen
                    rgbScreen.Visibility = Visibility.Hidden;
                    whiteUpScreen.Visibility = Visibility.Hidden;
                    warmScreen.Visibility = Visibility.Hidden;
                    wtUpScreen.Visibility = Visibility.Visible;

                    // DownScreen
                    whiteScreen.Visibility = Visibility.Hidden;
                    wtScreen.Visibility = Visibility.Hidden;
                    coldScreen.Visibility = Visibility.Hidden;

                    //MultiSliders
                    upMultiSlider.Visibility = Visibility.Visible;
                    downMultiSlider.Visibility = Visibility.Hidden;

                    screenTb.Visibility = Visibility.Collapsed;
                    screenSelector.Visibility = Visibility.Collapsed;

                    SetActiveList.Execute(UpSliderList);

                    break;
            }


        }

        void SetBinding()
        {
            switch (PointType)
            {
                case PointTypeEnum.RGB:
                    
                    BindingOperations.SetBinding(rgbScreen, ItemsControl.ItemsSourceProperty, new Binding("Pattern") { Source = this });

                    BindingOperations.SetBinding(addModeSelector, ComboBox.SelectedIndexProperty, new Binding("AddMode") { Source = upMultiSlider });
                    BindingOperations.SetBinding(upMultiSlider, MultiSlider.SelectedSliderProperty, new Binding("SelectedSlider") { Source = this, Mode = BindingMode.TwoWay });
                    break;

                case PointTypeEnum.RGBW:

                    BindingOperations.SetBinding(upMultiSlider, MultiSlider.SelectedSliderProperty, new Binding("SelectedSlider") { Source = this, Mode = BindingMode.TwoWay });
                    BindingOperations.SetBinding(addModeSelector, ComboBox.SelectedIndexProperty, new Binding("AddMode") { Source = upMultiSlider });

                    BindingOperations.SetBinding(rgbScreen, ItemsControl.ItemsSourceProperty, new Binding("Pattern") { Source = this });
                    BindingOperations.SetBinding(whiteScreen, ItemsControl.ItemsSourceProperty, new Binding("Pattern") { Source = this });
                    break;

                case PointTypeEnum.RGBWT:
                    BindingOperations.SetBinding(upMultiSlider, MultiSlider.SelectedSliderProperty, new Binding("SelectedSlider") { Source = this, Mode = BindingMode.TwoWay });
                    BindingOperations.SetBinding(addModeSelector, ComboBox.SelectedIndexProperty, new Binding("AddMode") { Source = upMultiSlider });

                    BindingOperations.SetBinding(rgbScreen, ItemsControl.ItemsSourceProperty, new Binding("Pattern") { Source = this });
                    BindingOperations.SetBinding(wtScreen, ItemsControl.ItemsSourceProperty, new Binding("Pattern") { Source = this });
                    break;
                case PointTypeEnum.CW:
                    BindingOperations.SetBinding(upMultiSlider, MultiSlider.SelectedSliderProperty, new Binding("SelectedSlider") { Source = this, Mode = BindingMode.TwoWay });
                    BindingOperations.SetBinding(addModeSelector, ComboBox.SelectedIndexProperty, new Binding("AddMode") { Source = upMultiSlider });

                    BindingOperations.SetBinding(warmScreen, ItemsControl.ItemsSourceProperty, new Binding("Pattern") { Source = this });
                    BindingOperations.SetBinding(coldScreen, ItemsControl.ItemsSourceProperty, new Binding("Pattern") { Source = this });
                    break;
                case PointTypeEnum.WT:
                    BindingOperations.SetBinding(upMultiSlider, MultiSlider.SelectedSliderProperty, new Binding("SelectedSlider") { Source = this, Mode = BindingMode.TwoWay });
                    BindingOperations.SetBinding(addModeSelector, ComboBox.SelectedIndexProperty, new Binding("AddMode") { Source = upMultiSlider });

                    BindingOperations.SetBinding(wtUpScreen, ItemsControl.ItemsSourceProperty, new Binding("Pattern") { Source = this });
                    //BindingOperations.SetBinding(coldScreen, ItemsControl.ItemsSourceProperty, new Binding("Pattern") { Source = this });
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
            DependencyProperty.Register("PointType", typeof(PointTypeEnum), typeof(EffectUC), new PropertyMetadata(PointTypeEnum.RGB/*, OnPointTypeChanged*/));

        //******* UpSliderList ********

        public List<SliderItem> UpSliderList
        {
            get { return (List<SliderItem>)GetValue(UpSliderListProperty); }
            set { SetValue(UpSliderListProperty, value); }
        }

        public static readonly DependencyProperty UpSliderListProperty =
            DependencyProperty.Register("UpSliderList", typeof(List<SliderItem>), typeof(EffectUC), new PropertyMetadata(null/*, OnUpSliderListChanged*/));

        //private static void OnUpSliderListChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    EffectUC uc = (EffectUC)d;
        //    //uc.rgbScreen.SetValue(ItemsControl.InputScopeProperty, uc.Pattern);
        //}

        //******* DownSliderList ********

        public List<SliderItem> DownSliderList
        {
            get { return (List<SliderItem>)GetValue(DownSliderListProperty); }
            set { SetValue(DownSliderListProperty, value); }
        }

        public static readonly DependencyProperty DownSliderListProperty =
            DependencyProperty.Register("DownSliderList", typeof(List<SliderItem>), typeof(EffectUC), new PropertyMetadata(null/*, OnDownSliderListChanged*/));

        //private static void OnDownSliderListChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
            
        //}

        //******* Pattern ********

        public PatternPoint[] Pattern
        {
            get { return (PatternPoint[])GetValue(PatternProperty); }
            set { SetValue(PatternProperty, value); }
        }

        public static readonly DependencyProperty PatternProperty =
            DependencyProperty.Register("Pattern", typeof(PatternPoint[]), typeof(EffectUC), new PropertyMetadata(null, OnPatternChanged));

        private static void OnPatternChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            EffectUC uc = (EffectUC)d;
            if (uc.Pattern != null)
                uc.TuneControl();
            uc.colorPanel.SetPanel(uc.UpSliderList[0].SliderType); 
        }


        //******* PointCount ********

        public int PointCount
        {
            get { return (int)GetValue(PointCountProperty); }
            set { SetValue(PointCountProperty, value); }
        }

        public static readonly DependencyProperty PointCountProperty =
            DependencyProperty.Register("PointCount", typeof(int), typeof(EffectUC), new PropertyMetadata(0));

        //******* ActiveSliderList ********

        //public List<SliderItem> ActiveSliderList
        //{
        //    get { return (List<SliderItem>)GetValue(ActiveSliderListProperty); }
        //    set { SetValue(ActiveSliderListProperty, value); }
        //}

        //public static readonly DependencyProperty ActiveSliderListProperty =
        //    DependencyProperty.Register("ActiveSliderList", typeof(List<SliderItem>), typeof(EffectUC), new PropertyMetadata(null));



        public ICommand SetActiveList
        {
            get { return (ICommand)GetValue(SetActiveListProperty); }
            set { SetValue(SetActiveListProperty, value); }
        }

        public static readonly DependencyProperty SetActiveListProperty =
            DependencyProperty.Register("SetActiveList", typeof(ICommand), typeof(EffectUC), new PropertyMetadata(null));


        public SliderItem SelectedSlider
        {
            get { return (SliderItem)GetValue(SelectedSliderProperty); }
            set { SetValue(SelectedSliderProperty, value); }
        }

        public static readonly DependencyProperty SelectedSliderProperty =
            DependencyProperty.Register("SelectedSlider", typeof(SliderItem), typeof(EffectUC), new PropertyMetadata(null, OnSelectedSliderChanged));

        private static void OnSelectedSliderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //throw new NotImplementedException();
            EffectUC uc = (EffectUC)d;
            //if (uc.SelectedSlider != null)
            //    uc.position.Text = uc.SelectedSlider.Pos.ToString();
            //else
            //    uc.position.Text = "";
        }



        public List<string> ScreenList
        {
            get { return (List<string>)GetValue(ScreenListProperty); }
            set { SetValue(ScreenListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ScreenSelector.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScreenListProperty =
            DependencyProperty.Register("ScreenList", typeof(List<string>), typeof(EffectUC), new PropertyMetadata(null));



        public int SelectedScreen
        {
            get { return (int)GetValue(SelectedScreenProperty); }
            set { SetValue(SelectedScreenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedScreen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedScreenProperty =
            DependencyProperty.Register("SelectedScreen", typeof(int), typeof(EffectUC), new PropertyMetadata(0, OnSelectedScreenChanged));

        private static void OnSelectedScreenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            List<SliderItem> activeSliderList = null;
            MultiSlider activeMultiSlider = null;
            MultiSlider passiveMultiSlider = null;

            if (d != null)
            {
                EffectUC uc = (EffectUC)d;
                uc.SelectedSlider = null;

                if (uc.SelectedScreen == 0)
                {
                    activeSliderList = uc.UpSliderList;
                    activeMultiSlider = uc.upMultiSlider;
                    passiveMultiSlider = uc.downMultiSlider;
                    uc.upMultiSlider.Visibility = Visibility.Visible;
                    uc.downMultiSlider.Visibility = Visibility.Hidden;
                }
                else
                {
                    activeSliderList = uc.DownSliderList;
                    activeMultiSlider = uc.downMultiSlider;
                    passiveMultiSlider = uc.upMultiSlider;
                    uc.upMultiSlider.Visibility = Visibility.Hidden;
                    uc.downMultiSlider.Visibility = Visibility.Visible;
                }

                BindingOperations.ClearBinding(passiveMultiSlider, MultiSlider.SelectedSliderProperty);
                BindingOperations.SetBinding(activeMultiSlider, MultiSlider.SelectedSliderProperty, new Binding("SelectedSlider") { Source = uc, Mode = BindingMode.TwoWay });

                foreach (SliderItem si in activeSliderList)
                    if (si.IsSelected)
                        uc.SelectedSlider = si;

                uc.SetActiveList.Execute(activeSliderList);
                uc.colorPanel.SetPanel(activeSliderList[0].SliderType);

                //if (uc.SelectedScreen == 0)
                //{
                //    BindingOperations.ClearBinding(uc.downMultiSlider, MultiSlider.SelectedSliderProperty);
                    
                //    uc.downMultiSlider.Visibility = Visibility.Hidden;
                //    uc.upMultiSlider.Visibility = Visibility.Visible;
                //    BindingOperations.SetBinding(uc.upMultiSlider, MultiSlider.SelectedSliderProperty, new Binding("SelectedSlider") { Source = uc, Mode = BindingMode.TwoWay });

                //    foreach (SliderItem si in uc.UpSliderList)
                //        if (si.IsSelected)
                //            uc.SelectedSlider = si;

                //    uc.SetActiveList.Execute(uc.UpSliderList);
                //    uc.colorPanel.SetPanel(uc.UpSliderList[0].SliderType);
                //}
                //else
                //{
                //    BindingOperations.ClearBinding(uc.upMultiSlider, MultiSlider.SelectedSliderProperty);
                //    uc.SelectedSlider = null;
                //    uc.upMultiSlider.Visibility = Visibility.Hidden;
                //    uc.downMultiSlider.Visibility = Visibility.Visible;
                //    BindingOperations.SetBinding(uc.downMultiSlider, MultiSlider.SelectedSliderProperty, new Binding("SelectedSlider") { Source = uc, Mode = BindingMode.TwoWay });

                //    foreach (SliderItem si in uc.DownSliderList)
                //        if (si.IsSelected)
                //            uc.SelectedSlider = si;
                //    uc.SetActiveList.Execute(uc.DownSliderList);
                //    uc.colorPanel.SetPanel(uc.DownSliderList[0].SliderType);
                //}
                
            }
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
            double halfPointWidth = 15.0;
            double margin = 5.5;
            bool skipNext = false;

            double width = patternView.ActualWidth;
            int pointCount = upMultiSlider.Maxlimit;
            if (pointCount != 0)
            {
                // default halfPointWidth = 15.0
                if (pointCount < 40)
                {
                    skipNext = true;
                    patternView.Width = halfPointWidth * 2 * pointCount;
                    patternView.HorizontalAlignment = HorizontalAlignment.Left;
                    upMultiSlider.Margin = new Thickness(margin, 0, margin, 0);
                    downMultiSlider.Margin = new Thickness(margin, 0, margin, 0);
                }
                else
                {
                    halfPointWidth = (width / pointCount) / 2;
                    margin = -9.5 + halfPointWidth;
                    upMultiSlider.Margin = new Thickness(margin, 0, margin, 0);
                    downMultiSlider.Margin = new Thickness(margin, 0, margin, 0);
                }
            }
        }
    }
}
