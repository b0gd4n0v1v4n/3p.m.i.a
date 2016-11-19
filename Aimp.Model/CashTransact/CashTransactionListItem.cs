using AIMP_v3._0.Model;
using System;

namespace Aimp.Model.CashTransact
{
    public class CashTransactionListItem : Identity
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
    }
}
