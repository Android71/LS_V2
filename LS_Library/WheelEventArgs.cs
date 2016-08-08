using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS_Library
{
    public class WheelEventArgs : EventArgs
    {
        public WheelEventArgs(object newValue) : base()
        {
            NewValue = newValue;
        }

        public object NewValue { get; set; }
    }
}
