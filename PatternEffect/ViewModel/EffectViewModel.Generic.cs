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
        SliderItem CreateSlider(int ix, int pos, PointVariant pVariant, SliderTypeEnum sliderType)
        {
            SliderItem si = new SliderItem();
            si.Ix = ix;
            si.PatternPoint = Pattern[pos - 1];
            //si.Variant = (PointVariant)int.Parse(basePoint.Attribute("Variant").Value);
            si.Variant = pVariant;
            si.Minimum = 1;
            si.Maximum = PointCount;
            si.SelectionStart = 1;
            si.SelectionEnd = si.Maximum;
            si.Value = pos;
            si.SliderType = sliderType;
            //sliderList.Add(si);
            return si;
        }

        void CreateRGBSliders(XElement root, List<SliderItem> sliderList)
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

                sliderList.Add(CreateSlider(ix, Pos, (PointVariant)int.Parse(basePoint.Attribute("Variant").Value), SliderTypeEnum.RGB));
                ix++;
            }
        }

        void Create_W_Sliders(XElement root, List<SliderItem> sliderList)
        {
            int ix = 0;
            foreach (XElement basePoint in root.Elements("BasePoint"))
            {
                int Pos = int.Parse(basePoint.Attribute("Pos").Value);
                PatternPoint pp = Pattern[Pos - 1];

                pp.White = int.Parse(basePoint.Attribute("W").Value);
                pp.WhiteD = pp.White / 255.0;

                DownSliderList.Add(CreateSlider(ix, Pos, (PointVariant)int.Parse(basePoint.Attribute("Variant").Value), SliderTypeEnum.W));
                ix++;
            }
        }
    }
}
