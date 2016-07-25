using LS_Designer_WPF.Model;
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
        IDataService _dataService;

        public EffectPopUpVM(IDataService dataService, Action<object> okCallBackAction, Action<object> cancelCallBackAction, int effectId = -1)
        {
            _dataService = dataService;
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

        object _effectVM;
        public object EffectVM
        {
            get { return _effectVM; }
            set { Set(ref _effectVM, value); }
        }
    }
}
