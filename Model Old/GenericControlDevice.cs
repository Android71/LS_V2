using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS_Designer_WPF.Model
{
    public class GenericControlDevice : ControlDevice
    {
        public GenericControlDevice()
        {
            ControlChannels = new List<ControlChannel>();
        }

        int _channelNo;
        public int ChannelNo
        {
            get
            {
                if (Id == 0)
                    return _channelNo;
                else
                    return ControlChannels[0].ChannelNo;
            }
            set
            {
                Set(ref _channelNo, value);
                if (Id != 0)
                    ControlChannels[0].ChannelNo = value;
            }
        }
    }
}
