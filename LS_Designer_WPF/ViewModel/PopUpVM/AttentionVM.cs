using GalaSoft.MvvmLight;
using System;
using System.Windows;

namespace LS_Designer_WPF.ViewModel
{
    public class AttentionVM : EmptyPopUpVM
    {
        
        public AttentionVM(string body, Action<Object> cancelCallBackAction, Action<Object> okCallBackAction )
        {
            AttentionBody = body;
            CancelAction = cancelCallBackAction;
            OKAction = okCallBackAction;
            Title = "Примите решение";
        }

        string _attentionBody;
        public string AttentionBody { get { return _attentionBody; } set { Set(ref _attentionBody, value); } }

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
