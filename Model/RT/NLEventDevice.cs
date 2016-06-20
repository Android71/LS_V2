using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Xml.Linq;

namespace LS_Designer_WPF.Model
{
    public class NLEventDevice : EventDevice
    {
        public NLEventDevice()
        {
            EventChannels = new ObservableCollection<EventChannel>();
            MultiChannel = true;
            CanAddChannel = false;
            Name = "NL_EventDev";
        }
        public override string Profile
        {
            get { return CreateProfile(); }
            set { ParseProfile(value); }
        }

        List<XElement> modes;
        List<XElement> channels;
        string currentMode;

        string CreateProfile()
        {
            //XElement profile =
            //    new XElement("Params",
            //        new XElement("IPAddress", new XAttribute("Value", IPAddress.ToString())),
            //        new XElement("VirtualIP", new XAttribute("Value", VirtualIP.ToString()))
            //        );
            //string s = profile.ToString();
            return "";
        }

        void ParseProfile(string profile)
        {
            XElement root = XElement.Parse(profile);
            EventChannel ech;
            currentMode = root.Attribute("CurrentMode").Value;
            modes = new List<XElement>(root.Elements());
            channels = new List<XElement>(modes.Find(p => p.Attribute("Value").Value == currentMode).Elements());
            //Mode = int.Parse(modes.Find(p => p.Attribute("Value").Value == currentMode).Attribute("Value").Value);
            Mode = int.Parse(currentMode);
            foreach(XElement ch in channels)
            {
                foreach(XElement evnt in ch.Elements())
                {
                    ech = new EventChannel();
                    ech.ChannelNo = int.Parse(ch.Attribute("ChannelNo").Value);
                    ech.Name = evnt.Attribute("Name").Value;
                    EventChannels.Add(ech);
                    //EventChannels.Add(new EventChannel()
                    //{ ChannelNo = int.Parse(ch.Attribute("ChannelNo").Value),
                    // Name = evnt.Attribute("Name").Value}
                    //);
                }
            }

            if (Id == 0)
            {
                //for (int i = 0; i < 4; i++)
                //{
                //    cc = new AN6UControlChannel();
                //    //cc.Profile = "<Params IP = \"2.0.0.2\" ChNum = \"2\" Port = \"0\"/>";
                //    cc.IPAddress = IPAddress;
                //    string s = cc.Profile;
                //    cc.PortNo = i;
                //    cc.ChannelNo = i;
                //    ControlChannels.Add(cc);
                //}
                //for (int i = 4; i < 6; i++)
                //{
                //    cc = new AN6UControlChannel();
                //    cc.IPAddress = VirtualIP;
                //    cc.PortNo = i;
                //    cc.ChannelNo = i;
                //    ControlChannels.Add(cc);
                //}
            }
        }

    }

}
