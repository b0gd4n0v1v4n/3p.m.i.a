using Aimp.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aimp.Model.Entities
{
    public abstract class TransactionProxy : Transaction, ITransactionProxy
    {
        public DateTime? DateProxy { get; set; }
        public string NumberProxy { get; set; }
        public string NumberRegistry { get; set; }
        [ForeignKey("Owner")]
        public int? OwnerId { get; set; }
        public virtual IContractor Owner { get; set; }
    }
}
