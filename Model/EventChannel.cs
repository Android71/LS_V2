using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS_Designer_WPF.Model
{
    public class EventChannel : ObservableObject
    {
        public int Id { get; set; }

        string _name;
        public string Name { get { return _name; } set { Set(ref _name, value); } }

        string _eventName;
        public string EventName { get; set; }

        int _channelNo;
        public int ChannelNo { get { return _channelNo; } set { Set(ref _channelNo, value); } }

        public EventDevice EventDevice { get; set; }

        public Event Event { get; set; }

        bool _isLinked = false;
        public bool IsLinked { get { return _isLinked; } set { Set(ref _isLinked, value); } }

        bool _isLinkedToSelected = false;
        public bool IsLinkedToSelected { get { return _isLinkedToSelected; } set { Set(ref _isLinkedToSelected, value); } }
    }
}
