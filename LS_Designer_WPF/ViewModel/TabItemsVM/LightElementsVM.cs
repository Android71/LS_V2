using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LS_Designer_WPF.Model;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using LS_Designer_WPF.PopUpMessages;
using System;
using System.ComponentModel;
using LS_Library;

namespace LS_Designer_WPF.ViewModel
{
    public class LightElementsVM : TabItemVM
    {
        public LightElementsVM(IDataService dataService)
        {
            _dataService = dataService;
            TabName = "Light Elements";

            MasterAddCmd = new RelayCommand(MasterExecAdd, MasterCanExecAdd);
            MasterRemoveCmd = new RelayCommand(MasterExecRemove, MasterCanExecRemove);
            MasterEditCmd = new RelayCommand(MasterExecEdit);
            MasterSaveCmd = new RelayCommand(MasterExecSave);
            MasterCancelCmd = new RelayCommand(MasterExecCancel);
            MasterTestCmd = new RelayCommand(MasterExecTest, MasterCanExecTest);

            ViewCmd = new RelayCommand(ExecViewCmd, CanExecViewCmd);

            TabItemEnabled = false;
        }

        public override void Refresh()
        {
            MasterSelectedItem = null;
            DetailSelectedItem = null;
            Load();
        }

        void Load()
        {
            if (AppContext.ControlSpace != null)
            {
                _dataService.GetLE_TypeList(AppContext.ControlSpace, (data, error) =>
                {
                    if (error != null) { return; } // Report error here
                    MasterSelectorList = data;
                });

                _dataService.GetPartitions((data, error) =>
                {
                    if (error != null) { return; } // Report error here
                    Partitions = new List<Partition>(data);
                });

                _dataService.GetLightElements(AppContext.ControlSpace, AppContext.Partition, (data, error) =>
                {
                    if (error != null) { return; } // Report error here
                    foreach (LightElement le in data)
                    {
                        LightElementVM leVM = new LightElementVM(le);
                        leVM.IsLinked = le.ControlChannel != null;
                        leVM.IsLinkedChanged = LinkingLogic;
                        MasterList.Add(leVM);
                    }
                });

                _dataService.GetControlChannelList(AppContext.ControlSpace, AppContext.Partition, (data, error) =>
                {
                    if (error != null) { return; } // Report error here
                    DetailList = data;
                });
                MasterObjectPanelVisibility = Visibility.Collapsed;
            }
        }

        protected override void ContextChanged(string obj)
        {
            if (AppContext.ControlSpace != null && AppContext.Partition != null)
            {
                TabItemEnabled = true;
                if (IsSelected)
                    Refresh();

            }
        }

        #region Link/Unlink 

        void LinkingLogic(bool value)
        {
            bool x;
            if (value)
            {
                //Link
                if (MasterSelectedItem.Model.ControlChannel == null)
                {
                    _dataService.LinkToChannel(MasterSelectedItem.Model, DetailSelectedItem, (updatesCount, error) =>
                    {
                        if (error != null) { return; } // Report error here
                        int uc = updatesCount;
                    });
                    MasterSelectedItem.Model.ControlChannel = DetailSelectedItem;
                    MasterSelectedItem.DirectChild = true;
                    DetailSelectedItem.DirectParent = true;
                    if (DetailSelectedItem.LE_Count == 0)
                        DetailSelectedItem.PointType = MasterSelectedItem.Model.PointType;
                    DetailSelectedItem.LE_Count++;
                }
            }
            else
            {
                //Unlink
                if (MasterSelectedItem.Model.ControlChannel != null)
                {
                    _dataService.UnlinkFromChannel(MasterSelectedItem.Model, (updatesCount, error) =>
                    {
                        if (error != null) { return; } // Report error here
                        int uc = updatesCount;
                    });
                    MasterSelectedItem.Model.ControlChannel = null;
                    MasterSelectedItem.DirectChild = false;
                    DetailSelectedItem.LE_Count--;
                    if (DetailSelectedItem.LE_Count == 0)
                    {
                        DetailSelectedItem.DirectParent = false;
                    }
                }
            }
        }

