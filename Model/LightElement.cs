using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Text;

namespace LS_Designer_WPF.Model
{
    public class LightElement : ObservableObject
    {

        public static List<string> ColorSequenseRGB = new List<string>()
                                                      { "RGB", "RBG", "GRB", "GBR", "BRG", "BGR" };

        public static List<string> ColorSequenseRGBW = new List<string>()
                                                      { "RGBW", "RGWB", "RBGW", "RBWG", "RWGB", "RWBG",
                                                        "GRBW", "GRWB", "GBRW", "GBWR", "GWRB", "GWBR",
                                                        "BRGW", "BRWG", "BGRW", "BGWR", "BWRG", "BWGR",
                                                        "WRGB", "WRBG", "WGRB", "WGBR", "WBRG", "WBGR"};

        public LightElement() { }

        public LightElement(PointTypeEnum pointType, ControlSpace space) : this()
        {
            ControlSpace = space;
            PointType = pointType;
            if (pointType == PointTypeEnum.RGB && space.Id == 1)
            {
                ColorSequenceList = LightElement.ColorSequenseRGB;
                ColorSequence = ColorSequenceList[0];
            }
            if (pointType == PointTypeEnum.RGBW && space.Id == 1)
            {
                ColorSequenceList = LightElement.ColorSequenseRGBW;
                ColorSequence = ColorSequenceList[0];
            }
            Name = string.Format($"{ControlSpace.Prefix}_LE");
        }

        public int Id { get; set; }

        string _name;
        public string Name  //{ get; set; }
        {
            get { return _name; }
            set { Set(ref _name, value); }
        }

        public PointTypeEnum PointType { get; set; }

        public int StartPoint { get; set; } = 1;

        public int PointCount { get; set; } = 1;

        public Direction Direction { get; set; } = Direction.Up;

        public string ColorSequence { get; set; }

        public string Remark { get; set; }

        Partition _partition;
        public Partition Partition //{ get; set; }
        {
            get { return _partition; }
            set { Set(ref _partition, value); }
        }
        public ControlSpace ControlSpace { get; set; }

        public ControlChannel ControlChannel { get; set; }

        Gamma _gamma;
        public Gamma Gamma //{ get; set; }
        {
            get { return _gamma; }
            set { Set(ref _gamma, value); }
        }

        public CustomGamma CustomGamma { get; set; }


        /*********************************************************************/
        //UI related
        /*********************************************************************/

        List<Partition> _partitions;
        public List<Partition> Partitions //{ get; set; }
        {
            get { return _partitions; }
            set { Set(ref _partitions, value); }
        }

        List<Gamma> _gammas;
        public List<Gamma> Gammas //{ get; set; }
        {
            get { return _gammas; }
            set { Set(ref _gammas, value); }
        }

        List<string> _colorSequenceList;
        public List<string> ColorSequenceList //{ get; set; }
        {
            get { return _colorSequenceList; }
            set { Set(ref _colorSequenceList, value); }
        }

        bool _isEditMode = false;
        public bool IsEditMode
        {
            get { return _isEditMode; }
            set { Set(ref _isEditMode, value); }
        }

        bool _isAddMode = false;
        public bool IsAddMode
        {
            get { return _isAddMode; }
            set { Set(ref _isAddMode, value); }
        }

        bool _isLinked = false;
        public bool IsLinked
        {
            get { return _isLinked; }
            set { Set(ref _isLinked, value); }
        }

        bool _canLink = false;
        public bool CanLink
        {
            get { return _canLink; }
            set { Set(ref _canLink, value); }
        }

        bool _isMappingMode = true;
        public bool IsMappingMode
        {
            get { return _isMappingMode; }
            set { Set(ref _isMappingMode, value); }
        }
    }
}
