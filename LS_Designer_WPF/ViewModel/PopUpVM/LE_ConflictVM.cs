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
            Width = 560;
            Height = 300;
        }

        List<LightElement> LE_List { get; set; }
    }
}
