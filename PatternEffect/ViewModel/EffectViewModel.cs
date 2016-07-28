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
            //UpdatePatternCmd = new RelayCommand<SliderDuplet>((sd) => ExecUpdatePattern(sd));
            UpdatePatternCmd = new RelayCommand<SliderItem>((si) => ExecUpdatePattern(si));
            CreateProfileCmd = new RelayCommand(ExecCreateProfileCmd);
            
            string path = Assembly.GetExecutingAssembly().Location;
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            string patternDir = dirInfo.Parent.Parent.Parent.FullName;
            string patternPath = patternDir + @"\Pattern.xml";
            //string patternPath = patternDir + @"\Pattern_1.xml";

            Params = File.ReadAllText(patternPath);

            //LightSliders = SliderList.Where(p => p.Variant == PointVariant.Lightness).ToList();

            var sw = System.Diagnostics.Stopwatch.StartNew();

            initialBuild = true;
            BuildPattern();
            initialBuild = false;
            //BuildLightGradient();

            sw.Stop();
            Console.WriteLine("Time taken: {0}ms", sw.Elapsed.TotalMilliseconds);
        }

        bool initialBuild = false;

        List<SliderItem> LightSliders { get; set; }


        ObservableCollection<SliderItem> _sliderList = new ObservableCollection<SliderItem>();
        public ObservableCollection<SliderItem> SliderList
        {
            get { return _sliderList; }
            set { Set(ref _sliderList, value); }
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
            if (SliderList.Count > 1)
            {
                foreach(SliderItem si in SliderList)
                {
                    
                    if (si.Variant != PointVariant.RangeRight)
                        PrepareAndBuildGradient(si);
                }
            }
        }

        void PrepareAndBuildGradient(SliderItem currentSlider)
        {
            //Console.WriteLine($"Slider.Value: {currentSlider.Value}");
            //return;

            SliderItem prevSlider = null;
            SliderItem nextSlider = null;
            SliderItem afterRangeRight = null;
            SliderItem beforeRangeLeft = null;
            if (currentSlider != null)
            {
                int ix = SliderList.IndexOf(currentSlider);
                if (ix != 0)
                    prevSlider = SliderList[ix - 1];
                if (ix != SliderList.Count - 1)
                    nextSlider = SliderList[ix + 1];

                if (prevSlider == null)
                    ClearLeftEnd();
                if (nextSlider == null)
                    ClearRightEnd();

                if (currentSlider.Variant == PointVariant.RangeLeft)
                {
                    currentSlider.PatternPoint.CopyTo(nextSlider.PatternPoint);
                    if (prevSlider != null)
                        BuildGradient(prevSlider, currentSlider);
                    BuildGradient(currentSlider, nextSlider);
                    if (!initialBuild)
                    {
                        if ((ix + 1) != (SliderList.Count - 1))
                        {
                            // Slider RangeRight не последний Slider в SliderList
                            afterRangeRight = SliderList[ix + 2];
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
                        beforeRangeLeft = SliderList[ix - 2];
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

        void BuildGradient(SliderItem leftSlider, SliderItem rightSlider, bool onlyLightness = false)
        {
            double deltaH, deltaS, deltaL;

            int leftIx = Convert.ToInt32(leftSlider.Value);
            int rightIx = Convert.ToInt32(rightSlider.Value);
            int stepCount = rightIx - leftIx;

            if (stepCount > 0)
            {
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
            int leftIx = (int)SliderList[0].Value - 1;
            for (int i = 0; i < leftIx; i++)
                Pattern[i].Clear();
        }

        public void ClearRightEnd()
        {
            int rightIx = (int)SliderList[SliderList.Count - 1].Value;
            for (int i = rightIx; i < Maxlimit; i++)
                Pattern[i].Clear();
        }

        //public RelayCommand<SliderDuplet> UpdatePatternCmd { get; private set; }
        public RelayCommand<SliderItem> UpdatePatternCmd { get; private set; }

        private void ExecUpdatePattern(SliderItem si)
        {
            PrepareAndBuildGradient(si);
        }

        //private void ExecUpdatePattern(SliderDuplet sd)
        //{
        //    if (sd.LeftSlider == null)
        //    {
        //        ClearLeftEnd();
        //        return;
        //    }
        //    if (sd.RightSlider == null)
        //    {
        //        ClearRightEnd();
        //        return;
        //    }
        //    if (sd.OnlyLightness)
        //        BuildGradient(sd.LeftSlider, sd.RightSlider, true);
        //    else
        //        BuildGradient(sd.LeftSlider, sd.RightSlider);
        //}

        void ParseEffectParams(string profile)
        {
            //string path = Assembly.GetExecutingAssembly().Location;
            //DirectoryInfo dirInfo = new DirectoryInfo(path);
            //string patternDir = dirInfo.Parent.Parent.Parent.FullName;
            //string patternPath = patternDir + @"\Pattern.xml";

            //string xmlContent = File.ReadAllText(patternPath);

            XElement root = XElement.Parse(profile);
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
                SliderList.Add(si);
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
            foreach (SliderItem si in SliderList)
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