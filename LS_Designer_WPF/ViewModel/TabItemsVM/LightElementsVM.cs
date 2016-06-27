using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LS_Designer_WPF.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace LS_Designer_WPF.ViewModel
{
    public class LightElementsVM : TabItemVM
    {
        public LightElementsVM(IDataService dataService)
        {
            _dataService = dataService;
            TabName = "Light Elements";

            MasterAddCmd = new RelayCommand(MasterExecAdd, MasterCanExecAdd);
            MasterRemoveCmd = new RelayCommand(MasterExecRemove, MasterCanExecRemove);
            MasterEditCmd = new RelayCommand(MasterExecEdit);
            MasterSaveCmd = new RelayCommand(MasterExecSave);
            MasterCancelCmd = new RelayCommand(MasterExecCancel);

            TabItemEnabled = false;
        }

        public override void Refresh()
        {
            MasterSelectedItem = null;
            Load();
        }

        void Load()
        {
            if (AppContext.ControlSpace != null)
            {
                _dataService.GetLE_TypeList(AppContext.ControlSpace, (data, error) =>
                {
                    if (error != null) { return; } // Report error here
                    MasterSelectorList = data;
                });

                _dataService.GetPartitions((data, error) =>
                {
                    if (error != null) { return; } // Report error here
                    Partitions = new List<Partition>(data);
                });



                //_dataService.GetControlDevices(AppContext.ControlSpace, AppContext.Partition, (data, error) =>
                //{
                //    if (error != null) { return; } // Report error here
                //    MasterList = data;
                //});
                //DetailContentVisibility = Visibility.Hidden;
                //MasterObjectPanelVisibility = Visibility.Collapsed;
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

        /*************************************************************/

        #region Master Properties

        List<Partition> _partitions;
        public List<Partition> Partitions
        {
            get { return _partitions; }
            set { Set(ref _partitions, value); }
        }

        bool MasterAddMode { get; set; } = false;

        public bool MasterEditMode { get; set; } = false;

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
                //if (MasterSelectedItem != null)
                //{
                //    msix = MasterList.IndexOf(value);
                //    _dataService.GetControlDevice(MasterSelectedItem.Id, (data, error) =>
                //     {
                //         if (error != null) { return; } // Report error here
                //         MasterCurrentObject = data;
                //     });

                //    //if (MasterSelectedItem.MultiChannel)
                //    //{
                //    //    DetailContentVisibility = Visibility.Visible;
                //    //    DetailList = MasterCurrentObject.ControlChannels;
                //    //}
                //    //else
                //    //    DetailContentVisibility = Visibility.Hidden;

                //    //MasterObjectPanelVisibility = Visibility.Visible;
                //    //MasterObjectCurtainVisibility = Visibility.Visible;
                //    MasterRemoveCmd.RaiseCanExecuteChanged();
                //}
                //else
                //{
                //    msix = -1;
                //    //MasterObjectPanelVisibility = Visibility.Collapsed;
                //}
            }
        }

        LightElement _masterCurrentObject;
        public LightElement MasterCurrentObject
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
                    LE_Type leType = (LE_Type)MasterSelectorSelectedItem;
                    MasterCurrentObject = new LightElement(leType.PointType, AppContext.ControlSpace);

                    MasterObjectPanelVisibility = Visibility.Visible;

                    MasterCurrentObject.Partition = Partitions.Find(p => p.Id == AppContext.Partition.Id);
                    MasterCurrentObject.Partitions = Partitions;

                    MasterAddMode = true;
                    MasterAddCmd.RaiseCanExecuteChanged();
                    MasterRemoveCmd.RaiseCanExecuteChanged();

                    //MasterSelectorVisibility = Visibility.Hidden;
                    //MasterSelectorSelectedItem = null;
                    //MasterListVisibility = Visibility.Hidden;
                    //MasterListButtonsVisibility = Visibility.Visible;

                    //MasterObjectPanelVisibility = Visibility.Visible;
                    //MasterObjectButtonsVisibility = Visibility.Visible;

                    //MasterListCurtainVisibility = Visibility.Visible;
                    //DetailListCurtainVisibility = Visibility.Visible;
                    //MasterObjectCurtainVisibility = Visibility.Collapsed;

                    return;
                }
                if (!IsMasterSelectorOpen)
                {
                    //MasterListButtonsVisibility = Visibility.Visible;
                    //MasterSelectorVisibility = Visibility.Hidden;
                    //MasterListVisibility = Visibility.Visible;
                    //if (MasterSelectedItem != null)
                    //{
                    //    MasterObjectPanelVisibility = Visibility.Visible;
                    //    if (MasterSelectedItem.MultiChannel)
                    //    {
                    //        DetailContentVisibility = Visibility.Visible;
                    //        if (DetailSelectedItem != null)
                    //            DetailObjectPanelVisibility = Visibility.Visible;
                    //    }
                    //}
                    //else
                    //    MasterObjectPanelVisibility = Visibility.Collapsed;
                    //MessengerInstance.Send("", AppContext.UnBlockUIMsg);
                }
            }
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

        Visibility _detailContentVisibility = Visibility.Collapsed;
        public Visibility DetailContentVisibility
        {
            get { return _detailContentVisibility; }
            set { Set(ref _detailContentVisibility, value); }
        }

        #endregion

        /*************************************************************/

        #region Master Commands

        #region Master Save Command

        public RelayCommand MasterSaveCmd { get; private set; }

        void MasterExecSave()
        {
            //    int i = -1;
            //    bool partitionChanged = false;
            //    //int dsi = -1;
            //    if (MasterEditMode)
            //    {
            //        if(MasterCurrentObject.Partition.Id != MasterSelectedItem.Partition.Id)
            //        {
            //            partitionChanged = true;
            //            foreach(ControlChannel ch in MasterCurrentObject.ControlChannels)
            //            {
            //                ch.Partition = MasterCurrentObject.Partition;
            //            }
            //        }
            //    }
            //    _dataService.UpdateControlDevice(MasterCurrentObject, (updatedCount, error) =>
            //    {
            //        if (error != null) { return; } // Report error here
            //        i = updatedCount;
            //    });

            //    MasterObjectButtonsVisibility = Visibility.Collapsed;
            //    MasterListCurtainVisibility = Visibility.Collapsed;
            //    DetailListCurtainVisibility = Visibility.Collapsed;


            //    if (MasterAddMode)
            //    {
            //        MasterSelectorSelectedItem = null;

            //        MasterList.Add(MasterCurrentObject);
            //        MasterSelectedItem = MasterCurrentObject;
            //        MasterListVisibility = Visibility.Visible;
            //    }
            //    if (MasterEditMode) //in EditMode MasterSelectedItem always not null
            //    {
            //        if (!partitionChanged)
            //        {
            //            MasterList[msix] = MasterCurrentObject;
            //            MasterSelectedItem = MasterCurrentObject;
            //            if (savedDsix != -1)
            //                DetailSelectedItem = DetailList[savedDsix];
            //        }
            //        else
            //        {
            //            MasterList.Remove(MasterSelectedItem);
            //            DetailContentVisibility = Visibility.Hidden;
            //            partitionChanged = false;
            //        }
            //    }

            //    MasterAddMode = false;
            //    MasterEditMode = false;
            //    MasterCurrentObject.IsEditMode=false;
            //    MasterRemoveCmd.RaiseCanExecuteChanged();
            //    MasterAddCmd.RaiseCanExecuteChanged();

            //    MessengerInstance.Send("", AppContext.UnBlockUIMsg);
        }

        //AttentionVM attentionVM;

        //private void OKCallbackAction(Object obj)
        //{
        //    //// Пользователь подтвердил изъятие ControlSpace из модели
        //    //// DeleteAllEntities(CurrentObject); // Операция удаления объектов ссылающихся на ControlSpace

        //    //attentionVM.PopUpVisibility = Visibility.Collapsed;
        //    //MessengerInstance.Send(CurrentObject, AppContext.CSRemovedMsg); // обновление ControlSpaces в MainViewModel
        //}

        //private void CancelCallbackAction(Object obj)
        //{
        //    //attentionVM.PopUpVisibility = Visibility.Collapsed;
        //    //ExecCancel();
        //}

        #endregion

        #region Master Cancel Command

        public RelayCommand MasterCancelCmd { get; private set; }

        void MasterExecCancel()
        {
            //if (MasterEditMode)
            //{
            //    _dataService.GetControlDevice(MasterSelectedItem.Id, (data, error) =>
            //         {
            //             if (error != null) { return; } // Report error here
            //             MasterCurrentObject = data;
            //         });
            //}
            //MasterAddMode = false;
            //MasterEditMode = false;
            //MasterCurrentObject.IsEditMode = false;

            //MasterAddCmd.RaiseCanExecuteChanged();
            //MasterRemoveCmd.RaiseCanExecuteChanged();

            //MasterListCurtainVisibility = Visibility.Collapsed;
            //MasterObjectCurtainVisibility = Visibility.Visible;

            //MasterObjectButtonsVisibility = Visibility.Collapsed;
            //MasterListVisibility = Visibility.Visible;

            //if (MasterSelectedItem != null)
            //{
            //    _dataService.GetControlDevice(MasterSelectedItem.Id, (data, error) =>
            //    {
            //        if (error != null) { return; } // Report error here
            //        MasterCurrentObject = data;
            //    });
            //}
            //else
            //    MasterObjectPanelVisibility = Visibility.Collapsed;
            //MessengerInstance.Send("", AppContext.UnBlockUIMsg);
        }

        #endregion

        #region Master Edit Command

        public RelayCommand MasterEditCmd { get; private set; }

        void MasterExecEdit()
        {
            MasterEditMode = true;
            MasterCurrentObject.IsEditMode = true;
            MasterCurrentObject.Partitions = Partitions;
            MasterCurrentObject.Partition = Partitions.Find(p => p.Id == AppContext.Partition.Id);

            MasterAddCmd.RaiseCanExecuteChanged();
            MasterRemoveCmd.RaiseCanExecuteChanged();
            MasterListCurtainVisibility = Visibility.Visible;
            MasterObjectButtonsVisibility = Visibility.Visible;
            MasterObjectCurtainVisibility = Visibility.Collapsed;
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

            MessengerInstance.Send("", AppContext.BlockUIMsg);
        }

        bool MasterCanExecAdd()
        {
            return !MasterAddMode && !MasterEditMode;
        }

        #endregion

        #region Master Remove Command

        public RelayCommand MasterRemoveCmd { get; private set; }

        void MasterExecRemove()
        {
        }

        bool MasterCanExecRemove()
        {
            return !MasterAddMode && !MasterEditMode && MasterSelectedItem != null;
        }

        #endregion

        #endregion

        /*************************************************************/







    }
}