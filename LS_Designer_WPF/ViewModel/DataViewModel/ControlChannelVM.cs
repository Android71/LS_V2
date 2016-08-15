using GalaSoft.MvvmLight;
using LS_Designer_WPF.Model;
using LS_Designer_WPF.PopUpMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS_Designer_WPF.ViewModel.DataViewModel
{
    public class ControlChannelVM : ViewModelBase
    {
        List<Partition> _partitions;
        public List<Partition> Partitions
        {
            get { return _partitions; }
            set { Set(ref _partitions, value); }
        }

        bool _directParent = false;
        public bool DirectParent
        {
            get { return _directParent; }
            set { Set(ref _directParent, value); }
        }

        bool _hasChildren = false;
        public bool HasChildren
        {
            //get { return _hasChildren; }
            get { return LE_Count != 0; }
            set { Set(ref _hasChildren, value); }
        }

        public int LE_Count { get; set; }

        ControlChannel _model;
        public ControlChannel Model
        {
            get { return _model; }
            set { Set(ref _model, value); }
        }

        public bool CanLinkLE(LightElement le, PopUpMessageVM messageVM)
        {
            bool result = true;
            if (Model is AN6UControlChannel)
            {
                if (LE_Count >= 1 && Model.PointType != le.PointType)
                {
                    messageVM.Message = AppMessages.UniverseLinkMsg();
                    result = false;
                }
            }
            if (Model is NLPowerChannel)
            {
                if (Model.PointType != le.PointType)
                {
                    messageVM.Message = AppMessages.LE_LinkMsg();
                    return false;
                }
            }
            return result;
        }
    }
}
