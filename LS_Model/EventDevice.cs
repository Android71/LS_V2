using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace LS_Designer_WPF.Model
{
    public class EventDevice : ObservableObject
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Model { get; set; }

        public virtual int Mode { get; set; } = 0;

        public virtual string Profile { get; set; }

        public string Remark { get; set; }

        public bool MultiChannel { get; set; }

        public bool CanAddChannel { get; set; }

        public string DotNetType { get; set; }

        public ControlSpace ControlSpace { get; set; }

        Partition _partition;
        public Partition Partition 
        {
            get { return _partition; }
            set { Set(ref _partition, value); }
        }

        public ObservableCollection<EventChannel> EventChannels { get; set; }

        /*********************************************************************/
        //UI related
        /*********************************************************************/

        public int OldMode
        { get; set; }

        protected List<int> channelNumbers;

        public int ChCount { get; set; }

        public List<string> ModeList { get; set; }

        string _selectedModeListItem;
        public string SelectedModeListItem
        {
            get { return _selectedModeListItem; }
            set
            {
                //_selectedModeListItem = value;
                Set(ref _selectedModeListItem, value);
                //Mode = ModeList.IndexOf(value);
            }
        }

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

        bool _isAddMode = false;
        public bool IsAddMode 
        {
            get { return _isAddMode; }
            set { Set(ref _isAddMode, value); }
        }
    }
}
