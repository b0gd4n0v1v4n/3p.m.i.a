using System;
using Aimp.Entities;
using Aimp.Model.PrintedDocument;
using Aimp.ServiceContracts.ClientReports;

namespace AIMP_v3._0.Aimp.Services.Clients
{
    public class ClientReportClient : IDisposable, IClientReportContract
    {
        public void DeleteClientReport(IClientReport document)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public ClientReportDto GetClientReport(int id)
        {
            throw new NotImplementedException();
        }

        public ExcelPrintedDocument GetClientReportPrintedDocument(ClientReports reports)
        {
            throw new NotImplementedException();
        }

        public ClientReports GetClientReports()
        {
            throw new NotImplementedException();
        }

        public ClientReportDto GetNewClientReport()
        {
            throw new NotImplementedException();
        }

        public void SaveClientReport(IClientReport document)
        {
            throw new NotImplementedException();
        }
    }
}
