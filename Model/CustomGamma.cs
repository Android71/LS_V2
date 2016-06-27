using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS_Designer_WPF.Model
{
    public class CustomGamma
    {
        public CustomGamma()
        {
           
        }

        public CustomGamma(string name, byte[] value):this()
        {
            Name = name;
            Value = value;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public byte[] Value { get; set; }

        public byte[] ValueR { get; set; }

        public byte[] ValueG { get; set; }

        public byte[] ValueB { get; set; }

        public string Remark { get; set; }

    }
}
