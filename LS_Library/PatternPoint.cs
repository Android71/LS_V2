using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;


namespace LS_Library
{

    public enum PointVariant { Gradient, RangeLeft, RangeRight, Lightness };

    public class PatternPoint : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private System.Windows.Media.Color _pointColor;
        public System.Windows.Media.Color PointColor
        {
            get { return _pointColor; }
            set
            {
                if (_pointColor != value)
                {
                    _pointColor = value;
                    OnPropertyChanged("PointColor");
                }
            }
        }

        public double H { get; set; }

        public double S { get; set; }

        public double L { get; set; }

        public void SetPointColor()
        { }

        int _lightness;
        public int Lightness
        {
            get { return _lightness; }
            set
            {
                if (_lightness != value)
                {
                    _lightness = value;
                    OnPropertyChanged("Lightness");
                }
            }
        }

        /// <summary>
        /// Converts HSL to RGB.
        /// </summary>
        /// <param name="h">Hue, must be in [0, 360].</param>
        /// <param name="s">Saturation, must be in [0, 1].</param>
        /// <param name="l">Luminance, must be in [0, 1].</param>
        public static Color HSLtoRGB(double h, double s, double l)
        {
            if (s == 0)
            {
                // achromatic color (gray scale)
                return Color.FromArgb(
                    //Convert.ToInt32(Double.Parse(String.Format("{0:0.00}",
                    //    l * 255.0))),
                    Convert.ToInt32(l * 255.0),
                    Convert.ToInt32(Double.Parse(String.Format("{0:0.00}",
                        l * 255.0))),
                    Convert.ToInt32(Double.Parse(String.Format("{0:0.00}",
                        l * 255.0)))
                    );
            }
            else
            {
                double q = (l < 0.5) ? (l * (1.0 + s)) : (l + s - (l * s));
                double p = (2.0 * l) - q;

                double Hk = h / 360.0;
                double[] T = new double[3];
                T[0] = Hk + (1.0 / 3.0);    // Tr
                T[1] = Hk;                // Tb
                T[2] = Hk - (1.0 / 3.0);    // Tg

                for (int i = 0; i < 3; i++)
                {
                    if (T[i] < 0) T[i] += 1.0;
                    if (T[i] > 1) T[i] -= 1.0;

                    if ((T[i] * 6) < 1)
                    {
                        T[i] = p + ((q - p) * 6.0 * T[i]);
                    }
                    else if ((T[i] * 2.0) < 1) //(1.0/6.0)<=T[i] && T[i]<0.5
                    {
                        T[i] = q;
                    }
                    else if ((T[i] * 3.0) < 2) // 0.5<=T[i] && T[i]<(2.0/3.0)
                    {
                        T[i] = p + (q - p) * ((2.0 / 3.0) - T[i]) * 6.0;
                    }
                    else T[i] = p;
                }

                return Color.FromArgb (
                    Convert.ToInt32(Double.Parse(String.Format("{0:0.00}",
                        T[0] * 255.0))),
                    Convert.ToInt32(Double.Parse(String.Format("{0:0.00}",
                        T[1] * 255.0))),
                    Convert.ToInt32(Double.Parse(String.Format("{0:0.00}",
                        T[2] * 255.0)))
                    );
            }
        }
    }


}
