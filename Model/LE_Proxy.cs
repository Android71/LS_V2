using System;
using System.Collections.Generic;
using System.Text;

namespace LS_Designer_WPF.Model
{
    public class LE_Proxy
    {
        public int Id { get; set; }

        public int Ix { get; set; }

        public LightZone LightZone { get; set; }

        public LightElement LightElement { get; set; }

        // UI Related

        public string QualifiedName
        {
            get { return string.Format($"[{LightElement.ControlChannel.Name}] {LightElement.Name}"); }
            //set;
        }
    }
}
