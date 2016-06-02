using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS_Designer_WPF.Model
{
    public class Gamma
    {
        public Gamma()
        {
           
        }

        public Gamma(string name, byte[] value):this()
        {
            Name = name;
            Value = value;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public byte[] Value { get; set; }
    }
}
