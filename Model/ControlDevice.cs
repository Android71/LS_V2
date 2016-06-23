using GalaSoft.MvvmLight;
using LS_Designer_WPF.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace LS_Designer_WPF.Model
{
    public class ControlDevice : ObservableObject
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Model { get; set; }

        public bool HaveDimmer { get; set; }

        public virtual string Profile { get; set; }

        public string Remark { get; set; }

        public bool MultiChannel { get; set; }

        public bool CanAddChannel { get; set; }

        public string DotNetType { get; set; }

        public ControlSpace ControlSpace { get; set; }

        Partition _partition;
        public Partition Partition //{ get; set; }
        {
            get { return _partition; }
            set { Set(ref _partition, value); }
        }

        public ObservableCollection<ControlChannel> ControlChannels {get; set;}

        /*********************************************************************/
        //UI related
        /*********************************************************************/

        List<Partition> _partitions;
        public List<Partition> Partitions //{ get; set; }
        {
            get { return _partitions; }
            set { Set(ref _partitions, value); }
        }

        bool _isEditMode = false;
        public bool IsEditMode
        {
            get { return _isEditMode; }
            set { Set(ref _isEditMode, value); }
        }
    }
}
