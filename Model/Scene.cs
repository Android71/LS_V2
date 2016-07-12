using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS_Designer_WPF.Model
{
    public class Scene : ObservableObject
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string IsAccent { get; set; }

        public string Remark { get; set; }

        public Partition Partition { get; set; }

        public Scene Parent { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<LightZone> LightZones { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Effect> Effects { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

        // UI related

        ObservableCollection<Scene> _accents;
        public  ObservableCollection<Scene> Accents
        {
            get { return _accents; }
            set { Set(ref _accents, value); }
        }

        Scene _selectedAccent;
        public Scene SelectedAccent
        {
            get { return _selectedAccent; }
            set
            {
                Scene tmp = _selectedAccent;
                Set(ref _selectedAccent, value);
                if (tmp != value)
                    ;
            }
        }
    }
}
