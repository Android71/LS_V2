using GalaSoft.MvvmLight;
using LS_Designer_WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS_Designer_WPF.Model
{
    public class LightElement : ObservableObject
    {
        public static List<string> rgbCsList = new List<string>() { "RGB", "RBG", "GBR", "GRB", "BRG", "BGR" };
        public LightElement()
        {
            Name = "";
            StartPoint = 1;
            Remark = "";
        }

        public int Id { get; set; }

        string _name = "";
        public string Name { get { return _name; } set { Set(ref _name, value); } }

        public string FullName { get { return Partition.Name + " / " + Name; } }

        public string QualifiedName { get { return ControlSpace.Name + " / " + FullName; } }

        internal int oldStartPoint;

        int _startPoint = 1;
        public int StartPoint { get { return _startPoint; } set { Set(ref _startPoint, value); } }

        bool _canDimming = false;
        public bool CanDimming { get { return _canDimming; } set { Set(ref _canDimming, value); } }

        bool _isLinked = false;
        public bool IsLinked { get { return _isLinked; } set { Set(ref _isLinked, value); } }

        int _linkCount;
        public int LinkCount { get { return _linkCount; } set { Set(ref _linkCount, value); } }

        string _remark;
        public string Remark { get { return _remark; } set { Set(ref _remark, value); } }

        PointTypeEnum _elementType = PointTypeEnum.W;
        public virtual PointTypeEnum PointType { get { return _elementType; } set { Set(ref _elementType, value); } }

        public int Ix { get; set; }

        public Partition Partition { get; set; }

        public ControlSpace ControlSpace { get; set; }

        public ControlChannel ControlChannel { get; set; }

        public void RaiseQualifiedNameChanged()
        {
            RaisePropertyChanged("QualifiedName");
        }

        public virtual bool Validate()
        {
            return true;
        }

        public virtual bool IsDirty { get { return oldStartPoint != StartPoint; } }

    }
}
