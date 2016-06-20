using System;
//using Lighting.Library;
using EFData;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using LS_Designer_WPF.Model.Enums;
using System.ComponentModel;
using EF_Connect;
using System.Collections.ObjectModel;

namespace LS_Designer_WPF.Model
{
    public class DataService : IDataService
    {
        /****************************************************************/

        #region Partitions

        public void GetPartitions(Action<ObservableCollection<Partition>, Exception> callback)
        {
            var x = new ObservableCollection<Partition>();
            Partition partition = null;

            using (var db = new LSModelContainer(LS.CS))
            {

                foreach (EFData.Partition dbPartition in db.Partitions)
                {
                    partition = new Partition();
                    Mapper.Db2O(dbPartition, partition);
                    x.Add(partition);
                }
            }
            callback(x, null);
        }

        public void GetPartition(int id, Action<Partition, Exception> callback)
        {
            EFData.Partition dbPartition;

            using (var db = new LSModelContainer(LS.CS))
                dbPartition = db.Partitions.FirstOrDefault(p => p.Id == id);

            Partition partition = new Partition();
            Mapper.Db2O(dbPartition, partition);
            callback(partition, null);
        }

        public void UpdatePartition(Partition item, Action<int, Exception> callback)
        {
            EFData.Partition dbPartition;
            int ix;
            if (item.Id != 0)
            {
                // Update
                using (var db = new LSModelContainer(LS.CS))
                {
                    dbPartition = db.Partitions.FirstOrDefault(p => p.Id == item.Id);
                    Mapper.O2Db(item, dbPartition);
                    ix = db.SaveChanges();
                }
                callback(ix, null);
            }
            else
            {
                // Create
                using (var db = new LSModelContainer(LS.CS))
                {
                    dbPartition = new EFData.Partition();
                    Mapper.O2Db(item, dbPartition);
                    db.Partitions.Add(dbPartition);
                    ix = db.SaveChanges();
                    item.Id = dbPartition.Id;
                }
                callback(ix, null);
            }
        }

        public void GetPartitionList(Action<List<Partition>, Exception> callback)
        {
            var x = new List<Partition>();
            Partition partition = null;

            using (var db = new LSModelContainer(LS.CS))
            {

                foreach (EFData.Partition dbPartition in db.Partitions)
                {
                    partition = new Partition();
                    Mapper.Db2O(dbPartition, partition);
                    x.Add(partition);
                }
            }
            callback(x, null);
        }

        #endregion

        /****************************************************************/

        #region ControlSpaces

        public void GetControlSpaces(Action<ObservableCollection<ControlSpace>, Exception> callback)
        {
            ControlSpace controlSpace;
            var x = new ObservableCollection<ControlSpace>();

            using (var db = new LSModelContainer(LS.CS))
            {
                foreach (EFData.ControlSpace dbControlSpace in db.ControlSpaces)
                {
                    controlSpace = new ControlSpace();
                    Mapper.Db2O(dbControlSpace, controlSpace);
                    x.Add(controlSpace);
                }
            }
            callback(x, null);
        }

        public void GetActiveControlSpaces(Action<ObservableCollection<ControlSpace>, Exception> callback)
        {
            ControlSpace controlSpace;
            var x = new ObservableCollection<ControlSpace>();

            using (var db = new LSModelContainer(LS.CS))
            {
                foreach (EFData.ControlSpace dbControlSpace in db.ControlSpaces.Where(p => p.IsActive))
                {
                    controlSpace = new ControlSpace();
                    Mapper.Db2O(dbControlSpace, controlSpace);
                    x.Add(controlSpace);
                }
            }
            callback(x, null);
        }

        public void GetControlSpace(int id, Action<ControlSpace, Exception> callback)
        {
            EFData.ControlSpace dbControlSpace;

            using (var db = new LSModelContainer(LS.CS))
                dbControlSpace = db.ControlSpaces.FirstOrDefault(p => p.Id == id);

            ControlSpace controlSpace = new ControlSpace();
            Mapper.Db2O(dbControlSpace, controlSpace);
            callback(controlSpace, null);
        }

        public void UpdateControlSpace(ControlSpace item, Action<int, Exception> callback)
        {
            EFData.ControlSpace dbControlSpace;
            int ix;
            if (item.Id != 0)
            {
                // Update
                using (var db = new LSModelContainer(LS.CS))
                {
                    dbControlSpace = db.ControlSpaces.FirstOrDefault(p => p.Id == item.Id);
                    Mapper.O2Db(item, dbControlSpace);
                    ix = db.SaveChanges();
                }
                callback(ix, null);
            }
            else
            {
                // Create
                using (var db = new LSModelContainer(LS.CS))
                {
                    dbControlSpace = new EFData.ControlSpace();
                    Mapper.O2Db(item, dbControlSpace);
                    db.ControlSpaces.Add(dbControlSpace);
                    ix = db.SaveChanges();
                    item.Id = dbControlSpace.Id;
                }
                callback(ix, null);
            }
        }

        #endregion

        /****************************************************************/

        //        #region 


        /****************************************************************/

        #region ControlDevices



