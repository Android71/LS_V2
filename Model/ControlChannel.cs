using GalaSoft.MvvmLight;
using LS_Designer_WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS_Designer_WPF.Model
{
    public class ControlChannel : ObservableObject
    {

        public int Id { get; set; }

        string _name = "";
        public string Name { get { return _name; } set { Set(ref _name, value); } }

        public string FullName
        //{ get { return ControlDevice.Name + " / " + Name; } }
        { get { return Name; } }

        public string QualifiedName
        //{ get { return ControlDevice.Name + " / " + Name; } }
        { get { return ControlDevice.ControlSpace.Name + " / " + Name; } }

        int _channelNo;
        public int ChannelNo { get { return _channelNo; } set { Set(ref _channelNo, value); } }

        bool _haveDimmer = false;
        public bool HaveDimmer { get { return _haveDimmer; } set { Set(ref _haveDimmer, value); } }

        public ControlDevice ControlDevice { get; set; }

        bool _isLinked = false;
        public bool IsLinked { get { return _isLinked; } set { Set(ref _isLinked, value); } }

        int _linkCount;
        public int LinkCount { get { return _linkCount; } set { Set(ref _linkCount, value); } }

        bool _isLinkedToSelected = false;
        public bool IsLinkedToSelected { get { return _isLinkedToSelected; } set { Set(ref _isLinkedToSelected, value); } }

        protected virtual bool CheckIntersect (LightElement le, out LinkErrorVM info)
        {
            info = null;
            return true;
        }

        public virtual void Link(LightElement le)
        {
            AppContext.DataSvc.AddLinkToLightElement(this, le);
            le.ControlChannel = this;
            LinkCount++;
            IsLinked = true;
            le.IsLinked = true;
        }

        public void Unlink(LightElement le)
        {
            AppContext.DataSvc.RemoveLinkFromLightElement(le, this);
            LinkCount--;
            if (LinkCount == 0)
                IsLinked = false;
            le.IsLinked = false;
        }
    }
}
