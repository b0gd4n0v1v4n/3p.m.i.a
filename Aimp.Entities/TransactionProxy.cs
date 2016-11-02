using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public abstract class TransactionProxy : Transaction
    {
        public DateTime? DateProxy { get; set; }
        public string NumberProxy { get; set; }
        public string NumberRegistry { get; set; }
        [ForeignKey("Owner")]
        public int? OwnerId { get; set; }
        public virtual Contractor Owner { get; set; }
    }
}
