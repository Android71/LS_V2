using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LS_Designer_WPF.ViewModel
{
    public class EffectPopUpVM : EmptyPopUpVM
    {
        public EffectPopUpVM(Action<object> okCallBackAction, Action<object> cancelCallBackAction)
        {
            CancelAction = cancelCallBackAction;
            OKAction = okCallBackAction;
        }

        public override void ExecCancel()
        {
            // Close PoUp
            PopUpVisibility = Visibility.Collapsed;
            CancelAction(null);
        }

        public override void ExecOK()
        {
            // Close PoUp
            PopUpVisibility = Visibility.Collapsed;
            OKAction("OK");
        }
    }
}
