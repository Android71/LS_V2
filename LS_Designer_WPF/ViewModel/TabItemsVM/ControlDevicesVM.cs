using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Windows;
using EFData;
using LS_Designer_WPF.Model;

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

            Load();
            
        }

        public override void Refresh()
        {
            Load();
        }

        void Load()
        {
            _dataService.GetEnvironmentItems(1, Model.DeviceTypeEnum.ControlDevice, (data, error) =>
            {
                if (error != null) { return; } // Report error here
                MasterSelectorList = data;
            });
        }

        protected override void ContextChanged(string obj)
        {
            if (AppContext.ControlSpace != null)
                TabItemEnabled = true;
            if (IsSelected)
                Refresh();
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

        object _masterList;
        public object MasterList
        {
            get { return _masterList; }
            set { Set(ref _masterList, value); }
        }

        object _masterSelectedItem;
        public object MasterSelectedItem 
        {
            get { return _masterSelectedItem; }
            set { Set(ref _masterSelectedItem, value); }
        }

        object _masterCurrentObject;
        public object MasterCurrentObject
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

        #endregion

        /*************************************************************/

        #region Detail Properties

        object _detailList;
        public object DetailList
        {
            get { return _detailList; }
            set { Set(ref _detailList, value); }
        }

        object _detailSelectedItem;
        public object DetailSelectedItem
        {
            get { return _detailSelectedItem; }
            set { Set(ref _detailSelectedItem, value); }
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
         
        AttentionVM attentionVM;

        void MasterExecSave()
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

        bool MasterCanExecAdd()
        {
            //return !AddMode && !EditMode;
            return false;
        }

        #endregion

        #region Master Remove Command

        public RelayCommand MasterRemoveCmd { get; private set; }

        void MasterExecRemove()
        {
        }

        bool MasterCanExecRemove()
        {
            //return !AddMode && !EditMode && SelectedItem != null;
            return true;
        }

        #endregion


        #endregion

        /*************************************************************/

        #region Detail UI State

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
