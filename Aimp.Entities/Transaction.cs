using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public abstract class Transaction : Entity
    {
        public DateTime Date { get; set; }

        public int Number { get; set; }

        [ForeignKey("Seller")]
        public int SellerId { get; set; }
        public virtual Contractor Seller { get; set; }
        [ForeignKey("Buyer")]
        public int? BuyerId { get; set; }
        public virtual Contractor Buyer { get; set; }
        [ForeignKey("Trancport")]
        public int TrancportId { get; set; }
        public virtual Trancport Trancport { get; set; }
        public decimal Price { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
