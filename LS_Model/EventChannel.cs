using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Text;

namespace LS_Designer_WPF.Model
{
    public class EventChannel : ObservableObject
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ChannelNo { get; set; }

        public string EventName { get; set; }

        public string Profile { get; set; } = "";

        public ControlSpace ControlSpace { get; set; }

        Partition _partition;
        public Partition Partition
        {
            get { return _partition; }
            set { Set(ref _partition, value); }
        }

        public EventDevice EventDevice { get; set; }

        public String EventDeviceName { get; set; }

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
