using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
//using Lighting.Library;
using LS_Designer_WPF.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace LS_Designer_WPF.ViewModel
{
    public class PartitionsVM : TabItemVM
    {
        /// <summary>
        /// Initializes a new instance of the PartitionVM class.
        /// </summary>
        public PartitionsVM(IDataService dataService)
        {
            _dataService = dataService;
            TabName = "Partitions";
            AddCmd = new RelayCommand(ExecAdd, CanExecAdd);
            RemoveCmd = new RelayCommand(ExecRemove, CanExecRemove);
            EditCmd = new RelayCommand(ExecEdit);
            SaveCmd = new RelayCommand(ExecSave);
            CancelCmd = new RelayCommand(ExecCancel);
            _dataService.GetPartitions((data, error) =>
            {
                if (error != null) { return; } // Report error here
                Partitions = data;
            });
        }

        private ObservableCollection<Partition> _partitions = null;
        public ObservableCollection<Partition> Partitions
        {
            get { return _partitions; }
            set { Set(ref _partitions, value); }
        }

        private Partition _selectedItem = null;
        public Partition SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                Set(ref _selectedItem, value);
                if (SelectedItem != null)
                {
                    _dataService.GetPartition(SelectedItem.Id, (data, error) =>
                     {
                         if (error != null) { return; } // Report error here
                        CurrentObject = data;
                     });
                    ObjectPanelVisibility = Visibility.Visible;
                    RemoveCmd.RaiseCanExecuteChanged();
                }
            }
        }

        private Partition _currentObject = null;
        public Partition CurrentObject
        {
            get { return _currentObject; }
            set { Set(ref _currentObject, value); }
        }

        

        public override void Refresh()
        {
            _dataService.GetPartitions((data, error) =>
            {
                if (error != null) { return; } // Report error here
                Partitions = data;
            });
            
        }

        #region UI State

        Visibility _listCurttainVisibility = Visibility.Collapsed;
        public Visibility ListCurtainVisibility
        {
            get { return _listCurttainVisibility; }
            set { Set(ref _listCurttainVisibility, value); }
        }

        Visibility _objectButtonsVisibility = Visibility.Collapsed;
        public Visibility ObjectButtonsVisibility
        {
            get { return _objectButtonsVisibility; }
            set { Set(ref _objectButtonsVisibility, value); }
        }

        Visibility _objectCurtainVisibility = Visibility.Visible;
        public Visibility ObjectCurtainVisibility
        {
            get { return _objectCurtainVisibility; }
            set { Set(ref _objectCurtainVisibility, value); }
        }

        Visibility _objectPanelVisibility = Visibility.Collapsed;
        public Visibility ObjectPanelVisibility
        {
            get { return _objectPanelVisibility; }
            set { Set(ref _objectPanelVisibility, value); }
        }

        void NormalUIState()
        {
            ListCurtainVisibility = Visibility.Collapsed;
            ObjectButtonsVisibility = Visibility.Collapsed;
            if (SelectedItem == null)
                ObjectPanelVisibility = Visibility.Collapsed;
            else
                ObjectPanelVisibility = Visibility.Visible;
            ObjectCurtainVisibility = Visibility.Visible;
        }

        void AddUIState()
        {
            ListCurtainVisibility = Visibility.Visible;
            ObjectButtonsVisibility = Visibility.Visible;
            ObjectCurtainVisibility = Visibility.Collapsed;
            ObjectPanelVisibility = Visibility.Visible;
        }

        void EditUIState()
        {

        }

        bool AddMode { get; set; } = false;

        bool EditMode { get; set; } = false;

        Partition Temp { get; set; } = null;

        #endregion

        /***********************************************************/

        #region Commands

        #region Add Command

        public RelayCommand AddCmd { get; private set; }

        void ExecAdd()
        {
            AddMode = true;
            if (SelectedItem != null)
                Temp = SelectedItem;
            AddCmd.RaiseCanExecuteChanged();
            RemoveCmd.RaiseCanExecuteChanged();
            SelectedItem = null;
            AddUIState();
            CurrentObject = new Partition() { Id = 0, Name = "Новый раздел" };
            MessengerInstance.Send("focus", "PartitionFocus");
        }

        bool CanExecAdd()
        {
            return !AddMode && !EditMode;
        }

        #endregion

        #region Remove Command

        public RelayCommand RemoveCmd { get; private set; }

        void ExecRemove()
        {
        }

        bool CanExecRemove()
        {
            return !AddMode && !EditMode && SelectedItem != null;
        }

        #endregion

        #region Edit Command

        public RelayCommand EditCmd { get; private set; }

        void ExecEdit()
        {
            if (SelectedItem != null)
            {
                EditMode = true;
                AddCmd.RaiseCanExecuteChanged();
                RemoveCmd.RaiseCanExecuteChanged();
                AddUIState();
                MessengerInstance.Send("focus", "PartitionFocus");
            }
        }

        #endregion

        #region Save Command

        public RelayCommand SaveCmd { get; private set; }

        void ExecSave()
        {
            int i = 0;
            if (AddMode)
            {
                AddMode = false;
                AddCmd.RaiseCanExecuteChanged();
                RemoveCmd.RaiseCanExecuteChanged();
                _dataService.UpdatePartition(CurrentObject, (data, error) =>
                {
                    if (error != null) { return; } // Report error here
                    i = data;
                });
                Partitions.Add(CurrentObject);
                SelectedItem = CurrentObject;
                NormalUIState();
                MessengerInstance.Send(SelectedItem, AppContext.PartitionAddedMsg);
                return;
            }
            if (EditMode)
            {
                EditMode = false;
                AddCmd.RaiseCanExecuteChanged();
                RemoveCmd.RaiseCanExecuteChanged();
                _dataService.UpdatePartition(CurrentObject, (data, error) =>
                {
                    if (error != null) { return; } // Report error here
                    i = data;
                });

                int ix = Partitions.IndexOf(SelectedItem);
                Partitions[ix] = CurrentObject;
                SelectedItem = CurrentObject;

                NormalUIState();
                MessengerInstance.Send(SelectedItem, AppContext.PartitionChangedMsg);
                return;
            }
        }

        #endregion

        #region Cancel Command

        public RelayCommand CancelCmd { get; private set; }

        void ExecCancel()
        {
            if (AddMode)
            {
                AddMode = false;
                AddCmd.RaiseCanExecuteChanged();
                RemoveCmd.RaiseCanExecuteChanged();
                NormalUIState();
                if (Temp != null)
                {
                    SelectedItem = Temp;
                    Temp = null;
                }
                else
                    CurrentObject = null;
                return;
            }
            if (EditMode)
            {
                EditMode = false;
                AddCmd.RaiseCanExecuteChanged();
                RemoveCmd.RaiseCanExecuteChanged();
                NormalUIState();
                _dataService.GetPartition(SelectedItem.Id, (data, error) =>
                     {
                         if (error != null) { return; } // Report error here
                         CurrentObject = data;
                     });
            }
        }

        #endregion

        #endregion

        /***********************************************************/
    }
}