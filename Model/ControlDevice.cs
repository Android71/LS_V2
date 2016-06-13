using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class ControlDevice
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Model { get; set; }

        public bool HaveDimmer { get; set; }

        public string Profile { get; set; }

        public string Remark { get; set; }

        public bool MultiChannel { get; set; }

        public bool CanAddChannel { get; set; }

        //public virtual ControlSpace ControlSpace { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<ControlChannel> ControlChannels { get; set; }
    }
}
