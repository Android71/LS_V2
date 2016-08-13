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
    // | PointType      |  UpView     |  DownView |  UpSliderType | DownSliderType | PatternInfo
    //************************************************************************************** 
    // | RGB            |  RGB        |   -       |  RGB          | -              | None
    // | RGBW           |  RGB        |  White    |  RGB          | White          | Complex
    // | RGBWT          |  RGB        |  WT       |  RGB          | WT             | Complex
    // | RGB_W          |  RGB/White  |  -        |  RGB          | White          | RGBOnly/WhiteOnly
    // | RGB_WT         |  RGB/WT     |  -        |  RGB          | WT             | RGBOnly/WhiteOnly
    // | W              |  White      |  -        |  White        | -              | None
    // | WT             |  WT         |  -        |  WT           | -              | None
    // | CW             |  Warm       | Cold      |  Warm         | Cold           | None
    

    public partial class EffectViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;

        public EffectViewModel(IDataService dataService)
        {
            _dataService = dataService;
            UpdatePatternCmd = new RelayCommand<SliderItem>((si) => ExecUpdatePattern(si));
            SaveProfileCmd = new RelayCommand(ExecSaveProfileCmd);
            SetActiveListCmd = new RelayCommand<List<SliderItem>>(ExecSetActiveListCmd);

            string path = Assembly.GetExecutingAssembly().Location;
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            string patternDir = dirInfo.Parent.Parent.Parent.FullName;

            //PointType = PointTypeEnum.RGB;
            //PointType = PointTypeEnum.RGBW;
            //PointType = PointTypeEnum.RGBWT;
            //PointType = PointTypeEnum.CW;
            PointType = PointTypeEnum.WT;


            LoadModel(PointType, patternDir);
            ActiveSliderList = UpSliderList;
        }


        #region Commands

        public RelayCommand<List<SliderItem>> SetActiveListCmd { get; private set; }

        private void ExecSetActiveListCmd(List<SliderItem> list)
        {
            ActiveSliderList = list;
        }

        public RelayCommand<SliderItem> UpdatePatternCmd { get; private set; }

        private void ExecUpdatePattern(SliderItem si)
        {
            UpdatePattern(si);
        }

        #endregion

        #region Effect Entity Properties

        public string Params
        {
            get { return CreatePatternParams(); }

            set { ParsePatternParams(PointType, value); }
        }

        PointTypeEnum _pointType = PointTypeEnum.RGB;
        public PointTypeEnum PointType
        {
            get { return _pointType; }
            set { Set(ref _pointType, value); }
        }

        #endregion

        #region UI Related

        List<SliderItem> _upSliderList = new List<SliderItem>();
        public List<SliderItem> UpSliderList
        {
            get { return _upSliderList; }
            set { Set(ref _upSliderList, value); }
        }

        List<SliderItem> _downSliderList = new List<SliderItem>();
        public List<SliderItem> DownSliderList
        {
            get { return _downSliderList; }
            set { Set(ref _downSliderList, value); }
        }

        public List<SliderItem> ActiveSliderList { get; set; }

        PatternPoint[] _pattern;
        public PatternPoint[] Pattern
        {
            get { return _pattern; }
            set { Set(ref _pattern, value); }
        }

        int _pointCount;
        public int PointCount
        {
            get { return _pointCount; }
            set { Set(ref _pointCount, value); }
        }

        PatternInfoEnum _patternInfo = PatternInfoEnum.None;
        PatternInfoEnum PatternInfo
        {
            get { return _patternInfo; }
            set
            {
                Set(ref _patternInfo, value);
            }
        }

        #endregion

        #region Debug Stuff

        string _fileName = "Pattern_1.xml";
        public string FileName
        {
            get { return _fileName; }
            set { Set(ref _fileName, value); }
        }

        public RelayCommand SaveProfileCmd { get; private set; }

        private void ExecSaveProfileCmd()
        {
            string path = Assembly.GetExecutingAssembly().Location;
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            string patternDir = dirInfo.Parent.Parent.Parent.FullName;
            string patternPath = patternDir + @"\" + FileName;
            string s = Params;
            File.WriteAllText(patternPath, s);
        }

        #endregion

    }


}