﻿using Entities;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Aimp.Model.Documents
{
    public class ClientReportDocument : IDocument
    {
        public IEnumerable<BankReportClient> BankReportClients { get; set; }
        public DocumentType DocumentType
        {
            get
            {
                return DocumentType.ClientOfReport;
            }
        }

        public int Id { get; set; }

        public string Identity { get; set; }

        public bool IsNew { get { if (Id != 0) return false; else return true; } }

        public int UserId
        {
            get
            {
                return BankReportClients.First().ClientReport.UserId;
            }
        }
    }
}
