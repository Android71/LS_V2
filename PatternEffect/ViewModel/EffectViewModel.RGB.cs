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

        #region RGB

        void LoadModel_RGB(string path)
        {
            string patternPath = path + @"\Pattern_RGB.xml";
            //string patternPath = patternDir + @"\Pattern_1.xml";
            //string patternPath = patternDir + @"\Pattern_2.xml";

            Params = File.ReadAllText(patternPath);

            initialBuild = true;
            BuildRGBPattern();
            initialBuild = false;
        }


        void ParseEffectParams_RGB(string profile)
        {
            XElement root = XElement.Parse(profile);
            int pointCount = int.Parse(root.Attribute("PointCount").Value);
            PointCount = pointCount;

            Pattern = new PatternPoint[pointCount];

            for (int i = 0; i < pointCount; i++)
                Pattern[i] = new PatternPoint();

            CreateRGBSliders(root, UpSliderList);

            //foreach (XElement basePoint in root.Elements("BasePoint"))
            //{
            //    int Pos = int.Parse(basePoint.Attribute("Pos").Value);

            //    System.Drawing.Color color = System.Drawing.Color.FromArgb
            //                        (0,
            //                         int.Parse(basePoint.Attribute("R").Value),
            //                         int.Parse(basePoint.Attribute("G").Value),
            //                         int.Parse(basePoint.Attribute("B").Value)
            //                        );

            //    PatternPoint pp = Pattern[Pos - 1];
            //    pp.H = color.GetHue();
            //    pp.S = color.GetSaturation();
            //    pp.L = color.GetBrightness();
            //    pp.SaveLightness();
            //    pp.PointColor = Color.FromRgb(color.R, color.G, color.B);
            //    pp.Lightness = Convert.ToInt32(pp.L * 255);

            //    SliderItem si = new SliderItem();
            //    si.PatternPoint = Pattern[Pos - 1];
            //    si.Variant = (PointVariant)int.Parse(basePoint.Attribute("Variant").Value);
            //    si.Minimum = 1;
            //    si.Maximum = pointCount;
            //    si.SelectionStart = 1;
            //    si.SelectionEnd = si.Maximum;
            //    si.Value = Pos;
            //    si.SliderType = SliderTypeEnum.RGB;
            //    UpSliderList.Add(si);
            //}
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

    }
}
