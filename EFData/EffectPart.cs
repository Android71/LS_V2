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
    
    public partial class EffectPart
    {
        public int Id { get; set; }
        public string Params { get; set; }
    
        public virtual EffectPartType EffectPartType { get; set; }
        public virtual Effect Effect { get; set; }
        public virtual EffectPart EffectParts { get; set; }
        public virtual EffectPart ParentPart { get; set; }
    }
}