        public void GetControlDevices(int csId, Action<ObservableCollection<ControlDevice>, Exception> callback)
        {
            //    IEnumerable<EFData.ControlDevice> query = null;
            ObservableCollection<ControlDevice> list = new ObservableCollection<ControlDevice>();
            ControlDevice controlDevice = null;
            ControlChannel controlChannel = null;

            using (var db = new LSModelContainer(LS.CS))
            {
                //var x = 
                foreach (EFData.ControlDevice dbControlDevice in
                               db.ControlDevices.Include("ControlChannels").Where(p => p.ControlSpace.Id == csId))
                {
                    controlDevice = new ControlDevice();
                    Mapper.Db2O(dbControlDevice, out controlDevice);
                    controlDevice.ControlSpace = new ControlSpace();
                    Mapper.Db2O(dbControlDevice.ControlSpace, controlDevice.ControlSpace);

                    foreach (EFData.ControlChannel dbCh in dbControlDevice.ControlChannels)
                    {
                        controlChannel = new ControlChannel();
                        Mapper.Db2O(dbCh, out controlChannel);
                        controlChannel.ControlSpace = controlDevice.ControlSpace;
                        controlDevice.ControlChannels.Add(controlChannel);
                    }
                    list.Add(controlDevice);
                }
            }
            callback(list, null);
        }

        public void GetControlDevice(int id, Action<ControlDevice, Exception> callback)
        {
            EFData.ControlDevice dbControlDevice;
            ControlDevice controlDevice = new ControlDevice();
            ControlChannel controlChannel = new ControlChannel();

            using (var db = new LSModelContainer(LS.CS))
            {
                dbControlDevice = db.ControlDevices.Include("ControlChannels").FirstOrDefault(p => p.Id == id);
                Mapper.Db2O(dbControlDevice, out controlDevice);
                controlDevice.ControlSpace = new ControlSpace();
                Mapper.Db2O(dbControlDevice.ControlSpace, controlDevice.ControlSpace);
                foreach (EFData.ControlChannel dbCh in dbControlDevice.ControlChannels)
                {
                    //if (ch is EFData.ArtNetControlChannel)
                    //    controlChannel = new ArtNetControlChannel();
                    Mapper.Db2O(dbCh, out controlChannel);
                    controlChannel.ControlSpace = new ControlSpace();
                    Mapper.Db2O(dbCh.ControlSpace, controlChannel.ControlSpace);
                    controlDevice.ControlChannels.Add(controlChannel);
                }
            }

            
            callback(controlDevice, null);
        }

        public void UpdateControlDevice(ControlDevice item, Action<int, Exception> callback)
        {
            EFData.ControlDevice dbControlDevice;
            //EFData.ControlSpace dbControlSpace;
            EFData.ControlChannel dbControlChannel;
            //ControlDevice controlDevice = null;
            //if (item.ControlSpace.Name == )
            int ix = -1;
            if (item.Id != 0)
            {
                //Update
                using (var db = new LSModelContainer(LS.CS))
                {
                    dbControlDevice = db.ControlDevices.FirstOrDefault(p => p.Id == item.Id);
                    var x = dbControlDevice.ControlChannels.ToList();
                    Mapper.O2Db(item, dbControlDevice);
                    for (int i = 0; i < item.ControlChannels.Count; i++)
                    {
                        Mapper.O2Db(item.ControlChannels[i], x[i]);
                    }
                    ix = db.SaveChanges();
                }
                callback(ix, null);
            }
            else
            {
                // Create
                using (var db = new LSModelContainer(LS.CS))
                {
                    dbControlDevice = new EFData.ControlDevice();
                    Mapper.O2Db(item, dbControlDevice);
                    dbControlDevice.ControlSpace = db.ControlSpaces.FirstOrDefault(p => p.Id == item.ControlSpace.Id);
                    foreach (ControlChannel ch in item.ControlChannels)
                    {
                        ch.ControlSpace = item.ControlSpace;
                        dbControlChannel = new EFData.ControlChannel();
                        Mapper.O2Db(ch, dbControlChannel);
                        dbControlChannel.ControlSpace = dbControlDevice.ControlSpace;
                        dbControlDevice.ControlChannels.Add(dbControlChannel);
                    }
                    db.ControlDevices.Add(dbControlDevice);
                    ix = db.SaveChanges();
                    item.Id = dbControlDevice.Id;
                    
                    int i = 0;
                    foreach (EFData.ControlChannel dbCh in dbControlDevice.ControlChannels)
                    {
                        item.ControlChannels[i].Id = dbCh.Id;
                        i++;
                    }
                }
                callback(ix, null);
            }
        }


        #endregion

        /****************************************************************/

        #region ControlChannel

        public void GetControlChannel(int id, Action<ControlChannel, Exception> callback)
        {
            EFData.ControlChannel dbControlChannel = null;
            ControlChannel ch = new ControlChannel();

            using (var db = new LSModelContainer(LS.CS))
            {
                dbControlChannel = db.ControlChannels.FirstOrDefault(p => p.Id == id);
                Mapper.Db2O(dbControlChannel, out ch);
            }
            callback(ch, null);
        }

