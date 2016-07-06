using LS_Designer_WPF.PopUpMessages;
using LS_Designer_WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Xml.Linq;

namespace LS_Designer_WPF.Model
{
    public class AN6UControlChannel : ControlChannel
    {
        public AN6UControlChannel()
        {
            HaveDimmer = true;
            DotNetType = typeof(AN6UControlChannel).AssemblyQualifiedName;
        }

        public IPAddress IPAddress { get; set; }

        public override string Name
        {
            get { return String.Format("Universe_{0}", ChannelNo); }
            set { base.Name = value; }
        }

        public int PortNo { get; set; }

        public override string Profile
        {
            get { return CreateProfile(); }
            set { ParseProfile(value); }
        }

        public byte[] DMXdata = new byte[512];

        //<Params IP = "2.0.0.2" ChNum = "2" Port = "0"/>
        

        void ParseProfile(string profile)
        {
            IPAddress ip;
            XElement xdata = XElement.Parse(profile);
            IPAddress.TryParse(xdata.Attribute("IP").Value, out ip);
            IPAddress = ip;
            ChannelNo = int.Parse(xdata.Attribute("ChNum").Value);
            PortNo = int.Parse(xdata.Attribute("Port").Value);
        }

        string CreateProfile()
        {
            return string.Format($"<Params IP = \"{IPAddress}\"  ChNum = \"{ChannelNo}\"  Port = \"{PortNo}\"/>"); ;
        }

        //UI related

        public override bool CanLinkLE(LightElement le, PopUpMessageVM messageVM)
        {
            if (LE_Count >= 1 && PointType != le.PointType)
            {
                messageVM.Message = AppMessages.UniverseLinkMsg();
                return false;
            }
            else
            {

                return true;
            }
        }
    }
}
