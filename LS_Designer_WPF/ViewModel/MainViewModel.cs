using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using LS_Designer_WPF.Model;
using System.Collections.ObjectModel;
using System.Windows;
using System.Collections.Generic;

namespace LS_Designer_WPF.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;

        public MainViewModel(IDataService dataService)
        {
            _dataService = dataService;
            AppContext.DataSvc = dataService;

            Messenger.Default.Register<NotificationMessage>(this, AppContext.BlockChangeContextMsg, BlockContext);
            Messenger.Default.Register<NotificationMessage>(this, AppContext.UnBlockChangeContextMsg, UnBlockContext);

            MessengerInstance.Register<String>(this, AppContext.BlockUIMsg, BlockUI);
            MessengerInstance.Register<String>(this, AppContext.UnBlockUIMsg, UnBlockUI);

            MessengerInstance.Register<EmptyPopUpVM>(this, AppContext.ShowPopUpMsg, ShowPopUp);

            MessengerInstance.Register<Partition>(this, AppContext.PartitionAddedMsg, PartitionAdded);
            MessengerInstance.Register<Partition>(this, AppContext.PartitionChangedMsg, PartitionChanged);
            MessengerInstance.Register<NotificationMessage>(this, AppContext.PartitionRemovedMsg, PartitionRemoved);

            MessengerInstance.Register<ControlSpace>(this, AppContext.CSRemovedMsg, CSRemoved);
            MessengerInstance.Register<ControlSpace>(this, AppContext.CSAddedMsg, CSAdded);

            PopUpVM = new EmptyPopUpVM();
            
            _dataService.GetPartitions((data, error) =>
            {
                if (error != null) { return; } // Report error here
                Partitions = data;
            });

            _dataService.GetActiveControlSpaces((data, error) =>
            {
                if (error != null) { return; } // Report error here
                ControlSpaces = data;
            });

            PartitionsVM = new PartitionsVM(dataService);
            ControlSpacesVM = new ControlSpacesVM(dataService);
            ControlDevicesVM = new ControlDevicesVM(dataService);
            EventDevicesVM = new EventDevicesVM(dataService);

            TabItems.Add(PartitionsVM);
            TabItems.Add(ControlSpacesVM);
            TabItems.Add(ControlDevicesVM);
            TabItems.Add(EventDevicesVM);
            //TabItems.Add(LightElementsVM);
            //TabItems.Add(LightZonesVM);

        }

        

        public PartitionsVM PartitionsVM { get; private set; }

        public ControlSpacesVM ControlSpacesVM { get; private set; }

        public ControlDevicesVM ControlDevicesVM { get; private set; }

        public EventDevicesVM EventDevicesVM { get; private set; }

        object _popupVM = null;
        public object PopUpVM { get { return _popupVM; } set { Set(ref _popupVM, value); } }

        

        public ObservableCollection<Partition> Partitions { get; private set; }

        private Partition _selectedPartition = null;
        public Partition SelectedPartition
        {
            get { return _selectedPartition; }
            set
            {
                Set<Partition>(ref _selectedPartition, value);
                AppContext.Partition = value;
                MessengerInstance.Send("", AppContext.ChanngeContextMsg);
            }
        }

        public ObservableCollection<ControlSpace> ControlSpaces { get; private set; }

        private ControlSpace _selectedSpace = null;
        public ControlSpace SelectedSpace
        {
            get { return _selectedSpace; }
            set
            {
                Set<ControlSpace>(ref _selectedSpace, value);
                AppContext.ControlSpace = value;
                MessengerInstance.Send("", AppContext.ChanngeContextMsg);
            }
        }

        ObservableCollection<ViewModelBase> _tabItems = new ObservableCollection<ViewModelBase>();
        public ObservableCollection<ViewModelBase> TabItems
        {
            get
            {
                return _tabItems;
            }
            private set
            {
                _tabItems = value;
                Set(ref _tabItems, value);
            }
        }

        private TabItemVM _selectedTabItem = null;
        public TabItemVM SelectedTabItem
        {
            get { return _selectedTabItem; }
            set
            {
                if (_selectedTabItem != null)
                    _selectedTabItem.IsSelected = false;
                Set(ref _selectedTabItem, value);
                value.IsSelected = true;
                value.Refresh();
            }
        }



        /************************************************************/

        #region Messendger Handlers

        private void PartitionAdded(Partition partition)
        {
            Partitions.Add(partition);
        }

        private void PartitionRemoved(NotificationMessage obj)
        {
            throw new NotImplementedException();
        }

        void PartitionChanged(Partition partition)
        {
            Partition tmp = null;
            foreach(Partition p in Partitions)
            {
                if (p.Id == partition.Id)
                {
                    tmp = p;
                    break;
                }
            }
            int ix = Partitions.IndexOf(tmp);
            if (tmp == SelectedPartition)
            {
                Partitions[ix] = partition;
                SelectedPartition = partition;
            }
            else
                Partitions[ix] = partition;
        }

        private void ShowPopUp(EmptyPopUpVM obj)
        {
            PopUpVM = obj;
            obj.PopUpVisibility = Visibility.Visible;
        }

        

        private void UnBlockContext(NotificationMessage obj)
        {
            throw new NotImplementedException();
        }

        private void BlockContext(NotificationMessage obj)
        {
            throw new NotImplementedException();
        }

        

        private void CSRemoved(ControlSpace obj)
        {
            //var x = 5;
        }

        private void CSAdded(ControlSpace obj)
        {
            ControlSpaces.Add(obj);
        }

        Visibility _contextCurtainVisibility = Visibility.Collapsed;
        public Visibility ContextCurtainVisibility
        {
            get { return _contextCurtainVisibility; }
            set { Set(ref _contextCurtainVisibility, value); }
        }

        List<bool> tabItemsEnabledState;
        private void BlockUI(string obj)
        {
            tabItemsEnabledState = new List<bool>();
            foreach(TabItemVM ti in TabItems)
            {
                tabItemsEnabledState.Add(ti.TabItemEnabled);
                if (ti.TabName != SelectedTabItem.TabName)
                    ti.TabItemEnabled = false;
            }
            ContextCurtainVisibility = Visibility.Visible;
        }

        private void UnBlockUI(string obj)
        {
            int i = 0;
            foreach (TabItemVM ti in TabItems)
            {
                ti.TabItemEnabled = tabItemsEnabledState[i];
                i++;
            }
            ContextCurtainVisibility = Visibility.Collapsed;
        }

        #endregion

        /************************************************************/
    }

    class TabItemState
    {
        public bool Selected { get; set; }
        public bool Enabled { get; set; }
    }

    
}