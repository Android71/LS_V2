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
    public class ScenesVM : TabItemVM
    {
        public ScenesVM(IDataService dataService)
        {
            _dataService = dataService;
            TabName = "Light Zones";

            SceneAddCmd = new RelayCommand(SceneExecAdd, SceneCanExecAdd);
            SceneRemoveCmd = new RelayCommand(SceneExecRemove, SceneCanExecRemove);
            SceneEditCmd = new RelayCommand(SceneExecEdit, SceneCanExecEdit);
            SceneCancelCmd = new RelayCommand(SceneExecCancel);
            SceneSaveCmd = new RelayCommand(SceneExecSave);

            AppContext.Partition = new Partition();
            AppContext.Partition.Id = 1;
            Load();
        }

        void Load()
        {
            
            _dataService.GetScenes(AppContext.Partition, (sceneList, Exception) =>
            {
                SceneList = sceneList;
            });
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

        int ssix;   //SelectedScene Index;

        Scene _selectedScene;
        public Scene SelectedScene
        {
            get { return _selectedScene; }
            set
            {
                Set(ref _selectedScene, value);
                
                if (SelectedScene != null)
                {
                    ssix = SceneList.IndexOf(SelectedScene);
                    SceneZones = SelectedScene.LightZones;

                    SceneObjectButtonsVisibility = Visibility.Collapsed;
                    SceneObjectPanelVisibility = Visibility.Visible;
                    SceneObjectCurtainVisibility = Visibility.Visible;
                    

                    if (!SceneAddMode && !SceneEditMode)
                    {
                        _dataService.GetScene(SelectedScene.Id, (scene, error) =>
                        {
                            if (error != null) { return; } // Report error here
                        CurrentScene = scene;
                        });
                    }
                    else
                    {
                        SceneAddMode = false;
                        SceneEditMode = false;
                    }
                }
                else
                {

                }

                SceneRemoveCmd.RaiseCanExecuteChanged();
                SceneAddCmd.RaiseCanExecuteChanged();
            }
        }

        Scene _currentScene;
        public Scene CurrentScene
        {
            get { return _currentScene; }
            set { Set(ref _currentScene, value); }
        }

        bool SceneAddMode { get; set; } = false;

        bool SceneEditMode { get; set; } = false;

        bool AccentAddMode { get; set; } = false;

        bool AccentEditMode { get; set; } = false;

        #endregion

        #region Commands

        #region Add Command

        public RelayCommand SceneAddCmd { get; private set; }

        void SceneExecAdd()
        {
            SceneAddMode = true;

            CurrentScene = new Scene();
            CurrentScene.Partition = AppContext.Partition;
            CurrentScene.LightZones = new ObservableCollection<LightZone>();

            SceneAddCmd.RaiseCanExecuteChanged();
            SceneRemoveCmd.RaiseCanExecuteChanged();

            SceneObjectPanelVisibility = Visibility.Visible;
            SceneObjectButtonsVisibility = Visibility.Visible;
            SceneObjectCurtainVisibility = Visibility.Collapsed;

            SceneListVisibility = Visibility.Hidden;
            SceneListCurtainVisibility = Visibility.Visible;

            MessengerInstance.Send("", AppContext.BlockUIMsg);
        }

        bool SceneCanExecAdd()
        {
            return !SceneAddMode && !SceneEditMode;
        }

        #endregion

        #region Edit Command

        public RelayCommand SceneEditCmd { get; private set; }

        void SceneExecEdit()
        {
            SceneEditMode = true;

            SceneAddCmd.RaiseCanExecuteChanged();
            SceneRemoveCmd.RaiseCanExecuteChanged();

            SceneObjectPanelVisibility = Visibility.Visible;
            SceneObjectButtonsVisibility = Visibility.Visible;
            SceneObjectCurtainVisibility = Visibility.Collapsed;

            SceneListCurtainVisibility = Visibility.Visible;
            MessengerInstance.Send("", AppContext.BlockUIMsg);
        }

        bool SceneCanExecEdit()
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
            return !SceneAddMode && !SceneEditMode && SelectedScene != null;
        }

        #endregion

        #region Cancel Command

        public RelayCommand SceneCancelCmd { get; private set; }

        void SceneExecCancel()
        {
            if (SceneAddMode)
            {
                SceneObjectButtonsVisibility = Visibility.Collapsed;
                SceneObjectPanelVisibility = Visibility.Collapsed;
                SceneListCurtainVisibility = Visibility.Collapsed;
                SceneListVisibility = Visibility.Visible;
            }

            if (SceneEditMode)
            {
                _dataService.GetScene(SelectedScene.Id, (scene, error) =>
                        {
                            if (error != null) { return; } // Report error here
                            CurrentScene = scene;
                        });
                SceneObjectButtonsVisibility = Visibility.Collapsed;
                SceneListCurtainVisibility = Visibility.Collapsed;
                SceneObjectCurtainVisibility = Visibility.Visible;
            }

            SceneAddMode = false;
            SceneEditMode = false;
            SceneAddCmd.RaiseCanExecuteChanged();
            SceneEditCmd.RaiseCanExecuteChanged();
            SceneRemoveCmd.RaiseCanExecuteChanged();

            MessengerInstance.Send("", AppContext.UnBlockUIMsg);
        }

        #endregion

        #region Save Command

        public RelayCommand SceneSaveCmd { get; private set; }

        void SceneExecSave()
        {
            _dataService.UpdateScene(CurrentScene, (updateCount, error) => { });

            SceneListCurtainVisibility = Visibility.Collapsed;

            if (SceneAddMode)
            {
                SceneList.Add(CurrentScene);
                SceneListVisibility = Visibility.Visible;
            }

            if (SceneEditMode)
            {
                CurrentScene.DirectParent = SceneList[ssix].DirectParent;
                SceneList[ssix] = CurrentScene;
            }

            SelectedScene = CurrentScene;

            MessengerInstance.Send("", AppContext.UnBlockUIMsg);
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

        Visibility _sceneListVisibility = Visibility.Visible;
        public Visibility SceneListVisibility
        {
            get { return _sceneListVisibility; }
            set { Set(ref _sceneListVisibility, value); }
        }

        Visibility _sceneObjectButtonsVisibility = Visibility.Collapsed;
        public Visibility SceneObjectButtonsVisibility
        {
            get { return _sceneObjectButtonsVisibility; }
            set { Set(ref _sceneObjectButtonsVisibility, value); }
        }

        Visibility _sceneObjectCurtainVisibility = Visibility.Visible;
        public Visibility SceneObjectCurtainVisibility
        {
            get { return _sceneObjectCurtainVisibility; }
            set { Set(ref _sceneObjectCurtainVisibility, value); }
        }

        Visibility _sceneObjectPanelVisibility = Visibility.Collapsed;
        public Visibility SceneObjectPanelVisibility
        {
            get { return _sceneObjectPanelVisibility; }
            set { Set(ref _sceneObjectPanelVisibility, value); }
        }

        #endregion

        #endregion

        /*************************************************************/

        #region SceneZones

        #region Properties

        ObservableCollection<LightZone> _sceneZones;
        public ObservableCollection<LightZone> SceneZones
        {
            get { return _sceneZones; }
            set { Set(ref _sceneZones, value); }
        }

        LightZone _selectedSceneZone;
        public LightZone SelectedSceneZone
        {
            get { return _selectedSceneZone; }
            set
            {
                Set(ref _selectedSceneZone, value);
            }
        }

        #endregion

        #region SceneZones UI State

        Visibility _sceneZonesCurtainVisibility = Visibility.Collapsed;
        public Visibility SceneZonesCurtainVisibility
        {
            get { return _sceneZonesCurtainVisibility; }
            set { Set(ref _sceneZonesCurtainVisibility, value); }
        }

        #endregion

        #endregion

        /*************************************************************/


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
