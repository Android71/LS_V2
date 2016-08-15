using LS_Designer_WPF.Model;
using LS_Library;
using System;
using System.Collections.Generic;
using System.Text;

namespace LS_Designer_WPF.Model
{
    public partial class LE_Type
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public PointTypeEnum PointType { get; set; }

        public bool? CanUseGamma { get; set; }
        //public bool IsActive { get; set; }
        //public string Remark { get; set; }
    }
}
