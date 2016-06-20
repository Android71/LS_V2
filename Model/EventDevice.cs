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

        public int Mode { get; set; }

        public virtual string Profile { get; set; }

        public string Remark { get; set; }

        public bool MultiChannel { get; set; }

        public bool CanAddChannel { get; set; }

        public string DotNetType { get; set; }

        public ControlSpace ControlSpace { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public Collection<EventChannel> EventChannels { get; set; }

        // UI related
        int ChCount { get; set; }
        List<string> ModeList { get; set; }
        int SelectedMode { get; set; }
    }
}
