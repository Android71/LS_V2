using LS_Library;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LS_Designer_WPF.Controls
{

    //public enum SliderVariant { Gradient, RangeLeft, RangeRight, Lightness };

    public class SliderItem : Slider
    {
        static SliderItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SliderItem), new FrameworkPropertyMetadata(typeof(SliderItem)));
        }

        #region DP

        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register("IsSelected", typeof(bool), typeof(SliderItem),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.None));

        /// <summary>
        /// Gets or sets a value that indicates if this slider is selected.
        /// A slider remains selected until another silder is selected.
        /// </summary>
        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        // Variant = 0 GradientStop
        // Variant = 1 RangeLeft
        // Variant = 2 RangeRight
        // Variant = 3 Lightness control
        public PointVariant Variant
        {
            get { return (PointVariant)GetValue(VariantProperty); }
            set { SetValue(VariantProperty, value); }
        }

        public static readonly DependencyProperty VariantProperty =
            DependencyProperty.Register("Variant", typeof(PointVariant), typeof(SliderItem), new FrameworkPropertyMetadata(PointVariant.Gradient));

        public ICommand UpdatePatternCommand
        {
            get { return (ICommand)GetValue(UpdatePatternCommandProperty); }
            set { SetValue(UpdatePatternCommandProperty, value); }
        }

        public static readonly DependencyProperty UpdatePatternCommandProperty =
            DependencyProperty.Register("UpdatePatternCommand", typeof(ICommand), typeof(SliderItem), new PropertyMetadata(null));


        #endregion

        #region Properties

        public PatternPoint PatternPoint { get; set; }

        public int Pos { get { return Convert.ToInt32(Value); } }

        public int Ix { get; set; }

        #endregion

        protected override void OnValueChanged(double oldValue, double newValue)
        {
            //if (!Busy)
            //{

                if (newValue >= SelectionStart && newValue <= SelectionEnd && oldValue >= SelectionStart && oldValue <= SelectionEnd)
                    base.OnValueChanged(oldValue, newValue);
            else
            {
                if (newValue < SelectionStart)
                    Value = SelectionStart;
                if (newValue > SelectionEnd)
                    Value = SelectionEnd;
            }

            //}
        }

        public void UpdatePattern()
        {
            UpdatePatternCommand.Execute(this);
        }

        public event EventHandler<EventArgs> LightnessChanged;

        public void RaiseLightnessChanged()
        {
            if (LightnessChanged != null)
                LightnessChanged(this, new EventArgs());
        }

    }
}
