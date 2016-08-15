using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Text;
using System.Xml.Linq;

namespace LS_Designer_WPF.Model
{
    public class AN6USPI : ControlDevice
    {
        public AN6USPI()
        {
            Name = "AN_Dev";
            HaveDimmer = true;
            Model = "AN6USPI";
            MultiChannel = true;
            CanAddChannel = false;
            DotNetType = typeof(AN6USPI).AssemblyQualifiedName;
            ControlChannels = new ObservableCollection<ControlChannel>();
        }

        public override string Profile
        {
            get { return CreateProfile(); }
            set { ParseProfile(value); }
        }

        IPAddress _ipAddress;
        public IPAddress IPAddress
        {
            get { return _ipAddress; }
            set
            {
                _ipAddress = value;
                for (int i = 0; i < 4; i++)
                {
                    (ControlChannels[i] as AN6UControlChannel).IPAddress = value;
                }
            }
        }

        IPAddress _virtualIP;
        public IPAddress VirtualIP
        {
            get { return _virtualIP; }
            set
            {
                _virtualIP = value;
                for (int i = 4; i < 6; i++)
                {
                    (ControlChannels[i] as AN6UControlChannel).IPAddress = value;
                }
            }
        }

        //<Params>
        //  <IPAddress Value = "2.0.0.2" ChCount="4"/>
        //  <VirtualIP Value = "2.0.0.3" ChCount="2"/>
        //</Params>

        string CreateProfile()
        {
            XElement profile =
                new XElement("Params",
                    new XElement("IPAddress", new XAttribute("Value", IPAddress.ToString())),
                    new XElement("VirtualIP", new XAttribute("Value", VirtualIP.ToString()))
                    );
            string s = profile.ToString();
            return s; 
        }

        

        void ParseProfile(string profile)
        {
            IPAddress ip;
            AN6UControlChannel cc;
            XElement xdata = XElement.Parse(profile);
            //int baseChCount;
            //int additionalChCount;

            foreach (XElement xel in xdata.Elements())
            {
                if (xel.Name == "IPAddress")
                {
                    IPAddress.TryParse(xel.Attribute("Value").Value, out ip);
                    _ipAddress = ip;
                    //baseChCount = int.Parse(xel.Attribute("ChCount").Value);
                    continue;
                }

                if (xel.Name == "VirtualIP")
                {
                    IPAddress.TryParse(xel.Attribute("Value").Value, out ip);
                    _virtualIP = ip;
                    //additionalChCount = int.Parse(xel.Attribute("ChCount").Value);
                }
            }
            if (Id == 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    cc = new AN6UControlChannel();
                    //cc.Profile = "<Params IP = \"2.0.0.2\" ChNum = \"2\" Port = \"0\"/>";
                    cc.IPAddress = IPAddress;
                    //string s = cc.Profile;
                    cc.PortNo = i;
                    cc.ChannelNo = i;
                    cc.ControlSpace = ControlSpace;
                    cc.Partition = Partition;
                    ControlChannels.Add(cc);
                }
                for (int i = 4; i < 6; i++)
                {
                    cc = new AN6UControlChannel();
                    cc.IPAddress = VirtualIP;
                    cc.PortNo = i;
                    cc.ChannelNo = i;
                    cc.ControlSpace = ControlSpace;
                    cc.Partition = Partition;
                    ControlChannels.Add(cc);
                }
            }
        }
    }
}
