using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LS_Designer_WPF.Model
{
    public class ArtNetControlDevice : ControlDevice
    {
        public ArtNetControlDevice():base()
        {
            //    //Name = "";
            //    //CanDimming = true;
            //    //Model = "";
            //    //IPAddress.TryParse("2.0.0.2", out _ipAddress);
            //    //IPAddress.TryParse("2.0.0.3", out _virtualIP);
            //ControlChannels = new List<ControlChannel>();
        }


        IPAddress _ipAddress; 
        public IPAddress IPAddress
        {
            get { return _ipAddress; }
            set { Set(ref _ipAddress, value); }
        }

        IPAddress _virtualIP;
        public IPAddress VirtualIP
        {
            get { return _virtualIP; }
            set { Set(ref _virtualIP, value); }
        }

        public int IPChCount { get; set; }

        public int VIPChCount { get; set; }

        //public virtual List<ArtNetControlChannel> ControlChannels { get; set; }
    }
}
