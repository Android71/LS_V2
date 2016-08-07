using LS_Designer_WPF.Controls;
using LS_Library;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Xml.Linq;

namespace PatternEffect.ViewModel
{
    public partial class EffectViewModel
    {

        #region RGBW

        void LoadModel_RGBW(string path)
        {
            string patternPath = path + @"\Pattern_RGBW.xml";

            Params = File.ReadAllText(patternPath);

            //initialBuild = true;
            //BuildRGBPattern();
            BuildPattern(UpSliderList);
            BuildPattern(DownSliderList);
            //initialBuild = false;
        }


        void ParseEffectParams_RGBW(string profile)
        {
            XElement root = XElement.Parse(profile);
            PointCount = int.Parse(root.Attribute("PointCount").Value);

            Pattern = new PatternPoint[PointCount];

            for (int i = 0; i < PointCount; i++)
                Pattern[i] = new PatternPoint();

            XElement rgbPoints = root.Elements().First(p => p.Name == "RGB");

            XElement whitePoints = root.Elements().First(p => p.Name == "White");

            CreateRGBSliders(rgbPoints, UpSliderList);
            Create_W_Sliders(whitePoints, DownSliderList);
        }


        #endregion

        void BuildPattern(List<SliderItem> sliderList)
        {
            foreach (SliderItem si in sliderList)
                MakeGradient(si);
        }

        void MakeGradient(SliderItem si)
        {
            // первый слайдер в списке
            if (si.Ix == 0)
            {
                ClearLeftEnd(si);
                return;
            }
            if(si.Ix == si.Owner.Count - 1)
            {
                ClearRightEnd(si);
            }
            BuildGradient_1(si.Owner[si.Ix - 1], si);
        }

        void ClearLeftEnd(SliderItem si)
        {
            //int leftIx = si.Pos - 1;
            for (int i = 0; i < si.PatIx; i++)
            {
                switch (si.SliderType)
                {
                    case SliderTypeEnum.RGB:
                        Pattern[i].Clear_RGB();
                        break;
                    case SliderTypeEnum.W:
                        Pattern[i].Clear_W();
                        break;
                }
            }
        }

        void ClearRightEnd(SliderItem si)
        {
            //int rightIx = si.Pos;
            for (int i = si.PatIx + 1; i < PointCount; i++)
            {
                switch (si.SliderType)
                {
                    case SliderTypeEnum.RGB:
                        Pattern[i].Clear_RGB();
                        break;
                    case SliderTypeEnum.W:
                        Pattern[i].Clear_W();
                        break;
                }
            }
        }

        void BuildGradient_1(SliderItem leftSlider, SliderItem rightSlider)
        {
            int leftIx = Convert.ToInt32(leftSlider.Value);
            int rightIx = Convert.ToInt32(rightSlider.Value);
            int stepCount = rightIx - leftIx;
            switch (leftSlider.SliderType)
            {
                case SliderTypeEnum.RGB:
                    double deltaH, deltaS, deltaL;
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
                    break;
                case SliderTypeEnum.W:
                    double deltaWhite;
                    {
                        if (stepCount > 0)
                        {
                            deltaWhite = (rightSlider.PatternPoint.WhiteD - leftSlider.PatternPoint.WhiteD) / stepCount;
                            for (int i = leftIx + 1; i < rightIx; i++)
                            {
                                PatternPoint pp = Pattern[i - 1 - 1];
                                Pattern[i - 1].SetWhite(pp.WhiteD + deltaWhite);
                            }
                        }
                    }
                    break;
            }
            
        }
    }
}
