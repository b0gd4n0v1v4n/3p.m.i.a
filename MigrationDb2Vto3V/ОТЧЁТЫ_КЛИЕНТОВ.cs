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
    
    public partial class ОТЧЁТЫ_КЛИЕНТОВ
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ОТЧЁТЫ_КЛИЕНТОВ()
        {
            this.БАНКИ_ДЛЯ_ОТЧЁТЫ_КЛИЕНТОВ = new HashSet<БАНКИ_ДЛЯ_ОТЧЁТЫ_КЛИЕНТОВ>();
        }
    
        public int код { get; set; }
        public Nullable<System.DateTime> дата { get; set; }
        public string источник { get; set; }
        public string фио { get; set; }
        public string тс { get; set; }
        public Nullable<decimal> стоимость { get; set; }
        public Nullable<decimal> общий_взнос { get; set; }
        public Nullable<int> спр_статусы_клиентов { get; set; }
        public string телефон { get; set; }
        public Nullable<int> спр_программы_кредитования { get; set; }
        public Nullable<decimal> сумма_кредита { get; set; }
        public Nullable<byte> комиссии_знает { get; set; }
        public Nullable<decimal> комиссия_за_снятие { get; set; }
        public Nullable<byte> комиссии_в_кредите { get; set; }
        public Nullable<decimal> акт_оценки { get; set; }
        public string дкп_дк { get; set; }
        public string комментарий { get; set; }
        public string комиссия_салона { get; set; }
        public Nullable<int> пользователи { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<БАНКИ_ДЛЯ_ОТЧЁТЫ_КЛИЕНТОВ> БАНКИ_ДЛЯ_ОТЧЁТЫ_КЛИЕНТОВ { get; set; }
        public virtual ПОЛЬЗОВАТЕЛИ ПОЛЬЗОВАТЕЛИ1 { get; set; }
    }
}
