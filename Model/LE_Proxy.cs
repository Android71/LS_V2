﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS_Designer_WPF.Model
{
    public class LE_Proxy
    {
        public int Id { get; set; }
        public int Ix { get; set; }

        public LightZone LightZone { get; set; }
        public LightElement LightElement { get; set; }
    }
}
