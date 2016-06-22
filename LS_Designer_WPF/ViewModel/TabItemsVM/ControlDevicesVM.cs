using System;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using LS_Designer_WPF.Model;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace LS_Designer_WPF.ViewModel
{
    public class ControlDevicesVM : TabItemVM
    {

        public ControlDevicesVM(IDataService dataService)
        {
            _dataService = dataService;
            TabName = "Control Devices";

            MasterAddCmd = new RelayCommand(MasterExecAdd, MasterCanExecAdd);
            MasterRemoveCmd = new RelayCommand(MasterExecRemove, MasterCanExecRemove);
            MasterEditCmd = new RelayCommand(MasterExecEdit);
            MasterSaveCmd = new RelayCommand(MasterExecSave);
            MasterCancelCmd = new RelayCommand(MasterExecCancel);

            DetailAddCmd = new RelayCommand(DetailExecAdd, DetailCanExecAdd);
            DetailRemoveCmd = new RelayCommand(DetailExecRemove, DetailCanExecRemove);
            DetailEditCmd = new RelayCommand(DetailExecEdit);
            DetailSaveCmd = new RelayCommand(DetailExecSave);
            DetailCancelCmd = new RelayCommand(DetailExecCancel);

            TabItemEnabled = false;

            //Load();
            
        }

        public override void Refresh()
        {
            MasterSelectedItem = null;
            DetailSelectedItem = null;
            Load();
        }

        void Load()
        {
            if (AppContext.ControlSpace != null)
            {
                _dataService.GetEnvironmentItems(AppContext.ControlSpace.Id, DeviceTypeEnum.ControlDevice, (data, error) =>
                {
                    if (error != null) { return; } // Report error here
                    MasterSelectorList = data;
                });
                
                _dataService.GetPartitions((data, error) =>
                {
                    if (error != null) { return; } // Report error here
                    Partitions = new List<Partition>(data);
                });
                


                _dataService.GetControlDevices(AppContext.ControlSpace, AppContext.Partition, (data, error) =>
                {
                    if (error != null) { return; } // Report error here
                    MasterList = data;
                });
                DetailContentVisibility = Visibility.Hidden;
                MasterObjectPanelVisibility = Visibility.Collapsed;
            }
        }

        protected override void ContextChanged(string obj)
        {
            if (AppContext.ControlSpace != null && AppContext.Partition != null)
            {
                TabItemEnabled = true;
                if (IsSelected)
                    Refresh();
            }
        }


        Visibility _detailContentVisibility = Visibility.Collapsed;
        public Visibility DetailContentVisibility
        {
            get { return _detailContentVisibility; }
            set { Set(ref _detailContentVisibility, value); }
        }

        /*************************************************************/

        #region Master Properties

        List<Partition> _partitions;
        public List<Partition> Partitions
        {
            get { return _partitions; }
            set { Set(ref _partitions, value); }
        }

        bool MasterAddMode { get; set; } = false;

        //bool _masterEditMode = false;
        public bool MasterEditMode { get; set; } = false;
        //{
        //    get { return _masterEditMode; }
        //    set { Set(ref _masterEditMode, value); }
        //}

        ObservableCollection<ControlDevice> _masterList = new ObservableCollection<ControlDevice>();
        public ObservableCollection<ControlDevice> MasterList
        {
            get { return _masterList; }
            set { Set(ref _masterList, value); }
        }

        int msix = -1; //MasterSelectedItem ix;

        ControlDevice _masterSelectedItem;
        public ControlDevice MasterSelectedItem 
        {
            get { return _masterSelectedItem; }
            set
            {
                Set(ref _masterSelectedItem, value);
                if (MasterSelectedItem != null)
                {
                    msix = MasterList.IndexOf(value);
                    _dataService.GetControlDevice(MasterSelectedItem.Id, (data, error) =>
                     {
                         if (error != null) { return; } // Report error here
                         MasterCurrentObject = data;
                     });

                    if (MasterSelectedItem.MultiChannel)
                    {
                        DetailContentVisibility = Visibility.Visible;
                        DetailList = MasterCurrentObject.ControlChannels;
                    }
                    else
                        DetailContentVisibility = Visibility.Hidden;

                    MasterObjectPanelVisibility = Visibility.Visible;
                    MasterObjectCurtainVisibility = Visibility.Visible;
                    MasterRemoveCmd.RaiseCanExecuteChanged();
                }
                else
                {
                    msix = -1;
                    MasterObjectPanelVisibility = Visibility.Collapsed;
                }
            }
        }

        ControlDevice _masterCurrentObject;
        public ControlDevice MasterCurrentObject
        {
            get { return _masterCurrentObject; }
            set { Set(ref _masterCurrentObject, value); }
        }

        object _masterSelectorList;
        public object MasterSelectorList
        {
            get { return _masterSelectorList; }
            set { Set(ref _masterSelectorList, value); }
        }

        object _masterSelectorSelectedItem;
        public object MasterSelectorSelectedItem
        {
            get { return _masterSelectorSelectedItem; }
            set { Set(ref _masterSelectorSelectedItem, value); }
        }

        bool _isMasterSelectorOpen = false;
        public bool IsMasterSelectorOpen
        {
            get { return _isMasterSelectorOpen; }
            set
            {
                Set(ref _isMasterSelectorOpen, value);
                if (!IsMasterSelectorOpen && MasterSelectorSelectedItem != null)
                {
                    //var t = typeof(AN6USPI).AssemblyQualifiedName;
                    dynamic d = MasterSelectorSelectedItem;
                    dynamic x = Activator.CreateInstance(Type.GetType(d.DotNetType));
                    x.ControlSpace = AppContext.ControlSpace;

                    //MasterObjectPanelVisibility = Visibility.Visible;

                    x.Partition = Partitions.Find(p => p.Id == AppContext.Partition.Id); 
                    x.Partitions = Partitions;
                    x.Profile = d.Profile;
                    x.Model = d.Model;
                    MasterCurrentObject = x;

                    MasterAddMode = true;
                    MasterAddCmd.RaiseCanExecuteChanged();
                    MasterRemoveCmd.RaiseCanExecuteChanged();

                    MasterSelectorVisibility = Visibility.Hidden;
                    MasterSelectorSelectedItem = null;
                    MasterListVisibility = Visibility.Hidden;
                    MasterListButtonsVisibility = Visibility.Visible;

                    MasterObjectPanelVisibility = Visibility.Visible;
                    MasterObjectButtonsVisibility = Visibility.Visible;

                    MasterListCurtainVisibility = Visibility.Visible;
                    DetailListCurtainVisibility = Visibility.Visible;
                    MasterObjectCurtainVisibility = Visibility.Collapsed;

                    return;
                }
                if (!IsMasterSelectorOpen)
                {
                    MasterListButtonsVisibility = Visibility.Visible;
                    MasterSelectorVisibility = Visibility.Hidden;
                    MasterListVisibility = Visibility.Visible;
                    if (MasterSelectedItem != null)
                    {
                        MasterObjectPanelVisibility = Visibility.Visible;
                        if (MasterSelectedItem.MultiChannel)
                        {
                            DetailContentVisibility = Visibility.Visible;
                            if (DetailSelectedItem != null)
                                DetailObjectPanelVisibility = Visibility.Visible;
                        }
                    }
                    else
                        MasterObjectPanelVisibility = Visibility.Collapsed;
                    MessengerInstance.Send("", AppContext.UnBlockUIMsg);
                }
            }
        }

        #endregion

        /*************************************************************/

        #region Detail Properties

        ObservableCollection<ControlChannel> _detailList;
        public ObservableCollection<ControlChannel> DetailList
        {
            get { return _detailList; }
            set { Set(ref _detailList, value); }
        }

        int dsix = -1; //DetailSelectedItem ix

        ControlChannel _detailSelectedItem;
        public ControlChannel DetailSelectedItem
        {
            get { return _detailSelectedItem; }
            set
            {
                Set(ref _detailSelectedItem, value);
                if (DetailSelectedItem != null)
                {
                    dsix = DetailList.IndexOf(value);
                    _dataService.GetControlChannel(DetailSelectedItem.Id, (data, error) =>
                     {
                         if (error != null) { return; } // Report error here
                         DetailCurrentObject = data;
                     });
                    DetailObjectPanelVisibility = Visibility.Visible;

                    DetailCurrentObject.Partitions = Partitions;
                    DetailCurrentObject.Partition = Partitions.Find(p => p.Id == DetailSelectedItem.Partition.Id);
                }
                else
                {
                    dsix = -1;
                    DetailObjectPanelVisibility = Visibility.Collapsed;
                }

            }
        }

        ControlChannel _detailCurrentObject;
        public ControlChannel DetailCurrentObject
        {
            get { return _detailCurrentObject; }
            set { Set(ref _detailCurrentObject, value); }
        }

        #endregion

        /*************************************************************/

        #region Master UI State
        
        Visibility _masterListButtonsVisibility = Visibility.Visible;
        public Visibility MasterListButtonsVisibility
        {
            get { return _masterListButtonsVisibility; }
            set { Set(ref _masterListButtonsVisibility, value); }
        }

        Visibility _masterSelectorVisibility = Visibility.Hidden;
        public Visibility MasterSelectorVisibility
        {
            get { return _masterSelectorVisibility; }
            set { Set(ref _masterSelectorVisibility, value); }
        }

        Visibility _masterListVisibility = Visibility.Visible;
        public Visibility MasterListVisibility
        {
            get { return _masterListVisibility; }
            set { Set(ref _masterListVisibility, value); }
        }

        Visibility _masterListCurtainVisibility = Visibility.Collapsed;
        public Visibility MasterListCurtainVisibility
        {
            get { return _masterListCurtainVisibility; }
            set { Set(ref _masterListCurtainVisibility, value); }
        }

        Visibility _masterObjectButtonsVisibility = Visibility.Collapsed;
        public Visibility MasterObjectButtonsVisibility
        {
            get { return _masterObjectButtonsVisibility; }
            set { Set(ref _masterObjectButtonsVisibility, value); }
        }

        Visibility _masterObjectCurtainVisibility = Visibility.Visible;
        public Visibility MasterObjectCurtainVisibility
        {
            get { return _masterObjectCurtainVisibility; }
            set { Set(ref _masterObjectCurtainVisibility, value); }
        }

        Visibility _masterObjectPanelVisibility = Visibility.Collapsed;
        public Visibility MasterObjectPanelVisibility
        {
            get { return _masterObjectPanelVisibility; }
            set { Set(ref _masterObjectPanelVisibility, value); }
        }

        void MasterNormalUIState()
        {
            MasterListCurtainVisibility = Visibility.Collapsed;
            MasterObjectButtonsVisibility = Visibility.Collapsed;
            if (MasterSelectedItem == null)
                MasterObjectPanelVisibility = Visibility.Collapsed;
            else
                MasterObjectPanelVisibility = Visibility.Visible;
            MasterObjectCurtainVisibility = Visibility.Visible;
        }

        void MasterEditUIState()
        {
            MasterListCurtainVisibility = Visibility.Visible;
            MasterObjectButtonsVisibility = Visibility.Visible;
            MasterObjectCurtainVisibility = Visibility.Collapsed;
            MasterObjectPanelVisibility = Visibility.Visible;
        }

        #endregion

        /*************************************************************/

        #region Master Commands

        

        #region Master Save Command
        public RelayCommand MasterSaveCmd { get; private set; }
         
        //AttentionVM attentionVM;

        void MasterExecSave()
        {
            int i = -1;
            bool partitionChanged = false;
            //int dsi = -1;
            if (MasterEditMode)
            {
                if(MasterCurrentObject.Partition.Id != MasterSelectedItem.Partition.Id)
                {
                    partitionChanged = true;
                    foreach(ControlChannel ch in MasterCurrentObject.ControlChannels)
                    {
                        ch.Partition = MasterCurrentObject.Partition;
                    }
                }
            }
            _dataService.UpdateControlDevice(MasterCurrentObject, (updatedCount, error) =>
            {
                if (error != null) { return; } // Report error here
                i = updatedCount;
            });

            MasterObjectButtonsVisibility = Visibility.Collapsed;
            MasterListCurtainVisibility = Visibility.Collapsed;
            DetailListCurtainVisibility = Visibility.Collapsed;


            if (MasterAddMode)
            {
                MasterSelectorSelectedItem = null;

                MasterList.Add(MasterCurrentObject);
                MasterSelectedItem = MasterCurrentObject;
                MasterListVisibility = Visibility.Visible;
            }
            if (MasterEditMode) //in EditMode MasterSelectedItem always not null
            {
                if (!partitionChanged)
                {
                    MasterList[msix] = MasterCurrentObject;
                    MasterSelectedItem = MasterCurrentObject;
                    if (savedDsix != -1)
                        DetailSelectedItem = DetailList[savedDsix];
                }
                else
                {
                    MasterList.Remove(MasterSelectedItem);
                    DetailContentVisibility = Visibility.Hidden;
                    partitionChanged = false;
                }
            }

            MasterAddMode = false;
            MasterEditMode = false;
            MasterCurrentObject.IsEditMode=false;
            MasterRemoveCmd.RaiseCanExecuteChanged();
            MasterAddCmd.RaiseCanExecuteChanged();

            MessengerInstance.Send("", AppContext.UnBlockUIMsg);
        }

        private void OKCallbackAction(Object obj)
        {
            //// Пользователь подтвердил изъятие ControlSpace из модели
            //// DeleteAllEntities(CurrentObject); // Операция удаления объектов ссылающихся на ControlSpace

            //attentionVM.PopUpVisibility = Visibility.Collapsed;
            //MessengerInstance.Send(CurrentObject, AppContext.CSRemovedMsg); // обновление ControlSpaces в MainViewModel
        }

        private void CancelCallbackAction(Object obj)
        {
            //attentionVM.PopUpVisibility = Visibility.Collapsed;
            //ExecCancel();
        }

        #endregion

        #region Master Cancel Command
        public RelayCommand MasterCancelCmd { get; private set; }

        void MasterExecCancel()
        {
            if (MasterEditMode)
            {
                _dataService.GetControlDevice(MasterSelectedItem.Id, (data, error) =>
                     {
                         if (error != null) { return; } // Report error here
                         MasterCurrentObject = data;
                     });
            }
            MasterAddMode = false;
            MasterEditMode = false;
            MasterCurrentObject.IsEditMode = false;

            MasterAddCmd.RaiseCanExecuteChanged();
            MasterRemoveCmd.RaiseCanExecuteChanged();

            MasterListCurtainVisibility = Visibility.Collapsed;
            MasterObjectCurtainVisibility = Visibility.Visible;
            DetailListCurtainVisibility = Visibility.Collapsed;

            MasterObjectButtonsVisibility = Visibility.Collapsed;
            MasterListVisibility = Visibility.Visible;

            if (MasterSelectedItem != null)
            {
                _dataService.GetControlDevice(MasterSelectedItem.Id, (data, error) =>
                {
                    if (error != null) { return; } // Report error here
                    MasterCurrentObject = data;
                });
                if (MasterSelectedItem.MultiChannel)
                {
                    DetailContentVisibility = Visibility.Visible;
                    if (DetailSelectedItem != null)
                        DetailObjectPanelVisibility = Visibility.Visible;
                }
            }
            else
                MasterObjectPanelVisibility = Visibility.Collapsed;
            MessengerInstance.Send("", AppContext.UnBlockUIMsg);
        }

        #endregion

        #region Master Edit Command

        public RelayCommand MasterEditCmd { get; private set; }

        int savedDsix;
        void MasterExecEdit()
        {
            savedDsix = dsix;

            MasterEditMode = true;
            MasterCurrentObject.IsEditMode = true;
            MasterCurrentObject.Partitions = Partitions;
            MasterCurrentObject.Partition = Partitions.Find(p => p.Id == AppContext.Partition.Id);

            MasterAddCmd.RaiseCanExecuteChanged();
            MasterRemoveCmd.RaiseCanExecuteChanged();
            MasterListCurtainVisibility = Visibility.Visible;
            MasterObjectButtonsVisibility = Visibility.Visible;
            MasterObjectCurtainVisibility = Visibility.Collapsed;
            DetailListCurtainVisibility = Visibility.Visible;
            MessengerInstance.Send("", AppContext.BlockUIMsg);
        }

        #endregion

        #region Master Add Command

        public RelayCommand MasterAddCmd { get; private set; }

        void MasterExecAdd()
        {
            MasterListButtonsVisibility = Visibility.Collapsed;
            MasterListVisibility = Visibility.Hidden;
            MasterSelectorVisibility = Visibility.Visible;
            MasterObjectPanelVisibility = Visibility.Hidden;
            IsMasterSelectorOpen = true;

            DetailContentVisibility = Visibility.Hidden;
            DetailObjectPanelVisibility = Visibility.Collapsed;
            MessengerInstance.Send("", AppContext.BlockUIMsg);
        }

        bool MasterCanExecAdd()
        {
            return !MasterAddMode && !MasterEditMode && !DetailEditMode;
        }

        #endregion

        #region Master Remove Command

        public RelayCommand MasterRemoveCmd { get; private set; }

        void MasterExecRemove()
        {
        }

        bool MasterCanExecRemove()
        {
            return !MasterAddMode && !MasterEditMode && !DetailEditMode && MasterSelectedItem != null;
        }

        #endregion


        #endregion

        /*************************************************************/

        #region Detail UI State

        Visibility _detailListButtonsVisibility = Visibility.Hidden;
        public Visibility DetailListButtonsVisibility
        {
            get { return _detailListButtonsVisibility; }
            set { Set(ref _detailListButtonsVisibility, value); }
        }

        Visibility _detailListCurtainVisibility = Visibility.Collapsed;
        public Visibility DetailListCurtainVisibility
        {
            get { return _detailListCurtainVisibility; }
            set { Set(ref _detailListCurtainVisibility, value); }
        }

        Visibility _detailObjectButtonsVisibility = Visibility.Collapsed;
        public Visibility DetailObjectButtonsVisibility
        {
            get { return _detailObjectButtonsVisibility; }
            set { Set(ref _detailObjectButtonsVisibility, value); }
        }

        Visibility _detailObjectCurtainVisibility = Visibility.Visible;
        public Visibility DetailObjectCurtainVisibility
        {
            get { return _detailObjectCurtainVisibility; }
            set { Set(ref _detailObjectCurtainVisibility, value); }
        }

        Visibility _detailObjectPanelVisibility = Visibility.Collapsed;
        public Visibility DetailObjectPanelVisibility
        {
            get { return _detailObjectPanelVisibility; }
            set { Set(ref _detailObjectPanelVisibility, value); }
        }

        void DetailNormalUIState()
        {
            DetailListCurtainVisibility = Visibility.Collapsed;
            DetailObjectButtonsVisibility = Visibility.Collapsed;
            if (DetailSelectedItem == null)
                DetailObjectPanelVisibility = Visibility.Collapsed;
            else
                DetailObjectPanelVisibility = Visibility.Visible;
            DetailObjectCurtainVisibility = Visibility.Visible;
        }

        void DetailEditUIState()
        {
            DetailListCurtainVisibility = Visibility.Visible;
            DetailObjectButtonsVisibility = Visibility.Visible;
            DetailObjectCurtainVisibility = Visibility.Collapsed;
            DetailObjectPanelVisibility = Visibility.Visible;
        }



        #endregion


        /*************************************************************/

        #region Detail Commands

        bool DetailEditMode = false;

        #region Detail Save Command
        public RelayCommand DetailSaveCmd { get; private set; }


        void DetailExecSave()
        {
            int updateCount = 0;
            _dataService.UpdateControlChannel(DetailCurrentObject, (data, error) =>
                     {
                         if (error != null) { return; } // Report error here
                         updateCount = data;
                     });
            DetailEditMode = false;
            MasterAddCmd.RaiseCanExecuteChanged();
            MasterRemoveCmd.RaiseCanExecuteChanged();

            //int i = DetailList.IndexOf(DetailSelectedItem);
            DetailList[dsix] = DetailCurrentObject;
            DetailSelectedItem = DetailCurrentObject;

            DetailObjectButtonsVisibility = Visibility.Collapsed;
            DetailListCurtainVisibility = Visibility.Collapsed;
            DetailObjectCurtainVisibility = Visibility.Visible;

            MasterListCurtainVisibility = Visibility.Collapsed;

            MessengerInstance.Send("", AppContext.UnBlockUIMsg);
        }

        private void DetailOKCallbackAction(Object obj)
        {
            //// Пользователь подтвердил изъятие ControlSpace из модели
            //// DeleteAllEntities(CurrentObject); // Операция удаления объектов ссылающихся на ControlSpace

            //attentionVM.PopUpVisibility = Visibility.Collapsed;
            //MessengerInstance.Send(CurrentObject, AppContext.CSRemovedMsg); // обновление ControlSpaces в MainViewModel
        }

        private void DetailCancelCallbackAction(Object obj)
        {
            //attentionVM.PopUpVisibility = Visibility.Collapsed;
            //ExecCancel();
        }

        #endregion

        #region Detail Cancel Command
        public RelayCommand DetailCancelCmd { get; private set; }

        void DetailExecCancel()
        {
            _dataService.GetControlChannel(DetailSelectedItem.Id, (data, error) =>
            {
                if (error != null) { return; } // Report error here
                DetailCurrentObject = data;
            });

            DetailCurrentObject.Partitions = Partitions;
            DetailCurrentObject.Partition = Partitions.Find(p => p.Id == DetailSelectedItem.Partition.Id);

            DetailEditMode = false;
            MasterAddCmd.RaiseCanExecuteChanged();
            MasterRemoveCmd.RaiseCanExecuteChanged();

            DetailListCurtainVisibility = Visibility.Collapsed;
            MasterListCurtainVisibility = Visibility.Collapsed;
            DetailObjectCurtainVisibility = Visibility.Visible;

            DetailObjectButtonsVisibility = Visibility.Collapsed;



            MessengerInstance.Send("", AppContext.UnBlockUIMsg);
        }

        #endregion

        #region Detail Edit Command

        public RelayCommand DetailEditCmd { get; private set; }


        void DetailExecEdit()
        {
            DetailEditMode = true;
            
            DetailObjectCurtainVisibility = Visibility.Collapsed;
            DetailObjectButtonsVisibility = Visibility.Visible;
            DetailListCurtainVisibility = Visibility.Visible;

            MasterRemoveCmd.RaiseCanExecuteChanged();
            MasterAddCmd.RaiseCanExecuteChanged();
            MasterListCurtainVisibility = Visibility.Visible;

            MessengerInstance.Send("", AppContext.BlockUIMsg);
        }

        #endregion

        #region Detail Add Command

        public RelayCommand DetailAddCmd { get; private set; }

        void DetailExecAdd()
        {
            //AddMode = true;
            //if (SelectedItem != null)
            //    Temp = SelectedItem;
            //AddCmd.RaiseCanExecuteChanged();
            //RemoveCmd.RaiseCanExecuteChanged();
            //SelectedItem = null;
            //AddUIState();
            //CurrentObject = new Partition() { Id = 0, Name = "Новый раздел" };
            //MessengerInstance.Send("focus", "PartitionFocus");
        }

        bool DetailCanExecAdd()
        {
            //return !AddMode && !EditMode;
            return true;
        }

        #endregion

        #region Detail Remove Command

        public RelayCommand DetailRemoveCmd { get; private set; }

        void DetailExecRemove()
        {
        }

        bool DetailCanExecRemove()
        {
            //return !AddMode && !EditMode && SelectedItem != null;
            return true;
        }

        #endregion


        #endregion

    }
}
