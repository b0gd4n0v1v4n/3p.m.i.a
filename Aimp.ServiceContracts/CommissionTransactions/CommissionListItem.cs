using System;

namespace Aimp.ServiceContracts.CommissionTransactions
{
    public class CommissionListItem : Identity
    {
        public DateTime  Date { get; set; }
        public string Number { get; set; }

        public string NumberProxy { get; set; }

        public string SellerFullName { get; set; }

        public string TrancportFullName { get; set; }

        public int? DocumentSellerId { get; set; }

        public int? PtsId { get; set; }
        public string Commission { get; set; }

        public string Parking { get; set; }
    }
}
