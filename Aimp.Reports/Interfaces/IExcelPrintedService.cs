using Aimp.Model.PrintedDocument;
using Aimp.Model.ReportOfClient;

namespace Aimp.Reports.Interfaces
{
    public interface IExcelPrintedService : IPrintedService
    {
        IPrintedDocument GetReportOfClientList(ClientReports reports);
    }
}
