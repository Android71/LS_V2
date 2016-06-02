using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LS_Designer_WPF.Model
{
    public class ControlDevice : ObservableObject
    {

        public ControlDevice()
        {
            ControlChannels = new List<ControlChannel>();
        }

        public int Id { get; set; }
        string _name = "";
        public string Name { get { return _name; } set { Set(ref _name, value); } }

        bool _canDimming = false;
        public bool CanDimming { get { return _canDimming; } set { Set(ref _canDimming, value); } }

        string _model;
        public string Model { get { return _model; } set { Set(ref _model, value); } }


        public ControlSpace ControlSpace { get; set; }

        public List<ControlChannel> ControlChannels { get; set; }

        public string Profile { get; set; }



        //string _profile = "";
        //public string Profile { get; set; }
        //{
        //    get { return _profile; }
        //    set { _profile = value; }
        //}
    }
}
