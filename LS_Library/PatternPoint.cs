﻿using System;
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

        #region Point operations

        public void ClearPoint(SliderTypeEnum sliderType)
        {
            switch (sliderType)
            {
                case SliderTypeEnum.RGB:
                    SetPoint_HSL(0.0, 0.0, 0.0);
                    break;
                case SliderTypeEnum.WT:
                    WhiteD = 0.0;
                    Temp = 0.0;
                    break;
                case SliderTypeEnum.W:
                    WhiteD = 0.0;
                    break;
                case SliderTypeEnum.Warm:
                    WarmD = 0.0;
                    break;
                case SliderTypeEnum.Cold:
                    ColdD = 0.0;
                    break;
            }
        }

        public void UpdatePoint_RGB()
        {
            PointColor = HSLtoRGB();
            Lightness = Convert.ToInt32(L * 255.0);
        }

        public void SaveLightness()
        {
            InitialL = L;
        }

        public void RestorePoint(SliderTypeEnum sliderType)
        {
            switch (sliderType)
            {
                case SliderTypeEnum.RGB:
                    L = InitialL;
                    break;
                case SliderTypeEnum.WT:
                case SliderTypeEnum.W:
                    WhiteD = InitialWhiteD;
                    break;
                case SliderTypeEnum.Warm:
                    WarmD = InitialWarmD;
                    break;
                case SliderTypeEnum.Cold:
                    ColdD = InitialColdD;
                    break;
            }
        }

        public void CopyTo(PatternPoint pp, SliderTypeEnum sliderType)
        {
            switch (sliderType)
            {
                case SliderTypeEnum.RGB:
                    pp.H = H;
                    pp.S = S;
                    pp.L = L;
                    pp.InitialL = InitialL;
                    pp.Lightness = Lightness;
                    pp.PointColor = PointColor;
                    break;
                case SliderTypeEnum.WT:
                    pp.WhiteD = WhiteD;
                    pp.InitialWhiteD = InitialWhiteD;
                    pp.Temp = Temp;
                    break;
                case SliderTypeEnum.W:
                    pp.WhiteD = WhiteD;
                    pp.InitialWhiteD = InitialWhiteD;
                    break;
                case SliderTypeEnum.Warm:
                    pp.WarmD = WarmD;
                    pp.InitialWarmD = InitialWarmD;
                    break;
                case SliderTypeEnum.Cold:
                    pp.ColdD = ColdD;
                    pp.InitialColdD = InitialColdD;
                    break;
            }
        }

        #endregion

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

        double l;
        public double L
        {
            get { return l; }
            set
            { if (l != value)
                {
                    l = value;
                    //InitialL = l;
                }
            }
        }

        double InitialL { get; set; } // исходное значение Lightness для использования в алгоритме построения
                                      // градиента яркости

        public void SetPoint_HSL(double h, double s, double l)
        {
            H = h;
            S = s;
            L = l;
            PointColor = HSLtoRGB();
            Lightness = Convert.ToInt32(L * 255.0);
        }

        //public void SetWhite(double w)
        //{
        //    WhiteD = w;
        //    White = Convert.ToInt32(w * 255.0);
        //}


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

        double _whiteD;
        public double WhiteD
        {
            get { return _whiteD; }
            set
            {
                if (_whiteD != value)
                {
                    _whiteD = value;
                    White = Convert.ToInt32(_whiteD * 255.0);
                }
            }
        }

        public double InitialWhiteD { get; set; }

        #endregion

        #region Temperature

        System.Windows.Media.Color _colorT;
        public System.Windows.Media.Color ColorT
        {
            get { return _colorT; }
            set
            {
                if (_colorT != value)
                {
                    _colorT = value;
                    OnPropertyChanged("ColorT");
                }
            }
        }


        double _temp;
        public double Temp
        {
            get { return _temp; }
            set
            {
                double hue = 0.0;
                if (_temp != value)
                {
                    _temp = value;
                    if (value < 0.5)
                    {
                        hue = value * 2.0 * 60.0;
                    }
                    if (value >= 0.5)
                    {
                        hue = (value - 0.5) * 90.0 * 2.0 + 180.0;
                    }
                    ColorT = ColorUtilities.Hsl2MediaColor(hue, 1.0, 0.5);
                }
            }
        }

        public double InitialT { get; set; }

        #endregion

        #region Cold

        int _cold;
        public int Cold
        {
            get { return _cold; }
            set
            {
                if (_cold != value)
                {
                    _cold = value;
                    OnPropertyChanged("Cold");
                }
            }
        }

        double _coldD;
        public double ColdD
        {
            get { return _coldD; }
            set
            {
                if (_coldD != value)
                {
                    _coldD = value;
                    Cold = Convert.ToInt32(_coldD * 255.0);
                }
            }
        }

        public double InitialColdD { get; set; }

        #endregion

        #region Warm

        int _warm;
        public int Warm
        {
            get { return _warm; }
            set
            {
                if (_warm != value)
                {
                    _warm = value;
                    OnPropertyChanged("Warm");
                }
            }
        }

        double _warmD;
        public double WarmD
        {
            get { return _warmD; }
            set
            {
                if (_warmD != value)
                {
                    _warmD = value;
                    Warm = Convert.ToInt32(_warmD * 255.0);
                }
            }
        }

        public double InitialWarmD { get; set; }

        #endregion

    }

}
