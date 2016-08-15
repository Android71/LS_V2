using GalaSoft.MvvmLight;
using LS_Designer_WPF.ViewModel;
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

    }
}
