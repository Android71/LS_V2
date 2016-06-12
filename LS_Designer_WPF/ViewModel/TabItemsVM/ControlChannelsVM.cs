using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Windows;

namespace LS_Designer_WPF.ViewModel
{
    public class ControlChannelsVM : TabItemVM
    {

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

        #region UI State

        Visibility _masterListCurtainVisibility = Visibility.Collapsed;
        public Visibility MasterListCurtainVisibility
        {
            get { return _masterListCurtainVisibility; }
            set { Set(ref _masterListCurtainVisibility, value); }
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
            if (MasterSelectedItem == null)
                ObjectPanelVisibility = Visibility.Collapsed;
            else
                ObjectPanelVisibility = Visibility.Visible;
            ObjectCurtainVisibility = Visibility.Visible;
        }

        void EditUIState()
        {
            ListCurtainVisibility = Visibility.Visible;
            ObjectButtonsVisibility = Visibility.Visible;
            ObjectCurtainVisibility = Visibility.Collapsed;
            ObjectPanelVisibility = Visibility.Visible;
        }



        ControlSpace Temp { get; set; } = null;

        #endregion

        /*************************************************************/

        #region Commands

        #region SaveCommand
        public RelayCommand SaveCommand { get; private set; }

        AttentionVM attentionVM;

        void ExecSave()
        {
            int ix = -1;
            if (CurrentObject.IsActive != SelectedItem.IsActive)
            {
                if (SelectedItem.IsActive)  // Изъятие ControlSpace из модели
                {
                    attentionVM = new AttentionVM("Внимание", CancelCallbackAction, OKCallbackAction);
                    MessengerInstance.Send<EmptyPopUpVM>(attentionVM, AppContext.ShowPopUpMsg);
                }
                else  // Добавление ControlSpace в модель
                {
                    _dataService.UpdateControlSpace(CurrentObject, (data, error) =>
                    {
                        if (error != null) { return; } // Report error here
                        ix = data;
                    });
                    ix = ControlSpaces.IndexOf(SelectedItem);
                    ControlSpaces[ix] = CurrentObject;
                    SelectedItem = CurrentObject;
                    NormalUIState();
                    MessengerInstance.Send<ControlSpace>(CurrentObject, AppContext.CSAddedMsg);
                }
            }
        }

        private void OKCallbackAction(Object obj)
        {
            // Пользователь подтвердил изъятие ControlSpace из модели
            // DeleteAllEntities(CurrentObject); // Операция удаления объектов ссылающихся на ControlSpace

            attentionVM.PopUpVisibility = Visibility.Collapsed;
            MessengerInstance.Send(CurrentObject, AppContext.CSRemovedMsg); // обновление ControlSpaces в MainViewModel
        }

        private void CancelCallbackAction(Object obj)
        {
            attentionVM.PopUpVisibility = Visibility.Collapsed;
            ExecCancel();
        }

        #endregion

        #region CancelCommand
        public RelayCommand CancelCommand { get; private set; }

        void ExecCancel()
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

        #region Edit Command

        public RelayCommand EditCmd { get; private set; }
        

        void ExecEdit()
        {
            if (SelectedItem != null)
            {
                EditUIState();
            }
        }

        #endregion

        #endregion

        /*************************************************************/

    }
}
