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

            initialBuild = true;
            BuildRGBPattern();
            initialBuild = false;
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
    }
}
