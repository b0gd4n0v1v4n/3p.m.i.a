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
    
    public partial class СДЕЛКИ
    {
        public int код { get; set; }
        public Nullable<System.DateTime> дата { get; set; }
        public Nullable<System.DateTime> дата_доверенность { get; set; }
        public string номер_доверенность { get; set; }
        public string номер_реестр { get; set; }
        public Nullable<int> продавец { get; set; }
        public Nullable<int> покупатель { get; set; }
        public Nullable<int> собственник { get; set; }
        public Nullable<int> транспорт { get; set; }
        public string стоимость { get; set; }
        public byte[] дкп { get; set; }
        public byte[] агенский { get; set; }
        public Nullable<int> пользователи { get; set; }
        public Nullable<byte> доверенность { get; set; }
        public Nullable<decimal> стоимость_банк { get; set; }
        public Nullable<decimal> первый_взнос { get; set; }
        public Nullable<decimal> сумма_кредит { get; set; }
        public Nullable<decimal> стоимость_реальная { get; set; }
        public Nullable<decimal> первый_взнос_касса { get; set; }
        public Nullable<int> кредиторы { get; set; }
        public Nullable<int> реквизиты { get; set; }
        public Nullable<decimal> отчёт_по_страховым { get; set; }
        public Nullable<decimal> откат { get; set; }
        public string источник { get; set; }
        public string дкп_наим { get; set; }
        public string агенский_наим { get; set; }
        public byte кредит { get; set; }
        public string номер { get; set; }
        public Nullable<decimal> комиссия_Касса { get; set; }
        public Nullable<System.DateTime> дата_ад { get; set; }
        public Nullable<decimal> комиссия { get; set; }
        public Nullable<decimal> стоянка { get; set; }
        public Nullable<byte> второй_месяц { get; set; }
        public Nullable<byte> тип { get; set; }
    
        public virtual КОНТРАГЕНТЫ КОНТРАГЕНТЫ { get; set; }
        public virtual КОНТРАГЕНТЫ КОНТРАГЕНТЫ1 { get; set; }
        public virtual КОНТРАГЕНТЫ КОНТРАГЕНТЫ2 { get; set; }
        public virtual ПОЛЬЗОВАТЕЛИ ПОЛЬЗОВАТЕЛИ1 { get; set; }
        public virtual ТРАНСПОРТ ТРАНСПОРТ1 { get; set; }
    }
}
