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
            FileName = "Pattern_RGBW.xml";
            string patternPath = path + @"\" + FileName;

            Params = File.ReadAllText(patternPath);

            BuildPattern(UpSliderList);
            BuildPattern(DownSliderList);
        }


        void ParsePatternParams_RGBW(string profile)
        {
            XElement root = XElement.Parse(profile);
            PointCount = int.Parse(root.Attribute("PointCount").Value);

            Pattern = new PatternPoint[PointCount];

            for (int i = 0; i < PointCount; i++)
                Pattern[i] = new PatternPoint();

            XElement rgbPoints = root.Elements().First(p => p.Name == "RGB");

            XElement whitePoints = root.Elements().First(p => p.Name == "White");

            Create_RGB_SliderList(rgbPoints, UpSliderList);
            Create_W_SliderList(whitePoints, DownSliderList);
        }


        string CreatePatternParams_RGBW()
        {
            XElement profile = new XElement("Params", new XAttribute("PointCount", PointCount));

            XElement rgb = new XElement("RGB");
            XElement white = new XElement("White");
            profile.Add(rgb);
            profile.Add(white);
            foreach (SliderItem si in UpSliderList)
            {
                rgb.Add(rgbPoint(si));
            }
            foreach (SliderItem si in DownSliderList)
            {
                white.Add(whitePoint(si));
            }
            return profile.ToString();
        }

        

        #endregion

    }
}