        public void UpdateControlChannel(ControlChannel ch, Action<int, Exception> callback)
        {
            int updateCount = -1;
            EFData.ControlChannel dbControlChannel = null;
            if (ch.Id != 0)
            {
                using (var db = new LSModelContainer(LS.CS))
                {
                    dbControlChannel = db.ControlChannels.FirstOrDefault(p => p.Id == ch.Id);
                    Mapper.O2Db(ch, dbControlChannel);
                    updateCount = db.SaveChanges();
                }
                callback(updateCount, null);
            }
        }

        //public void GetControlChannels(ControlSpace space, LightElement le, FilterEnum filter, Action<List<ControlChannel>, Exception> callback, bool includeLE = true)
        //{
        //    ControlChannel controlChannel;
        //    var x = new List<ControlChannel>();

        //    using (var db = new LSModelContainer(LS.CS))
        //    {
        //        foreach (EFData.ControlChannel dbControlChannel in db.ControlChannels.Where(p => p.ControlDevice.ControlSpace.Id == space.Id))
        //        {
        //            if (dbControlChannel is EFData.ArtNetControlChannel)
        //                controlChannel = new ArtNetControlChannel();
        //            else
        //                controlChannel = new ControlChannel();
        //            Mapper.Db2O(dbControlChannel, controlChannel);
        //            if (dbControlChannel.ControlDevice.ControlSpace.Name == "ArtNet_DMX")
        //                controlChannel.ControlDevice = new ArtNetControlDevice();
        //            else
        //                controlChannel.ControlDevice = new GenericControlDevice();
        //            Mapper.Db2O(dbControlChannel.ControlDevice, controlChannel.ControlDevice);
        //            controlChannel.LinkCount = dbControlChannel.LightElements.Count;
        //            if (filter == FilterEnum.Linked)
        //            {
        //                if (controlChannel.LinkCount != 0)
        //                {
        //                    controlChannel.IsLinked = true;
        //                }
        //            }
        //            if (filter == FilterEnum.All)
        //            {
        //                if (controlChannel.LinkCount != 0)
        //                    controlChannel.IsLinked = true;

        //                x.Add(controlChannel);
        //                continue;
        //            }
        //            if (filter == FilterEnum.Linked)
        //            {
        //                if (le != null)
        //                {
        //                    EFData.LightElement dbLightElement = db.LightElements.FirstOrDefault(p => p.Id == le.Id);
        //                    if (dbLightElement != null)
        //                    {
        //                        if (dbControlChannel.LightElements.Contains(dbLightElement))
        //                        {
        //                            controlChannel.IsLinked = true;
        //                            x.Add(controlChannel);
        //                        }
        //                        continue;
        //                    }
        //                }
        //            }
        //            if (filter == FilterEnum.Unlinked)
        //            {
        //                if (controlChannel.LinkCount == 0)
        //                    x.Add(controlChannel);
        //            }
        //        }

        //    }
        //    callback(x, null);
        //}

        #endregion


        /****************************************************************/

        #region EnvironmentItems

        public void GetEnvironmentItems(int controlSpaceId, DeviceTypeEnum deviceType, Action<List<EnvironmentItem>, Exception> callback)
        {
            var x = new List<EnvironmentItem>();
            EnvironmentItem environmentItem = null;

            using (var db = new LSModelContainer(LS.CS))
            {

                foreach (EFData.CSEnvItem dbCSEnvItem in db.CSEnvItems.
                               Where(p => (p.ControlSpace.Id == controlSpaceId) && ((int)p.EnvironmentItem.DeviceType == (int)deviceType)))
                {
                    environmentItem = new EnvironmentItem();
                    Mapper.Db2O(dbCSEnvItem.EnvironmentItem, environmentItem);
                    x.Add(environmentItem);
                }
            }
            callback(x, null);
        }

        //public void GetEnvironmentItem(string model, Action<EnvironmentItem, Exception> callback)
        //{
        //    EnvironmentItem item = null;
        //    //EFData.EnvironmentItem dbItem = null;
        //    using (var db = new LSModelContainer(LS.CS))
        //    {
        //        EFData.EnvironmentItem dbItem = db.EnvironmentItems.FirstOrDefault(p => p.Model == model);
        //        item = new EnvironmentItem();
        //        Mapper.Db2O(dbItem, item);
        //    }

        //    callback(item, null);
        //}

        #endregion

        ///****************************************************************/

        //#region ControlChannel

        //public void GetControlChannels(ControlSpace space, LightElement le, FilterEnum filter, Action<List<ControlChannel>, Exception> callback, bool includeLE = true)
        //{
        //    ControlChannel controlChannel;
        //    var x = new List<ControlChannel>();

        //    using (var db = new LSModelContainer(LS.CS))
        //    {
        //        foreach (EFData.ControlChannel dbControlChannel in db.ControlChannels.Where(p => p.ControlDevice.ControlSpace.Id == space.Id))
        //        {
        //            if (dbControlChannel is EFData.ArtNetControlChannel)
        //                controlChannel = new ArtNetControlChannel();
        //            else
        //                controlChannel = new ControlChannel();
        //            Mapper.Db2O(dbControlChannel, controlChannel);
        //            if (dbControlChannel.ControlDevice.ControlSpace.Name == "ArtNet_DMX")
        //                controlChannel.ControlDevice = new ArtNetControlDevice();
        //            else
        //                controlChannel.ControlDevice = new GenericControlDevice();
        //            Mapper.Db2O(dbControlChannel.ControlDevice, controlChannel.ControlDevice);
        //            controlChannel.LinkCount = dbControlChannel.LightElements.Count;
        //            if (filter == FilterEnum.Linked)
        //            {
        //                if (controlChannel.LinkCount != 0)
        //                {
        //                    controlChannel.IsLinked = true;
        //                }
        //            }
        //            if (filter == FilterEnum.All)
        //            {
        //                if (controlChannel.LinkCount != 0)
        //                    controlChannel.IsLinked = true;

