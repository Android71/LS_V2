﻿using System;
//using Lighting.Library;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LS_Designer_WPF.Model.Enums;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace LS_Designer_WPF.Model
{
    public interface IDataService
    {
        /************************************************************/

        #region Partitions

        void GetPartitions(Action<ObservableCollection<Partition>, Exception> callback);
        void GetPartition(int id, Action<Partition, Exception> callback);
        void UpdatePartition(Partition item, Action<int, Exception> callback);
        void GetPartitionList(Action<List<Partition>, Exception> callback);

        #endregion

        /************************************************************/

        #region ControlSpaces

        void GetControlSpaces(Action<ObservableCollection<ControlSpace>, Exception> callback);

        void GetActiveControlSpaces(Action<ObservableCollection<ControlSpace>, Exception> callback);

        void GetControlSpace(int id, Action<ControlSpace, Exception> callback);

        void UpdateControlSpace(ControlSpace item, Action<int, Exception> callback);

        #endregion

        /************************************************************/

        #region EnvironmentItems

        void GetEnvironmentItems(int controlSpaceId, DeviceTypeEnum deviceType, Action<List<EnvironmentItem>, Exception> callback);

        #endregion

        /************************************************************/

        #region ControlDevice

        void UpdateControlDevice(ControlDevice item, Action<int, Exception> callback);

        void GetControlDevice(int id, Action<ControlDevice, Exception> callback);

        void GetControlDevices(ControlSpace space, Partition partition, Action<ObservableCollection<ControlDevice>, Exception> callback);
        //void GetControlDevices(ControlSpace space, Action<BindingList<ControlDevice>, Exception> callback, bool includeChannels = true);

        #endregion

        /************************************************************/

        #region ControlChannel

        void GetControlChannel(int ControlDeviceId, Action<ControlChannel, Exception> callback);

        void UpdateControlChannel(ControlChannel ch, Action<int, Exception> callback);

        void GetControlChannelList(ControlSpace space, Partition partition, Action<List<ControlChannel>, Exception> callback);

        #endregion

        /************************************************************/

        #region EventDevices

        void GetEventDevices(ControlSpace space, Partition partition, Action<ObservableCollection<EventDevice>, Exception> callback);

        void GetEventDevice(int id, Action<EventDevice, Exception> callback);

        void UpdateEventDevice(EventDevice item, Action<int, Exception> callback);

        #endregion

        /************************************************************/

        #region EventChannels

        void GetEventChannel(int id, Action<EventChannel, Exception> callback);

        // Only Partition change
        void UpdateEventChannel(EventChannel eCh, Action<int, Exception> callback);

        #endregion

        /************************************************************/

        #region LightZone

        void GetPartitionZones(Partition partition, Action<ObservableCollection<LightZone>, Exception> callback);

        void GetLightZones(ControlSpace space, Partition partition, Action<ObservableCollection<LightZone>, Exception> callback);

        void GetLightZone(int Id, Action<LightZone, Exception> callback);

        void UpdateLightZone(LightZone zone, Action<int, Exception> callback);

        #endregion

        /************************************************************/

        //void GetEnvironmentItems(int controlSpaceId, DeviceTypeEnum deviceType, Action<List<EnvironmentItem>, Exception> callback);

        //void GetEnvironmentItem(string model, Action<EnvironmentItem, Exception> callback);

        //void UpdateControlDevice(ControlDevice item, Action<int, Exception> callback);

        //#region ControlChannel

        //void GetControlChannels(ControlSpace space, LightElement le, FilterEnum filter, Action<List<ControlChannel>, Exception> callback, bool includeLE = true);

        //#endregion

        //void GetEventDevices(ControlSpace space, Partition partition, Action<ObservableNotifiableCollection<EventDevice>, Exception> callback);

        //void UpdateEventDevice(EventDevice item, Action<int, Exception> callback);

        /********************************************************************/

        #region LE_Type

        void GetLE_TypeList(ControlSpace space, Action<List<LE_Type>, Exception> callback);

        #endregion

        /********************************************************************/

        #region LightElement

        void GetLightElement(int Id, Action<LightElement, Exception> callback);

        void GetLightElements(ControlSpace space, Partition partition, Action<ObservableCollection<LightElement>, Exception> callback);

        void GetZoneLightElements(/*ControlSpace space, Partition partition,*/ LightZone zone, Action<ObservableCollection<LightElement>, Exception> callback);

        //void GetPartitionLightElements(ControlSpace space, Partition partition, Action<ObservableCollection<LightElement>, Exception> callback);

        //void GetLightElementsOfZone(LightZone zone, Partition partition, ControlSpace controlSpace,
        //                                   FilterEnum filter, Action<BindingList<LightElement>, Exception> callback);

        void UpdateLightElement(LightElement item, Action<int, Exception> callback);

        void LinkToChannel(LightElement le, ControlChannel ch, Action<int, Exception> callback);

        void LinkToZone(LightElement le, LightZone zone, int ix, Action<LE_Proxy, Exception> callback);

        void UnlinkFromZone(LightElement le, LightZone zone, Action<int, Exception> callback);

        void UnlinkFromChannel(LightElement le, Action<int, Exception> callback);

        void DeleteLightElement(LightElement le, Action<int, Exception> callback);

        #endregion

        /********************************************************************/

        #region LE_Proxy

        void GetLightElementZones(LightElement le, Action<List<LightZone>, Exception> callback);

        void SwapProxy(LE_Proxy proxy1, LE_Proxy proxy2);

        #endregion

        /********************************************************************/

        #region Scene

        void GetScenes(Partition partition, Action<ObservableCollection<Scene>, Exception> callback);

        void GetScene(int sceneId, Action<Scene, Exception> callback);

        void UpdateScene(Scene scene, Action<int, Exception> callback);

        #endregion

        /********************************************************************/

        //void GetLE_Types(ControlSpace space, Action<List<LE_Type>, Exception> callback);

        //void AddLinkToLightElement(ControlChannel controlChannel, LightElement le);

        //void GetUnlinkedLE_OfZone(LightZone zone, Action<BindingList<LightElement>, Exception> callback);

        //void RemoveLinkFromLightElement(LightElement le, ControlChannel controlChannel);

        //bool IsLinked(LightElement le, ControlChannel controlChannel);

        ///********************************************************************/

        //#region LightZone

        //void GetLightZones(Partition partition, ControlSpace space, Action<BindingList<LightZone>, Exception> callback);

        //bool ZoneContainsLE(LightZone zone, LightElement lightElement);

        //void UpdateLightZone(LightZone item, Action<int, Exception> callback);

        //void AddLE_ToZone(LightZone zone, LE_Proxy leProxy);

        //void RemoveLE_FromZone(LightZone zone, LE_Proxy leProxy);

        //#endregion

        ///********************************************************************/

        //#region LE_Proxy

        //void UpdateLE_Proxy(LE_Proxy leProxy, Action<int, Exception> callback);

        //#endregion

        ///********************************************************************/

        //#region Gamma

        //void UpdateGamma(Gamma gamma, Action<int, Exception> callback);

        //#endregion

        ///********************************************************************/
    }
}
