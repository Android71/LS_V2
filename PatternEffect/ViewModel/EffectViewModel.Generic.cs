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
                    case SliderTypeEnum.WT:
                        Pattern[i].Clear_WT();
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
            }

        }

        #endregion

        #region Update Pattern

        void UpdatePattern_1(SliderItem si)
        {
            SliderItem prevItem = null;
            SliderItem nextItem = null;
            SliderItem rangeRight = null;
            SliderItem rangeLeft = null;
            List<SliderItem> leftLights = new List<SliderItem>();
            List<SliderItem> rightLights = new List<SliderItem>();

            if (si.Variant == PointVariant.Lightness && si.SliderType == SliderTypeEnum.RGB)
            {
                prevItem = si.Owner[si.Ix - 1];
                nextItem = si.Owner[si.Ix + 1];
                UpdateLightGradient(prevItem, si);
                UpdateLightGradient(si, nextItem);
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
            prevItem = PrevSliderItem_1(rangeLeft, leftLights);
            nextItem = NextSliderItem_1(rangeRight, rightLights);


            if (prevItem != null)
                BuildGradient_1(prevItem, rangeLeft);
            else
                ClearLeftEnd(rangeLeft);

            if (si.SliderType == SliderTypeEnum.RGB && prevItem != null)
                UpdateLightGradient(prevItem, leftLights, rangeLeft);

            if (rangeLeft != rangeRight)
                BuildGradient_1(rangeLeft, rangeRight);

            if (nextItem != null)
                BuildGradient_1(rangeRight, nextItem);
            else
                ClearRightEnd(rangeRight);

            if (si.SliderType == SliderTypeEnum.RGB && nextItem != null)
                UpdateLightGradient(si, rightLights, nextItem);


        }

        SliderItem PrevSliderItem_1(SliderItem si, List<SliderItem> lightsSliders)
        {
            int prevIx = si.Ix - 1;

            SliderItem item = null;

            if (si.SliderType == SliderTypeEnum.RGB)
            {
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
            }
            else
            {
                if (si.Ix == 0)
                    return null;
                else
                    item = si.Owner[si.Ix - 1];
            }
            return item;
        }

        SliderItem NextSliderItem_1(SliderItem si, List<SliderItem> lightsSliders)
        {
            int nextIx = si.Ix + 1;
            SliderItem item = null;

            if (si.SliderType == SliderTypeEnum.RGB)
            {
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
            }
            else
            {
                if (si.Ix == si.Owner.Count - 1)
                    return null;
                else
                    item = si.Owner[si.Ix + 1];
            }
            return item;
        }

        void UpdateLightGradient(SliderItem leftSlider, SliderItem rightSlider)
        {
            double deltaL;

            int leftPointIx = leftSlider.Pos;
            int rightPointIx = rightSlider.Pos;
            int stepCount = rightPointIx - leftPointIx;

            leftSlider.PatternPoint.RestoreLightness();
            leftSlider.PatternPoint.UpdatePoint_RGB();
            rightSlider.PatternPoint.RestoreLightness();
            rightSlider.PatternPoint.UpdatePoint_RGB();

            if (stepCount > 0)
            {
                deltaL = (rightSlider.PatternPoint.L - leftSlider.PatternPoint.L) / stepCount;

                for (int i = 0; i < stepCount - 1; i++)
                {
                    double prevLight = Pattern[leftPointIx - 1 + i].L;
                    Pattern[leftPointIx + i].SetPoint_HSL(Pattern[leftPointIx + i].H, Pattern[leftPointIx + i].S, prevLight + deltaL);
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

        #endregion

        #region Parse Related

        void Create_RGB_SliderList(XElement root, List<SliderItem> sliderList)
        {
            int ix = 0;
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

                sliderList.Add(CreateSlider(sliderList, ix, Pos, (PointVariant)int.Parse(basePoint.Attribute("Variant").Value), SliderTypeEnum.RGB));
                ix++;
            }
        }

        void Create_W_SliderList(XElement root, List<SliderItem> sliderList)
        {
            int ix = 0;
            foreach (XElement basePoint in root.Elements("BasePoint"))
            {
                int Pos = int.Parse(basePoint.Attribute("Pos").Value);
                PatternPoint pp = Pattern[Pos - 1];

                //pp.White = int.Parse(basePoint.Attribute("W").Value);
                //pp.WhiteD = pp.White / 255.0;

                pp.WhiteD = double.Parse(basePoint.Attribute("W").Value);
                pp.White = Convert.ToInt32(pp.WhiteD * 255.0);

                DownSliderList.Add(CreateSlider(sliderList,ix, Pos, (PointVariant)int.Parse(basePoint.Attribute("Variant").Value), SliderTypeEnum.W));
                ix++;
            }
        }

        void Create_WT_SliderList(XElement root, List<SliderItem> sliderList)
        {
            int ix = 0;
            foreach (XElement basePoint in root.Elements("BasePoint"))
            {
                int Pos = int.Parse(basePoint.Attribute("Pos").Value);
                PatternPoint pp = Pattern[Pos - 1];

                //pp.White = int.Parse(basePoint.Attribute("W").Value);
                //pp.WhiteD = pp.White / 255.0;

                pp.WhiteD = double.Parse(basePoint.Attribute("W").Value);
                pp.Temp = double.Parse(basePoint.Attribute("T").Value);
                //pp.White = Convert.ToInt32(pp.WhiteD * 255.0);

                DownSliderList.Add(CreateSlider(sliderList, ix, Pos, (PointVariant)int.Parse(basePoint.Attribute("Variant").Value), SliderTypeEnum.WT));
                ix++;
            }
        }

        SliderItem CreateSlider(List<SliderItem> sList, int ix, int pos, PointVariant pVariant, SliderTypeEnum sliderType)
        {
            SliderItem si = new SliderItem();
            si.Ix = ix;
            si.Owner = sList;
            si.PatternPoint = Pattern[pos - 1];
            si.Variant = pVariant;
            si.Minimum = 1;
            si.Maximum = PointCount;
            si.SelectionStart = 1;
            si.SelectionEnd = si.Maximum;
            si.Value = pos;
            si.SliderType = sliderType;
            return si;
        }

        XElement whitePoint(SliderItem si)
        {

            //< BasePoint Pos = "1" R = "0" G = "128" B = "0"  Variant = "0" />
            XElement xe = new XElement("BasePoint",
                    new XAttribute("Pos", ((int)si.Value).ToString()),
                    new XAttribute("W", (si.PatternPoint.WhiteD).ToString()),
                    new XAttribute("Variant", ((int)si.Variant).ToString())
                );
            return xe;
        }

        XElement rgbPoint(SliderItem si)
        {
            //< BasePoint Pos = "1" R = "0" G = "128" B = "0"  Variant = "0" />
            XElement xe = new XElement("BasePoint",
                    new XAttribute("Pos", ((int)si.Value).ToString()),
                    new XAttribute("R", (si.PatternPoint.PointColor.R).ToString()),
                    new XAttribute("G", (si.PatternPoint.PointColor.G).ToString()),
                    new XAttribute("B", (si.PatternPoint.PointColor.B).ToString()),
                    new XAttribute("Variant", ((int)si.Variant).ToString())
                );
            return xe;
        }


        string RGB_SliderListToXML(List<SliderItem> sliderList)
        {
            return "";
        }

        #endregion

    }
}
