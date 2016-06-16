using System;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using LS_Designer_WPF.Model;
using System.Collections.ObjectModel;

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

            Load();
            
        }

        public override void Refresh()
        {
            Load();
        }

        void Load()
        {
            if (AppContext.ControlSpace != null)
            {
                _dataService.GetEnvironmentItems(AppContext.ControlSpace.Id, Model.DeviceTypeEnum.ControlDevice, (data, error) =>
                {
                    if (error != null) { return; } // Report error here
                    MasterSelectorList = data;
                });
            }
        }

        protected override void ContextChanged(string obj)
        {
            if (AppContext.ControlSpace != null)
            {
                TabItemEnabled = true;
                //if (IsSelected)
                Refresh();
            }
        }

        Object TempDetailObject { get; set; } = null;

        Object TempMasterObject { get; set; } = null;

        Visibility _detailContentVisibility = Visibility.Collapsed;
        public Visibility DetailContentVisibility
        {
            get { return _detailContentVisibility; }
            set { Set(ref _detailContentVisibility, value); }
        }

        /*************************************************************/

        #region Master Properties

        ObservableCollection<ControlDevice> _masterList = new ObservableCollection<ControlDevice>();
        public ObservableCollection<ControlDevice> MasterList
        {
            get { return _masterList; }
            set { Set(ref _masterList, value); }
            
        }

        ControlDevice _masterSelectedItem;
        public ControlDevice MasterSelectedItem 
        {
            get { return _masterSelectedItem; }
            set
            {
                Set(ref _masterSelectedItem, value);
                if (MasterSelectedItem != null)
                {
                    _dataService.GetControlDevice(MasterSelectedItem.Id, (data, error) =>
                     {
                         if (error != null) { return; } // Report error here
                         MasterCurrentObject = data;
                     });
                    MasterObjectPanelVisibility = Visibility.Visible;
                    MasterObjectCurtainVisibility = Visibility.Visible;
                    MasterRemoveCmd.RaiseCanExecuteChanged();
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
                    x.Profile = d.Profile;
                    MasterCurrentObject = x;

                    MasterAddMode = true;

                    MasterObjectPanelVisibility = Visibility.Visible;
                    MasterSelectorVisibility = Visibility.Hidden;
                    MasterListButtonsVisibility = Visibility.Visible;
                    MasterListVisibility = Visibility.Visible;
                    MasterAddCmd.RaiseCanExecuteChanged();
                    MasterRemoveCmd.RaiseCanExecuteChanged();
                    MasterObjectCurtainVisibility = Visibility.Collapsed;
                    MasterObjectButtonsVisibility = Visibility.Visible;
                    MasterListCurtainVisibility = Visibility.Visible;
                    //MessengerInstance.Send("focus", "DeviceControlFocus");

                    //ControlDevice cd = (ControlChannel)Activator.CreateInstance(Type.GetType(dbLE_Proxy.LightElement.ControlChannel.DotNetChannelType));
                    //}
                    //    Temp = SelectedItem;
                    //AddCmd.RaiseCanExecuteChanged();
                    //RemoveCmd.RaiseCanExecuteChanged();
                    //SelectedItem = null;
                    //AddUIState();
                    //CurrentObject = new Partition() { Id = 0, Name = "Новый раздел" };
                    //MessengerInstance.Send("focus", "PartitionFocus");
                    return;
                }
                if (!IsMasterSelectorOpen)
                {
                    MasterListButtonsVisibility = Visibility.Visible;
                    MasterSelectorVisibility = Visibility.Hidden;
                    MasterListVisibility = Visibility.Visible;
                    if (MasterSelectedItem != null)
                        MasterObjectPanelVisibility = Visibility.Visible;
                    MessengerInstance.Send("", AppContext.UnBlockUIMsg);
                }
            }
        }

        #endregion

        /*************************************************************/

        #region Detail Properties

        object _detailList;
        public object DetailList
        {
            get { return _detailList; }
            set { Set(ref _detailList, value); }
        }

        ControlChannel _detailSelectedItem;
        public ControlChannel DetailSelectedItem
        {
            get { return _detailSelectedItem; }
            set
            {
                Set(ref _detailSelectedItem, value);
                if (DetailSelectedItem != null)
                {
                    _dataService.GetControlChannel(DetailSelectedItem.Id, (data, error) =>
                     {
                         if (error != null) { return; } // Report error here
                         DetailCurrentObject = data;
                     });
                    DetailObjectPanelVisibility = Visibility.Visible;
                    DetailObjectCurtainVisibility = Visibility.Visible;
                    //DetailRemoveCmd.RaiseCanExecuteChanged();
                }
            }
        }

        object _detailCurrentObject;
        public object DetailCurrentObject
        {
            get { return _detailCurrentObject; }
            set { Set(ref _detailCurrentObject, value); }
        }

        #endregion

        /*************************************************************/

        #region Master UI State

        //Visibility _masterListPanelVisibility = Visibility.Collapsed;
        //public Visibility MasterListPanelVisibility
        //{
        //    get { return _masterListPanelVisibility; }
        //    set { Set(ref _masterListPanelVisibility, value); }
        //}

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

        bool MasterAddMode { get; set; } = false;
        bool MasterEditMode { get; set; } = false;

        #region Master Save Command
        public RelayCommand MasterSaveCmd { get; private set; }
         
        AttentionVM attentionVM;

        void MasterExecSave()
        {
            int i = -1;
            _dataService.UpdateControlDevice(MasterCurrentObject, (objCnt, error) =>
            {
                if (error != null) { return; } // Report error here
                i = objCnt;
            });
            MasterList.Add(MasterCurrentObject);
            MasterSelectedItem = MasterCurrentObject;
            DetailList = MasterSelectedItem.ControlChannels;

            MasterObjectButtonsVisibility = Visibility.Collapsed;
            MasterListCurtainVisibility = Visibility.Collapsed;
            MasterAddMode = false;
            MasterEditMode = false;
            MasterSelectorSelectedItem = null;
            MasterAddCmd.RaiseCanExecuteChanged();
            MasterRemoveCmd.RaiseCanExecuteChanged();
            if (MasterCurrentObject.MultiChannel)
                DetailContentVisibility = Visibility.Visible;

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
            //if (CurrentObject != null)
            //{
            //    ControlSpace cs = null;
            //    _dataService.GetControlSpace(CurrentObject.Id, (item, error) =>
            //    {
            //        if (error != null) { return; }  // Report error here
            //        cs = item;
            //    });
            //    if (cs != null)
            //    {
            //        int i = ControlSpaces.IndexOf(SelectedItem);
            //        ControlSpaces[i] = cs;
            //        SelectedItem = cs;
            //    }
            //    NormalUIState();
            //}
        }

        #endregion

        #region Master Edit Command

        public RelayCommand MasterEditCmd { get; private set; }
        

        void MasterExecEdit()
        {
            //if (SelectedItem != null)
            //{
            //    EditUIState();
            //}
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
            //int ix = -1;
            //if (CurrentObject.IsActive != SelectedItem.IsActive)
            //{
            //    if (SelectedItem.IsActive)  // Изъятие ControlSpace из модели
            //    {
            //        attentionVM = new AttentionVM("Внимание", CancelCallbackAction, OKCallbackAction);
            //        MessengerInstance.Send<EmptyPopUpVM>(attentionVM, AppContext.ShowPopUpMsg);
            //    }
            //    else  // Добавление ControlSpace в модель
            //    {
            //        _dataService.UpdateControlSpace(CurrentObject, (data, error) =>
            //        {
            //            if (error != null) { return; } // Report error here
            //            ix = data;
            //        });
            //        ix = ControlSpaces.IndexOf(SelectedItem);
            //        ControlSpaces[ix] = CurrentObject;
            //        SelectedItem = CurrentObject;
            //        NormalUIState();
            //        MessengerInstance.Send<ControlSpace>(CurrentObject, AppContext.CSAddedMsg);
            //    }
            //}
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
            DetailListCurtainVisibility = Visibility.Collapsed;
            MasterListCurtainVisibility = Visibility.Collapsed;
            DetailObjectButtonsVisibility = Visibility.Collapsed;
            DetailObjectCurtainVisibility = Visibility.Visible;
            DetailEditMode = false;
            MasterAddCmd.RaiseCanExecuteChanged();
            MasterRemoveCmd.RaiseCanExecuteChanged();
            MasterListCurtainVisibility = Visibility.Collapsed;
            //if (CurrentObject != null)
            //{
            //    ControlSpace cs = null;
            //    _dataService.GetControlSpace(CurrentObject.Id, (item, error) =>
            //    {
            //        if (error != null) { return; }  // Report error here
            //        cs = item;
            //    });
            //    if (cs != null)
            //    {
            //        int i = ControlSpaces.IndexOf(SelectedItem);
            //        ControlSpaces[i] = cs;
            //        SelectedItem = cs;
            //    }
            //    NormalUIState();
            //}
        }

        #endregion

        #region Detail Edit Command

        public RelayCommand DetailEditCmd { get; private set; }


        void DetailExecEdit()
        {
            //if (SelectedItem != null)
            //{
            //    EditUIState();
            //}
            DetailEditMode = true;
            MasterRemoveCmd.RaiseCanExecuteChanged();
            MasterAddCmd.RaiseCanExecuteChanged();
            DetailObjectCurtainVisibility = Visibility.Collapsed;
            DetailObjectButtonsVisibility = Visibility.Visible;
            DetailListCurtainVisibility = Visibility.Visible;
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
