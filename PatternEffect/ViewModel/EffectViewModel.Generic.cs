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

        #region Pattern Related

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
            if (si.Ix == si.Owner.Count - 1)
            {
                ClearRightEnd(si);
            }
            BuildGradient(si.Owner[si.Ix - 1], si);
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
                    case SliderTypeEnum.WT:
                        Pattern[i].Clear_WT();
                        break;
                    case SliderTypeEnum.Cold:
                        Pattern[i].Clear_Cold();
                        break;
                    case SliderTypeEnum.Warm:
                        Pattern[i].Clear_Warm();
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
                    case SliderTypeEnum.WT:
                        Pattern[i].Clear_WT();
                        break;
                    case SliderTypeEnum.Cold:
                        Pattern[i].Clear_Cold();
                        break;
                    case SliderTypeEnum.Warm:
                        Pattern[i].Clear_Warm();
                        break;
                }
            }
        }

        void BuildGradient(SliderItem leftSlider, SliderItem rightSlider)
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
                            Pattern[i - 1].SetPoint_HSL(pp.H + deltaH, pp.S + deltaS, pp.L + deltaL);
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
                                Pattern[i - 1].WhiteD = pp.WhiteD + deltaWhite;
                            }
                        }
                    }
                    break;
                case SliderTypeEnum.WT:
                    double deltaTemp;
                    {
                        if (stepCount > 0)
                        {
                            deltaTemp = (rightSlider.PatternPoint.Temp - leftSlider.PatternPoint.Temp) / stepCount;
                            deltaWhite = (rightSlider.PatternPoint.WhiteD - leftSlider.PatternPoint.WhiteD) / stepCount;

                            for (int i = leftIx + 1; i < rightIx; i++)
                            {
                                PatternPoint pp = Pattern[i - 1 - 1];
                                Pattern[i - 1].Temp = pp.Temp + deltaTemp;
                                Pattern[i - 1].WhiteD = pp.WhiteD + deltaWhite;
                            }
                        }
                    }
                    break;
                case SliderTypeEnum.Cold:
                    double deltaCold;
                    {
                        if (stepCount > 0)
                        {
                            deltaCold = (rightSlider.PatternPoint.ColdD - leftSlider.PatternPoint.ColdD) / stepCount;
                            for (int i = leftIx + 1; i < rightIx; i++)
                            {
                                PatternPoint pp = Pattern[i - 1 - 1];
                                Pattern[i - 1].ColdD = pp.ColdD + deltaCold;
                            }
                        }
                    }
                    break;
                case SliderTypeEnum.Warm:
                    double deltaWarm;
                    {
                        if (stepCount > 0)
                        {
                            deltaWarm = (rightSlider.PatternPoint.WarmD - leftSlider.PatternPoint.WarmD) / stepCount;
                            for (int i = leftIx + 1; i < rightIx; i++)
                            {
                                PatternPoint pp = Pattern[i - 1 - 1];
                                Pattern[i - 1].WarmD = pp.WarmD + deltaWarm;
                            }
                        }
                    }
                    break;

            }

        }

        #endregion

        #region Update Pattern

        void UpdatePattern(SliderItem si)
        {
            SliderItem prevItem = null;
            SliderItem nextItem = null;
            SliderItem rangeRight = null;
            SliderItem rangeLeft = null;
            List<SliderItem> leftLights = new List<SliderItem>();
            List<SliderItem> rightLights = new List<SliderItem>();

            if (si.Variant == PointVariant.Lightness)
            {
                prevItem = si.Owner[si.Ix - 1];
                nextItem = si.Owner[si.Ix + 1];
                UpdateLuminosityGradient(prevItem, si);
                UpdateLuminosityGradient(si, nextItem);
                return;
            }

            if (si.Variant == PointVariant.RangeLeft || si.Variant == PointVariant.RangeRight)
            {
                if (si.Variant == PointVariant.RangeLeft)
                {
                    rangeLeft = si;
                    rangeRight = si.Owner[si.Ix + 1];
                }
                else
                {
                    rangeRight = si;
                    rangeLeft = si.Owner[si.Ix - 1];
                }
            }
            else
            {
                rangeLeft = si;
                rangeRight = si;
            }
            prevItem = PrevSliderItem(rangeLeft, leftLights);
            nextItem = NextSliderItem(rangeRight, rightLights);


            if (prevItem == null)
                ClearLeftEnd(rangeLeft);
            else
            {
                BuildGradient(prevItem, rangeLeft);
                LightListHandler(prevItem, leftLights, rangeLeft);
            }

            if (rangeLeft != rangeRight)
                BuildGradient(rangeLeft, rangeRight);

            if (nextItem == null)
                ClearRightEnd(rangeRight);
            else
            {
                BuildGradient(rangeRight, nextItem);
                LightListHandler(rangeRight, rightLights, nextItem);
            }

        }

        SliderItem PrevSliderItem(SliderItem si, List<SliderItem> lightsSliders)
        {
            int prevIx = si.Ix - 1;

            SliderItem item = null;

            while (prevIx >= 0)
            {
                if (si.Owner[prevIx].Variant == PointVariant.Lightness)
                {
                    lightsSliders.Insert(0, si.Owner[prevIx]);
                    prevIx--;
                }
                else
                {
                    item = si.Owner[prevIx];
                    break;
                }
            }
            return item;
        }

        SliderItem NextSliderItem(SliderItem si, List<SliderItem> lightsSliders)
        {
            int nextIx = si.Ix + 1;
            SliderItem item = null;

            while (nextIx <= si.Owner.Count - 1)
            {
                if (si.Owner[nextIx].Variant == PointVariant.Lightness)
                {
                    lightsSliders.Add(si.Owner[nextIx]);
                    nextIx++;
                }
                else
                {
                    item = si.Owner[nextIx];
                    break;
                }
            }
            
            return item;
        }


        void LightListHandler(SliderItem leftItem, List<SliderItem> lightList, SliderItem rightItem)
        {
            SliderItem prevLight = null;

            if (lightList.Count != 0)
            {
                prevLight = leftItem;
                foreach (SliderItem si in lightList)
                {
                    UpdateLuminosityGradient(prevLight, si);
                    prevLight = si;
                }
                UpdateLuminosityGradient(prevLight, rightItem);
            }
        }

        void UpdateLuminosityGradient(SliderItem leftSlider, SliderItem rightSlider)
        {
            double delta;

            //int leftPointIx = leftSlider.Pos;
            //int rightPointIx = rightSlider.Pos;
            //int stepCount = rightPointIx - leftPointIx;
            int stepCount = rightSlider.Pos - leftSlider.Pos;


            switch (leftSlider.SliderType)
            {
                case SliderTypeEnum.RGB:
                    leftSlider.PatternPoint.RestoreLightness();
                    leftSlider.PatternPoint.UpdatePoint_RGB();
                    rightSlider.PatternPoint.RestoreLightness();
                    rightSlider.PatternPoint.UpdatePoint_RGB();
                    break;

                case SliderTypeEnum.WT:
                case SliderTypeEnum.W:
                    leftSlider.PatternPoint.RestoreWhiteD();
                    rightSlider.PatternPoint.RestoreWhiteD();
                    break;
                case SliderTypeEnum.Cold:
                    leftSlider.PatternPoint.RestoreColdD();
                    rightSlider.PatternPoint.RestoreColdD();
                    break;
                case SliderTypeEnum.Warm:
                    leftSlider.PatternPoint.RestoreWarmD();
                    rightSlider.PatternPoint.RestoreWarmD();
                    break;
            }

            if (stepCount > 0)
            {
                switch (leftSlider.SliderType)
                {
                    case SliderTypeEnum.RGB:
                        delta = (rightSlider.PatternPoint.L - leftSlider.PatternPoint.L) / stepCount;
                        for (int i = 0; i < stepCount - 1; i++)
                        {
                            double prevLight = Pattern[leftSlider.Pos - 1 + i].L;
                            Pattern[leftSlider.Pos + i].SetPoint_HSL(Pattern[leftSlider.Pos + i].H, Pattern[leftSlider.Pos + i].S, prevLight + delta);
                        }
                        break;
                    case SliderTypeEnum.WT:
                    case SliderTypeEnum.W:
                        delta = (rightSlider.PatternPoint.WhiteD - leftSlider.PatternPoint.WhiteD) / stepCount;
                        for (int i = 0; i < stepCount - 1; i++)
                        {
                            double prevWhiteD = Pattern[leftSlider.Pos - 1 + i].WhiteD;
                            Pattern[leftSlider.Pos + i].WhiteD = prevWhiteD + delta;
                        }
                        break;
                }
            }
        }


        #endregion

    }
}
