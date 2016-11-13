using Aimp.Model.PrintedDocument;
using Aimp.Model.ReportOfClient;
using Entities;
using System.Collections.Generic;

namespace Aimp.Reports.Interfaces
{
    public interface IExcelPrintedService : IPrintedService
    {
        IPrintedDocument GetReportOfClientList(IEnumerable<Bank> banks, IEnumerable<ClientReportListItem> reports);
    }
}
