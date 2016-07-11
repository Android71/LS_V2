using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
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
    public class ScenesVM : ViewModelBase
    {
        IDataService _dataService;
        public ScenesVM(IDataService dataService)
        {
            _dataService = dataService;
        }

        /*************************************************************/

        #region Scene

        #region Properties

        ObservableCollection<Scene> _sceneList;
        public ObservableCollection<Scene> SceneList
        {
            get { return _sceneList; }
            set { Set(ref _sceneList, value); }
        }

        Scene _selectedScene;
        public Scene SelectedScene
        {
            get { return _selectedScene; }
            set { Set(ref _selectedScene, value); }
        }

        bool SceneAddMode { get; set; } = false;

        bool SceneEditMode { get; set; } = false;

        #endregion

        #region Commands

        #region Add Command

        public RelayCommand SceneAddCmd { get; private set; }

        void SceneExecAdd()
        {
            
            MessengerInstance.Send("", AppContext.BlockUIMsg);
        }

        bool SceneCanExecAdd()
        {
            return !SceneAddMode && !SceneEditMode;
        }

        #endregion

        #region Remove Command

        public RelayCommand SceneRemoveCmd { get; private set; }

        void SceneExecRemove()
        {

            MessengerInstance.Send("", AppContext.BlockUIMsg);
        }

        bool SceneCanExecRemove()
        {
            return !SceneAddMode && !SceneEditMode;
        }

        #endregion

        #region Cancel Command

        public RelayCommand SceneCancelCmd { get; private set; }

        void SceneExecCancel()
        {

            MessengerInstance.Send("", AppContext.BlockUIMsg);
        }

        bool SceneCanExecCancel()
        {
            return !SceneAddMode && !SceneEditMode;
        }

        #endregion

        #region Save Command

        public RelayCommand SceneSaveCmd { get; private set; }

        void SceneExecSave()
        {

            MessengerInstance.Send("", AppContext.BlockUIMsg);
        }

        bool SceneCanExecSave()
        {
            return !SceneAddMode && !SceneEditMode;
        }

        #endregion

        #region Accent Add Command

        public RelayCommand AccentAddCmd { get; private set; }

        void AccentExecAdd()
        {
            MessengerInstance.Send("", AppContext.BlockUIMsg);
        }

        bool AccentCanExecAdd()
        {
            return !SceneAddMode && !SceneEditMode;
        }

        #endregion

        #region Accent Remove Command

        public RelayCommand AccentRemoveCmd { get; private set; }

        void AccentExecRemove()
        {
            MessengerInstance.Send("", AppContext.BlockUIMsg);
        }

        bool AccentCanExecRemove()
        {
            return !SceneAddMode && !SceneEditMode;
        }

        #endregion

        #endregion

        #region Scene UI State

        Visibility _sceneListCurtainVisibility = Visibility.Collapsed;
        public Visibility SceneListCurtainVisibility
        {
            get { return _sceneListCurtainVisibility; }
            set { Set(ref _sceneListCurtainVisibility, value); }
        }

        Visibility _sceneObjectButtonsVisibility = Visibility.Collapsed;
        public Visibility SceneObjectButtonsVisibility
        {
            get { return _sceneObjectButtonsVisibility; }
            set { Set(ref _sceneObjectButtonsVisibility, value); }
        }

        #endregion

        #endregion

        /*************************************************************/

        ObservableCollection<LightZone> _sceneZones;
        ObservableCollection<LightZone> SceneZones
        {
            get { return _sceneZones; }
            set { Set(ref _sceneZones, value); }
        }

        ObservableCollection<LightZone> _zoneList;
        ObservableCollection<LightZone> ZoneList
        {
            get { return _zoneList; }
            set { Set(ref _zoneList, value); }
        }

        ObservableCollection<Scene> _sceneAccents;
        ObservableCollection<Scene> SceneAccents
        {
            get { return _sceneAccents; }
            set { Set(ref _sceneAccents, value); }
        }

    }
}
