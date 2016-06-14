using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Text;
using System.Xml.Linq;

namespace Model
{
    public class AN6USPI : ControlDevice
    {
        public AN6USPI()
        {
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

        public IPAddress IPAddress { get; set; }

        public IPAddress VirtualIP { get; set; }

        string CreateProfile()
        {
            return "";
        }

        //<Params>
        //  <IPAddress Value = "2.0.0.2" ChCount="4"/>
        //  <VirtualIP Value = "2.0.0.3" ChCount="2"/>
        //</Params>

        void ParseProfile(string profile)
        {
            IPAddress ip;
            AN6UControlChannel cc;
            XElement xdata = XElement.Parse(profile);
            int baseChCount;
            int additionalChCount;

            foreach (XElement xel in xdata.Elements())
            {
                if (xel.Name == "IPAddress")
                {
                    IPAddress.TryParse(xel.Attribute("Value").Value, out ip);
                    IPAddress = ip;
                    baseChCount = int.Parse(xel.Attribute("ChCount").Value);
                    continue;
                }

                if (xel.Name == "VirtualIP")
                {
                    IPAddress.TryParse(xel.Attribute("Value").Value, out ip);
                    VirtualIP = ip;
                    additionalChCount = int.Parse(xel.Attribute("ChCount").Value);
                }
            }
            for (int i = 0; i < 4; i++)
            {
                cc = new AN6UControlChannel();
                //cc.Profile = "<Params IP = \"2.0.0.2\" ChNum = \"2\" Port = \"0\"/>";
                cc.IPAddress = IPAddress;
                string s = cc.Profile;
                cc.PortNo = i;
                ControlChannels.Add(cc);
            }
            for (int i = 4; i < 6; i++)
            {
                cc = new AN6UControlChannel();
                cc.IPAddress = VirtualIP;
                cc.PortNo = i;
                ControlChannels.Add(cc);
            }
        }
    }
}
