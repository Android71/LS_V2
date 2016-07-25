using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
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
using System.Linq;

namespace PatternEffect.ViewModel
{
    public class EffectViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;
        
        public EffectViewModel(IDataService dataService)
        {
            _dataService = dataService;
            UpdatePatternCmd = new RelayCommand<SliderDuplet>((sd) => ExecUpdatePattern(sd));
            CreateProfileCmd = new RelayCommand(ExecCreateProfileCmd);
            
            string path = Assembly.GetExecutingAssembly().Location;
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            string patternDir = dirInfo.Parent.Parent.Parent.FullName;
            string patternPath = patternDir + @"\Pattern.xml";

            Params = File.ReadAllText(patternPath);

            LightSliders = SliderItems.Where(p => p.Variant == PointVariant.Lightness).ToList();

            var sw = System.Diagnostics.Stopwatch.StartNew();

            BuildPattern();
            BuildLightGradient();

            sw.Stop();
            Console.WriteLine("Time taken: {0}ms", sw.Elapsed.TotalMilliseconds);
        }


        List<SliderItem> LightSliders { get; set; }


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

        int _maxlimit;
        public int Maxlimit
        {
            get { return _maxlimit; }
            set { Set(ref _maxlimit, value); }
        }

        public string Params
        {
            get { return CreateEffectParams(); }

            set { ParseEffectParams(value); }
        }

        void BuildPattern()
        {
            SliderItem prevSlider = null;
            if (SliderItems.Count > 1)
            {
                //int ix = 0;
                foreach(SliderItem si in SliderItems)
                {
                    // first slider
                    if (prevSlider == null)
                    {
                        if (si.Variant != PointVariant.Lightness)
                            prevSlider = si;
                        continue;
                    }

                    if (si.Variant != PointVariant.Lightness)
                    {
                        BuildGradient(prevSlider, si);
                        prevSlider = si;
                    }
                }
            }
        }

        void BuildLightGradient()
        {
            foreach(SliderItem si in LightSliders)
            {
                int ix = SliderItems.IndexOf(si);
                // после построения градиента по цветности необходимо восстановить
                // значение L 
                si.PatternPoint.L = si.PatternPoint.InitialL;
                si.PatternPoint.UpdateColor();

                BuildGradient(SliderItems[ix - 1], si, true);
                BuildGradient(si, SliderItems[ix + 1], true);
            }
        }

        void BuildGradient(SliderItem leftSlider, SliderItem rightSlider, bool onlyLightness = false)
        {
            double deltaH, deltaS, deltaL;

            int leftIx = Convert.ToInt32(leftSlider.Value);
            int rightIx = Convert.ToInt32(rightSlider.Value);
            int stepCount = rightIx - leftIx;

            if (stepCount > 0)
            {
                if (onlyLightness)
                {
                    deltaH = 0.0;
                    deltaS = 0.0;
                    deltaL = (rightSlider.PatternPoint.L - leftSlider.PatternPoint.L) / stepCount;
                    for (int i = leftIx + 1; i < rightIx; i++)
                    {
                        PatternPoint prevPoint = Pattern[i - 1 - 1];
                        Pattern[i - 1].L = prevPoint.L + deltaL;
                        Pattern[i - 1].UpdateColor();
                    }
                    return;
                }

                if (leftSlider.Variant == PointVariant.RangeLeft)
                {
                    for (int i = leftIx + 1; i < rightIx; i++)
                    {
                        leftSlider.PatternPoint.CopyTo(Pattern[i - 1]);
                    }
                    return;
                }

                deltaH = (rightSlider.PatternPoint.H - leftSlider.PatternPoint.H) / stepCount;
                deltaS = (rightSlider.PatternPoint.S - leftSlider.PatternPoint.S) / stepCount;
                deltaL = (rightSlider.PatternPoint.L - leftSlider.PatternPoint.L) / stepCount;
                for (int i = leftIx + 1; i < rightIx; i++)
                {
                    PatternPoint pp = Pattern[i - 1 - 1];
                    Pattern[i - 1].SetColorFromHSL(pp.H + deltaH, pp.S + deltaS, pp.L + deltaL);
                }
            }
        }

        public void ClearLeftEnd()
        {
            int leftIx = (int)SliderItems[0].Value - 1;
            for (int i = 0; i < leftIx; i++)
                Pattern[i].Clear();
        }

        public void ClearRightEnd()
        {
            int rightIx = (int)SliderItems[SliderItems.Count - 1].Value;
            for (int i = rightIx; i < Maxlimit; i++)
                Pattern[i].Clear();
        }

        public RelayCommand<SliderDuplet> UpdatePatternCmd { get; private set; }

        private void ExecUpdatePattern(SliderDuplet sd)
        {
            if (sd.LeftSlider == null)
            {
                ClearLeftEnd();
                return;
            }
            if (sd.RightSlider == null)
            {
                ClearRightEnd();
                return;
            }
            if (sd.OnlyLightness)
                BuildGradient(sd.LeftSlider, sd.RightSlider, true);
            else
                BuildGradient(sd.LeftSlider, sd.RightSlider);
        }

        void ParseEffectParams(string profile)
        {
            string path = Assembly.GetExecutingAssembly().Location;
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            string patternDir = dirInfo.Parent.Parent.Parent.FullName;
            string patternPath = patternDir + @"\Pattern.xml";

            string xmlContent = File.ReadAllText(patternPath);

            XElement root = XElement.Parse(xmlContent);
            int pointCount = int.Parse(root.Attribute("PointCount").Value);
            Maxlimit = pointCount;

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
                pp.InitialL = pp.L;
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

        string _fileName = "Pattern_1.xml";
        public string FileName
        {
            get { return _fileName; }
            set { Set(ref _fileName, value); }
        }

        public RelayCommand CreateProfileCmd { get; private set; }

        private void ExecCreateProfileCmd()
        {
            string path = Assembly.GetExecutingAssembly().Location;
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            string patternDir = dirInfo.Parent.Parent.Parent.FullName;
            string patternPath = patternDir + @"\" + FileName;
            string s = Params;
            File.WriteAllText(patternPath, Params);
        }


        string CreateEffectParams()
        {
            XElement profile = new XElement("Params", new XAttribute("PointCount", Maxlimit));

            //< BasePoint Pos = "1" R = "0" G = "128" B = "0"  Variant = "0" />
            foreach (SliderItem si in SliderItems)
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
    }
}