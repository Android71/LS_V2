using GalaSoft.MvvmLight;
using System;
using System.Windows;

namespace LS_Designer_WPF.ViewModel
{
    public class AttentionVM : EmptyPopUpVM
    {
        
        public AttentionVM(string body, Action<Object> cancelAction, Action<Object> okAction )
        {
            AttentionBody = body;
            CancelAction = cancelAction;
            OKAction = okAction;
            Title = "Примите решение";
        }

        string _attentionBody;
        public string AttentionBody { get { return _attentionBody; } set { Set(ref _attentionBody, value); } }

        public override void ExecCancel()
        {
            CancelAction(null);
        }

        public override void ExecOK()
        {
            PopUpVisibility = Visibility.Collapsed; 
            OKAction("OK");
        }

        
    }
}
