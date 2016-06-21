using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace LS_Designer_WPF.Model
{
    public class EventDevice
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Model { get; set; }

        public virtual int Mode { get; set; } = 0;

        public int OldMode { get; set; }

        public virtual string Profile { get; set; }

        public string Remark { get; set; }

        public bool MultiChannel { get; set; }

        public bool CanAddChannel { get; set; }

        public string DotNetType { get; set; }

        public ControlSpace ControlSpace { get; set; }

        public Partition Partition { get; set; }

        public List<EventChannel> EventChannels { get; set; }

        // UI related

        protected List<int> channelNumbers;

        public int ChCount { get; set; }

        public List<string> ModeList { get; set; }

        string _selectedModeListItem;
        public string SelectedModeListItem
        {
            get { return _selectedModeListItem; }
            set
            {
                _selectedModeListItem = value;
                Mode = ModeList.IndexOf(value);
            }
        }
    }
}
