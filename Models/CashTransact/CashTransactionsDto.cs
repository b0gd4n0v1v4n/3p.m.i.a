using System.Collections.Generic;

namespace Models.CashTransact
{
    public class CashTransactionsDto : IResponse
    {
        public bool Error
        {
            get; set;
        }

        public IEnumerable<CashTransactionListItem> Items { get; set; }

        public string Message
        {
            get; set;
        }
    }
}
