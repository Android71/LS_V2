using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LS_Designer_WPF.Controls;
using LS_Library;
using PatternEffect.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Windows.Media;
using System.Xml.Linq;
using System.Linq;

namespace PatternEffect.ViewModel
{
    // | PointType      |  UpView     |  DownView |  UpSliderType | DownSliderType | PatternInfo
    //************************************************************************************** 
    // | RGB            |  RGB        |   -       |  RGB          | -              | None
    // | RGBW           |  RGB        |  White    |  RGB          | White          | Complex
    // | RGBWT          |  RGB        |  WT       |  RGB          | WT             | Complex
    // | RGB_W          |  RGB/White  |  -        |  RGB          | White          | RGBOnly/WhiteOnly
    // | RGB_WT         |  RGB/WT     |  -        |  RGB          | WT             | RGBOnly/WhiteOnly
    // | W              |  White      |  -        |  White        | -              | None
    // | WT             |  WT         |  -        |  WT           | -              | None
    // | CW             |  Warm       | Cold      |  Warm         | Cold           | None
    

    public class EffectViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;
        bool initialBuild = false;

        public EffectViewModel(IDataService dataService)
        {
            _dataService = dataService;
            UpdatePatternCmd = new RelayCommand<SliderItem>((si) => ExecUpdatePattern(si));
            CreateProfileCmd = new RelayCommand(ExecCreateProfileCmd);
            SetActiveListCmd = new RelayCommand<List<SliderItem>>(ExecSetActiveListCmd);

            string path = Assembly.GetExecutingAssembly().Location;
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            string patternDir = dirInfo.Parent.Parent.Parent.FullName;

            PointType = PointTypeEnum.RGB;

            switch (PointType)
            {
                case PointTypeEnum.RGB:
                    ActiveSliderList = UpSliderList;
                    InitRGB_EffectViewModel(patternDir);
                    break;
            }
            
        }


        #region Commands

        public RelayCommand<List<SliderItem>> SetActiveListCmd { get; private set; }

        private void ExecSetActiveListCmd(List<SliderItem> list)
        {
            ActiveSliderList = list;
        }

        public RelayCommand<SliderItem> UpdatePatternCmd { get; private set; }

        private void ExecUpdatePattern(SliderItem si)
        {
            UpdatePattern(si);
        }

        #endregion

        #region Effect Entity Properties

        public string Params
        {
            get { return CreateRGB_EffectParams(); }

            set { ParseRGB_EffectParams(value); }
        }

        PointTypeEnum _pointType = PointTypeEnum.RGB;
        public PointTypeEnum PointType
        {
            get { return _pointType; }
            set { Set(ref _pointType, value); }
        }

        #endregion

        List<SliderItem> _upSliderList = new List<SliderItem>();
        public List<SliderItem> UpSliderList
        {
            get { return _upSliderList; }
            set { Set(ref _upSliderList, value); }
        }

        List<SliderItem> _downSliderList = new List<SliderItem>();
        public List<SliderItem> DownSliderList
        {
            get { return _downSliderList; }
            set { Set(ref _downSliderList, value); }
        }

        public List<SliderItem> ActiveSliderList { get; set; }

        PatternPoint[] _pattern;
        public PatternPoint[] Pattern
        {
            get { return _pattern; }
            set { Set(ref _pattern, value); }
        }

        int _pointCount;
        public int PointCount
        {
            get { return _pointCount; }
            set { Set(ref _pointCount, value); }
        }

        

        PatternInfoEnum _patternInfo = PatternInfoEnum.None;
        PatternInfoEnum PatternInfo
        {
            get { return _patternInfo; }
            set
            {
                Set(ref _patternInfo, value);
            }
        }

        void BuildGradient(SliderItem leftSlider, SliderItem rightSlider, bool onlyLightness = false)
        {
            double deltaH, deltaS, deltaL;

            int leftIx = Convert.ToInt32(leftSlider.Value);
            int rightIx = Convert.ToInt32(rightSlider.Value);
            int stepCount = rightIx - leftIx;

            if (stepCount > 0)
            {
                deltaH = (rightSlider.PatternPoint.H - leftSlider.PatternPoint.H) / stepCount;
                deltaS = (rightSlider.PatternPoint.S - leftSlider.PatternPoint.S) / stepCount;
                deltaL = (rightSlider.PatternPoint.L - leftSlider.PatternPoint.L) / stepCount;
                for (int i = leftIx + 1; i < rightIx; i++)
                {
                    PatternPoint pp = Pattern[i - 1 - 1];
                    Pattern[i - 1].SetColorFromHSL(pp.H + deltaH, pp.S + deltaS, pp.L + deltaL);
                }
            }
        }


