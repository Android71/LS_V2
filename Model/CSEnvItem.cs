using LS_Designer_WPF.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class CSEnvItem
    {
        public int Id { get; set; }

        public virtual EnvironmentItem EnvironmentItem { get; set; }
        public virtual ControlSpace ControlSpace { get; set; }
    }
}
