using GalaSoft.MvvmLight;
using LS_Designer_WPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS_Designer_WPF.ViewModel
{
    public class LightElementVM : ViewModelBase
    {

        IDataService _dataService = null;

        public LightElementVM(LightElement model)
        {
            Model = model;
            _dataService = ViewModelLocator.DataService;
        }

        LightElement _model;
        public LightElement Model
        {
            get { return _model; }
            set { Set(ref _model, value); }
        }

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

        public bool IsLinkedBeforeAction;

        bool _isLinked = false;
        public bool IsLinked
        {
            get { return Model.ControlChannel != null; }
            set
            {

                Set(ref _isLinked, value);

                if ((value && Model.ControlChannel == null) || (!value && Model.ControlChannel != null))
                    MessengerInstance.Send("", AppContext.LE_LinkChangedMsg);
            }
        }

        public void RaiseIsLinkedChanged()
        {
            RaisePropertyChanged("IsLinked");
        }

        bool _canChangeLink = false;
        public bool ChangeLinkEnable
        {
            get { return _canChangeLink; }
            set { Set(ref _canChangeLink, value); }
        }

        bool _directChild = false;
        public bool DirectChild
        {
            get { return _directChild; }
            set { Set(ref _directChild, value); }
        }

        public void SetSilentIsLinked(bool val, bool raisePropertyChanged = false)
        {
            _isLinked = val;
            if (raisePropertyChanged)
                RaisePropertyChanged("IsLinked");
        }

        public bool InConflict { get; set; }

        List<LE_Proxy> _proxies;
        public List<LE_Proxy> Proxies
        {
            get { return _proxies; }
            set { Set(ref _proxies, value); }
        }

        //LE_Proxy _proxy;
        public LE_Proxy LE_Proxy { get; set; }

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

        public void RaiseLinkedToZoneChanged()
        {
            RaisePropertyChanged("LinkedToZone");
        }

    }
}
