//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MigrationDb2Vto3V
{
    using System;
    using System.Collections.Generic;
    
    public partial class ТИПЫ_ШАБЛОНОВ
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ТИПЫ_ШАБЛОНОВ()
        {
            this.ШАБЛОНЫ = new HashSet<ШАБЛОНЫ>();
        }
    
        public int код { get; set; }
        public string наименование { get; set; }
        public string запрос { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ШАБЛОНЫ> ШАБЛОНЫ { get; set; }
    }
}
