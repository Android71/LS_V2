using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Windows;

namespace LS_Designer_WPF.ViewModel
{
    public class EmptyPopUpVM : ViewModelBase
    {
        protected Action<Object> CancelAction = null;
        protected Action<Object> OKAction = null;

        public EmptyPopUpVM()
        {
            CancelCmd = new RelayCommand(ExecCancel);
            OKCmd = new RelayCommand(ExecOK);
        }

        string _title;
        public string Title { get { return _title; } set { Set(ref _title, value); } }

        double _width = 300;
        public double Width { get { return _width; } set { Set(ref _width, value); } }

        double _height = 300;
        public double Height { get { return _height; } set { Set(ref _height, value); } }

        Visibility _popUpVisibility = Visibility.Collapsed;
        public Visibility PopUpVisibility { get { return _popUpVisibility; } set { Set(ref _popUpVisibility, value); } }

        public RelayCommand CancelCmd { get; private set; }

        public virtual void ExecCancel()
        {
            if (CancelAction == null)
                PopUpVisibility = Visibility.Collapsed;
        }

        public RelayCommand OKCmd { get; private set; }

        public virtual void ExecOK()
        {
            if (OKAction == null)
                PopUpVisibility = Visibility.Collapsed;
        }

    }
}