        //                x.Add(controlChannel);
        //                continue;
        //            }
        //            if (filter == FilterEnum.Linked)
        //            {
        //                if (le != null)
        //                {
        //                    EFData.LightElement dbLightElement = db.LightElements.FirstOrDefault(p => p.Id == le.Id);
        //                    if (dbLightElement != null)
        //                    {
        //                        if (dbControlChannel.LightElements.Contains(dbLightElement))
        //                        {
        //                            controlChannel.IsLinked = true;
        //                            x.Add(controlChannel);
        //                        }
        //                        continue;
        //                    }
        //                }
        //            }
        //            if (filter == FilterEnum.Unlinked)
        //            {
        //                if (controlChannel.LinkCount == 0)
        //                    x.Add(controlChannel);
        //            }
        //        }

        //    }
        //    callback(x, null);
        //}

        //#endregion

        /****************************************************************/

        #region EventDevice

        public void GetEventDevices(ControlSpace space, Partition partition, Action<ObservableCollection<EventDevice>, Exception> callback)
        {
            IEnumerable<EFData.EventDevice> y = null;
            var x = new ObservableCollection<EventDevice>();
            EventDevice eventDevice = null;
            EventChannel eventChannel = null;

            //using (var db = new LSModelContainer(LS.CS))
            //{
            //    if (space != null)
            //        y = db.EventDevices.Where(p => p.ControlSpace.Id == space.Id && p.Partition.Id == partition.Id);
            //    else
            //        y = db.EventDevices;
            //    foreach (EFData.EventDevice dbEventDevice in y)
            //    {
            //        eventDevice = new EventDevice();

            //        Mapper.Db2O(dbEventDevice, eventDevice);
            //        foreach (EFData.EventChannel ch in dbEventDevice.EventChannels)
            //        {

            //            eventChannel = new EventChannel();
            //            Mapper.Db2O(ch, eventChannel);
            //            eventDevice.EventChannels.Add(eventChannel);
            //        }

            //        x.Add(eventDevice);
            //    }
            //}
            callback(x, null);

        }

        public void UpdateEventDevice(EventDevice item, Action<int, Exception> callback)
        {
            EFData.EventDevice dbEventDevice;
            ////EFData.ControlSpace dbControlSpace;
            EFData.EventChannel dbEventChannel;
            //EventSource eventSource = null;
            ////EventDevice eventDevice = null;
            ////if (item.ControlSpace.Name == )
            int ix = -1;
            //if (item.Id != 0)
            //{
            //    // Update if !IsModeChanged 
            //    if (item.NewEventChannels.Count == 0)
            //    {
            //        using (var db = new LSModelContainer(LS.CS))
            //        {
            //            dbEventDevice = db.EventDevices.FirstOrDefault(p => p.Id == item.Id);
            //            var x = dbEventDevice.EventChannels.ToList();
            //            Mapper.O2Db(item, dbEventDevice);
            //            //db.Entry(dbEventDevice).State = EntityState.Modified;
            //            for (int i = 0; i < item.EventChannels.Count; i++)
            //            {
            //                Mapper.O2Db(item.EventChannels[i], x[i]);
            //            }
            //            ix = db.SaveChanges();
            //        }
            //    }
            //    else
            //    {
            //        // Update if IsModeChanged 
            //        using (var db = new LSModelContainer(LS.CS))
            //        {
            //            dbEventDevice = db.EventDevices.FirstOrDefault(p => p.Id == item.Id);
            //            var x = dbEventDevice.EventChannels.ToList();
            //            foreach (EFData.EventChannel eCh in x)
            //                db.Entry(eCh).State = EntityState.Deleted;
            //            Mapper.O2Db(item, dbEventDevice);
            //            for (int i = 0; i < item.NewEventChannels.Count; i++)
            //            {
            //                dbEventChannel = new EFData.EventChannel();
            //                Mapper.O2Db(item.NewEventChannels[i], dbEventChannel);

            //                dbEventChannel.Event = db.Events.FirstOrDefault(p => p.Name == dbEventChannel.EventName);
            //                dbEventDevice.EventChannels.Add(dbEventChannel);
            //            }
            //            ix = db.SaveChanges();
            //        }
            //    }
            //    callback(ix, null);
            //}
            //else
            //{
            //    // Create
            //    using (var db = new LSModelContainer(LS.CS))
            //    {
            //        dbEventDevice = new EFData.EventDevice();

            //        Mapper.O2Db(item, dbEventDevice);
            //        dbEventDevice.ControlSpace = db.ControlSpaces.FirstOrDefault(p => p.Id == item.ControlSpace.Id);
            //        dbEventDevice.Partition = db.Partitions.FirstOrDefault(p => p.Id == item.Partition.Id);

            //        foreach (EventChannel ch in item.EventChannels)
            //        {
            //            dbEventChannel = new EFData.EventChannel();
            //            Mapper.O2Db(ch, dbEventChannel);
            //            dbEventChannel.Event = db.Events.FirstOrDefault(p => p.Name == ch.EventName);
            //            dbEventDevice.EventChannels.Add(dbEventChannel);
            //        }
            //        db.EventDevices.Add(dbEventDevice);
            //        try
            //        {
            //            ix = db.SaveChanges();
            //        }
            //        catch (Exception)
            //        {
            //            //var x = 5;
            //        }
            //        item.Id = dbEventDevice.Id;
            //    }
            //    callback(ix, null);
            //}
        }


