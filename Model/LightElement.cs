using GalaSoft.MvvmLight;
using LS_Designer_WPF.ViewModel;
using LS_Library;
using System;
using System.Collections.Generic;
using System.Text;

namespace LS_Designer_WPF.Model
{
    public class LightElement : ViewModelBase
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

            if (space.Prefix == "AN" || space.Prefix == "DX")
            {
                if (pointType == PointTypeEnum.RGB)
                    ColorSequence = "RGB";
                if (pointType == PointTypeEnum.RGBW)
                    ColorSequence = "RGBW";
            }
            Name = string.Format($"{ControlSpace.Prefix}_{pointType}_");
        }


#region Entity Properties

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

        string _colorSequence;
        public string ColorSequence
        {
            get { return _colorSequence; }
            set { Set(ref _colorSequence, value); }
        }

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

        List<LE_Proxy> _le_Proxies;
        public List<LE_Proxy> LE_Proxies
        {
            get { return _le_Proxies; }
            set { Set(ref _le_Proxies, value); }
        }

        #endregion

        #region Auxiliary properties


        public int EndPoint { get { return StartPoint + PointCount - 1; } }

        public int StartDMX     // 1 - 512
        {
            get { return (StartPoint - 1) * AppContext.CountByType[PointType] + 1; }
        }

        public int StartDMX_511     // 0 - 511
        {
            get { return StartDMX - 1; }
        }

        public int EndDMX   // 1 - 512
        {
            get { return StartDMX + PointCount * AppContext.CountByType[PointType] - 1; }
        }

        public int EndDMX_511   // 0 - 511
        {
            get { return EndDMX - 1; }
        }

        public int DMXlength
        {
            get { return PointCount * AppContext.CountByType[PointType]; }
        }

        public string FullName
        {
            get { return string.Format($"{Name}   StartPoint: {StartPoint}   EndPoint: {EndPoint}   PointCount: {PointCount}"); }
        }

        #endregion



        

        




        /*********************************************************************/
        //UI related
        /*********************************************************************/

        // LightElementsVM and LightZonesVM
        List<Partition> _partitions;
        public List<Partition> Partitions //{ get; set; }
        {
            get { return _partitions; }
            set { Set(ref _partitions, value); }
        }

        //List<Gamma> _gammas;
        //public List<Gamma> Gammas //{ get; set; }
        //{
        //    get { return _gammas; }
        //    set { Set(ref _gammas, value); }
        //}

        // LightElementsVM and LightZonesVM
        bool _isLinked = false;
        public bool IsLinked
        {
            //get { return _isLinked; }
            get { return ControlChannel != null; }
            set
            {
                Set(ref _isLinked, value);
                //if ((value && ControlChannel == null) || (!value && ControlChannel != null))
                //    MessengerInstance.Send("", AppContext.LE_LinkChangedMsg);
            }
        }

        // LightElementsVM and LightZonesVM
        bool _canChangeLink = false;
        public bool ChangeLinkEnable
        {
            get { return _canChangeLink; }
            set { Set(ref _canChangeLink, value); }
        }

        // LightElementsVM and LightZonesVM
        bool _directChild = false;
        public bool DirectChild
        {
            get { return _directChild; }
            set { Set(ref _directChild, value); }
        }

        /**********************************************************/

        //LightElementsVM
        List<string> _colorSequenceList;
        public List<string> ColorSequenceList //{ get; set; }
        {
            get { return _colorSequenceList; }
            set { Set(ref _colorSequenceList, value); }
        }

        //LightElementsVM
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

        // LightElementsVM
        public void RaiseIsLinkedChanged()
        {
            RaisePropertyChanged("IsLinked");
        }

        // LightElementsVM
        public void SetSilentIsLinked(bool val, bool raisePropertyChanged = false)
        {
            _isLinked = val;
            if (raisePropertyChanged)
                RaisePropertyChanged("IsLinked");
        }

        // LightElementsVM
        public bool InConflict { get; set; }

        // LightZonesVM
        public LE_Proxy  LE_Proxy {get; set;}

        // LightZonesVM
        public void RaiseLinkedToZoneChanged()
        {
            RaisePropertyChanged("LinkedToZone");
        }

        /**********************************************************/

        bool _linkedToZone = false;
        public bool LinkedToZone
        {
            get { return LE_Proxy != null; }
            set
            {
                Set(ref _linkedToZone, value);
                if ((value && LE_Proxy == null) || (!value && LE_Proxy != null))
                    MessengerInstance.Send("", AppContext.LE_LinkToZoneChangedMsg);
            }
        }
    }
}
