using GalaSoft.MvvmLight;
using LS_Designer_WPF.Model;
using LS_Library;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace LS_Designer_WPF.Model
{
    public class LightZone : ObservableObject
    {
        public int Id { get; set; }

        public string Name { get; set; } = "";

        public bool IsNode { get; set; }

        public Nullable<PointTypeEnum> PointType { get; set; }

        public string Remark { get; set; }

        public Partition Partition { get; set; }

        public ControlSpace ControlSpace { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Scene> Scenes { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Effect> Effects { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<LE_Proxy> LE_Proxies { get; set; }

        // UI related

        public ObservableCollection<LE_Proxy> LE_ProxyList { get; set; }

        bool _directParent = false;
        public bool DirectParent
        {
            get { return _directParent; }
            set { Set(ref _directParent, value); }
        }

        bool _hasChildren = false;
        public bool HasChildren
        {
            //get { return _hasChildren; }
            get { return LE_ProxyList.Count != 0; }
            set { Set(ref _hasChildren, value); }
        }

        public void RaiseHasChildrenChanged()
        {
            RaisePropertyChanged("HasChildren");
        }
    }
}
