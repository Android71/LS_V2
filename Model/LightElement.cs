using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Text;

namespace LS_Designer_WPF.Model
{
    public class LightElement : ObservableObject
    {
        public LightElement() { }

        public LightElement(PointTypeEnum pointType, ControlSpace space) : this()
        {

        }

        public int Id { get; set; }

        public string Name { get; set; }

        public PointTypeEnum PointType { get; set; }

        public int StartPoint { get; set; }

        public int PointCount { get; set; }

        public Direction Direction { get; set; }

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

        bool _isEditMode = false;
        public bool IsEditMode
        {
            get { return _isEditMode; }
            set { Set(ref _isEditMode, value); }
        }
    }
}
