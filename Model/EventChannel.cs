using System;
using System.Collections.Generic;
using System.Text;

namespace LS_Designer_WPF.Model
{
    public class EventChannel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ChannelNo { get; set; }

        public string EventName { get; set; }

        public string Profile { get; set; } = "";

        public ControlSpace ControlSpace { get; set; }

        public Partition Partition { get; set; }
        
        public EventDevice EventDevice { get; set; }
    }
}
