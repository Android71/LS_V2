using LS_Designer_WPF.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace LS_Designer_WPF.Model
{ 

    public class NLPowerChannel : ControlChannel
    {
        public NLPowerChannel()
        {
            DotNetType = typeof(NLPowerChannel).AssemblyQualifiedName;
        }

        public override string Name
        {
            get { return String.Format("NL_PowerChannel_{0}", ChannelNo); }
        }

        public override string Profile
        {
            get { return CreateProfile(); }
            set { ParseProfile(value); }
        }

        void ParseProfile(string profile)
        {
            PointTypeEnum pt = PointTypeEnum.W;
            XElement xdata = XElement.Parse(profile);
            HaveDimmer = bool.Parse(xdata.Attribute("HaveDimmer").Value);
            Enum.TryParse(xdata.Attribute("PointType").Value, out pt);
            PointType = pt;
        }

        string CreateProfile()
        {
            XElement profile =
                new XElement("Params", 
                            new XAttribute("HaveDimmer", HaveDimmer.ToString()),
                            new XAttribute("PointType", PointType.ToString())
                            );
            return profile.ToString();
        }
    }
}
