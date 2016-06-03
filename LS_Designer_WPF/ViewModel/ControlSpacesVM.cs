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

            Refresh();
        }

        public override void Refresh()
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
            //Messenger.Default.Send(new NotificationMessage("DoSomething"), messgeToken);
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
                    CurrentObject = SelectedItem;
            }
        }

        private ControlSpace _currentObject = null;
        public ControlSpace CurrentObject
        {
            get { return _currentObject; }
            set { Set<ControlSpace>(ref _currentObject, value); }
        }


        /*************************************************************/

        #region Commands

        #region SaveCommand
        public RelayCommand SaveCommand
        {
            get;
            private set;
        }

        void ExecSave()
        {
            int ix = -1;
            if (CurrentObject != null)
                _dataService.UpdateControlSpace(CurrentObject, (id, error) =>
                {
                    if (error != null) { return; }  // Report error here
                    ix = id;
                });
            Refresh();
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
                    if (error != null)
                    {
                        // Report error here
                        return;
                    }
                    var x = ControlSpaces.FirstOrDefault(p => p.Id == item.Id);
                    int i = ControlSpaces.IndexOf(x);
                    ControlSpaces.Remove(x);
                    ControlSpaces.Insert(i, item);
                    SelectedItem = item;
                });
        }

        #endregion

        #endregion

        /*************************************************************/
    }

}
