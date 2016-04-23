using AIMP_v3._0.Model;
using System;

namespace Models.CreditTransact
{
    public class CreditTransactionListItem : Identity
    {
        public DateTime Date { get; set; }
        public string Number { get; set; }
        public string NumberProxy { get; set; }
        public string SellerFullName { get; set; }
        public string BuyerFullName { get; set; }
        public string TrancportFullName { get; set; }
        public int? DocumentSellerId { get; set; }
        public int? DocumentBuyerId { get; set; }
        public int? PtsId { get; set; }
        public int? DkpId { get; set; }
        public int? AdId { get; set; }
        public int? PhotoSellerId { get; set; }
        public int? PhotoBuyerId { get; set; }
    }
}