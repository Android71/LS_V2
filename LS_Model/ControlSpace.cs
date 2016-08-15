using GalaSoft.MvvmLight;
//using LS_Designer_WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LS_Designer_WPF.Model
{
    public partial class ControlSpace : ObservableObject
    {
        public ControlSpace() { }

        public int Id { get; set; }

        string _name = "";
        public string Name { get { return _name; } set { Set<string>(ref _name, value); } }

        bool _isActive;
        public bool IsActive { get { return _isActive; } set { Set(ref _isActive, value); } }

        public string Prefix { get; set; }



        //public static ArtNetControlDevice CreateArtNetControlDevice(IDataService dataService, string model)
        //{
        //    //int ipChCount = 0;
        //    //int vipChCount = 0;
        //    IPAddress ipAddr;
        //    EnvironmentItem ei = null;

        //    ArtNetControlDevice an = new ArtNetControlDevice();
        //    an.Name = "ArtNet";
        //    an.CanDimming = true;

        //    dataService.GetEnvironmentItem(model, (data, error) =>
        //    {
        //        if (error != null) { return; } // Report error here
        //        ei = data;
        //    });
        //    an.Model = ei.Model;

        //    XElement xdata;
        //    xdata = XElement.Parse(ei.Profile);
        //    var x = xdata.Element("IPAddress").Attribute("Value").Value;
        //    var y = xdata.Element("VirtualIP").Attribute("Value").Value;
        //    IPAddress.TryParse(x, out ipAddr);
        //    an.IPAddress = ipAddr;
        //    IPAddress.TryParse(y, out ipAddr);
        //    an.VirtualIP = ipAddr;
        //    an.IPChCount = int.Parse(xdata.Element("IPAddress").Attribute("ChCount").Value);
        //    an.VIPChCount = int.Parse(xdata.Element("VirtualIP").Attribute("ChCount").Value);

        //    for (int i = 0; i < an.IPChCount; i++)
        //    {
        //        an.ControlChannels.Add(new ArtNetControlChannel()
        //        {
        //            //Name = an.Name + "/Universe",
        //            Name = an.Name,
        //            IPAddress = an.IPAddress,
        //            ChannelNo = i,
        //            PortNo = i
        //        });
        //    }
        //    for (int i = 0; i < an.VIPChCount; i++)
        //    {
        //        an.ControlChannels.Add(new ArtNetControlChannel()
        //        {
        //            //Name = an.Name + "/Universe",
        //            Name = an.Name,
        //            IPAddress = an.VirtualIP,
        //            ChannelNo = i + an.IPChCount,
        //            PortNo = i + an.IPChCount
        //        });
        //    }
        //    //an.Profile = 
        //    return an;
        //}

        //public static GenericControlDevice CreateGenericControlDevice(IDataService dataService, string model)
        //{
        //    EnvironmentItem ei = null;
        //    dataService.GetEnvironmentItem(model, (data, error) =>
        //    {
        //        if (error != null) { return; } // Report error here
        //        ei = data;
        //    });

        //    GenericControlDevice gcd = new GenericControlDevice();
        //    gcd.Model = model;
        //    gcd.Name = "Generic";
        //    gcd.ChannelNo = 1;
        //    XElement xdata;
        //    xdata = XElement.Parse(ei.Profile);
        //    gcd.CanDimming = Boolean.Parse(xdata.Attribute("HaveDimmer").Value);
        //    gcd.ControlChannels.Add(new ControlChannel() { Name = gcd.Name, ChannelNo = 1 });
        //    return gcd;
        //}

        //public static EventDevice CreateEventDevice(IDataService dataService, string model)
        //{
        //    EnvironmentItem ei = null;
        //    EventDevice eventDevice = new EventDevice();

        //    dataService.GetEnvironmentItem(model, (data, error) =>
        //    {
        //        if (error != null) { return; } // Report error here
        //        ei = data;
        //    });
        //    eventDevice.Mode = 1;
        //    eventDevice.OldMode = 1;
        //    eventDevice.Model = ei.Model;
        //    eventDevice.Profile = ei.Profile;

        //    eventDevice.ControlSpace = AppContext.ControlSpace;
        //    eventDevice.Partition = AppContext.Partition;
        //    return eventDevice;
        //}

        //public static LightElement CreateLightElement(PointTypeEnum elementType)
        //{
        //    LightElement lightElement = null;
        //    //PointTypeEnum elType = elementType;

        //    if (AppContext.ControlSpace.Name == "ArtNet_DMX")
        //        lightElement = new LightStrip();
        //    else
        //        lightElement = new LightElement();

        //    if (elementType == PointTypeEnum.DRGB)
        //    {
        //        (lightElement as LightStrip).ColorSequenceList = LightElement.rgbCsList;
        //    }

        //    lightElement.PointType = elementType;
        //    lightElement.ControlSpace = AppContext.ControlSpace;
        //    lightElement.Partition = AppContext.Partition;
        //    return lightElement;
        //}

        //public static LightZone CreateLightZone()
        //{
        //    LightZone lightZone = new LightZone();
        //    lightZone.LE_Proxies = new List<LE_Proxy>();
        //    lightZone.Partition = AppContext.Partition;
        //    lightZone.ControlSpace = AppContext.ControlSpace;
        //    return lightZone;
        //}
    }
}
