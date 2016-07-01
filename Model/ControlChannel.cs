using GalaSoft.MvvmLight;
using LS_Designer_WPF.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace LS_Designer_WPF.Model
{
    public class ControlChannel : ObservableObject
    {
        public int Id { get; set; }

        public virtual string Name { get; set; }

        public int ChannelNo { get; set; }

        public PointTypeEnum PointType { get; set; }

        public bool HaveDimmer { get; set; }

        public string DotNetType { get; set; }

        public virtual string Profile { get; set; }

        public ControlSpace ControlSpace { get; set; }

        Partition _partition;
        public Partition Partition //{ get; set; }
        {
            get { return _partition; }
            set { Set(ref _partition, value); }
        }

        /*********************************************************************/
        //UI related
        /*********************************************************************/

        List<Partition> _partitions;
        public List<Partition> Partitions //{ get; set; }
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
            get { return _hasChildren; }
            set { Set(ref _hasChildren, value); }
        }

        public int LE_Count { get; set; }

        public virtual bool CanLinkLE(LightElement le)
        {
            return true;
        }

    }
}
