using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Media = System.Windows.Media;
using System.Drawing;

namespace LS_Library
{
    public class ColorRange
    {
        public ColorRange(Media.Color from, Media.Color to)
        {
            FromColor = from;
            ToColor = to;
            if (from == Media.Colors.Black)
            {
                HueMinimum = 0.0;
                HueMaximum = 0.0;
                HueMiddle = 0.0;
            }
            else
            {
                Color tmp = Color.FromArgb(from.R, from.G, from.B);
                Color tmp1 = Color.FromArgb(to.R, to.G, to.B);
                HueMinimum = Math.Round(tmp.GetHue());
                HueMaximum = Math.Round(tmp1.GetHue());               //Math.Round(tmp1.GetHue());
                if (HueMaximum == 0.0)
                    HueMaximum = 360.0;
                HueMiddle = HueMinimum + (HueMaximum - HueMinimum) / 2;
            }
        }
        public double HueMinimum { get; set; }
        public double HueMaximum { get; set; }
        public double HueMiddle { get; set; }
        public Media.Color FromColor { get; set; }
        public Media.Color ToColor { get; set; }

        public static ColorRange BlackRange
        {
            get
            {
                return new ColorRange(Media.Colors.Black, Media.Colors.Black);
            }
        }
    }

}
