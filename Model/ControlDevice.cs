using LS_Designer_WPF.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace LS_Designer_WPF.Model
{
    public class ControlDevice
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

        public ObservableCollection<ControlChannel> ControlChannels {get; set;}
    }
}
