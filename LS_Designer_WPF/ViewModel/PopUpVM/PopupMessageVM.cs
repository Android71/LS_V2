using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Command;

namespace LS_Designer_WPF.ViewModel
{
    public class PopUpMessageVM : EmptyPopUpVM
    {
        public PopUpMessageVM(string message)
        {
            //CloseCommand = new RelayCommand(ExecClose);
            Message = message;
        }

        //public void ShowPopUp()
        //{
        //    ShowMessage = true;
        //}

        //bool _showMessage = false;
        //public bool ShowMessage { get { return _showMessage; } set { Set(ref _showMessage, value); } }

        string _message = "Simple message";
        public string Message { get { return _message; } set { Set(ref _message, value); } }

        //string _title = "";
        //public string Title { get { return _title; } set { Set(ref _title, value); } }

        //public RelayCommand CloseCommand
        //{
        //    get;
        //    private set;
        //}

        //void ExecClose()
        //{
        //    ShowMessage = false;
        //}

    }


}