        #endregion



        //#region LightElement

        //public void GetLightElementsOfZone(LightZone zone, Partition partition, ControlSpace controlSpace, 
        //                                   FilterEnum filter, Action<BindingList<LightElement>, Exception> callback)
        //{
        //    IList<LightElement> list = new List<LightElement>();
        //    LightElement lightElement = null;
        //    IEnumerable<EFData.LightElement> query = null;

        //    using (var db = new LSModelContainer(LS.CS))
        //    {

        //        query = db.LightElements.Include("LightZones").Where( p => p.Partition.Id == partition.Id );

        //        foreach (EFData.LightElement dbLightElement in query)
        //        {
        //            //if (dbLightElement.LightZones.FirstOrDefault(p => p.Id == zone.Id) == null)
        //            //{
        //            //    if (dbLightElement is EFData.LightStrip)
        //            //        lightElement = new LightStrip();
        //            //    else
        //            //        lightElement = new LightElement();
        //            //    Mapper.Db2O(dbLightElement, lightElement);
        //            //    lightElement.IsLinked = false;
        //            //    lightElement.LinkCount = 0;
        //            //    list.Add(lightElement);
        //            //}
        //        }
        //        callback(new BindingList<LightElement>(list.OrderBy(p => p.QualifiedName).ToList()), null);
        //    }
        //}

        //public void GetUnlinkedLE_OfZone(LightZone zone, Action<BindingList<LightElement>, Exception> callback)
        //{
        //    IEnumerable<EFData.LightElement> query = null;
        //    IList<LightElement> list = new List<LightElement>();
        //    LightElement lightElement = null;
        //    LightZone lightZone = null;

        //    using (var db = new LSModelContainer(LS.CS))
        //    {
        //        query = db.LightElements.Include("LE_Proxies").Where(p => (p.Partition.Id == zone.Partition.Id) &&
        //                                                                  (p.ControlSpace.Id == zone.ControlSpace.Id));
        //        foreach (EFData.LightElement dbLightElement in query)
        //        {
        //            if (dbLightElement.LE_Proxies.Count == 0)
        //            {
        //                if (dbLightElement is EFData.LightStrip)
        //                    lightElement = new LightStrip();
        //                else
        //                    lightElement = new LightElement();
        //                Mapper.Db2O(dbLightElement, lightElement);
        //                list.Add(lightElement);
        //            }
        //            else
        //            {
        //                if (dbLightElement.LE_Proxies.FirstOrDefault(p => p.LightZone.Id == zone.Id) == null)
        //                {
        //                    if (dbLightElement is EFData.LightStrip)
        //                        lightElement = new LightStrip();
        //                    else
        //                        lightElement = new LightElement();
        //                    Mapper.Db2O(dbLightElement, lightElement);
        //                    lightElement.IsLinked = true;
        //                    lightElement.LinkCount = dbLightElement.LE_Proxies.Count;
        //                    list.Add(lightElement);
        //                }
        //            }
        //        }

        //    }
        //    callback(new BindingList<LightElement>(list.ToList()), null);
        //}

        //public LightElement GetLightElement(int Id)
        //{
        //    LightElement le = null;
        //    using (var db = new LSModelContainer(LS.CS))
        //    {
        //        EFData.LightElement dbLe = db.LightElements.FirstOrDefault(p => p.Id == Id);
        //        if (dbLe != null)
        //        {
        //            if (dbLe is EFData.LightStrip)
        //                le = new LightStrip();
        //            else
        //                le = new LightElement();
        //            Mapper.Db2O(dbLe, le);
        //        }
        //    }
        //    return le;
        //}

        //public void GetLightElements(ControlSpace space, Partition partition, Action<ObservableNotifiableCollection<LightElement>, Exception> callback)
        //{
        //    IEnumerable<EFData.LightElement> y = null;
        //    var x = new ObservableNotifiableCollection<LightElement>();
        //    LightElement lightElement = null;
        //    //EventChannel eventChannel = null;

        //    using (var db = new LSModelContainer(LS.CS))
        //    {
        //        if (space != null)
        //            y = db.LightElements.Where(p => p.ControlSpace.Id == space.Id && p.Partition.Id == partition.Id);
        //        //    else
        //        //        y = db.EventDevices;
        //        foreach (EFData.LightElement dbLightElement in y)
        //        {
        //            if (dbLightElement is EFData.LightStrip)
        //                lightElement = new LightStrip();
        //            else
        //                lightElement = new LightElement();
        //            Mapper.Db2O(dbLightElement, lightElement);
        //            lightElement.IsLinked = lightElement.ControlChannel != null;
        //            x.Add(lightElement);
        //        }
        //    }
        //    callback(x, null);
        //}

