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
    
    public partial class EffectPartType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EffectPartType()
        {
            this.EffectParts = new HashSet<EffectPart>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Params { get; set; }
        public string Category { get; set; }
        public PointTypeEnum PointType { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EffectPart> EffectParts { get; set; }
    }
}