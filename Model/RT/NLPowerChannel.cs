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
            XElement xdata = XElement.Parse(profile);
            HaveDimmer = bool.Parse(xdata.Attribute("HaveDimmer").Value);
            
        }

        string CreateProfile()
        {
            XElement profile =
                new XElement("Params", new XAttribute("HaveDimmer", HaveDimmer.ToString()));
            return profile.ToString();
        }
    }
}
