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
    
    public partial class CommissionTransactions
    {
        public int Id { get; set; }
        public decimal Commission { get; set; }
        public decimal Parking { get; set; }
        public bool IsTwoMounth { get; set; }
        public Nullable<System.DateTime> DateProxy { get; set; }
        public string NumberProxy { get; set; }
        public string NumberRegistry { get; set; }
        public int OwnerId { get; set; }
        public System.DateTime Date { get; set; }
        public int Number { get; set; }
        public int SellerId { get; set; }
        public int BuyerId { get; set; }
        public int TrancportId { get; set; }
        public decimal Price { get; set; }
        public int UserId { get; set; }
    
        public virtual Contractors Contractors { get; set; }
        public virtual Contractors Contractors1 { get; set; }
        public virtual Contractors Contractors2 { get; set; }
        public virtual Trancports Trancports { get; set; }
        public virtual Users Users { get; set; }
    }
}
