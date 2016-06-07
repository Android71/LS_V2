using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
//using Lighting.Library;
using LS_Designer_WPF.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LS_Designer_WPF.ViewModel
{
    public class ControlSpacesVM : TabItemVM
    {
        public ControlSpacesVM(IDataService dataService)
        {
            _dataService = dataService;
            TabName = "ControlSpaces";
            SaveCommand = new RelayCommand(ExecSave);
            CancelCommand = new RelayCommand(ExecCancel);
            EditCmd = new RelayCommand(ExecEdit);
            LoadData();
        }

        public override void Refresh() { LoadData(); }

        void LoadData()
        {
            _dataService.GetControlSpaces((data, error) =>
           {
               if (error != null) { return; }   // Report error here
               ControlSpaces = data;
           });
        }

        protected override void ContextChanged(string obj)
        {
            
        }

        private ObservableCollection<ControlSpace> _controlSpaces = null;
        public ObservableCollection<ControlSpace> ControlSpaces
        {
            get { return _controlSpaces; }
            set { Set(ref _controlSpaces, value); }
        }

        private ControlSpace _selectedItem = null;
        public ControlSpace SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                Set<ControlSpace>(ref _selectedItem, value);
                if (SelectedItem != null)
                {
                    _dataService.GetControlSpace(SelectedItem.Id,(data, error) =>
                    {
                        if (error != null) { return; }   // Report error here
                        CurrentObject = data;
                    });
                    ObjectPanelVisibility = Visibility.Visible;
                }
            }
        }

        private ControlSpace _currentObject = null;
        public ControlSpace CurrentObject
        {
            get { return _currentObject; }
            set { Set<ControlSpace>(ref _currentObject, value); }
        }

        /*************************************************************/

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
        public RelayCommand SaveCommand { get;  private set;}

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
            if (CurrentObject != null)
            {
                ControlSpace cs = null;
                _dataService.GetControlSpace(CurrentObject.Id, (item, error) =>
                {
                    if (error != null) { return; }  // Report error here
                    cs = item;
                });
                if (cs != null)
                {
                    int i = ControlSpaces.IndexOf(SelectedItem);
                    ControlSpaces[i] = cs;
                    SelectedItem = cs;
                }
                NormalUIState();
            }
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
