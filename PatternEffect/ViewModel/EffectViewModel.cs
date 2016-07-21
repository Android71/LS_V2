using GalaSoft.MvvmLight;
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

namespace PatternEffect.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class EffectViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;
        
        public EffectViewModel(IDataService dataService)
        {
            _dataService = dataService;
            //SliderItem si = new SliderItem();
            //si.Minimum = 0;
            //si.Maximum = 10;
            //si.SmallChange = 1;
            //si.LargeChange = 1;
            //si.SelectionEnd = 8;
            //si.SelectionStart = 3;
            //si.Value = 5;
            
            //SliderItems.Add(si);
            //ParseProfile("Pattern.xml");
            string path = Assembly.GetExecutingAssembly().Location;
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            string patternDir = dirInfo.Parent.Parent.Parent.FullName;
            string patternPath = patternDir + @"\Pattern.xml";

            PatternXML = File.ReadAllText(patternPath);
        }

        ObservableCollection<SliderItem> _sliderItems = new ObservableCollection<SliderItem>();
        public ObservableCollection<SliderItem> SliderItems
        {
            get { return _sliderItems; }
            set { Set(ref _sliderItems, value); }
        }

        PatternPoint[] _pattern;
        public PatternPoint[] Pattern
        {
            get { return _pattern; }
            set { Set(ref _pattern, value); }
        }


        public string PatternXML
        {
            get { return ""; }

            set { ParsePatternParams(value); }
        }

        void ParsePatternParams(string profile)
        {
            string path = Assembly.GetExecutingAssembly().Location;
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            string patternDir = dirInfo.Parent.Parent.Parent.FullName;
            string patternPath = patternDir + @"\Pattern.xml";

            string xmlContent = File.ReadAllText(patternPath);

            XElement root = XElement.Parse(xmlContent);
            int pointCount = int.Parse(root.Attribute("PointCount").Value);

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
                SliderItems.Add(si);
            }
        }
    }
}