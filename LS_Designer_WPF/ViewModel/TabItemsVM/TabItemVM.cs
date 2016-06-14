using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using LS_Designer_WPF.Model;
using System;

namespace LS_Designer_WPF.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class TabItemVM : ViewModelBase
    {
        protected IDataService _dataService;
        //protected Guid messageToken = MainViewModel.MessageToken;
        
        public TabItemVM()
        {
            
            //RequireControlSpace = false;
            //RequirePartition = false;
            MessengerInstance.Register<String>(this, AppContext.ChanngeContextMsg, ContextChanged);
        }

        public virtual void Refresh() { }

        protected virtual void ContextChanged(String obj)
        {
            //if (AppContext.ControlSpace != null)
            //    TabItemEnabled = true;
            //if (IsSelected)
            //    Refresh();
        }

        public bool IsSelected { get; set; }

        public string TabName { get; set; }

        bool _tabItemEnabled = true;
        public bool TabItemEnabled
        { get { return _tabItemEnabled; } set { Set(ref _tabItemEnabled, value); } }

        //public bool RequireControlSpace { get; set; }

        //public bool RequirePartition { get; set; }

        
    }
}