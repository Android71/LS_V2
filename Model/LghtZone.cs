using GalaSoft.MvvmLight;
using LS_Designer_WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS_Designer_WPF.Model
{
    public class LightZone : ObservableObject
    {
        /*****************************************************************/

        #region DBfields

        public int Id { get; set; }

        string _name = "";
        public string Name { get { return _name; } set { Set(ref _name, value); } }

        string _remark = "";
        public string Remark { get { return _remark; } set { Set(ref _remark, value); } }

        bool _isNode = false;
        public bool IsNode { get { return _isNode; } set { Set(ref _isNode, value); } }

        Direction _direction = Direction.Up;
        public Direction Direction { get { return _direction; } set { Set(ref _direction, value); } }

        List<LE_Proxy> _leProxies;
        public List<LE_Proxy> LE_Proxies { get { return _leProxies; } set { Set(ref _leProxies, value); } }

        List<Scene> _scenes;
        public List<Scene> Scenes { get { return _scenes; } set { Set(ref _scenes, value); } }

        List<EventChannel> _eventChannel;
        public List<EventChannel> EventChannels { get { return _eventChannel; } set { Set(ref _eventChannel, value); } }

        Partition _partition;
        public Partition Partition { get { return _partition; } set { Set(ref _partition, value); } }

        ControlSpace _controlSpace;
        public ControlSpace ControlSpace { get { return _controlSpace; } set { Set(ref _controlSpace, value); } }

        List<Effect> _effects;
        public List<Effect> Effects { get { return _effects; } set { Set(ref _effects, value); } }

        #endregion

        /*****************************************************************/

        #region UIfields

        //string _qulifiedName = "";
        public string QualifiedName { get { return Partition.Name + " / " + Name; } }

        bool _isLinked = false;
        public bool IsLinked { get { return _isLinked; } set { Set(ref _isLinked, value); } }

        int _linkCount;
        public int LinkCount { get { return _linkCount; } set { Set(ref _linkCount, value); } }

        public void RaiseQualifiedNameChanged()
        {
            RaisePropertyChanged("QualifiedName");
        }

        #endregion

        /*****************************************************************/

        #region BuisnessLogic

        public bool Validate()
        {
            return true;
        }

        public bool Link(LE_Proxy leProxy)
        {
            // Если LightZone еще не содержит ни одного LightElement - подключить LightElement и установить this.ControlSpace = LightElement.ControlSpace
            // Если LightElement is LightStrip тестировать пересечение в Universe
            //if (!IsLinked)
            //    ControlSpace = le.ControlSpace;
            //if (le.ControlSpace.Name == "ArtNet_DMX")
            //{
            //    PopUpMessageVM popUp = new PopUpMessageVM("Intersection conflict");
            //    List<LightZone> zones = new List<LightZone>() { this };

            //    if (CheckIntersectionConflict(le, zones, popUp))
            //    {
            //        AppContext.DataSvc.AddLE_ToZone(this, le);
            //    }
            //    IsLinked = true;
            //    LinkCount++;
            //    le.IsLinked = true;
            //    le.LinkCount++;
            //}
            //else
            //{
            //    IsLinked = true;
            //    LinkCount++;
            //    le.IsLinked = true;
            //    le.LinkCount++;
            //}
            //leProxy.LightZone = this;
            AppContext.DataSvc.AddLE_ToZone(this, leProxy);
            IsLinked = true;
            LinkCount++;
            leProxy.LightElement.IsLinked = true;
            leProxy.LightElement.LinkCount++;
            return true;
        }

        public void Unlink(LE_Proxy leProxy)
        {
            AppContext.DataSvc.RemoveLE_FromZone(this, leProxy);
            LinkCount--;
            if (LinkCount == 0)
                IsLinked = false;
        }

        bool CheckIntersectionConflict(LightElement le, List<LightZone> zones, PopUpMessageVM popUp)
        {
            //List<ArtNetControlChannel> universes = new List<ArtNetControlChannel>();
            //foreach (LightZone zone in zones)
            //{
            //    universes.Clear();
            //    foreach(LightElement lightElement in zone.LightElements)
            //    {
            //        if (universes.FirstOrDefault(u => u.Id == lightElement.ControlChannel.Id) == null)
            //            universes.Add(lightElement.ControlChannel as ArtNetControlChannel);
            //    }

            //    foreach (ArtNetControlChannel channel in universes)
            //    {

            //    }
            //}
            return true;
        }

        #endregion
    }
}
