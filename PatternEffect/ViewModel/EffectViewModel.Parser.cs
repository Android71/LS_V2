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

namespace LS_Designer_WPF.Model
//namespace PatternEffect.ViewModel
{
    public partial class Effect
    {

        void LoadModel(PointTypeEnum pointType, string path)
        {
            switch (pointType)
            {
                case PointTypeEnum.RGB:
                    FileName = "Pattern_RGB.xml";
                    //FileName = "Pattern_RGB_3.xml";
                    break;
                case PointTypeEnum.RGBW:
                    FileName = "Pattern_RGBW.xml";
                    break;
                case PointTypeEnum.RGBWT:
                    FileName = "Pattern_RGBWT.xml";
                    break;
                case PointTypeEnum.CW:
                    FileName = "Pattern_CW.xml";
                    break;
                case PointTypeEnum.WT:
                    FileName = "Pattern_WT.xml";
                    break;
            }

            string patternPath = path + @"\" + FileName;

            Params = File.ReadAllText(patternPath);

            BuildPattern(UpSliderList);
            BuildPattern(DownSliderList);
        }

        void CreateSliderList(XElement root, List<SliderItem> sliderList)
        {
            SliderTypeEnum sliderType = SliderTypeEnum.RGB;
            int ix = 0;
            if (root != null)
            {
                foreach (XElement basePoint in root.Elements("BasePoint"))
                {
                    int Pos = int.Parse(basePoint.Attribute("Pos").Value);
                    PatternPoint pp = Pattern[Pos - 1];
                    switch (root.Name.ToString())
                    {
                        case "RGB":
                            System.Drawing.Color color = System.Drawing.Color.FromArgb
                                        (0,
                                         int.Parse(basePoint.Attribute("R").Value),
                                         int.Parse(basePoint.Attribute("G").Value),
                                         int.Parse(basePoint.Attribute("B").Value)
                                        );

                            pp = Pattern[Pos - 1];
                            pp.H = color.GetHue();
                            pp.S = color.GetSaturation();
                            pp.L = color.GetBrightness();
                            pp.SaveLightness();
                            pp.PointColor = Color.FromRgb(color.R, color.G, color.B);
                            pp.Lightness = Convert.ToInt32(pp.L * 255.0);
                            sliderType = SliderTypeEnum.RGB;
                            break;
                        case "White":
                            pp.WhiteD = double.Parse(basePoint.Attribute("W").Value);
                            pp.InitialWhiteD = pp.WhiteD;
                            sliderType = SliderTypeEnum.W;
                            break;
                        case "WhiteTemp":
                            pp.WhiteD = double.Parse(basePoint.Attribute("W").Value);
                            pp.InitialWhiteD = pp.WhiteD;
                            pp.Temp = double.Parse(basePoint.Attribute("T").Value);
                            sliderType = SliderTypeEnum.WT;
                            break;
                        case "Warm":
                            pp.WarmD = double.Parse(basePoint.Attribute("W").Value);
                            pp.InitialWarmD = pp.WarmD;
                            sliderType = SliderTypeEnum.Warm;
                            break;
                        case "Cold":
                            pp.ColdD = double.Parse(basePoint.Attribute("W").Value);
                            pp.InitialColdD = pp.ColdD;
                            sliderType = SliderTypeEnum.Cold;
                            break;
                    }

                    sliderList.Add(CreateSlider(sliderList, ix, Pos, (PointVariant)int.Parse(basePoint.Attribute("Variant").Value), sliderType));
                    ix++;
                }
            }
        }

        void ParsePatternParams(PointTypeEnum pointType, string profile)
        {
            XElement basePoints1 = null;
            XElement basePoints2 = null;


            XElement root = XElement.Parse(profile);
            int pointCount = int.Parse(root.Attribute("PointCount").Value);
            PointCount = pointCount;
            Pattern = new PatternPoint[pointCount];
            for (int i = 0; i < pointCount; i++)
                Pattern[i] = new PatternPoint();

            switch (pointType)
            {
                case PointTypeEnum.RGB:
                    basePoints1 = root.Elements("RGB").First();
                    break;
                case PointTypeEnum.RGBW:
                    basePoints1 = root.Elements().First(p => p.Name == "RGB");
                    basePoints2 = root.Elements().First(p => p.Name == "White");
                    break;
                case PointTypeEnum.RGBWT:
                    basePoints1 = root.Elements().First(p => p.Name == "RGB");
                    basePoints2 = root.Elements().First(p => p.Name == "WhiteTemp");
                    break;
                case PointTypeEnum.CW:
                    basePoints1 = root.Elements().First(p => p.Name == "Warm");
                    basePoints2 = root.Elements().First(p => p.Name == "Cold");
                    break;
                case PointTypeEnum.WT:
                    basePoints1 = root.Elements().First(p => p.Name == "WhiteTemp");
                    break;
                case PointTypeEnum.W:
                    basePoints1 = root.Elements().First(p => p.Name == "White");
                    break;
            }

            CreateSliderList(basePoints1, UpSliderList);
            CreateSliderList(basePoints2, DownSliderList);
        }

        string CreatePatternParams()
        {
            XElement part1 = null;
            XElement part2 = null;

            XElement profile = new XElement("Params", new XAttribute("PointCount", PointCount));
            switch (PointType)
            {
                case PointTypeEnum.RGB:
                    part1 = new XElement("RGB");
                    profile.Add(part1);
                    break;
                case PointTypeEnum.RGBW:
                    part1 = new XElement("RGB");
                    profile.Add(part1);
                    part2 = new XElement("White");
                    profile.Add(part2);
                    break;
                case PointTypeEnum.RGBWT:
                    part1 = new XElement("RGB");
                    profile.Add(part1);
                    part2 = new XElement("WhiteTemp");
                    profile.Add(part2);
                    break;
                case PointTypeEnum.CW:
                    part1 = new XElement("Warm");
                    profile.Add(part1);
                    part2 = new XElement("Cold");
                    profile.Add(part2);
                    break;
                case PointTypeEnum.WT:
                    part1 = new XElement("WarmTemp");
                    profile.Add(part1);
                    break;
                case PointTypeEnum.W:
                    part1 = new XElement("White");
                    profile.Add(part1);
                    break;
            }

            foreach (SliderItem si in UpSliderList)
            {
                part1.Add(BasePoint(si));
            }

            foreach (SliderItem si in DownSliderList)
            {
                part2.Add(BasePoint(si));
            }

            return profile.ToString();
        }


        #region CW

        //void ParsePatternParams_CW(string profile)
        //{
        //    XElement root = XElement.Parse(profile);
        //    PointCount = int.Parse(root.Attribute("PointCount").Value);

        //    Pattern = new PatternPoint[PointCount];

        //    for (int i = 0; i < PointCount; i++)
        //        Pattern[i] = new PatternPoint();

        //    XElement warmPoints = root.Elements().First(p => p.Name == "Warm");

        //    XElement coldPoints = root.Elements().First(p => p.Name == "Cold");

        //    CreateSliderList(warmPoints, UpSliderList);
        //    CreateSliderList(coldPoints, DownSliderList);
        //}


        //string CreatePatternParams_CW()
        //{
        //    XElement profile = new XElement("Params", new XAttribute("PointCount", PointCount));

        //    XElement cold = new XElement("Cold");
        //    XElement warm = new XElement("Warm");
        //    profile.Add(cold);
        //    profile.Add(warm);
        //    foreach (SliderItem si in UpSliderList)
        //    {
        //        warm.Add(whitePoint(si));
        //    }
        //    foreach (SliderItem si in DownSliderList)
        //    {
        //        cold.Add(whitePoint(si));
        //    }
        //    return profile.ToString();
        //}

        #endregion

        #region BasePoint

        XElement BasePoint(SliderItem si)
        {
            XElement xe = null;
            switch (si.SliderType)
            {
                case SliderTypeEnum.RGB:
                    xe = new XElement("BasePoint",
                    new XAttribute("Pos", ((int)si.Value).ToString()),
                    new XAttribute("R", (si.PatternPoint.PointColor.R).ToString()),
                    new XAttribute("G", (si.PatternPoint.PointColor.G).ToString()),
                    new XAttribute("B", (si.PatternPoint.PointColor.B).ToString()),
                    new XAttribute("Variant", ((int)si.Variant).ToString()));
                    break;
                case SliderTypeEnum.W:
                case SliderTypeEnum.Cold:
                case SliderTypeEnum.Warm:
                    xe = new XElement("BasePoint",
                    new XAttribute("Pos", ((int)si.Value).ToString()),
                    new XAttribute("W", (si.PatternPoint.WhiteD).ToString()),
                    new XAttribute("Variant", ((int)si.Variant).ToString()));
                    break;
                case SliderTypeEnum.WT:
                    xe = new XElement("BasePoint",
                    new XAttribute("Pos", ((int)si.Value).ToString()),
                    new XAttribute("W", (si.PatternPoint.WhiteD).ToString()),
                    new XAttribute("T", (si.PatternPoint.Temp).ToString()),
                    new XAttribute("Variant", ((int)si.Variant).ToString()));
                    break;
            }
            return xe;
        }

        //XElement whitePoint(SliderItem si)
        //{

        //    //< BasePoint Pos = "1" W = "0"  Variant = "0" />
        //    XElement xe = new XElement("BasePoint",
        //            new XAttribute("Pos", ((int)si.Value).ToString()),
        //            new XAttribute("W", (si.PatternPoint.WhiteD).ToString()),
        //            new XAttribute("Variant", ((int)si.Variant).ToString())
        //        );
        //    return xe;
        //}

        #endregion

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

    }
}
