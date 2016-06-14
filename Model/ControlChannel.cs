using LS_Designer_WPF.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class ControlChannel
    {
        public int Id { get; set; }

        public virtual string Name { get; set; }

        public int ChannelNo { get; set; }

        public PointTypeEnum PointType { get; set; }

        public bool HaveDimmer { get; set; }

        public string DotNetType { get; set; }

        public virtual string Profile { get; set; }

        public ControlSpace ControlSpace { get; set; }

    }
}
