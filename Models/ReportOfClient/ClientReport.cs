﻿using Models.Documents;
using Models.Entities;
using System;
using System.Collections.Generic;

namespace Models.ReportOfClient
{
    public class ClientReport : IResponse
    {
        public bool Error
        {
            get; set;
        }

        public string Message
        {
            get; set;
        }

        public ClientReportDocument Document { get; set; } 
        
        public IEnumerable<ClientStatus> ClientStatuses { get; set; }

        public IEnumerable<CreditProgramm> CreditProgramms { get; set; }

        public IEnumerable<Bank> Banks { get; set; }

        public IEnumerable<BankStatus> BankStatuses { get; set; }
    }
}