        void MarkDirectInDetail()
        {
            if (DetailList != null)
            {
                foreach (ControlChannel ch in DetailList)
                {
                    if (MasterSelectedItem.Model.ControlChannel != null)
                        if (ch.Id == MasterSelectedItem.Model.ControlChannel.Id)
                            ch.DirectParent = true;
                        else
                            ch.DirectParent = false;
                    else
                        ch.DirectParent = false;
                }
            }
        }

        void MarkDirectInMaster()
        {
            if (MasterList != null)
            {
                foreach (LightElementVM leVM in MasterList)
                {
                    if (leVM.Model.ControlChannel != null)
                    {
                        if (leVM.Model.ControlChannel.Id == DetailSelectedItem.Id)
                            leVM.DirectChild = true;
                        else
                            leVM.DirectChild = false;
                    }
                }
            }
        }

        void UpdateChangeLinkEnable()
        {
            LightElementVM master = MasterSelectedItem;
            ControlChannel detail = DetailSelectedItem;

            if (master != null && DetailSelectedItem != null)
            {
                if (master.DirectChild)
                {
                    master.ChangeLinkEnable = true;
                    return;
                }

                if (!master.IsLinked && detail.LE_Count == 0)
                {
                    master.ChangeLinkEnable = true;
                    return;
                }

                if (!master.IsLinked &&
                      detail.LE_Count > 0 &&
                      detail.Multilink &&
                      master.Model.PointType == detail.PointType)
                {
                    master.ChangeLinkEnable = true;
                    return;
                }

                if (master.IsLinked && !detail.DirectParent)
                {
                    master.ChangeLinkEnable = false;
                    return;
                }
            }
            else
            {
                if (master != null)
                    master.ChangeLinkEnable = false;
            }
        }

        #endregion

        #region Message Handlers

        


