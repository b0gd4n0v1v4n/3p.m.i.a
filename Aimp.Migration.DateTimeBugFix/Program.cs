using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aimp.Migration.DateTimeBugFix
{
    class Program
    {
        static void Main(string[] args)
        {
            using(AimpService service = new AimpService())
            {
                var items = service.GetClientReports();
                foreach (var iReportClinet in items.Items)
                {
                    var report = service.GetClientReport(48);

                    report.Document.BankReportClients.First().ClientReport.Date = new DateTime(report.Document.BankReportClients.First().ClientReport.Date.Year, report.Document.BankReportClients.First().ClientReport.Date.Month, report.Document.BankReportClients.First().ClientReport.Date.Day);

                    service.SaveClientReport(report.Document);

                    Console.WriteLine($"Update client report id: {iReportClinet.Id}");
                }
            }
        }
    }
}
