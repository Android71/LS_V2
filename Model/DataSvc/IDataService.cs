using System;
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
        void GetPartitionList(Action<List<Partition>, Exception> callback);

        #endregion

        /************************************************************/

        #region ControlSpaces

        void GetControlSpaces(Action<ObservableCollection<ControlSpace>, Exception> callback);

        void GetControlSpace(int id, Action<ControlSpace, Exception> callback);

        void UpdateControlSpace(ControlSpace item, Action<int, Exception> callback);

        #endregion

        /************************************************************/



        //#region ControlDevice

        //void GetControlDevices(ControlSpace space, Action<BindingList<ControlDevice>, Exception> callback, bool includeChannels = true);

        //#endregion

        //void GetEnvironmentItems(int controlSpaceId, DeviceTypeEnum deviceType, Action<List<EnvironmentItem>, Exception> callback);

        //void GetEnvironmentItem(string model, Action<EnvironmentItem, Exception> callback);

        //void UpdateControlDevice(ControlDevice item, Action<int, Exception> callback);

        //#region ControlChannel

        //void GetControlChannels(ControlSpace space, LightElement le, FilterEnum filter, Action<List<ControlChannel>, Exception> callback, bool includeLE = true);

        //#endregion

        //void GetEventDevices(ControlSpace space, Partition partition, Action<ObservableNotifiableCollection<EventDevice>, Exception> callback);

        //void UpdateEventDevice(EventDevice item, Action<int, Exception> callback);



        ///********************************************************************/

        //#region LightElement

        //LightElement GetLightElement(int Id);

        //void GetLightElements(ControlSpace space, Partition partition, Action<ObservableNotifiableCollection<LightElement>, Exception> callback);

        //void GetLightElementsOfZone(LightZone zone, Partition partition, ControlSpace controlSpace,
        //                                   FilterEnum filter, Action<BindingList<LightElement>, Exception> callback);

        //void GetLinkedLightElements(ControlSpace space, ControlChannel channel, Action<List<LightElement>, Exception> callback);

        //void UpdateLightElement(LightElement item, Action<int, Exception> callback);

        //#endregion

        ///********************************************************************/

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
