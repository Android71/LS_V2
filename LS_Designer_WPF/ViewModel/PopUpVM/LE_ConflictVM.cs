using LS_Designer_WPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS_Designer_WPF.ViewModel
{
    public class LE_ConflictVM : EmptyPopUpVM
    {
        public LE_ConflictVM(List<LightElement> leList)
        {
            LE_List = leList;
            Width = 600;
            Height = 300;
        }

        public List<LightElement> LE_List { get; set; }
    }
}
