using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Xml.Linq;
using System.Linq;

namespace LS_Designer_WPF.Model
{
    public class NLEventDevice : EventDevice
    {
        public NLEventDevice()
        {
            EventChannels = new ObservableCollection<EventChannel>();
            MultiChannel = true;
            CanAddChannel = false;
            Name = "NL_EDev";
            ModeList = new List<string>();
            DotNetType = typeof(NLEventDevice).AssemblyQualifiedName;
        }

        public override string Profile
        {
            get { return CreateProfile(); }
            set { ParseProfile(value); }
        }

        string _profile;
        string CreateProfile()
        {
            //XElement root = XElement.Parse(_profile);
            //IEnumerable<XElement> channels = root.Elements("Mode")
            //                                 .Where(p => p.Attribute("Value").Value == Mode.ToString()).Elements();
            //int i = 0;
            //foreach (XElement ch in channels)
            //{
            //    ch.Attribute("ChannelNo").Value = channelNumbers[i].ToString();
            //    i++;
            //}

            //    //XElement profile =
            //    //    new XElement("Params",
            //    //        new XElement("IPAddress", new XAttribute("Value", IPAddress.ToString())),
            //    //        new XElement("VirtualIP", new XAttribute("Value", VirtualIP.ToString()))
            //    //        );
            //    //string s = profile.ToString();
             return _profile;
        }
        
        void ParseProfile(string profile)
        {
            EventChannel ech;
            channelNumbers = new List<int>();

            _profile = profile;
            //OldMode = Mode;
            XElement root = XElement.Parse(profile);

            IEnumerable<XElement> channels = root.Elements("Mode")
                                             .Where(p => p.Attribute("Value").Value == Mode.ToString()).Elements();

            ChCount = 0;
            foreach (XElement ch in channels)
            {
                channelNumbers.Add(int.Parse(ch.Attribute("ChannelNo").Value));
                ChCount++;
            }

            int modeCount = root.Elements().Count();
            for (int i = 0; i < root.Elements().Count(); i++)
            {
                ModeList.Add(String.Format("Mode_{0}", i + 1));
            }

            //SelectedModeListItem = ModeList[Mode];

            if (Id == 0)
            {
                foreach (XElement ch in channels)
                {
                    foreach (XElement evnt in ch.Elements())
                    {
                        ech = new EventChannel();
                        ech.ChannelNo = int.Parse(ch.Attribute("ChannelNo").Value);
                        ech.EventName = evnt.Attribute("Name").Value;
                        //ech.Name = string.Format($"[{ech.ChannelNo}] {ech.EventName}");
                        EventChannels.Add(ech);
                    }
                }
            }
        }

        int _mode = 0;
        public override int Mode
        {
            get
            {
                return _mode;
            }

            set
            {
                //SelectedModeListItem = ModeList[Mode];
                Set(ref _mode, value); 
                if (Mode != OldMode)
                {
                    //EventChannel ech;

                    XElement root = XElement.Parse(_profile);
                    //ObservableCollection<EventChannel> eventChannels = new ObservableCollection<EventChannel>();
                    IEnumerable<XElement> xmlChannels = root.Elements("Mode")
                                             .Where(p => p.Attribute("Value").Value == value.ToString()).Elements();

                    //int i = 0;
                    //foreach (XElement ch in channels)
                    //{
                    //    ch.Attribute("ChannelNo").Value = channelNumbers[i].ToString();
                    //    foreach (XElement evnt in ch.Elements())
                    //    {
                    //        ech = new EventChannel();
                    //        ech.ChannelNo = int.Parse(ch.Attribute("ChannelNo").Value);
                    //        ech.EventName = evnt.Attribute("Name").Value;
                    //        //ech.Name = string.Format($"[{ech.ChannelNo}] {ech.EventName}");
                    //        eventChannels.Add(ech);
                    //    }
                    //    i++;
                    //}

                    EventChannels = CreateChannels(xmlChannels);
                    if (Id != 0)
                        foreach(EventChannel ech in EventChannels)
                            ech.Name = string.Format($"[{ech.ChannelNo}] {ech.EventName}");
                }
                base.Mode = value;
            }
        }

        ObservableCollection<EventChannel> CreateChannels(IEnumerable<XElement> channels)
        {
            EventChannel ech;
            ObservableCollection<EventChannel> eventChannels = new ObservableCollection<EventChannel>();
            int i = 0;
            foreach (XElement ch in channels)
            {
                ch.Attribute("ChannelNo").Value = channelNumbers[i].ToString();
                foreach (XElement evnt in ch.Elements())
                {
                    ech = new EventChannel();
                    ech.ChannelNo = int.Parse(ch.Attribute("ChannelNo").Value);
                    ech.EventName = evnt.Attribute("Name").Value;
                    //ech.Name = string.Format($"[{ech.ChannelNo}] {ech.EventName}");
                    eventChannels.Add(ech);
                }
                i++;
            }
            return eventChannels;
        }

        void UpdateEventChannels(ObservableCollection<EventChannel> eventChannels)
        {
            for (int i = 0; i < EventChannels.Count; i++)
            {
                EventChannels[i].ChannelNo = eventChannels[i].ChannelNo;
            }
        }

        // UI Staff

        void UpdateProfile(int ix)
        {
            List<XElement> chList;
            ObservableCollection<EventChannel> eventChannels;
            XElement root = XElement.Parse(_profile);
            foreach(XElement mode in root.Elements())
            {
                chList = mode.Elements().ToList();
                chList[ix].Attribute("ChannelNo").Value = channelNumbers[ix].ToString();
            }
            _profile = root.ToString();
            root = XElement.Parse(_profile);
            IEnumerable<XElement> channels = root.Elements("Mode")
                                             .Where(p => p.Attribute("Value").Value == Mode.ToString()).Elements();
            eventChannels = CreateChannels(channels);
            UpdateEventChannels(eventChannels);
        }

        public int Channel_1
        {
            get
            {
                return channelNumbers[0];
            }
            set
            {
                channelNumbers[0] = value;
                UpdateProfile(0);
            }
        }

        public int Channel_2
        {
            get
            {
                if (channelNumbers.Count > 1)
                    return channelNumbers[1];
                else
                    return 0;
            }
            set
            {
                if (channelNumbers.Count > 1)
                {
                    channelNumbers[1] = value;
                    UpdateProfile(1);
                }
            }
        }

        public int Channel_3
        {
            get
            {
                if (channelNumbers.Count > 2)
                    return channelNumbers[2];
                else
                    return 0;
            }
            set
            {
                if (channelNumbers.Count > 2)
                {
                    channelNumbers[2] = value;
                    UpdateProfile(2);
                }
            }
        }

        public int Channel_4
        {
            get
            {
                if (channelNumbers.Count > 3)
                    return channelNumbers[3];
                else
                    return 0;
            }
            set
            {
                if (channelNumbers.Count > 3)
                {
                    channelNumbers[3] = value;
                    UpdateProfile(3);
                }
            }
        }
    }

}
