using LS_Designer_WPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS_Designer_WPF.ViewModel
{
    public class LE_VisualVM : EmptyPopUpVM
    {
        public LE_VisualVM(List<LightElement> leList, string title)
        {
            LE_List = leList;
            Title = title;
        }

        public List<LightElement> LE_List { get; set; }

        //public string Title { get; set; }
    }
}
