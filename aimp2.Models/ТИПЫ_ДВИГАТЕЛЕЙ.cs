//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace aimp2.models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ТИПЫ_ДВИГАТЕЛЕЙ
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ТИПЫ_ДВИГАТЕЛЕЙ()
        {
            this.ТРАНСПОРТ = new HashSet<ТРАНСПОРТ>();
        }
    
        public int код { get; set; }
        public string наименование { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ТРАНСПОРТ> ТРАНСПОРТ { get; set; }
    }
}
