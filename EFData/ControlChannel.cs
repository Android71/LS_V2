//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EFData
{
    using System;
    using System.Collections.Generic;
    
    public partial class ControlChannel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ControlChannel()
        {
            this.LightElements = new HashSet<LightElement>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public int ChannelNo { get; set; }
        public Nullable<PointTypeEnum> PointType { get; set; }
        public Nullable<bool> HaveDimmer { get; set; }
        public string DotNetType { get; set; }
        public string Profile { get; set; }
        public Nullable<bool> Multilink { get; set; }
    
        public virtual ControlSpace ControlSpace { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LightElement> LightElements { get; set; }
        public virtual ControlDevice ControlDevice { get; set; }
        public virtual Partition Partition { get; set; }
    }
}
