using AIMP_v3._0.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMP_v3._0
{
    public class WTFdateTimeBugFix
    {
        public void Start()
        {
            using (AimpService service = new AimpService())
            {
                foreach(var rep in service.GetClientReports().Items)
                {
                    var report = service.GetClientReport(rep.ClientReportId);
                    report.Document.BankReportClients.First().ClientReport.Date =
                        new DateTime(report.Document.BankReportClients.First().ClientReport.Date.Year, report.Document.BankReportClients.First().ClientReport.Date.Month, report.Document.BankReportClients.First().ClientReport.Date.Day);

                    service.SaveClientReport(report.Document);

                    Console.WriteLine($"Client report id: {rep.ClientReportId} update!");

                }
            }
        }
    }
}
