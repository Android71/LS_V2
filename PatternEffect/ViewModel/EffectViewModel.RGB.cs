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

        void LoadModel_RGB(string path)
        {
            FileName = "Pattern_RGB.xml";
            string patternPath = path + @"\" + FileName;
            //string patternPath = patternDir + @"\Pattern_1.xml";
            //string patternPath = patternDir + @"\Pattern_2.xml";

            Params = File.ReadAllText(patternPath);
            BuildPattern(UpSliderList);
        }

        void ParsePatternParams_RGB(string profile)
        {
            XElement root = XElement.Parse(profile);
            int pointCount = int.Parse(root.Attribute("PointCount").Value);
            PointCount = pointCount;

            XElement rgb = root.Elements("RGB").First();

            Pattern = new PatternPoint[pointCount];

            for (int i = 0; i < pointCount; i++)
                Pattern[i] = new PatternPoint();

            Create_RGB_SliderList(rgb, UpSliderList);

        }

        string CreatePatternParams_RGB()
        {
            XElement profile = new XElement("Params", new XAttribute("PointCount", PointCount));

            XElement rgb = new XElement("RGB");
            profile.Add(rgb);
            foreach (SliderItem si in UpSliderList)
            {
                rgb.Add(rgbPoint(si));
            }
            return profile.ToString();
        }

        

    }
}