        void UpdateLightGradient(SliderItem leftSlider, SliderItem rightSlider)
        {
            double deltaL;

            int leftPointIx = leftSlider.Pos;
            int rightPointIx = rightSlider.Pos;
            int stepCount = rightPointIx - leftPointIx;

            leftSlider.PatternPoint.RestoreLightness();
            leftSlider.PatternPoint.UpdateColor();
            rightSlider.PatternPoint.RestoreLightness();
            rightSlider.PatternPoint.UpdateColor();

            if (stepCount > 0)
            {
                deltaL = (rightSlider.PatternPoint.L - leftSlider.PatternPoint.L) / stepCount;

                for (int i = 0; i < stepCount - 1; i++)
                {
                    double prevLight = Pattern[leftPointIx - 1 + i].L;
                    Pattern[leftPointIx + i].SetColorFromHSL(Pattern[leftPointIx + i].H, Pattern[leftPointIx + i].S, prevLight + deltaL);
                }
            }
        }

        void UpdateColorGradient(SliderItem leftSlider, SliderItem rightSlider)
        {
            double deltaH, deltaS, deltaL;

            int leftIx = Convert.ToInt32(leftSlider.Value);
            int rightIx = Convert.ToInt32(rightSlider.Value);
            int stepCount = rightIx - leftIx;

            if (stepCount > 0)
            {
                deltaH = (rightSlider.PatternPoint.H - leftSlider.PatternPoint.H) / stepCount;
                deltaS = (rightSlider.PatternPoint.S - leftSlider.PatternPoint.S) / stepCount;
                deltaL = (rightSlider.PatternPoint.L - leftSlider.PatternPoint.L) / stepCount;
                for (int i = leftIx + 1; i < rightIx; i++)
                {
                    PatternPoint pp = Pattern[i - 1 - 1];
                    Pattern[i - 1].SetColorFromHSL(pp.H + deltaH, pp.S + deltaS, pp.L + deltaL);
                }
            }
        }

        void UpdateLightGradient(SliderItem leftItem, List<SliderItem> lightList, SliderItem rightItem)
        {
            SliderItem prevLight = null;
             
            if (lightList.Count != 0)
            {
                prevLight = leftItem;
                foreach (SliderItem si in lightList)
                {
                    UpdateLightGradient(prevLight, si);
                    prevLight = si;
                }
                UpdateLightGradient(prevLight, rightItem);
            }
        }



        #region Debug Stuff

        string _fileName = "Pattern_1.xml";
        public string FileName
        {
            get { return _fileName; }
            set { Set(ref _fileName, value); }
        }

        public RelayCommand CreateProfileCmd { get; private set; }

        private void ExecCreateProfileCmd()
        {
            string path = Assembly.GetExecutingAssembly().Location;
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            string patternDir = dirInfo.Parent.Parent.Parent.FullName;
            string patternPath = patternDir + @"\" + FileName;
            string s = Params;
            File.WriteAllText(patternPath, Params);
        }

        #endregion

        #region Depend of SliderList

