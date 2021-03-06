﻿using EFData;
using LS_Designer_WPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LS_Designer_WPF.Model
{
    public static class Mapper
    {
        /****************************************************************/

        #region Partition

        public static void Db2O(EFData.Partition dbObj, Partition obj)
        {
            obj.Id = dbObj.Id;
            obj.Name = dbObj.Name;
        }

        public static void O2Db(Partition obj, EFData.Partition dbObj)
        {
            //if (obj.Id != 0)
            //    dbObj.Id = obj.Id;
            dbObj.Name = obj.Name;
        }

        #endregion

        /****************************************************************/

        #region ControlSpace

        public static void O2Db(ControlSpace obj, EFData.ControlSpace dbObj)
        {
            //dbObj.Id = obj.Id;
            dbObj.Name = obj.Name;
            dbObj.IsActive = obj.IsActive;
        }

        public static void Db2O(EFData.ControlSpace dbObj, ControlSpace obj)
        {
            obj.Id = dbObj.Id;
            obj.Name = dbObj.Name;
            obj.IsActive = dbObj.IsActive;
            obj.Prefix = dbObj.Prefix;
        }

        #endregion

        /****************************************************************/

        #region ControlDevice

        public static void O2Db(ControlDevice obj, EFData.ControlDevice dbObj)
        {
            //XElement data;
            //EFData.ControlSpace dbCS = new EFData.ControlSpace();
            //ControlSpace cs = obj.ControlSpace;
            
            //if (dbObj.Id == 0)
            //    dbObj.Id = obj.Id;
            dbObj.Model = obj.Model;
            dbObj.Name = obj.Name;
            dbObj.HaveDimmer = obj.HaveDimmer;
            dbObj.Remark = obj.Remark;
            dbObj.MultiChannel = obj.MultiChannel;
            dbObj.CanAddChannel = obj.CanAddChannel;
            dbObj.DotNetType = obj.DotNetType;
            dbObj.Profile = obj.Profile;
            //dbObj.ControlSpace = new EFData.ControlSpace();
            //Mapper.O2Db(obj.ControlSpace, dbObj.ControlSpace);
            //EFData.ControlChannel dbCh;
            //foreach(ControlChannel ch in obj.ControlChannels)
            //{
            //    dbCh = new EFData.ControlChannel();
            //    O2Db(ch, dbCh);
            //    dbObj.ControlChannels.Add(dbCh);
            //}
        //if (obj is ArtNetControlDevice)
        //{
        //    data = new XElement
        //    ("Params",
        //        new XElement("IPAddress",
        //                     new XAttribute("Value", (obj as ArtNetControlDevice).IPAddress.ToString()),
        //                     new XAttribute("ChCount", (obj as ArtNetControlDevice).IPChCount.ToString())),
        //        new XElement("VirtualIP",
        //                     new XAttribute("Value", (obj as ArtNetControlDevice).VirtualIP.ToString()),
        //                     new XAttribute("ChCount", (obj as ArtNetControlDevice).VIPChCount.ToString()))
        //    );
        //    dbObj.Profile = data.ToString();
        //}

    }

        public static void Db2O(EFData.ControlDevice dbObj, out ControlDevice obj)
        {
            //IPAddress adr;
            //XElement data;

            obj = (ControlDevice)Activator.CreateInstance(Type.GetType(dbObj.DotNetType));
            obj.Id = dbObj.Id;
            obj.Name = dbObj.Name;
            obj.Model = dbObj.Model;
            obj.HaveDimmer = dbObj.HaveDimmer;
            obj.Profile = dbObj.Profile;
            obj.Remark = dbObj.Remark;
            obj.MultiChannel = dbObj.MultiChannel;
            obj.CanAddChannel = dbObj.CanAddChannel;
            obj.DotNetType = dbObj.DotNetType;

            //if (obj is ArtNetControlDevice)
            //{
            //    data = XElement.Parse(dbObj.Profile);
            //    var x = data.Element("IPAddress").Attribute("Value").Value;
            //    var y = data.Element("VirtualIP").Attribute("Value").Value;
            //    IPAddress.TryParse(x, out adr);
            //    (obj as ArtNetControlDevice).IPAddress = adr;
            //    IPAddress.TryParse(y, out adr);
            //    (obj as ArtNetControlDevice).VirtualIP = adr;
            //    (obj as ArtNetControlDevice).IPChCount = int.Parse(data.Element("IPAddress").Attribute("ChCount").Value);
            //    (obj as ArtNetControlDevice).VIPChCount = int.Parse(data.Element("VirtualIP").Attribute("ChCount").Value);
            //}
            //if (obj is GenericControlDevice)
            //{
            //    data = XElement.Parse(dbObj.Profile);
            //    obj.CanDimming = Boolean.Parse(data.Attribute("HaveDimmer").Value);
            //}
//            (Data.ControlChannel)Activator.CreateInstance(Type.GetType(dbLE_Proxy.LightElement.ControlChannel.DotNetChannelType));
            //obj.ControlSpace = new EFData.ControlSpace();
            //Db2O(dbObj.ControlSpace, obj.ControlSpace);
        }

        #endregion

        /****************************************************************/

        #region Control Channel

        public static void O2Db(ControlChannel obj, EFData.ControlChannel dbObj)
        {
            dbObj.Name = obj.Name;
            dbObj.HaveDimmer = obj.HaveDimmer;
            dbObj.ChannelNo = obj.ChannelNo;
            dbObj.PointType = (EFData.PointTypeEnum)obj.PointType;
            dbObj.HaveDimmer = obj.HaveDimmer;
            dbObj.Multilink = obj.Multilink;
            dbObj.DotNetType = obj.DotNetType;
            dbObj.Profile = obj.Profile;
        }

        public static void Db2O(EFData.ControlChannel dbObj, out ControlChannel obj)
        {
            obj = (ControlChannel)Activator.CreateInstance(Type.GetType(dbObj.DotNetType));
            obj.Id = dbObj.Id;
            obj.HaveDimmer = (bool)dbObj.HaveDimmer;
            obj.ChannelNo = dbObj.ChannelNo;
            obj.PointType = (LS_Library.PointTypeEnum)dbObj.PointType;
            obj.HaveDimmer = (bool)dbObj.HaveDimmer;
            obj.Multilink = (bool)dbObj.Multilink;
            obj.DotNetType = dbObj.DotNetType;
            obj.Profile = dbObj.Profile;
        }

        #endregion

        /****************************************************************/

        #region EnvironmentItem

        public static void Db2O(EFData.EnvironmentItem dbObj, EnvironmentItem obj)
        {
            obj.Id = dbObj.Id;
            obj.Model = dbObj.Model;
            obj.Profile = dbObj.Profile;
            obj.DeviceType = (DeviceTypeEnum)dbObj.DeviceType;
            obj.DotNetType = dbObj.DotNetType;
            //Mapper.Db2O(dbObj.ControlSpace, obj.ControlSpace);
        }

        #endregion

        /****************************************************************/

        #region EventDevice

        public static void O2Db(EventDevice obj, EFData.EventDevice dbObj)
        {
            dbObj.Name = obj.Name;
            dbObj.Model = obj.Model;
            dbObj.Mode = obj.Mode;
            dbObj.Profile = obj.Profile;
            dbObj.Remark = obj.Remark;
            dbObj.MultiChannel = obj.MultiChannel;
            dbObj.CanAddChannel = obj.CanAddChannel;
            dbObj.DotNetType = obj.DotNetType;
        }

        public static void Db2O(EFData.EventDevice dbObj, out EventDevice obj)
        {
            obj = (EventDevice)Activator.CreateInstance(Type.GetType(dbObj.DotNetType));
            obj.Id = dbObj.Id;
            obj.Profile = dbObj.Profile;

            
            obj.Name = dbObj.Name;
            obj.Model = dbObj.Model;
            obj.OldMode = dbObj.Mode;
            obj.Mode = dbObj.Mode;
            obj.Remark = dbObj.Remark;
            obj.MultiChannel = dbObj.MultiChannel;
            obj.CanAddChannel = dbObj.CanAddChannel;
            obj.DotNetType = dbObj.DotNetType;
        }

        #endregion

        /****************************************************************/

        #region EventChannel

        public static void O2Db(EventChannel obj, EFData.EventChannel dbObj)
        {
            dbObj.Name = obj.Name;
            dbObj.ChannelNo = obj.ChannelNo;
            dbObj.EventName = obj.EventName;
            dbObj.Profile = obj.Profile;
        }

        public static void Db2O(EFData.EventChannel dbObj, EventChannel obj)
        {
            obj.Id = dbObj.Id;
            obj.Name = dbObj.Name;
            obj.ChannelNo = dbObj.ChannelNo;
            obj.EventName = dbObj.EventName;
            obj.Profile = dbObj.Profile;
        }

        #endregion

        /****************************************************************/

        #region LE_Type

        public static void Db2O(EFData.LE_Type dbObj, LE_Type obj)
        {
            obj.Id = dbObj.Id;
            obj.Name = dbObj.Name;
            obj.PointType = (LS_Library.PointTypeEnum)dbObj.PointType;
            obj.CanUseGamma = dbObj.CanUseGamma;
        }

        #endregion

        /****************************************************************/

        #region LightElement

        public static void O2Db(LightElement obj, EFData.LightElement dbObj)
        {
            dbObj.Name = obj.Name;
            dbObj.PointType = (EFData.PointTypeEnum)obj.PointType;
            dbObj.StartPoint = obj.StartPoint;
            dbObj.PointCount = obj.PointCount;
            dbObj.Direction = (EFData.Direction)obj.Direction;
            dbObj.ColorSequence = obj.ColorSequence;
            dbObj.Remark = obj.Remark;
            
            //dbObj.Ix = obj.Ix;
                    

            //if (obj is LightStrip)
            //{
            //    (dbObj as EFData.LightStrip).Direction = (EFData.Direction)(obj as LightStrip).Direction;
            //    (dbObj as EFData.LightStrip).PointCount = (obj as LightStrip).PointCount;
            //    (dbObj as EFData.LightStrip).ColorSequence = (obj as LightStrip).ColorSequence;
            //}
        }

        public static void Db2O(EFData.LightElement dbObj, LightElement obj)
        {
            obj.Id = dbObj.Id;
            obj.Name = dbObj.Name;
            obj.PointType = (LS_Library.PointTypeEnum)dbObj.PointType;
            obj.StartPoint = dbObj.StartPoint;
            obj.PointCount = dbObj.PointCount;
            obj.Direction = (Direction)dbObj.Direction;
            obj.ColorSequence = dbObj.ColorSequence;
            obj.Remark = dbObj.Remark;

            obj.Partition = new Partition();
            obj.ControlSpace = new ControlSpace();

            Db2O(dbObj.Partition, obj.Partition);
            Db2O(dbObj.ControlSpace, obj.ControlSpace);

            if (dbObj.ControlChannel != null)
            {
                ControlChannel cCh = new ControlChannel();
                Db2O(dbObj.ControlChannel, out cCh);
                obj.ControlChannel = cCh;
            }

            if (obj.ControlSpace.Prefix == "AN" || obj.ControlSpace.Prefix == "DX")
            {
                if (obj.PointType == LS_Library.PointTypeEnum.RGB)
                    obj.ColorSequenceList = LightElement.ColorSequenseRGB;
                if (obj.PointType == LS_Library.PointTypeEnum.RGBW)
                    obj.ColorSequenceList = LightElement.ColorSequenseRGBW;
            }

        }

        #endregion

        /****************************************************************/

        #region LightZone

        public static void Db2O(EFData.LightZone dbObj, LightZone obj)
        {
            obj.Id = dbObj.Id;
            obj.Name = dbObj.Name;
            obj.IsNode = dbObj.IsNode;
            obj.PointType = (LS_Library.PointTypeEnum)dbObj.PointType;
            obj.Remark = dbObj.Remark;
            obj.Partition = new Partition();
            Db2O(dbObj.Partition, obj.Partition);
            obj.ControlSpace = new ControlSpace();
            Db2O(dbObj.ControlSpace, obj.ControlSpace);
        }

        public static void O2Db(LightZone obj, EFData.LightZone dbObj)
        {
            dbObj.Name = obj.Name;
            dbObj.IsNode = obj.IsNode;
            dbObj.PointType = (EFData.PointTypeEnum)obj.PointType;
            dbObj.Remark = obj.Remark;
        }

        #endregion

        /****************************************************************/

        #region LE_Proxy

        public static void Db2O(EFData.LE_Proxy dbObj, LE_Proxy obj)
        {
            obj.Id = dbObj.Id;
            obj.Ix = dbObj.Ix;
            obj.LightZone = new LightZone();
            Db2O(dbObj.LightZone, obj.LightZone);
            obj.LightElement = new LightElement();
            Db2O(dbObj.LightElement, obj.LightElement);
        }

        #endregion

        /****************************************************************/

        #region Scene

        public static void Db2O(EFData.Scene dbObj, Scene obj)
        {
            obj.Id = dbObj.Id;
            obj.Name = dbObj.Name;
            
            obj.Remark = dbObj.Remark;
            //if (dbObj.Parent != null)
            //{
            //    obj.Parent = new Scene();
            //}
            obj.Partition = new Partition();
            Db2O(dbObj.Partition, obj.Partition);
        }

        public static void O2Db(Scene obj, EFData.Scene dbObj)
        {
            dbObj.Name = obj.Name;
            dbObj.Remark = obj.Remark;
        }

        #endregion

        /****************************************************************/


        //#region LE_Type

        //public static void Db2O(EFData.LE_Type dbObj, LE_Type obj)
        //{
        //    obj.Id = dbObj.Id;
        //    obj.Name = dbObj.Name;
        //    obj.ControlSpace = new ControlSpace();
        //    Db2O(dbObj.ControlSpace, obj.ControlSpace);
        //    obj.PointType = (PointTypeEnum)dbObj.PointType;
        //}

        //#endregion

        ///****************************************************************/

        //#region LightZone

        //public static void Db2O(EFData.LightZone dbObj, LightZone obj)
        //{

        //    obj.Id = dbObj.Id;
        //    obj.Name = dbObj.Name;
        //    obj.Direction = (Direction)dbObj.Direction;
        //    obj.IsNode = dbObj.IsNode;

        //    obj.Partition = new Partition();
        //    Db2O(dbObj.Partition, obj.Partition);

        //    if (dbObj.ControlSpace != null)
        //    {
        //        obj.ControlSpace = new ControlSpace();
        //        Db2O(dbObj.ControlSpace, obj.ControlSpace);
        //    }
        //}

        //public static void O2Db(LightZone obj, EFData.LightZone dbObj)
        //{
        //    //if (dbObj.Id == 0)
        //    //    dbObj.Id = obj.Id;
        //    dbObj.Direction = (EFData.Direction)obj.Direction;
        //    dbObj.IsNode = obj.IsNode;
        //    dbObj.Name = obj.Name;
        //}

        //#endregion

        ///****************************************************************/

        //#region LE_Proxy

        //public static void Db2O(EFData.LE_Proxy dbObj, LE_Proxy obj)
        //{
        //    obj.Id = dbObj.Id;
        //    obj.Ix = dbObj.Ix;
        //    if (obj.LightElement == null)
        //    {
        //        if (dbObj.LightElement is EFData.LightStrip)
        //            obj.LightElement = new LightStrip();
        //        else
        //            obj.LightElement = new LightElement();
        //    }
        //    Db2O(dbObj.LightElement, obj.LightElement);
        //    if (obj.LightZone == null)
        //        obj.LightZone = new LightZone();
        //    Db2O(dbObj.LightZone, obj.LightZone);
        //}

        //public static void O2Db(LE_Proxy obj, EFData.LE_Proxy dbObj)
        //{
        //    dbObj.Ix = obj.Ix;
        //    //O2Db(obj.LightElement, dbObj.LightElement);
        //    //O2Db(obj.LightZone, dbObj.LightZone);
        //}

        //#endregion

        ///****************************************************************/

        //#region Gamma

        //public static void O2Db(Gamma obj, EFData.Gamma dbObj)
        //{
        //    dbObj.Name = obj.Name;
        //    dbObj.Value = obj.Value;
        //}

        //public static void DB2Object(EFData.Gamma dbObj, Gamma obj)
        //{
        //    obj.Id = dbObj.Id;
        //    obj.Name = dbObj.Name;
        //    obj.Value = dbObj.Value;
        //}

        //#endregion

        /****************************************************************/
    }
}
