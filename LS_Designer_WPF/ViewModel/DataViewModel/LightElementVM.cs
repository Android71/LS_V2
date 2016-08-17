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

        bool _canChangeLink = false;
        public bool ChangeLinkEnable
        {
            get { return _canChangeLink; }
            set { Set(ref _canChangeLink, value); }
        }

        public Action<bool> IsLinkedChanged;

        bool _isLinked = false;
        public bool IsLinked
        {
            
            get { return _isLinked; }
            set
            {
                bool tmp = value;
                Set(ref _isLinked, value);
                if (IsLinkedChanged != null)
                    IsLinkedChanged(tmp);
            }
        }

        bool _directChild = false;
        public bool DirectChild
        {
            get { return _directChild; }
            set { Set(ref _directChild, value); }
        }

        public bool InConflict { get; set; }

        public bool Validate()
        {
            int maxCount = 512 / AppContext.CountByType[Model.PointType];
            StringBuilder sb = new StringBuilder();
            bool result = true;
            bool rule1 = true, rule2 = true, rule3 = true;

            // Validade fields

            if (Model.StartPoint < 1)
            {
                sb.AppendLine("StartPoint не может быть меньше 1");
                rule1 = false;
            }

            if (Model.PointCount < 1)
            {
                sb.AppendLine("PointCount не может быть меньше 1");
                rule2 = false;
            }

            if (rule1 & rule2)
            {
                int tmp = (Model.StartPoint - 1) * AppContext.CountByType[Model.PointType] + Model.PointCount * AppContext.CountByType[Model.PointType];
                if (tmp > 512)
                {
                    sb.AppendLine("LightStrip выходит за границы Universe");
                    sb.AppendLine("Измените либо StartPoint либо PointCount");
                    rule3 = false;
                }
            }

            result = rule1 & rule2 & rule3;

            if (!result)
            {
                PopUpMessageVM info = new PopUpMessageVM(sb.ToString());
                MessengerInstance.Send<EmptyPopUpVM>(info, AppContext.ShowPopUpMsg);
            }

            return result;
        }


    }
}