        //public void GetLinkedLightElements(ControlSpace space, ControlChannel channel, Action<List<LightElement>, Exception> callback)
        //{
        //    IEnumerable<EFData.LightElement> y = null;
        //    var x = new List<LightElement>();
        //    LightElement lightElement = null;
        //    //EventChannel eventChannel = null;

        //    using (var db = new LSModelContainer(LS.CS))
        //    {
        //        if (space != null)
        //            /*!!!!!!!!!!!!!!!!!!!!!!!!!!!
        //            y = db.LightElements.Where(p => p.ControlSpace.Id == space.Id &&
        //                                        p.ControlChannels.FirstOrDefault(c => c.Id == channel.Id) != null)
        //                                        .OrderBy(p => p.Partition.Name);

        //            !!!!!!!!!!!!!!!!!!!!!!!!!!!!!*/
        //            //    else
        //            //        y = db.EventDevices;
        //            y = db.ControlChannels.FirstOrDefault(p => p.Id == channel.Id).LightElements;
        //        foreach (EFData.LightElement dbLightElement in y)
        //        {
        //            if (dbLightElement is EFData.LightStrip)
        //                lightElement = new LightStrip();
        //            else
        //                lightElement = new LightElement();
        //            Mapper.Db2O(dbLightElement, lightElement);
        //            lightElement.IsLinked = lightElement.ControlChannel != null;
        //            x.Add(lightElement);
        //        }
        //    }
        //    callback(x, null);
        //}

        ////public void GetLinkedLightElements(ControlSpace space, Partition partition, Action<List<LightElement>, Exception> callback)
        ////{
        ////    IEnumerable<EFData.LightElement> y = null;
        ////    var x = new List<LightElement>();
        ////    LightElement lightElement = null;
        ////    //EventChannel eventChannel = null;

        ////    using (var db = new LSModelContainer(LS.CS))
        ////    {
        ////        if (space != null)
        ////            y = db.LightElements.Where(p => p.ControlSpace.Id == space.Id && p.Partition.Id == partition.Id);
        ////        //    else
        ////        //        y = db.EventDevices;
        ////        foreach (EFData.LightElement dbLightElement in y)
        ////        {
        ////            lightElement = new LightElement();
        ////            Mapper.Db2O(dbLightElement, lightElement);
        ////            x.Add(lightElement);
        ////        }
        ////    }
        ////    callback(x, null);
        ////}

        //public void AddLinkToLightElement(ControlChannel controlChannel, LightElement le )
        //{
        //    using (var db = new LSModelContainer(LS.CS))
        //    {
        //        EFData.LightElement dbLightElement = db.LightElements.FirstOrDefault(p => p.Id == le.Id);
        //        EFData.ControlChannel dbControlChannel = db.ControlChannels.FirstOrDefault(p => p.Id == controlChannel.Id);
        //        Mapper.O2Db(controlChannel, dbControlChannel);
        //        dbLightElement.ControlChannel = dbControlChannel;
        //        int i = db.SaveChanges();
        //    }
        //}

        //public void RemoveLinkFromLightElement(LightElement le, ControlChannel controlChannel)
        //{
        //    using (var db = new LSModelContainer(LS.CS))
        //    {
        //        EFData.LightElement dbLightElement = db.LightElements.FirstOrDefault(p => p.Id == le.Id);
        //        /*!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        //        EFData.ControlChannel dbControlChannel = dbLightElement.ControlChannels.FirstOrDefault(p => p.Id == controlChannel.Id);
        //        dbLightElement.ControlChannels.Remove(dbControlChannel);
        //        !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!*/
        //        //dbLightElement.ControlChannel = null;
        //        //db.Entry(dbLightElement.ControlChannel).State = EntityState.Deleted
        //        dbLightElement.ControlChannel.LightElements.Remove(dbLightElement);
        //        int i = db.SaveChanges();
        //    }
        //}

        //public bool IsLinked(LightElement le, ControlChannel controlChannel)
        //{
        //    using (var db = new LSModelContainer(LS.CS))
        //    {
        //        EFData.LightElement dbLightElement = db.LightElements.FirstOrDefault(p => p.Id == le.Id);
        //        /*!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        //        EFData.ControlChannel dbControlChannel = dbLightElement.ControlChannels.FirstOrDefault(p => p.Id == controlChannel.Id);

        //        if (dbLightElement.ControlChannels.Contains(dbControlChannel))
        //            return true;
        //        else
        //        !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!*/
        //        return false;
        //    }
        //}

