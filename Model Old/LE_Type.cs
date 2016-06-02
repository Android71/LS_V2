using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS_Designer_WPF.Model
{
    public class LE_Type
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Remark { get; set; }

        public PointTypeEnum PointType { get; set; }

        public ControlSpace ControlSpace { get; set; }
    }
}
