using Models.Entities;
using System;
using System.Collections.Generic;

namespace Models.ReportOfClient
{
    public class ClientReports : IResponse
    {
        public bool Error
        {
            get; set;
        }

        public string Message
        {
            get; set;
        }

        public ClientReports()
        {

        }

        public ClientReports(Exception ex)
        {
            Error = true;

            Message = ex.Message;
        }
        public string UserLastNameForFilter { get; set; }
        public IEnumerable<string> ClientStatusesForFilter { get; set; }
        public IEnumerable<Bank> Banks { get; set; }
        
        public IEnumerable<ClientReportListItem> Items { get; set; } 
    }
}
