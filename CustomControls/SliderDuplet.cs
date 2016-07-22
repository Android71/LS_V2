using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS_Designer_WPF.Controls
{
    public class SliderDuplet
    {
        public SliderDuplet(SliderItem leftSlider, SliderItem rightSlider, bool onlyLightness = false)
        {
            LeftSlider = leftSlider;
            RightSlider = rightSlider;
            OnlyLightness = onlyLightness;
        }

        public SliderItem LeftSlider { get; set; }

        public SliderItem  RightSlider { get; set; }

        public bool OnlyLightness { get; set; }

    }
}
