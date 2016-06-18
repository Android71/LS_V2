using LS_Designer_WPF.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Xml.Linq;

namespace LS_Designer_WPF.Model
{
    public class NLPowerBlock : ControlDevice
    {
        public NLPowerBlock()
        {
            Name = "NL_PB";
            MultiChannel = false;
            CanAddChannel = false;
            DotNetType = typeof(NLPowerBlock).AssemblyQualifiedName;
            ControlChannels = new ObservableCollection<ControlChannel>();
        }

        public int ChannelNo
        {
            get { return ControlChannels[0].ChannelNo; }
            set { ControlChannels[0].ChannelNo = value; }
        }

        public override string Profile
        {
            get { return CreateProfile(); }
            set { ParseProfile(value); }
        }

        PointTypeEnum PointType { get; set; }

        //<Params Model="SU111-300" HaveDimmer="True"/>
        string CreateProfile()
        {
            XElement profile =
                new XElement("Params",
                    new XAttribute("Model", Model), new XAttribute("HaveDimmer", HaveDimmer.ToString()), new XAttribute("PointType", PointType.ToString())
                    );
            string s = profile.ToString();
            return s;
        }

        void ParseProfile(string profile)
        {
            PointTypeEnum pt = PointTypeEnum.W;
            XElement xdata = XElement.Parse(profile);
            Model = xdata.Attribute("Model").Value;
            HaveDimmer = bool.Parse(xdata.Attribute("HaveDimmer").Value);
            if (xdata.Attribute("PoinType") != null)
            {
                Enum.TryParse(xdata.Attribute("PoinType").Value, out pt);
                PointType = pt;
            }
            if (Id == 0)
                ControlChannels.Add(new NLPowerChannel() { HaveDimmer = HaveDimmer, PointType = PointType });
        }
    }
}
