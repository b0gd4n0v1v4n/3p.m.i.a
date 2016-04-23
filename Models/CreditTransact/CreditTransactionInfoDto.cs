using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models.CreditTransact
{
    public class CreditTransactionInfoDto : IResponse
    {
        public bool Error
        {
            get; set;
        }

        public string Message
        {
            get; set;
        }
        public IEnumerable<Creditor> Creditors { get; set; }
        public IEnumerable<Requisit> Requisits { get; set; }
    }
}