using System;

namespace Aimp.Entities
{
    public interface ITransaction : IEntity
    {
         DateTime Date { get; set; }
         int Number { get; set; }
         int SellerId { get; set; }
        IContractor Seller { get; set; }
         int? BuyerId { get; set; }
         IContractor Buyer { get; set; }
         int TrancportId { get; set; }
         ITrancport Trancport { get; set; }
         decimal Price { get; set; }
         int UserId { get; set; }
         IUser User { get; set; }
    }
}
