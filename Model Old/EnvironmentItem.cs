using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS_Designer_WPF.Model
{
    public class EnvironmentItem
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public DeviceTypeEnum DeviceType { get; set; }

        //string _profile = "";
        public string Profile { get; set; }
        //{
        //    get { return _profile; }
        //    set { _profile = value; }
        //}
        public ControlSpace ControlSpace { get; set; }
    }
}