        bool CheckIntersectionLE()
        {
            bool result = false;
            //List<LightElement> leList = MasterList.Where(p => (p.ControlChannel == null) ? false : p.ControlChannel.Id == DetailSelectedItem.Id).ToList();  
            //foreach (LightElement le in leList)
            //{
            //    if (MasterSelectedItem.StartPoint >= le.StartPoint && MasterSelectedItem.StartPoint <= le.EndPoint ||
            //        MasterSelectedItem.EndPoint >= le.StartPoint && MasterSelectedItem.EndPoint <= le.EndPoint)
            //    {
            //        result = true;
            //        MasterSelectedItem.InConflict = true;
            //        MasterSelectedItem.SetSilentIsLinked(false, true);
            //        leList.Add(MasterSelectedItem);
            //        LE_VisualVM popupVM = new LE_VisualVM(leList.OrderBy(p => p.StartPoint).ToList(), string.Format($"Conflict in {DetailSelectedItem.Name}"));
            //        MessengerInstance.Send<EmptyPopUpVM>(popupVM, AppContext.ShowPopUpMsg);
            //        break;
            //    }
            //    else
            //        MasterSelectedItem.InConflict = false;
            //}      
            return result;
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
            return DetailSelectedItem != null && DetailSelectedItem.HasChildren 
                   && DetailSelectedItem.Multilink && !MasterAddMode && !MasterEditMode;
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

        

        bool _masterAddMode = false;
        public bool MasterAddMode
        {
            get { return _masterAddMode; }
            set { Set(ref _masterAddMode, value); }
        }

        bool _masterEditMode = false;
        public bool MasterEditMode
        {
            get { return _masterEditMode; }
            set { Set(ref _masterEditMode, value); }
        }

        List<LightElementVM> _masterList = new List<LightElementVM>();
        public List<LightElementVM> MasterList
        {
            get { return _masterList; }
            set { Set(ref _masterList, value); }
        }

        int msix = -1; //MasterSelectedItem ix

        LightElementVM _masterSelectedItem;
        public LightElementVM MasterSelectedItem
        {
            get { return _masterSelectedItem; }
            set
            {
                Set(ref _masterSelectedItem, value);

                MarkDirectInDetail();

                UpdateChangeLinkEnable();

                if (MasterSelectedItem != null)
                {
                    

                    MasterObjectPanelVisibility = Visibility.Visible;
                    MasterObjectCurtainVisibility = Visibility.Visible;

                    msix = MasterList.IndexOf(value);

                    _dataService.GetLightElement(MasterSelectedItem.Model.Id, (data, error) =>
                     {
                         if (error != null) { return; } // Report error here
                         MasterCurrentObject = new LightElementVM(data);
                     });

                    if (MasterCurrentObject.Model.ControlSpace.Prefix == "AN" || MasterSelectedItem.Model.ControlSpace.Prefix == "DX")
                    {
                        if (MasterCurrentObject.Model.PointType == LS_Library.PointTypeEnum.RGB)
                            MasterCurrentObject.ColorSequenceList = LightElement.ColorSequenseRGB;
                        if (MasterCurrentObject.Model.PointType == LS_Library.PointTypeEnum.RGBW)
                            MasterCurrentObject.ColorSequenceList = LightElement.ColorSequenseRGBW;
                    }
                }

                //else
                //{
                //    msix = -1;
                //    MasterObjectPanelVisibility = Visibility.Collapsed;
                //}
                //MasterRemoveCmd.RaiseCanExecuteChanged();

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
                //    foreach (LightElement le in MasterList)
                //        le.DirectChild = false;

                //    if (MasterSelectedItem != null)
                //    {
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

                //        if (MasterSelectedItem.IsLinked)
                //        {
                //            selectionFromMaster = true;
                //            DetailSelectedItem = DetailList.FirstOrDefault(p => p.Id == MasterSelectedItem.ControlChannel.Id);
                //        }
                //    }
                //}
            }
        }

        

        LightElementVM _masterCurrentObject;
        public LightElementVM MasterCurrentObject
        {
            get { return _masterCurrentObject; }
            set { Set(ref _masterCurrentObject, value); }
        }

        #region Add LightElement

        #region Master Add Command

        public RelayCommand MasterAddCmd { get; private set; }

        void MasterExecAdd()
        {
            MasterListButtonsVisibility = Visibility.Collapsed;
            MasterListVisibility = Visibility.Hidden;
            MasterSelectorVisibility = Visibility.Visible;
            MasterObjectPanelVisibility = Visibility.Hidden;
            IsMasterSelectorOpen = true;

            DetailListCurtainVisibility = Visibility.Visible;

            MasterAddMode = true;
            ViewCmd.RaiseCanExecuteChanged();

            MessengerInstance.Send("", AppContext.BlockUIMsg);
        }

        bool MasterCanExecAdd()
        {
            return !MasterAddMode && !MasterEditMode;
        }

        #endregion


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
                    LE_Type leType = (LE_Type)MasterSelectorSelectedItem;
                    //MasterCurrentObject = new LightElement(leType.PointType, AppContext.ControlSpace);
                    MasterCurrentObject = CreateLightElementVM(leType.PointType);
                    MasterCurrentObject.Partition = Partitions.Find(p => p.Id == AppContext.Partition.Id);
                    MasterCurrentObject.Partitions = Partitions;

                    MasterObjectPanelVisibility = Visibility.Visible;

                    MasterAddMode = true;
                    MasterCurrentObject.IsAddMode = true;
                    MasterAddCmd.RaiseCanExecuteChanged();
                    MasterRemoveCmd.RaiseCanExecuteChanged();
                    ViewCmd.RaiseCanExecuteChanged();

                    MasterSelectorVisibility = Visibility.Hidden;
                    MasterSelectorSelectedItem = null;
                    MasterListVisibility = Visibility.Hidden;
                    MasterListButtonsVisibility = Visibility.Visible;

                    MasterObjectPanelVisibility = Visibility.Visible;
                    MasterObjectButtonsVisibility = Visibility.Visible;

                    MasterListCurtainVisibility = Visibility.Visible;
                    //DetailListCurtainVisibility = Visibility.Visible;
                    MasterObjectCurtainVisibility = Visibility.Collapsed;

                    return;
                }
                if (!IsMasterSelectorOpen)
                {
                    MasterListButtonsVisibility = Visibility.Visible;
                    MasterSelectorVisibility = Visibility.Hidden;
                    MasterListVisibility = Visibility.Visible;

                    MasterAddMode = false;
                    ViewCmd.RaiseCanExecuteChanged();

                    DetailListCurtainVisibility = Visibility.Collapsed;

                    if (MasterSelectedItem != null)
                    {
                        MasterObjectPanelVisibility = Visibility.Visible;
                    }
                    else
                        MasterObjectPanelVisibility = Visibility.Collapsed;
                    MessengerInstance.Send("", AppContext.UnBlockUIMsg);
                }
            }
        }

        LightElementVM CreateLightElementVM(PointTypeEnum pointType)
        {
            LightElementVM leVM = null;
            LightElement le = new LightElement(pointType, AppContext.ControlSpace);
            le.Partition = AppContext.Partition;
            
            leVM = new LightElementVM(le);

            if (leVM.Model.ControlSpace.Prefix == "AN" || leVM.Model.ControlSpace.Prefix == "DX")
            {
                if (leVM.Model.PointType == PointTypeEnum.RGB)
                    leVM.ColorSequenceList = LightElement.ColorSequenseRGB;
                if (leVM.Model.PointType == PointTypeEnum.RGBW)
                    leVM.ColorSequenceList = LightElement.ColorSequenseRGBW;
            }

            return leVM;
        }

        #endregion

        #region Edit LightElement



        #endregion

        #endregion

        /*************************************************************/

        #region Detail Properties

        List<ControlChannel> _detailList;
        public List<ControlChannel> DetailList
        {
            get { return _detailList; }
            set { Set(ref _detailList, value); }
        }

        int dsix = -1; //DetailSelectedItem ix
        bool selectionFromMaster;

        ControlChannel _detailSelectedItem;
        public ControlChannel DetailSelectedItem
        {
            get { return _detailSelectedItem; }
            set
            {
                Set(ref _detailSelectedItem, value);

                MarkDirectInMaster();

                UpdateChangeLinkEnable();
                ViewCmd.RaiseCanExecuteChanged();

                if (DetailSelectedItem != null)
                {
                    //dsix = DetailList.IndexOf(value);
                    //_dataService.GetControlChannel(DetailSelectedItem.Id, (data, error) =>
                    // {
                    //     if (error != null) { return; } // Report error here
                    //     DetailCurrentObject = data;
                    // });
                    //DetailObjectPanelVisibility = Visibility.Visible;

                    //DetailCurrentObject.Partitions = Partitions;
                    //DetailCurrentObject.Partition = Partitions.Find(p => p.Id == DetailSelectedItem.Partition.Id);

                    
                    //if (selectionFromMaster)
                    //{
                    //    foreach (LightElement le in MasterList)
                    //    {
                    //        if (le.ControlChannel != null)
                    //        {
                    //            if (le.ControlChannel.Id == DetailSelectedItem.Id)
                    //                le.DirectChild = true;
                    //            else
                    //                le.DirectChild = false;
                    //        }
                    //    }
                    //    selectionFromMaster = false;
                    //}
                    //else
                    //{
                    //    foreach (ControlChannel ch in DetailList)
                    //        ch.DirectParent = false;

                    //    foreach (LightElement le in MasterList)
                    //    {
                    //        if (le.ControlChannel != null)
                    //        {
                    //            if (le.ControlChannel.Id == DetailSelectedItem.Id)
                    //                le.DirectChild = true;
                    //            else
                    //                le.DirectChild = false;
                    //        }
                    //    }

                    //    if (DetailSelectedItem.HasChildren)
                    //    {
                    //        LightElement le1 = MasterList.FirstOrDefault(p => (p.ControlChannel == null) ? false : p.ControlChannel.Id == value.Id);
                    //        selectionFromDetail = true;
                    //        MasterSelectedItem = le1;
                    //    }
                    //    else
                    //    {
                    //        if (MasterSelectedItem != null && MasterSelectedItem.IsLinked)
                    //            MasterSelectedItem = null;
                    //    }
                    //}
                }
                else
                {
                    dsix = -1;
                    DetailObjectPanelVisibility = Visibility.Collapsed;
                }
            }
        }

        

        ControlChannel _detailCurrentObject;
        public ControlChannel DetailCurrentObject
        {
            get { return _detailCurrentObject; }
            set { Set(ref _detailCurrentObject, value); }
        }

        #endregion

        /*************************************************************/


        #region Master UI State

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

        Visibility _detailContentVisibility = Visibility.Visible;
        public Visibility DetailContentVisibility
        {
            get { return _detailContentVisibility; }
            set { Set(ref _detailContentVisibility, value); }
        }

        #endregion

        /*************************************************************/

        #region Master Commands

        #region Master Save Command

        public RelayCommand MasterSaveCmd { get; private set; }

        void MasterExecSave()
        {
            //if (MasterCurrentObject.Validate())
            //{
            //    int i = -1;
            //    bool partitionChanged = false;
            //    if (MasterEditMode)
            //    {
            //        if (MasterCurrentObject.Partition.Id != MasterSelectedItem.Partition.Id)
            //        {
            //            partitionChanged = true;
            //        }
            //    }
            //    _dataService.UpdateLightElement(MasterCurrentObject, (updatedCount, error) =>
            //    {
            //        if (error != null) { return; } // Report error here
            //        i = updatedCount;
            //    });

            //    MasterObjectButtonsVisibility = Visibility.Collapsed;
            //    MasterListCurtainVisibility = Visibility.Collapsed;
            //    DetailListCurtainVisibility = Visibility.Collapsed;


            //    if (MasterAddMode)
            //    {
            //        MasterSelectorSelectedItem = null;

            //        MasterList.Add(MasterCurrentObject);
            //        MasterSelectedItem = MasterCurrentObject;
            //        MasterListVisibility = Visibility.Visible;
            //    }
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

            //    MasterAddMode = false;
            //    MasterEditMode = false;
            //    MasterCurrentObject.IsEditMode = false;
            //    MasterCurrentObject.IsAddMode = false;
            //    MasterRemoveCmd.RaiseCanExecuteChanged();
            //    MasterAddCmd.RaiseCanExecuteChanged();
            //    //MainSwitchCmd.RaiseCanExecuteChanged();

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

        //AttentionVM attentionVM;

        //private void OKCallbackAction(Object obj)
        //{
        //    //// Пользователь подтвердил изъятие ControlSpace из модели
        //    //// DeleteAllEntities(CurrentObject); // Операция удаления объектов ссылающихся на ControlSpace

        //    //attentionVM.PopUpVisibility = Visibility.Collapsed;
        //    //MessengerInstance.Send(CurrentObject, AppContext.CSRemovedMsg); // обновление ControlSpaces в MainViewModel
        //}

        //private void CancelCallbackAction(Object obj)
        //{
        //    //attentionVM.PopUpVisibility = Visibility.Collapsed;
        //    //ExecCancel();
        //}

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
            MessengerInstance.Send("", AppContext.UnBlockUIMsg);
        }

        #endregion

        #region Master Edit Command

        public RelayCommand MasterEditCmd { get; private set; }

        void MasterExecEdit()
        {
            // Если LightElement IsLinked, то редактирование возможно только если он первый в ControlChannel
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


        #region Master Remove Command

        public RelayCommand MasterRemoveCmd { get; private set; }

        void MasterExecRemove()
        {
            //_dataService.DeleteLightElement(MasterSelectedItem, (data, error) =>
            //    {
            //        if (error != null) { return; } // Report error here
            //        int updateCount = data;
            //    });
            //MasterList.Remove(MasterSelectedItem);
            //MasterSelectedItem = null;
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

        Visibility _detailListButtonsVisibility = Visibility.Visible;
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

        #endregion

    }
}