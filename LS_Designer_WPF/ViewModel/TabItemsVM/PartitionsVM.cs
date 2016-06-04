using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
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
        public PartitionsVM(IDataService dataService) : base(dataService)
        {
            TabName = "Partitions";
            AddCmd = new RelayCommand(ExecAdd, CanExecAdd);
            RemoveCmd = new RelayCommand(ExecRemove);
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
            ObjectPanelVisibility = Visibility.Collapsed;
            ObjectCurtainVisibility = Visibility.Visible;
        }

        void AddUIState()
        {
            ListCurtainVisibility = Visibility.Visible;
            ObjectButtonsVisibility = Visibility.Visible;
            ObjectCurtainVisibility = Visibility.Collapsed; 
        }

        void EditUIState()
        {

        }

        bool AddMode { get; set; } = false;

        bool EditMode { get; set; } = false;

        #endregion

        /***********************************************************/

        #region Commands

        #region Add Command

        public RelayCommand AddCmd { get; private set; }

        void ExecAdd()
        {
            AddMode = true;
            AddCmd.RaiseCanExecuteChanged();
            SelectedItem = null;
            AddUIState();
            CurrentObject = new Partition() { Id = 0, Name = "Новый раздел" };
        }

        bool CanExecAdd()
        {
            return !AddMode && !EditMode;
        }

        #endregion

        #region Remove Command

        public RelayCommand RemoveCmd { get; private set; }

        void ExecRemove()
        { }

        #endregion

        #region Edit Command

        public RelayCommand EditCmd { get; private set; }

        void ExecEdit()
        { }

        #endregion

        #region Save Command

        public RelayCommand SaveCmd { get; private set; }

        void ExecSave()
        {
            int i = 0;
            if (AddMode)
            {
                _dataService.UpdatePartition(CurrentObject, (data, error) =>
                {
                    if (error != null) { return; } // Report error here
                    i = data;
                });
                Partitions.Add(CurrentObject);
                SelectedItem = CurrentObject;
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
                NormalUIState();
                CurrentObject = null;
            }
        }

        #endregion

        #endregion

        /***********************************************************/
    }
}