
using Aimp.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aimp.Model.Entities
{
    public abstract class Transaction : Entity, ITransaction
    {
        public DateTime Date { get; set; }

        public int Number { get; set; }

        [ForeignKey("Seller")]
        public int SellerId { get; set; }
        public virtual IContractor Seller { get; set; }
        [ForeignKey("Buyer")]
        public int? BuyerId { get; set; }
        public virtual IContractor Buyer { get; set; }
        [ForeignKey("Trancport")]
        public int TrancportId { get; set; }
        public virtual ITrancport Trancport { get; set; }
        public decimal Price { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual IUser User { get; set; }
    }
}
