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
    
    public partial class Users
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Users()
        {
            this.CashTransactions = new HashSet<CashTransactions>();
            this.ClientReports = new HashSet<ClientReports>();
            this.CommissionTransactions = new HashSet<CommissionTransactions>();
            this.CreditTransactions = new HashSet<CreditTransactions>();
        }
    
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string FirstNameGenitive { get; set; }
        public string LastNameGenitive { get; set; }
        public string MiddleNameGenitive { get; set; }
        public string Number { get; set; }
        public System.DateTime Date { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool Add { get; set; }
        public bool Delete { get; set; }
        public bool View { get; set; }
        public bool Admin { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CashTransactions> CashTransactions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClientReports> ClientReports { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CommissionTransactions> CommissionTransactions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CreditTransactions> CreditTransactions { get; set; }
    }
}
