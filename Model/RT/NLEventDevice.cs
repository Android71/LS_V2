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
            Modes = new List<string>();
        }
        public override string Profile
        {
            get { return CreateProfile(); }
            set { ParseProfile(value); }
        }

        List<XElement> modes;
        List<XElement> channels;
        string currentModeS;
        int currentModeInt;

        public List<String> Modes { get; set; }

        public string CurrentMode { get; set; }

        public int ChannelCount { get; set; }


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
            currentModeS = root.Attribute("CurrentMode").Value;
            currentModeInt = int.Parse(currentModeS);
            modes = new List<XElement>(root.Elements());
            int i = 1;
            foreach (XElement xel in modes)
            {
                Modes.Add(String.Format("Mode_{0}", i));
                i++;
            }
            CurrentMode = Modes[currentModeInt];
            channels = new List<XElement>(modes.Find(p => p.Attribute("Value").Value == currentModeS).Elements());
            //Mode = int.Parse(modes.Find(p => p.Attribute("Value").Value == currentMode).Attribute("Value").Value);
            Mode = int.Parse(currentModeS);
            ChannelCount = channels.Count;

            foreach (XElement ch in channels)
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

        // UI Staff

        public int Channel_1
        {
            get
            {
                XElement xel = channels[0];
                return int.Parse(xel.Attribute("ChannelNo").Value);
            }
            set
            {
                channels[0].Attribute("ChannelNo").Value = value.ToString();
            }
        }

        public int Channel_2
        {
            get
            {
                XElement xel = channels[1];
                return int.Parse(xel.Attribute("ChannelNo").Value);
            }
            set
            {
                channels[1].Attribute("ChannelNo").Value = value.ToString();
            }
        }

        public int Channel_3
        {
            get
            {
                if (channels.Count > 2)
                    return int.Parse(channels[2].Attribute("ChannelNo").Value);
                else
                    return 0;
            }
            set
            {
                if (channels.Count > 2)
                    channels[2].Attribute("ChannelNo").Value = value.ToString();
            }
        }

        public int Channel_4
        {
            get
            {
                if (channels.Count > 3)
                    return int.Parse(channels[3].Attribute("ChannelNo").Value);
                else
                    return 0;
            }
            set
            {
                if (channels.Count >3)
                    channels[3].Attribute("ChannelNo").Value = value.ToString();
            }
        }
    }

}