        //public void UpdateLightElement(LightElement item, Action<int, Exception> callback)
        //{
        //    EFData.LightElement dbLightElement = null;
        //    int ix = -1;
        //    if (item.Id != 0)
        //    {
        //        //Update
        //        using (var db = new LSModelContainer(LS.CS))
        //        {
        //            dbLightElement = db.LightElements.FirstOrDefault(p => p.Id == item.Id);
        //            Mapper.O2Db(item, dbLightElement);
        //            EFData.Partition dbPartition = db.Partitions.FirstOrDefault(p => p.Id == item.Partition.Id);
        //            dbLightElement.Partition = dbPartition;
        //            ix = db.SaveChanges();
        //        }
        //        callback(ix, null);
        //    }
        //    else
        //    {
        //        // Create
        //        using (var db = new LSModelContainer(LS.CS))
        //        {
        //            if (item is LightStrip)
        //                dbLightElement = new EFData.LightStrip();
        //            else
        //                dbLightElement = new EFData.LightElement();
        //            Mapper.O2Db(item, dbLightElement);
        //            dbLightElement.ControlSpace = db.ControlSpaces.FirstOrDefault(p => p.Id == item.ControlSpace.Id);
        //            dbLightElement.Partition = db.Partitions.FirstOrDefault(p => p.Id == item.Partition.Id);
        //            db.LightElements.Add(dbLightElement);
        //            try
        //            {
        //                ix = db.SaveChanges();
        //                item.Id = dbLightElement.Id;
        //            }
        //            catch (Exception ex)
        //            {
        //                callback(ix, ex);
        //            }
        //        }
        //        callback(ix, null);
        //    }
        //}


        //#endregion

        ///****************************************************************/

        //#region LE_Type

        //public void GetLE_Types(ControlSpace space, Action<List<LE_Type>, Exception> callback)
        //{
        //    IEnumerable<EFData.LE_Type> y = null;
        //    LE_Type leType = null;
        //    var x = new List<LE_Type>();

        //    using (var db = new LSModelContainer(LS.CS))
        //    {
        //        if (space != null)
        //            y = db.LE_Types.Where(p => p.ControlSpace.Id == space.Id);
        //        else
        //            y = db.LE_Types;
        //        foreach (EFData.LE_Type dbLeType in y)
        //        {

        //            leType = new LE_Type();
        //            Mapper.Db2O(dbLeType, leType);
        //            x.Add(leType);
        //        }
        //    }
        //    callback(x, null);
        //}

        //#endregion

        ///****************************************************************/

        //#region LightZone

        //public void GetLightZones(Partition partition, ControlSpace space, Action<BindingList<LightZone>, Exception> callback)
        //{
        //    IEnumerable<EFData.LightZone> query = null;
        //    LightZone lightZone = null;
        //    LE_Proxy leProxy = null;
        //    var x = new BindingList<LightZone>();

        //    using (var db = new LSModelContainer(LS.CS))
        //    {
        //        if (partition != null)
        //            query = db.LightZones.Include("LE_Proxies").Where(p => ( p.Partition.Id == partition.Id ) &&
        //                                                                ( p.ControlSpace.Id == space.Id ));
        //        else
        //            query = db.LightZones.Include("LE_Proxies");
        //        foreach (EFData.LightZone dbLightZone in query)
        //        {
        //            lightZone = new LightZone();
        //            Mapper.Db2O(dbLightZone, lightZone);
        //            if (dbLightZone.LE_Proxies.Count != 0)
        //            {
        //                lightZone.LinkCount = dbLightZone.LE_Proxies.Count;
        //                lightZone.IsLinked = true;
        //            }
        //            lightZone.LE_Proxies = new List<LE_Proxy>();
        //            foreach (EFData.LE_Proxy dbLE_Proxy in dbLightZone.LE_Proxies)
        //            {
        //                leProxy = new LE_Proxy();
        //                Mapper.Db2O(dbLE_Proxy, leProxy);
        //                if (dbLE_Proxy.LightElement.LE_Proxies.Count != 0)
        //                {
        //                    leProxy.LightElement.IsLinked = true;
        //                    leProxy.LightElement.LinkCount = dbLE_Proxy.LightElement.LE_Proxies.Count;
        //                }
        //                lightZone.LE_Proxies.Add(leProxy);
        //            }
        //            if (lightZone.LE_Proxies.Count != 0)
        //            {
        //                lightZone.IsLinked = true;
        //                lightZone.LinkCount = lightZone.LE_Proxies.Count;
        //            }
        //            x.Add(lightZone);
        //        }
        //    }
        //    callback(x, null);
        //}

        //public bool ZoneContainsLE(LightZone zone, LightElement lightElement)
        //{
        //    using (var db = new LSModelContainer(LS.CS))
        //    {
        //        EFData.LightZone dbLightZone = db.LightZones.Include("LightElements").First(p => p.Id == zone.Id);

        //        //if (dbLightZone.LightElements.FirstOrDefault(m => m.Id == lightElement.Id) == null)
        //        //    return false;
        //        //else
        //        //    return true;
        //    }
        //    return true;
        //}

        //public void UpdateLightZone(LightZone item, Action<int, Exception> callback)
        //{
        //    EFData.LightZone dbLightZone = null;
        //    EFData.Partition dbPartition = null;

