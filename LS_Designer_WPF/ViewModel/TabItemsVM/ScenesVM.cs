using GalaSoft.MvvmLight;
using LS_Designer_WPF.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS_Designer_WPF.ViewModel
{ 
    public class ScenesVM : ViewModelBase
    {
        IDataService _dataService;
        public ScenesVM(IDataService dataService)
        {
            _dataService = dataService;
        }

        ObservableCollection<Scene> _sceneList;
        ObservableCollection<Scene> SceneList
        {
            get { return _sceneList; }
            set { Set(ref _sceneList, value); }
        }

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
