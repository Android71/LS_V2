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

        public string Profile { get; set; }

        public string DotNetType { get; set; }
    }
}
