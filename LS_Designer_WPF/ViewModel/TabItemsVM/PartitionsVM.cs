using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
//using Lighting.Library;
using LS_Designer_WPF.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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
            AddPartitionCmd = new RelayCommand(ExecAddPartition);
            RemovePartitionCmd = new RelayCommand(ExecRemovePartition);
            SavePartitionCmd = new RelayCommand(ExecSavePartition);
            Refresh();
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
                    _dataService.GetPartition(SelectedItem.Id,(data, error) =>
                    {
                        if (error != null) { return; } // Report error here
                        CurrentObject = data;
                    });
            }
        }

        private Partition _currentObject = null;
        public Partition CurrentObject
        {
            get { return _currentObject; }
            set { Set(ref _currentObject, value); }
        }

        public bool AddMode { get; set; }

        public override void Refresh()
        {
            _dataService.GetPartitions((data, error) =>
            {
                if (error != null) { return; } // Report error here
                Partitions = data;
            });
            
        }

        /***********************************************************/

        #region Commands

        #region AddPartition

        public RelayCommand AddPartitionCmd { get; private set; }

        void ExecAddPartition()
        {
            CurrentObject = new Partition() { Id = 0, Name = "Новый раздел" };
        }

        #endregion

        #region RemovePartition

        public RelayCommand RemovePartitionCmd { get; private set; }

        void ExecRemovePartition()
        { }

        #endregion

        #region SavePartition

        public RelayCommand SavePartitionCmd { get; private set; }

        void ExecSavePartition()
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

        #endregion

        /***********************************************************/
    }
}