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
    
    public partial class LE_Type
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }
        public PointTypeEnum PointType { get; set; }
        public Nullable<bool> IsActive { get; set; }
    
        public virtual ControlSpace ControlSpace { get; set; }
    }
}