using System.Collections.Generic;

namespace Models.CreditTransact
{
    public class CreditTransactions : IResponse
    {
        public bool Error
        {
            get; set;
        }

        public IEnumerable<CreditTransactionListItem> Items { get; set; }

        public string Message
        {
            get; set;
        }
    }
}