        void UpdatePattern(SliderItem si)
        {
            SliderItem prevItem = null;
            SliderItem nextItem = null;
            SliderItem rangeRight = null;
            SliderItem rangeLeft = null;
            int rangeLeftIx = -1;
            int rangeRightIx = -1;

            List<SliderItem> leftLights = new List<SliderItem>();
            List<SliderItem> rightLights = new List<SliderItem>();

            int ix;

            // Для слайдера - градиента по яркости всегда есть предыдущaя и последующая точка
            if (si.Variant == PointVariant.Lightness)
            {
                ix = ActiveSliderList.IndexOf(si);
                prevItem = ActiveSliderList[ix - 1];
                nextItem = ActiveSliderList[ix + 1];
                UpdateLightGradient(prevItem, si);
                UpdateLightGradient(si, nextItem);
                return;
            }

            ix = ActiveSliderList.IndexOf(si);

            if (si.Variant == PointVariant.RangeLeft || si.Variant == PointVariant.RangeRight)
            {
                // Для слайдера - левой границы диапазона всегда есть слайдер правой границы диапазона
                if (si.Variant == PointVariant.RangeLeft)
                {
                    rangeLeft = si;
                    rangeLeftIx = ix;
                    rangeRight = ActiveSliderList[ix + 1];
                    rangeRightIx = ix + 1;
                    for (int i = (int)rangeLeft.Value + 1; i <= (int)rangeRight.Value; i++)
                        rangeLeft.PatternPoint.CopyTo(Pattern[i - 1]);
                }
                // Для слайдера - правой границы диапазона всегда есть слайдер левой границы диапазона
                if (si.Variant == PointVariant.RangeRight)
                {
                    rangeRight = si;
                    rangeRightIx = ix;
                    rangeLeft = ActiveSliderList[ix - 1];
                    rangeLeftIx = ix - 1;
                    for (int i = (int)rangeRight.Value; i >= (int)rangeLeft.Value; i--)
                    {
                        rangeRight.PatternPoint.CopyTo(Pattern[i - 1]);
                    }
                }
            }

            // если слайдер - градиент
            if (si.Variant == PointVariant.Gradient)
            {
                rangeLeft = si;
                rangeLeftIx = ix;
                rangeRight = si;
                rangeRightIx = ix;
            }

            prevItem = PrevSliderItem(rangeLeftIx, leftLights);
            nextItem = NextSliderItem(rangeRightIx, rightLights);

            if (prevItem != null)
            {
                UpdateColorGradient(prevItem, rangeLeft);
                UpdateLightGradient(prevItem, leftLights, rangeLeft);
            }
            else
                ClearLeftEnd(si);
            if (nextItem != null)
            {
                UpdateColorGradient(rangeRight, nextItem);
                UpdateLightGradient(rangeRight, rightLights, nextItem);
            }
            else
                ClearRightEnd(si);
            return;
        }

        SliderItem PrevSliderItem(int sliderIx, List<SliderItem> leftLights)
        {
            int prevIx = sliderIx - 1;
            SliderItem item = null;

            while (prevIx >= 0)
            {
                if (ActiveSliderList[prevIx].Variant == PointVariant.Lightness)
                {
                    leftLights.Insert(0, ActiveSliderList[prevIx]);
                    prevIx--;
                }
                else
                {
                    item = ActiveSliderList[prevIx];
                    break;
                }
            }
            return item;
        }

        SliderItem NextSliderItem(int sliderIx, List<SliderItem> rightLights)
        {
            int nextIx = sliderIx + 1;
            SliderItem item = null;

            while (nextIx <= ActiveSliderList.Count - 1)
            {
                if (ActiveSliderList[nextIx].Variant == PointVariant.Lightness)
                {
                    rightLights.Add(ActiveSliderList[nextIx]);
                    nextIx++;
                }
                else
                {
                    item = ActiveSliderList[nextIx];
                    break;
                }
            }
            return item;
        }

        void ClearLeftEnd(SliderItem si)
        {
            int leftIx = si.Pos - 1;
            for (int i = 0; i < leftIx; i++)
            {
                switch (si.SliderType)
                {
                    case SliderTypeEnum.RGB:
                        Pattern[i].Clear();
                        break;
                }
            }
        }

        void ClearRightEnd(SliderItem si)
        {
            int rightIx = si.Pos;
            for (int i = rightIx; i < PointCount; i++)
            {
                switch (si.SliderType)
                {
                    case SliderTypeEnum.RGB:
                        Pattern[i].Clear();
                        break;
                }
            }
        }

        


        #endregion

        #region Dependent Of PointType

        #region RGB

        void InitRGB_EffectViewModel(string path)
        {
            string patternPath = path + @"\Pattern.xml";
            //string patternPath = patternDir + @"\Pattern_1.xml";
            //string patternPath = patternDir + @"\Pattern_2.xml";

            Params = File.ReadAllText(patternPath);

            initialBuild = true;
            BuildRGBPattern();
            initialBuild = false;
        }

        void BuildRGBPattern()
        {
            if (UpSliderList.Count > 1)
            {
                foreach (SliderItem si in UpSliderList)
                {

                    if (si.Variant != PointVariant.RangeRight)
                        PrepareAndBuildGradient(si);
                }
            }
        }

