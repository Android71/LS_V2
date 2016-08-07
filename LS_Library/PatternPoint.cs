using System;
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

        #region RGB

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

        double InitialL { get; set; } //исходное значение Lightness для использования в алгоритме построения
                                             //градиента яркости


        public void Clear_RGB()
        {
            SetColorFromHSL(0.0, 0.0, 0.0);
        }

        public void Clear_W()
        {
            WhiteD = 0.0;
            White = 0;
        }

        public void SetColorFromHSL(double h, double s, double l)
        {
            H = h;
            S = s;
            L = l;
            PointColor = HSLtoRGB();
            Lightness = Convert.ToInt32(L * 255.0);
        }

        public void SetWhite(double w)
        {
            WhiteD = w;
            White = Convert.ToInt32(w * 255.0);
        }

        public void UpdateColor()
        {
            PointColor = HSLtoRGB();
            Lightness = Convert.ToInt32(L * 255.0);
        }

        public void SaveLightness()
        {
            InitialL = L;
        }

        public void RestoreLightness()
        {
            L = InitialL;
        }

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

        public void CopyTo(PatternPoint pp)
        {
            pp.H = H;
            pp.S = S;
            pp.L = L;
            pp.InitialL = InitialL;
            pp.Lightness = Lightness;
            pp.PointColor = PointColor;
        }

        /// <summary>
        /// Converts HSL to RGB.
        /// </summary>
        /// <param name="h">Hue, must be in [0, 360].</param>
        /// <param name="s">Saturation, must be in [0, 1].</param>
        /// <param name="l">Luminance, must be in [0, 1].</param>
        //public static Color HSLtoRGB(double h, double s, double l)
        System.Windows.Media.Color HSLtoRGB(/*double h, double s, double l*/)
        {
            if (S > 1.0)
                S = 1.0;
            if (H > 360.0)
                H = 360.0 - H;
            if (L > 1.0)
                L = 1.0;

            if (S == 0)
            {
                // achromatic color (gray scale)
                return System.Windows.Media.Color.FromRgb(
                    Convert.ToByte(L * 255.0),
                    Convert.ToByte(L * 255.0),
                    Convert.ToByte(L * 255.0)
                    );
            }
            else
            {
                double q = (L < 0.5) ? (L * (1.0 + S)) : (L + S - (L * S));
                double p = (2.0 * L) - q;

                double Hk = H / 360.0;
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

                return System.Windows.Media.Color.FromRgb (
                    Convert.ToByte(T[0] * 255.0),
                    Convert.ToByte(T[1] * 255.0),
                    Convert.ToByte(T[2] * 255.0)
                    );
            }
        }

        #endregion

        #region White

        int _white;
        public int White
        {
            get { return _white; }
            set
            {
                if (_white != value)
                {
                    _white = value;
                    OnPropertyChanged("White");
                }
            }
        }

        public double WhiteD { get; set; }

        #endregion
    }


}
