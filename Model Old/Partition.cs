using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS_Designer_WPF.Model
{
    public class Partition : ObservableObject
    {
        public Partition()
        {
            Name = "";
        }

        public int Id { get; set; }

        string _name = "";
        public string Name { get { return _name; } set { Set<string>(ref _name, value); } }
    }

    
}
