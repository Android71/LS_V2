using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using LS_Designer_WPF.Model;
using System.Collections.ObjectModel;
using System.Windows;

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
            
        }

        public PartitionsVM PartitionsVM { get; private set; }

        public ControlSpacesVM ControlSpacesVM { get; private set; }

        public ControlDevicesVM ControlDevicesVM { get; private set; }

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
                //Messenger.Default.Send(new NotificationMessage(""), AppContext.ChanngeContextMsg);
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

        private void BlockUI()
        {
            //foreach (TabItemVM tabItem in )
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