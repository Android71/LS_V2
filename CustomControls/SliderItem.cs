using System.Windows;
using System.Windows.Controls;

namespace LS_Designer_WPF.Controls
{

    public enum SliderVariant { Gradient, RangeLeftLimit, RangeRightLimit, Lightness };

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
        // Variant = 1 Range Left Limit
        // Variant = 2 Range Right Limit
        // Variant = 3 Lightness control
        public SliderVariant Variant
        {
            get { return (SliderVariant)GetValue(VariantProperty); }
            set { SetValue(VariantProperty, value); }
        }

        public static readonly DependencyProperty VariantProperty =
            DependencyProperty.Register("Variant", typeof(SliderVariant), typeof(SliderItem), new FrameworkPropertyMetadata(SliderVariant.Gradient));

        #endregion

        #region Properties

        public int LeftLimit { get; set; }

        #endregion

        protected override void OnValueChanged(double oldValue, double newValue)
        {
            if (newValue >= SelectionStart && newValue <= SelectionEnd)
                base.OnValueChanged(oldValue, newValue);
            else
            {
                if (newValue < SelectionStart)
                    Value = SelectionStart;
                if (newValue > SelectionEnd)
                    Value = SelectionEnd;
            }
        }
    }
}
