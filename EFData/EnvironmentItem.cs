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
    
    public partial class EnvironmentItem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EnvironmentItem()
        {
            this.Profile = "\"\"";
            this.CSEnvItems = new HashSet<CSEnvItem>();
        }
    
        public int Id { get; set; }
        public string Model { get; set; }
        public DeviceTypeEnum DeviceType { get; set; }
        public string Profile { get; set; }
        public string DotNetType { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CSEnvItem> CSEnvItems { get; set; }
    }
}
