using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LS_Designer_WPF.Model
{
    public class EventDevice : ObservableObject
    {
        public EventDevice()
        {
            ModeList = new List<DeviceMode>();
            EventChannels = new List<EventChannel>();
            NewEventChannels = new List<EventChannel>();
            Name = "";
            Mode = 1;
        }

        public int Id { get; set; }

        public bool IsNew
        {
           get { return Id == 0; }
        }

        string _name;
        public string Name { get { return _name; } set { Set(ref _name, value); } }

        public int OldMode { get; set; }

        int _mode;
        public int Mode { get { return _mode; } set { Set(ref _mode, value); } }

        string _model;
        public string Model { get { return _model; } set { Set(ref _model, value); } }

        bool IsModeChanged
        { get { return OldMode != SelectedMode.ModeNo; } }

        public int ChCount { get; set; }

        string _profile;
        public string Profile
        { get { return _profile; }
            set
            {
                if (value != _profile)
                {
                    _profile = value;
                    BuildConfigHelper();
                }
            }
        }

        public ControlSpace ControlSpace { get; set; }

        public Partition Partition { get; set; }

        public List<DeviceMode> ModeList { get; set; }

        DeviceMode _selectedMode = null;
        public DeviceMode SelectedMode
        {
            get { return _selectedMode; }
            set
            {
                UpdateEventSources();
                Set(ref _selectedMode, value);
            }

        }

        public List<EventChannel> EventChannels { get; set; }

        public List<EventChannel> NewEventChannels { get; set; }

        public void PrepareToSave()
        {
            ReEnumChannels();
            RebuilProfile();
            UpdateEventChannels();
        }

        void ReEnumChannels()
        {
            //// Update profile
            //foreach (DeviceMode devMode in ModeList)
            //{
            //    if (devMode != SelectedMode)
            //    {
            //        for (int i = 0; i < SelectedMode.EventSources.Count; i++)
            //            devMode.EventSources[i].ChannelNo = SelectedMode.EventSources[i].ChannelNo;
            //    }
            //}
            // Update EventChannels
            //if (EventChannels.Count != 0)
            //{
            //    EventSource es = null;

            //    foreach(EventChannel eCh in EventChannels)
            //    {
            //        es = SelectedMode.EventSources.FirstOrDefault(p => p.OldChannelNo == eCh.ChannelNo);
            //        if (es.IsDirty)
            //            eCh.ChannelNo = es.ChannelNo;
            //        //forea
            //        //eventChannel.Name = Name + " | " + es. + " | " + es.ChannelNo.ToString();
            //    }
            //}
        }

        void UpdateEventSources()
        {
            if (SelectedMode != null)
            {
                for (int i = 0; i < ModeList.Count; i++)
                {
                    if (ModeList[i] != SelectedMode)
                        for (int j = 0; j < ChCount; j++)
                            ModeList[i].EventSources[j].ChannelNo = SelectedMode.EventSources[j].ChannelNo;
                }
            }
        }

        void RebuilProfile()
        {
            XElement root = XElement.Parse(Profile);
            foreach (EventSource evntSource in ModeList[Mode - 1].EventSources)
            {
                foreach (XElement mode in root.Elements())
                {
                    foreach (XElement esXML in mode.Elements())
                    {
                        if (int.Parse(esXML.Attribute("ChannelNo").Value) == evntSource.OldChannelNo)
                            esXML.Attribute("ChannelNo").Value = evntSource.ChannelNo.ToString();
                    }
                }
            }
            // чтобы не вызывать set метод
            _profile = root.ToString();
        }

        void UpdateEventChannels()
        {
            EventChannel eventChannel;
            if (IsNew)
            {
                foreach(EventSource evntSource in SelectedMode.EventSources)
                {
                    foreach(string s in evntSource.EventNames)
                    {
                        eventChannel = new EventChannel();
                        eventChannel.ChannelNo = evntSource.ChannelNo;
                        eventChannel.Name = Name + " | " + s + " | " + evntSource.ChannelNo.ToString();
                        eventChannel.EventName = s;
                        EventChannels.Add(eventChannel);
                    }
                }
                return;
            }
            
            if (!IsModeChanged)
            {
                foreach (EventSource evntSource in ModeList[Mode - 1].EventSources)
                {
                    if (evntSource.IsDirty)
                    {
                        var x = EventChannels.Where(p => p.ChannelNo == evntSource.OldChannelNo);
                        foreach (EventChannel eCh in x)
                            eCh.ChannelNo = evntSource.ChannelNo;
                    }
                }

                foreach (EventChannel eCh in EventChannels)
                    eCh.Name = Name + " | " + eCh.EventName + " | " + eCh.ChannelNo.ToString();
            }
            else
            {
                foreach (EventSource evntSource in SelectedMode.EventSources)
                {
                    foreach (string s in evntSource.EventNames)
                    {
                        eventChannel = new EventChannel();
                        eventChannel.ChannelNo = evntSource.ChannelNo;
                        eventChannel.Name = Name + " | " + s + " | " + evntSource.ChannelNo.ToString();
                        eventChannel.EventName = s;
                        NewEventChannels.Add(eventChannel);
                    }
                }
            }
            
        }

        void BuildConfigHelper()
        {
            EventSource eventSource = null;
            DeviceMode deviceMode = null;
            XElement xdata = XElement.Parse(Profile);
            int chNo;
            int modeNo;

            foreach (XElement mode in xdata.Elements())
            {
                deviceMode = new DeviceMode();
                modeNo = int.Parse(mode.Attribute("Value").Value);
                deviceMode.ModeNo = modeNo;
                foreach (XElement esXML in mode.Elements())
                {
                    chNo = int.Parse(esXML.Attribute("ChannelNo").Value);
                    eventSource = new EventSource(modeNo, chNo);
                    foreach (XElement evnt in esXML.Elements())
                    {
                        eventSource.EventNames.Add(evnt.Attribute("Name").Value);
                    }
                    deviceMode.EventSources.Add(eventSource);
                }
                ModeList.Add(deviceMode);
            }
            SelectedMode = ModeList[Mode - 1];
            ChCount = ModeList[0].EventSources.Count;
            Mode = SelectedMode.ModeNo;
        }
    }

    public class DeviceMode : ObservableObject
    {
        public DeviceMode()
        {
            EventSources = new List<EventSource>();
        }

        int _modeNo;
        public int ModeNo { get { return _modeNo; } set { Set(ref _modeNo, value); } }

        public List<EventSource> EventSources { get; set; }
    }

    public class EventSource : ObservableObject
    {
        public EventSource(int mode, int chNo)
        {
            EventChannels = new List<EventChannel>();
            EventNames = new List<string>();
            Mode = mode;
            ChannelNo = chNo;
            OldChannelNo = chNo;
        }

        public int Mode { get; set; }

        public bool IsDirty
        {
           get { return (ChannelNo != OldChannelNo); }
        }

        public int OldChannelNo { get; set; }

        int _channelNo;
        public int ChannelNo
        {
            get { return _channelNo; }
            set { Set(ref _channelNo, value); }
        }

        public string ToolTip
        {
           

            get
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < EventNames.Count; i++)
                {
                    sb.Append(EventNames[i] + " ");
                    if (i != EventNames.Count - 1)
                        sb.Append("| ");
                }
                return sb.ToString();
            }
        }

        public List<EventChannel> EventChannels { get; set; }

        public List<string> EventNames { get; set; }
    }
}
