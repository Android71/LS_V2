using GalaSoft.MvvmLight;
//using Lighting.Library;
using LS_Designer_WPF.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace LS_Designer_WPF.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class PartitionsVM : TabItemVM
    {
        /// <summary>
        /// Initializes a new instance of the PartitionVM class.
        /// </summary>
        public PartitionsVM(IDataService dataService) : base(dataService)
        {
            TabName = "Partitions";
            //Refresh();
            RefreshList();
        }

        private ObservableCollection<Partition> _partitions = null;
        public ObservableCollection<Partition> Partitions
        {
            get { return _partitions; }
            set { Set(ref _partitions, value); }
        }

        private List<Partition> _partitionList = null;
        public List<Partition> PartitionList
        {
            get { return _partitionList; }
            set { Set<List<Partition>>(ref _partitionList, value); }
        }
        public override void Refresh()
        {
            _dataService.GetPartitions((data, error) =>
            {
                if (error != null)
                {
                    // Report error here
                    return;
                }
                Partitions = data;
            });
            
        }

        public void RefreshList()
        {
            List<Partition> pList = new List<Partition>();
            _dataService.GetPartitions((data, error) =>
            {
                if (error != null)
                {
                    // Report error here
                    return;
                }
                foreach (Partition p in data)
                    pList.Add(p);
                PartitionList = pList;
                Partitions = new ObservableCollection<Partition>(pList);
            });
            
        }
    }
}