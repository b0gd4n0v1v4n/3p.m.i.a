//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MigrationDb2Vto3V
{
    using System;
    using System.Collections.Generic;
    
    public partial class спр_СТАТУСЫ_БАНКА
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public спр_СТАТУСЫ_БАНКА()
        {
            this.БАНКИ_ДЛЯ_ОТЧЁТЫ_КЛИЕНТОВ = new HashSet<БАНКИ_ДЛЯ_ОТЧЁТЫ_КЛИЕНТОВ>();
        }
    
        public int код { get; set; }
        public string наименование { get; set; }
        public string наименование2 { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<БАНКИ_ДЛЯ_ОТЧЁТЫ_КЛИЕНТОВ> БАНКИ_ДЛЯ_ОТЧЁТЫ_КЛИЕНТОВ { get; set; }
    }
}
