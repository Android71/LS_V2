using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace LS_Library
{
    public class ColorUtilities
    {
        /// <summary>
        /// Converts HSL to RGB.
        /// </summary>
        /// <param name="hue"> Hue, must be in [0, 360].</param>
        /// <param name="sat"> Saturation, must be in [0, 1].</param>
        /// <param name="light"> Luminance, must be in [0, 1].</param>
        public static Color Hsl2MediaColor(double hue, double sat, double light)
        {
            if (sat == 0)
            {
                // achromatic color (gray scale)
                return System.Windows.Media.Color.FromRgb(
                    Convert.ToByte(light * 255.0),
                    Convert.ToByte(light * 255.0),
                    Convert.ToByte(light * 255.0)
                    );
            }
            else
            {
                double q = (light < 0.5) ? (light * (1.0 + sat)) : (light + sat - (light * sat));
                double p = (2.0 * light) - q;

                double Hk = hue / 360.0;
                double[] T = new double[3];
                T[0] = Hk + (1.0 / 3.0);    // Tr
                T[1] = Hk;                  // Tb
                T[2] = Hk - (1.0 / 3.0);    // Tg

                for (int i = 0; i < 3; i++)
                {
                    if (T[i] < 0) T[i] += 1.0;
                    if (T[i] > 1) T[i] -= 1.0;

                    if ((T[i] * 6) < 1)
                    {
                        T[i] = p + ((q - p) * 6.0 * T[i]);
                    }
                    else if ((T[i] * 2.0) < 1)      //(1.0/6.0)<=T[i] && T[i]<0.5
                    {
                        T[i] = q;
                    }
                    else if ((T[i] * 3.0) < 2)      // 0.5<=T[i] && T[i]<(2.0/3.0)
                    {
                        T[i] = p + (q - p) * ((2.0 / 3.0) - T[i]) * 6.0;
                    }
                    else T[i] = p;
                }

                return System.Windows.Media.Color.FromRgb(
                    Convert.ToByte(T[0] * 255.0),
                    Convert.ToByte(T[1] * 255.0),
                    Convert.ToByte(T[2] * 255.0)
                    );
            }
        }
    }

}