        //    int ix = -1;
        //    if (item.Id != 0)
        //    {
        //        //Update
        //        using (var db = new LSModelContainer(LS.CS))
        //        {
        //            dbLightZone = db.LightZones.FirstOrDefault(p => p.Id == item.Id);
        //            Mapper.O2Db(item, dbLightZone);
        //            ix = db.SaveChanges();
        //        }
        //        callback(ix, null);
        //    }
        //    else
        //    {
        //        // Create
        //        using (var db = new LSModelContainer(LS.CS))
        //        {
        //            dbLightZone = new EFData.LightZone();
        //            try
        //            {
        //                Mapper.O2Db(item, dbLightZone);
        //                if (item.Partition != null)
        //                    dbLightZone.Partition = db.Partitions.FirstOrDefault(p => p.Id == item.Partition.Id);
        //                if (item.ControlSpace != null)
        //                    dbLightZone.ControlSpace = db.ControlSpaces.FirstOrDefault(p => p.Id == item.ControlSpace.Id);
        //                db.LightZones.Add(dbLightZone);
        //                ix = db.SaveChanges();
        //                item.Id = dbLightZone.Id;
        //                callback(ix, null);
        //            }
        //            catch (Exception ex)
        //            {
        //                callback(ix, ex);
        //            }
        //        }
        //    }
        //}

        //public void AddLE_ToZone(LightZone zone, LE_Proxy leProxy)
        //{
        //    EFData.LightElement dbLightElement = null;
        //    EFData.LE_Proxy dbLE_Proxy = null;
        //    using (var db = new LSModelContainer(LS.CS))
        //    {
        //        try
        //        {
        //            dbLE_Proxy = new EFData.LE_Proxy();
        //            Mapper.O2Db(leProxy, dbLE_Proxy);
        //            EFData.LightZone dbLightZone = db.LightZones.Include("LE_Proxies").FirstOrDefault(p => p.Id == zone.Id);
        //            dbLightElement = db.LightElements.FirstOrDefault(p => p.Id == leProxy.LightElement.Id);
        //            dbLE_Proxy.LightElement = dbLightElement;
        //            dbLE_Proxy.LightZone = dbLightZone;

        //            db.LE_Proxies.Add(dbLE_Proxy);
        //            dbLightZone.LE_Proxies.Add(dbLE_Proxy);

        //            //dbLightElement = db.LightElements.FirstOrDefault(p => p.Id == le.Id);
        //            //dbLightZone.ControlSpace = dbLightElement.ControlSpace;
        //            //dbLightZone.LightElements.Add(dbLightElement);

        //            int i = 0;
        //            foreach (EFData.LE_Proxy lep in dbLightZone.LE_Proxies)
        //            {
        //                lep.Ix = i;
        //                i++;
        //            }

        //            i = db.SaveChanges();
        //            leProxy.Id = dbLightZone.LE_Proxies.Last().Id;
        //            leProxy.LightZone = zone;
        //        }
        //        catch (Exception ex)
        //        {
        //            var x = ex;
        //        }
        //    }
        //}

        //public void RemoveLE_FromZone(LightZone zone, LE_Proxy leProxy)
        //{
        //    using (var db = new LSModelContainer(LS.CS))
        //    {
        //        try
        //        {
        //            EFData.LightZone dbLightZone = db.LightZones.Include("LE_Proxies").FirstOrDefault(p => p.Id == zone.Id);
        //            EFData.LE_Proxy dbLE_proxy = dbLightZone.LE_Proxies.FirstOrDefault(p => p.Id == leProxy.Id);
        //            dbLightZone.LE_Proxies.Remove(dbLE_proxy);
        //            int i = 0;
        //            foreach (EFData.LE_Proxy lep in dbLightZone.LE_Proxies)
        //            {
        //                lep.Ix = i;
        //                i++;
        //            }
        //            db.Entry(dbLE_proxy).State = EntityState.Deleted;

        //            i = db.SaveChanges();
        //        }
        //        catch (Exception ex)
        //        {
        //            var x = ex;
        //        }
        //    }
        //}

        //#endregion

        ///****************************************************************/

        //#region LE_Proxy

        //public void UpdateLE_Proxy(LE_Proxy leProxy, Action<int, Exception> callback)
        //{
        //    EFData.LE_Proxy dbLE_Proxy = null;

        //    int ix = -1;

        //    //Update
        //    using (var db = new LSModelContainer(LS.CS))
        //    {
        //        dbLE_Proxy = db.LE_Proxies.FirstOrDefault(p => p.Id == leProxy.Id);
        //        Mapper.O2Db(leProxy, dbLE_Proxy);
        //        ix = db.SaveChanges();
        //    }
        //    callback(ix, null);
        //}

        //#endregion

        ///****************************************************************/

        //#region Gamma

        //public void UpdateGamma(Gamma gamma, Action<int, Exception> callback)
        //{
        //    int ix;
        //    if (gamma.Id != 0)
        //    {
        //        // Update
        //        using (var db = new LSModelContainer(LS.CS))
        //        {
        //            EFData.Gamma dbGamma = db.Gammas.FirstOrDefault(p => p.Id == gamma.Id);
        //            Mapper.O2Db(gamma, dbGamma);
        //            ix = db.SaveChanges();
        //        }
        //        callback(ix, null);
        //    }
        //    else
        //    {
        //        // Create
        //        using (var db = new LSModelContainer(LS.CS))
        //        {
        //            EFData.Gamma dbGamma = new EFData.Gamma();
        //            Mapper.O2Db(gamma, dbGamma);
        //            db.Gammas.Add(dbGamma);
        //            ix = db.SaveChanges();
        //            gamma.Id = dbGamma.Id;
        //        }
        //        callback(ix, null);
        //    }
        //}

        //#endregion

        ///****************************************************************/

    }
}