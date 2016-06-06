using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Windows;

namespace LS_Designer_WPF.ViewModel
{
    public class EmptyPopUpVM : ViewModelBase
    {
        protected Action<Object> CancelAction;
        protected Action<Object> OKAction;
        public EmptyPopUpVM()
        {
            CancelCmd = new RelayCommand(ExecCancel);
            OKCmd = new RelayCommand(ExecOK);
        }

        string _title;
        public string Title { get { return _title; } set { Set(ref _title, value); } }

        Visibility _popUpVisibility = Visibility.Collapsed;
        public Visibility PopUpVisibility { get { return _popUpVisibility; } set { Set(ref _popUpVisibility, value); } }

        public RelayCommand CancelCmd { get; private set; }

        public virtual void ExecCancel() { }

        public RelayCommand OKCmd { get; private set; }

        public virtual void ExecOK() { }

    }
}
