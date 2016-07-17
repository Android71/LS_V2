using GalaSoft.MvvmLight.Command;
using LS_Designer_WPF.Model;
using LS_Designer_WPF.PopUpMessages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LS_Designer_WPF.ViewModel
{
    public class LightZonesVM : TabItemVM
    {
        public LightZonesVM(IDataService dataService)
        {
            _dataService = dataService;
            TabName = "Light Zones";

            MasterAddCmd = new RelayCommand(MasterExecAdd, MasterCanExecAdd);
            MasterRemoveCmd = new RelayCommand(MasterExecRemove, MasterCanExecRemove);
            MasterEditCmd = new RelayCommand(MasterExecEdit);
            MasterSaveCmd = new RelayCommand(MasterExecSave);
            MasterCancelCmd = new RelayCommand(MasterExecCancel);
            MasterTestCmd = new RelayCommand(MasterExecTest, MasterCanExecTest);

            ProxyUpCmd = new RelayCommand(ProxyExecUp, ProxyCanExecUp);
            ProxyDownCmd = new RelayCommand(ProxyExecDown, ProxyCanExecDown);

            ViewCmd = new RelayCommand(ExecViewCmd, CanExecViewCmd);

            MessengerInstance.Register<string>(this, AppContext.LE_LinkToZoneChangedMsg, LE_LinkToZoneChanged);

            TabItemEnabled = false;
        }

        public override void Refresh()
        {
            MasterSelectedItem = null;
            DetailList = null;
            SelectedProxy = null;
            ProxyList = null;
            Load();
        }

        void Load()
        {
            if (AppContext.ControlSpace != null)
            {
                _dataService.GetPartitions((data, error) =>
                {
                    if (error != null) { return; } // Report error here
                    Partitions = new List<Partition>(data);
                });

                _dataService.GetLightZones(AppContext.ControlSpace, AppContext.Partition, (data, error) =>
                {
                    if (error != null) { return; } // Report error here
                    MasterList = data;
                });

                //_dataService.GetPartitionLightElements(AppContext.ControlSpace, AppContext.Partition, (data, error) =>
                //{
                //    if (error != null) { return; } // Report error here
                //    DetailList = data;
                //});

                ////DetailContentVisibility = Visibility.Hidden;
                //MasterObjectPanelVisibility = Visibility.Collapsed;
            }
        }
    



        #region Message Handlers

        protected override void ContextChanged(string obj)
        {
            if (AppContext.ControlSpace != null && AppContext.Partition != null)
            {
                TabItemEnabled = true;
                if (IsSelected)
                    Refresh();

            }
        }

        private void LE_LinkToZoneChanged(string obj)
        {
            if (DetailSelectedItem.LE_Proxy == null)
            {
                //Link Operation

                int ix = ProxyList.Count;
                _dataService.LinkToZone(DetailSelectedItem, MasterSelectedItem, ix, (leProxy, error) =>
                {
                    if (error != null) { return; } // Report error here
                    {
                        DetailSelectedItem.LE_Proxy = leProxy;
                        ProxyList.Add(leProxy);
                    }
                });

                if (MasterSelectedItem.PointType != DetailSelectedItem.PointType)
                {
                    MasterSelectedItem.PointType = DetailSelectedItem.PointType;

                    _dataService.UpdateLightZone(MasterSelectedItem, (i, er) => { });
                }
                
                MasterSelectedItem.DirectParent = true;

                DetailSelectedItem.RaiseLinkedToZoneChanged();
                MasterSelectedItem.RaiseHasChildrenChanged();
            }
            else
            {
                // Unlink
                _dataService.UnlinkFromZone(DetailSelectedItem, MasterSelectedItem, (updateCount, error) => { });
                LE_Proxy proxy = ProxyList.FirstOrDefault(p => p.LightElement.Id == DetailSelectedItem.Id);
                ProxyList.Remove(proxy);
                int i = 0;
                foreach(LE_Proxy leProxy in ProxyList)
                {
                    leProxy.Ix = i;
                    i++;
                }
                DetailSelectedItem.LE_Proxy = null;
                MasterSelectedItem.DirectParent = false;
                DetailSelectedItem.RaiseLinkedToZoneChanged();
                MasterSelectedItem.RaiseHasChildrenChanged();
            }

        }


        #endregion

        #region View Command

        public RelayCommand ViewCmd { get; private set; }

        void ExecViewCmd()
        {
            //List<LightElement> leList = MasterList.Where(p => (p.ControlChannel == null) ? false : p.ControlChannel.Id == DetailSelectedItem.Id).ToList();
            //LE_VisualVM popupVM = new LE_VisualVM(leList.OrderBy(p => p.StartPoint).ToList(), string.Format($"LightElement mapping to {DetailSelectedItem.Name}"));
            //MessengerInstance.Send<EmptyPopUpVM>(popupVM, AppContext.ShowPopUpMsg);
        }

        bool CanExecViewCmd()
        {
            //return DetailSelectedItem != null && DetailSelectedItem.HasChildren && !MasterAddMode && !MasterEditMode;
            return false;
        }

        #endregion

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

        ObservableCollection<LightZone> _masterList = new ObservableCollection<LightZone>();
        public ObservableCollection<LightZone> MasterList
        {
            get { return _masterList; }
            set { Set(ref _masterList, value); }
        }

        int msix = -1; //MasterSelectedItem ix;

        //bool selectionFromDetail = false;

        LightZone _masterSelectedItem;
        public LightZone MasterSelectedItem
        {
            get { return _masterSelectedItem; }
            set
            {
                Set(ref _masterSelectedItem, value);

                //UpdateChangeLinkEnable();

                //if (selectionFromDetail)
                //{
                //    foreach (ControlChannel ch in DetailList)
                //    {
                //        if (MasterSelectedItem.ControlChannel != null)
                //            if (ch.Id == MasterSelectedItem.ControlChannel.Id)
                //                ch.DirectParent = true;
                //            else
                //                ch.DirectParent = false;
                //        else
                //            ch.DirectParent = false;
                //    }
                //    selectionFromDetail = false;
                //}
                //else
                //{
                foreach (LightZone lz in MasterList)
                    lz.DirectParent = false;

                if (MasterSelectedItem != null)
                {
                    //        foreach (ControlChannel ch in DetailList)
                    //        {
                    //            if (MasterSelectedItem.ControlChannel != null)
                    //                if (ch.Id == MasterSelectedItem.ControlChannel.Id)
                    //                    ch.DirectParent = true;
                    //                else
                    //                    ch.DirectParent = false;
                    //            else
                    //                ch.DirectParent = false;
                    //        }

                    msix = MasterList.IndexOf(value);
                    _dataService.GetLightZone(MasterSelectedItem.Id, (data, error) =>
                     {
                         if (error != null) { return; } // Report error here
                                 MasterCurrentObject = data;
                     });

                    ProxyList = MasterSelectedItem.LE_ProxyList;

                    _dataService.GetZoneLightElements(MasterSelectedItem, (data, error) =>
                    {
                        if (error != null) { return; } // Report error here
                        DetailList = data;
                    });

                    MasterObjectPanelVisibility = Visibility.Visible;
                    MasterObjectCurtainVisibility = Visibility.Visible;

                    //        MasterRemoveCmd.RaiseCanExecuteChanged();

                    //        if (MasterSelectedItem.IsLinked)
                    //        {
                    //            selectionFromMaster = true;
                    //            DetailSelectedItem = DetailList.FirstOrDefault(p => p.Id == MasterSelectedItem.ControlChannel.Id);
                    //        }
                    //    }
                    //    else
                    //    {
                    //        msix = -1;
                    //        MasterObjectPanelVisibility = Visibility.Collapsed;
                    //    }
                }
            }
        }


        LightZone _masterCurrentObject;
        public LightZone MasterCurrentObject
        {
            get { return _masterCurrentObject; }
            set { Set(ref _masterCurrentObject, value); }
        }

        #endregion

        /*************************************************************/

        #region LEproxy

        #region Properties

        ObservableCollection<LE_Proxy> _proxyList;
        public ObservableCollection<LE_Proxy> ProxyList
        {
            get { return _proxyList; }
            set { Set(ref _proxyList, value); }
        }

        LE_Proxy _selectedProxy;
        public LE_Proxy SelectedProxy
        {
            get { return _selectedProxy; }
            set
            {
                Set(ref _selectedProxy, value);
                ProxyUpCmd.RaiseCanExecuteChanged();
                ProxyDownCmd.RaiseCanExecuteChanged();
            }
        }

        

        Visibility _proxyListCurtainVisibility = Visibility.Collapsed;
        public Visibility ProxyListCurtainVisibility
        {
            get { return _proxyListCurtainVisibility; }
            set { Set(ref _proxyListCurtainVisibility, value); }
        }

        #endregion

        #region Commands

        #region UpCmd

        public RelayCommand ProxyUpCmd { get; private set; }

        void ProxyExecUp()
        {
            int ix = ProxyList.IndexOf(SelectedProxy);
            LE_Proxy prevProxy = ProxyList[ix - 1];
            int ix1 = SelectedProxy.Ix;
            _dataService.SwapProxy(SelectedProxy, prevProxy);
            SelectedProxy.Ix = prevProxy.Ix;
            prevProxy.Ix = ix1;
            ProxyList[ix - 1] = SelectedProxy;
            ProxyList[ix] = prevProxy;
            SelectedProxy = ProxyList[ix - 1];
        }

        bool ProxyCanExecUp()
        {
            if (SelectedProxy == null)
                return false;
            if (SelectedProxy.Ix == 0)
                return false;
            return true;
        }

        #endregion

        #region DownCmd

        public RelayCommand ProxyDownCmd { get; private set; }

        void ProxyExecDown()
        {
            int ix = ProxyList.IndexOf(SelectedProxy);
            LE_Proxy nextProxy = ProxyList[ix + 1];
            int ix1 = SelectedProxy.Ix;
            _dataService.SwapProxy(SelectedProxy, nextProxy);
            SelectedProxy.Ix = nextProxy.Ix;
            nextProxy.Ix = ix1;
            ProxyList[ix + 1] = SelectedProxy;
            ProxyList[ix] = nextProxy;
            SelectedProxy = ProxyList[ix + 1];
        }

        bool ProxyCanExecDown()
        {
            if (SelectedProxy == null)
                return false;
            if (SelectedProxy.Ix == ProxyList.Count - 1)
                return false;
            return true;
        }

        #endregion

        #endregion

        #endregion

        /*************************************************************/

        #region Detail Properties

        ObservableCollection<LightElement> _detailList;
        public ObservableCollection<LightElement> DetailList
        {
            get { return _detailList; }
            set { Set(ref _detailList, value); }
        }

        int dsix = -1; //DetailSelectedItem ix
        //bool selectionFromMaster;

        LightElement _detailSelectedItem;
        public LightElement DetailSelectedItem
        {
            get { return _detailSelectedItem; }
            set
            {
                Set(ref _detailSelectedItem, value);

                if (DetailSelectedItem != null)
                {
                    dsix = DetailList.IndexOf(value);
                    _dataService.GetLightElement(DetailSelectedItem.Id, (data, error) =>
                     {
                         if (error != null) { return; } // Report error here
                         DetailCurrentObject = data;
                     });
                    DetailObjectPanelVisibility = Visibility.Visible;

                    DetailCurrentObject.Partitions = Partitions;
                    DetailCurrentObject.Partition = Partitions.Find(p => p.Id == DetailSelectedItem.Partition.Id);

                    if (DetailSelectedItem.IsLinked)
                    {
                        List<LightZone> lzList = null;
                        _dataService.GetLightElementZones(DetailSelectedItem, (data, error) =>
                         {
                             if (error != null) { return; } // Report error here
                             lzList = data;
                         });
                        foreach (LightZone zone in MasterList)
                        {
                            var x = lzList.FirstOrDefault(p => p.Id == zone.Id);
                            if (x != null)
                                zone.DirectParent = true;
                            else
                                zone.DirectParent = false;
                        }
                    }
                    UpdateChangeLinkEnable();
                    
                }
                else
                {
                    dsix = -1;
                    DetailObjectPanelVisibility = Visibility.Collapsed;
                }
                
            }
        }

        void UpdateChangeLinkEnable()
        {
            if (MasterSelectedItem.LE_ProxyList.Count != 0 &&
                MasterSelectedItem.PointType != DetailSelectedItem.PointType)
                DetailSelectedItem.ChangeLinkEnable = false;
            else
                DetailSelectedItem.ChangeLinkEnable = true;
        } 

        LightElement _detailCurrentObject;
        public LightElement DetailCurrentObject
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

        #endregion

        /*************************************************************/

        #region Master Commands

        #region Master Save Command

        public RelayCommand MasterSaveCmd { get; private set; }

        void MasterExecSave()
        {
            int i = -1;
            
            _dataService.UpdateLightZone(MasterCurrentObject, (updatedCount, error) =>
            {
                if (error != null) { return; } // Report error here
                    i = updatedCount;
            });

            MasterObjectButtonsVisibility = Visibility.Collapsed;
            MasterListCurtainVisibility = Visibility.Collapsed;
            DetailListCurtainVisibility = Visibility.Collapsed;
            ProxyListCurtainVisibility = Visibility.Collapsed;


            if (MasterAddMode)
            {
                MasterList.Add(MasterCurrentObject);
                MasterSelectedItem = MasterCurrentObject;
            }
            //    if (MasterEditMode) //in EditMode MasterSelectedItem always not null
            //    {
            //        if (!partitionChanged)
            //        {
            //            MasterList[msix] = MasterCurrentObject;
            //            //MasterSelectedItem.RaiseIsLinkedChanged();
            //            MasterSelectedItem = MasterCurrentObject;
            //            //if (savedDsix != -1)
            //            //    DetailSelectedItem = DetailList[savedDsix];
            //        }
            //        else
            //        {
            //            MasterList.Remove(MasterSelectedItem);
            //            //DetailContentVisibility = Visibility.Hidden;
            //            partitionChanged = false;
            //        }
            //    }

            MasterAddMode = false;
            MasterEditMode = false;
            MasterRemoveCmd.RaiseCanExecuteChanged();
            MasterAddCmd.RaiseCanExecuteChanged();

            //    //if (DetailSelectedItem != null)
            //    //{
            //    //    // при выполнении Save происходит замена .net объекта в MasterList
            //    //    // необходимо восстановить Visual State
            //    //    LightElement letmp = MasterSelectedItem;
            //    //    ControlChannel tmp = DetailSelectedItem;
            //    //    DetailSelectedItem = null;
            //    //    DetailSelectedItem = tmp;
            //    //    MasterSelectedItem = letmp;
            //    //}
            //}
            MessengerInstance.Send("", AppContext.UnBlockUIMsg);
        }

        

        #endregion

        #region Master Cancel Command

        public RelayCommand MasterCancelCmd { get; private set; }

        void MasterExecCancel()
        {
            //if (MasterEditMode)
            //{
            //    _dataService.GetLightElement(MasterSelectedItem.Id, (data, error) =>
            //         {
            //             if (error != null) { return; } // Report error here
            //             MasterCurrentObject = data;
            //         });
            //}
            //MasterAddMode = false;
            //MasterEditMode = false;
            //MasterCurrentObject.IsEditMode = false;
            //MasterCurrentObject.IsAddMode = false;
            //MasterAddCmd.RaiseCanExecuteChanged();
            //MasterRemoveCmd.RaiseCanExecuteChanged();

            //DetailListCurtainVisibility = Visibility.Collapsed;

            //MasterListCurtainVisibility = Visibility.Collapsed;
            //MasterObjectCurtainVisibility = Visibility.Visible;

            //MasterObjectButtonsVisibility = Visibility.Collapsed;
            //MasterListVisibility = Visibility.Visible;

            //if (MasterSelectedItem != null)
            //{
            //    _dataService.GetLightElement(MasterSelectedItem.Id, (data, error) =>
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
            //if (MasterSelectedItem.IsLinked)
            //{
            //    ControlChannel ch = DetailList.FirstOrDefault(p => p.Id == MasterSelectedItem.ControlChannel.Id);
            //    if (ch.LE_Count != 1)
            //    {
            //        PopUpMessageVM popupVM = new PopUpMessageVM(AppMessages.LE_EditingMsg());
            //        MessengerInstance.Send<EmptyPopUpVM>(popupVM, AppContext.ShowPopUpMsg);
            //        return;
            //    }
            //}


            //MasterEditMode = true;

            //DetailListCurtainVisibility = Visibility.Visible;

            //MasterCurrentObject.IsEditMode = true;
            //MasterCurrentObject.Partitions = Partitions;
            //MasterCurrentObject.Partition = Partitions.Find(p => p.Id == AppContext.Partition.Id);

            //MasterAddCmd.RaiseCanExecuteChanged();
            //MasterRemoveCmd.RaiseCanExecuteChanged();
            ////MainSwitchCmd.RaiseCanExecuteChanged();

            //MasterListCurtainVisibility = Visibility.Visible;
            //MasterObjectButtonsVisibility = Visibility.Visible;
            //MasterObjectCurtainVisibility = Visibility.Collapsed;
            //MessengerInstance.Send("", AppContext.BlockUIMsg);
        }

        #endregion

        #region Master Add Command

        public RelayCommand MasterAddCmd { get; private set; }

        void MasterExecAdd()
        {
            MasterAddMode = true;

            MasterCurrentObject = new LightZone();
            MasterCurrentObject.PointType = PointTypeEnum.W;
            MasterCurrentObject.Partition = AppContext.Partition;
            MasterCurrentObject.ControlSpace = AppContext.ControlSpace;
            MasterCurrentObject.LE_ProxyList = new ObservableCollection<LE_Proxy>();

            MasterAddCmd.RaiseCanExecuteChanged();
            MasterRemoveCmd.RaiseCanExecuteChanged();
            ViewCmd.RaiseCanExecuteChanged();

            MasterObjectPanelVisibility = Visibility.Visible;
            MasterObjectButtonsVisibility = Visibility.Visible;

            MasterListCurtainVisibility = Visibility.Visible;
            DetailListCurtainVisibility = Visibility.Visible;
            MasterObjectCurtainVisibility = Visibility.Collapsed;

            ProxyListCurtainVisibility = Visibility.Visible;

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
            //_dataService.DeleteLightElement(MasterSelectedItem, (data, error) =>
            //    {
            //        if (error != null) { return; } // Report error here
            //        int updateCount = data;
            //    });
            MasterList.Remove(MasterSelectedItem);
            MasterSelectedItem = null;
        }

        bool MasterCanExecRemove()
        {
            return !MasterAddMode && !MasterEditMode && MasterSelectedItem != null;
        }

        #endregion

        #region Master Test Command

        public RelayCommand MasterTestCmd { get; private set; }

        void MasterExecTest()
        {
            //_dataService.DeleteLightElement(MasterSelectedItem, (data, error) =>
            //    {
            //        if (error != null) { return; } // Report error here
            //        int updateCount = data;
            //    });
            //MasterList.Remove(MasterSelectedItem);
            //MasterSelectedItem = null;
        }

        bool MasterCanExecTest()
        {
            return false;
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

        #endregion

    }
}