        void ParseRGB_EffectParams(string profile)
        {
            XElement root = XElement.Parse(profile);
            int pointCount = int.Parse(root.Attribute("PointCount").Value);
            PointCount = pointCount;

            Pattern = new PatternPoint[pointCount];

            for (int i = 0; i < pointCount; i++)
                Pattern[i] = new PatternPoint();

            foreach (XElement basePoint in root.Elements("BasePoint"))
            {
                int Pos = int.Parse(basePoint.Attribute("Pos").Value);

                System.Drawing.Color color = System.Drawing.Color.FromArgb
                                    (0,
                                     int.Parse(basePoint.Attribute("R").Value),
                                     int.Parse(basePoint.Attribute("G").Value),
                                     int.Parse(basePoint.Attribute("B").Value)
                                    );

                PatternPoint pp = Pattern[Pos - 1];
                pp.H = color.GetHue();
                pp.S = color.GetSaturation();
                pp.L = color.GetBrightness();
                pp.SaveLightness();
                pp.PointColor = Color.FromRgb(color.R, color.G, color.B);
                pp.Lightness = Convert.ToInt32(pp.L * 255);

                SliderItem si = new SliderItem();
                si.PatternPoint = Pattern[Pos - 1];
                si.Variant = (PointVariant)int.Parse(basePoint.Attribute("Variant").Value);
                si.Minimum = 1;
                si.Maximum = pointCount;
                si.SelectionStart = 1;
                si.SelectionEnd = si.Maximum;
                si.Value = Pos;
                si.SliderType = SliderTypeEnum.RGB;
                UpSliderList.Add(si);
            }
        }

        string CreateRGB_EffectParams()
        {
            XElement profile = new XElement("Params", new XAttribute("PointCount", PointCount));

            //< BasePoint Pos = "1" R = "0" G = "128" B = "0"  Variant = "0" />
            foreach (SliderItem si in UpSliderList)
            {
                XElement xe = new XElement("BasePoint",
                    new XAttribute("Pos", ((int)si.Value).ToString()),
                    new XAttribute("R", (si.PatternPoint.PointColor.R).ToString()),
                    new XAttribute("G", (si.PatternPoint.PointColor.G).ToString()),
                    new XAttribute("B", (si.PatternPoint.PointColor.B).ToString()),
                    new XAttribute("Variant", ((int)si.Variant).ToString())
                );
                profile.Add(xe);
            }
            return profile.ToString();
        }

        void PrepareAndBuildGradient(SliderItem currentSlider)
        {
            SliderItem prevSlider = null;
            SliderItem nextSlider = null;
            SliderItem afterRangeRight = null;
            SliderItem beforeRangeLeft = null;
            if (currentSlider != null)
            {
                int ix = UpSliderList.IndexOf(currentSlider);
                if (ix != 0)
                    prevSlider = UpSliderList[ix - 1];
                if (ix != UpSliderList.Count - 1)
                    nextSlider = UpSliderList[ix + 1];

                if (prevSlider == null)
                    ClearLeftEnd(currentSlider);
                if (nextSlider == null)
                    ClearRightEnd(currentSlider);

                if (currentSlider.Variant == PointVariant.RangeLeft)
                {
                    currentSlider.PatternPoint.CopyTo(nextSlider.PatternPoint);
                    if (prevSlider != null)
                        BuildGradient(prevSlider, currentSlider);
                    BuildGradient(currentSlider, nextSlider);
                    if (!initialBuild)
                    {
                        // Slider RangeRight не последний Slider в SliderList
                        if ((ix + 1) != (UpSliderList.Count - 1))
                        {
                            afterRangeRight = UpSliderList[ix + 2];
                            BuildGradient(nextSlider, afterRangeRight);
                        }
                    }
                    return;
                }
                if (currentSlider.Variant == PointVariant.RangeRight)
                {
                    currentSlider.PatternPoint.CopyTo(prevSlider.PatternPoint);
                    BuildGradient(prevSlider, currentSlider);
                    if (nextSlider != null)
                        BuildGradient(currentSlider, nextSlider);
                    if ((ix - 1) != 0)
                    {
                        // Slider RangeLeft не первый Slider в SliderList
                        beforeRangeLeft = UpSliderList[ix - 2];
                        BuildGradient(beforeRangeLeft, prevSlider);
                    }
                    return;
                }

                if (prevSlider != null)
                    BuildGradient(prevSlider, currentSlider);
                if (nextSlider != null)
                    BuildGradient(currentSlider, nextSlider);
            }
        }

        #endregion

        #endregion

    }


}