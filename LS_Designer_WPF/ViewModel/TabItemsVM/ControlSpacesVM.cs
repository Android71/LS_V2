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
        public ControlSpacesVM(IDataService dataService) : base(dataService)
        {
            TabName = "ControlSpaces";
            //myType = GetType();

            //RequireControlSpace = true;

            //Messenger.Default.Register<NotificationMessage<Type>>(this, HandlePersonalMessage);
            SaveCommand = new RelayCommand(ExecSave);
            CancelCommand = new RelayCommand(ExecCancel);
            EditCmd = new RelayCommand(ExecEdit);

            LoadData();
        }

        public override void Refresh()
        {
            LoadData();   
            //Messenger.Default.Send(new NotificationMessage("DoSomething"), messgeToken);
        }

        
        void LoadData()
        {
            _dataService.GetControlSpaces((data, error) =>
           {
               if (error != null)
               {
                    // Report error here
                    return;
               }
               ControlSpaces = data;
           });
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

        //bool AddMode { get; set; } = false;

        bool EditMode { get; set; } = false;

        ControlSpace Temp { get; set; } = null;

        #endregion

        /*************************************************************/

        #region Commands

        #region SaveCommand
        public RelayCommand SaveCommand
        {
            get;
            private set;
        }

        AttentionVM attentionVM;

        void ExecSave()
        {
            if (CurrentObject.IsActive != SelectedItem.IsActive)
            {
                if (SelectedItem.IsActive)  // Изъятие ControlSpace из модели
                {
                    attentionVM = new AttentionVM("Внимание", CancelAction, OKAction);
                    MessengerInstance.Send<EmptyPopUpVM>(attentionVM, AppContext.ShowPopUpMsg);
                }
            }
            //int i = 0;
            //if (AddMode)
            //{
            //    AddMode = false;
            //    AddCmd.RaiseCanExecuteChanged();
            //    RemoveCmd.RaiseCanExecuteChanged();
            //    _dataService.UpdatePartition(CurrentObject, (data, error) =>
            //    {
            //        if (error != null) { return; } // Report error here
            //        i = data;
            //    });
            //    Partitions.Add(CurrentObject);
            //    SelectedItem = CurrentObject;
            //    NormalUIState();
            //    MessengerInstance.Send(SelectedItem, AppContext.PartitionAddedMsg);
            //    return;
            //}
            //if (EditMode)
            //{
            //    EditMode = false;
            //    AddCmd.RaiseCanExecuteChanged();
            //    RemoveCmd.RaiseCanExecuteChanged();
            //    _dataService.UpdatePartition(CurrentObject, (data, error) =>
            //    {
            //        if (error != null) { return; } // Report error here
            //        i = data;
            //    });

            //    int ix = Partitions.IndexOf(SelectedItem);
            //    Partitions[ix] = CurrentObject;
            //    SelectedItem = CurrentObject;

            //    NormalUIState();
            //    MessengerInstance.Send(SelectedItem, AppContext.PartitionChangedMsg);
            //    return;
            //}
        }

        private void OKAction(Object obj)
        {
            // Пользователь подтвердил изъятие ControlSpace из модели

            // Операция удаления объектов ссылающихся на ControlSpace
            // DeleteAllEntities(CurrentObject);
            attentionVM.PopUpVisibility = Visibility.Collapsed;
            //Console.WriteLine("OKAction");
            MessengerInstance.Send(CurrentObject, AppContext.CSRemovedMsg);
        }

        private void CancelAction(Object obj)
        {
        }

        #endregion

        #region CancelCommand
        public RelayCommand CancelCommand
        {
            get;
            private set;
        }

        void ExecCancel()
        {
            if (CurrentObject != null && CurrentObject.Id != 0)
                _dataService.GetControlSpace(CurrentObject.Id, (item, error) =>
                {
                    if (error != null) { return; }  // Report error here
                    var x = ControlSpaces.FirstOrDefault(p => p.Id == item.Id);
                    int i = ControlSpaces.IndexOf(x);
                    ControlSpaces.Remove(x);
                    ControlSpaces.Insert(i, item);
                    SelectedItem = item;    // Activate Normal UI
                });
        }

        #endregion

        #region Edit Command

        public RelayCommand EditCmd { get; private set; }

        void ExecEdit()
        {
            if (SelectedItem != null)
            {
                EditMode = true;
                AddUIState();
                MessengerInstance.Send("focus", "CSFocus");
            }
        }

        #endregion

        #endregion

        /*************************************************************/
    }

}
