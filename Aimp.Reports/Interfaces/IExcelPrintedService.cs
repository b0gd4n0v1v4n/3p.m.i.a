using Aimp.Model.PrintedDocument;
using Aimp.Model.ReportOfClient;

namespace Aimp.Reports.Services.Excel
{
    public interface IExcelPrintedService : IPrintedService
    {
        IPrintedDocument GetReportOfClientList(ClientReports reports);
    }
}
