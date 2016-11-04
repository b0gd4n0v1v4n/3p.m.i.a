using System.Collections.Generic;
using System.ServiceModel.Web;
using System;
using Aimp.Entities;
using Aimp.Model;
using Aimp.Model.CardTrancports;
using Aimp.Model.CashTransact;
using Aimp.Model.Commission;
using Aimp.Model.ContractorInfo;
using Aimp.Model.CreditTransact;
using Aimp.Model.Dictionar;
using Aimp.Model.Documents;
using Aimp.Model.PrintDocumentTemplate;
using Aimp.Model.PrintedDocument;
using Aimp.Model.ReportOfClient;
using Aimp.Model.TrancportInfo;
using Aimp.ServiceContract.Services;
using Entities;

namespace AIMP_v3._0.DataAccess
{
    public class AimpService : BaseHttpServer<IAimpWcfService>
    {
        private void _SetHeaderRequestLoginAndPassword()
        {
            var headers = WebOperationContext.Current.OutgoingRequest.Headers;

            headers.Add("login", ConnectionSettings.Login);

            headers.Add("password", ConnectionSettings.Password);
        }
        public AimpService()
            : base(ConnectionSettings.UriService)
        {
            _SetHeaderRequestLoginAndPassword();
        }
        public AimpUserDto Auth()
        {
            return Proxy.Auth();
        }
        #region client report
        public ClientReports GetClientReports()
        {
            return Proxy.GetClientReports();
        }
        public Aimp.Model.ReportOfClient.ClientReport GetClientReport(int? id)
        {
            if (id == null)
                return Proxy.GetNewClientReport();
            else
                return Proxy.GetClientReport((int)id);
        }
        public void SaveClientReport(ClientReportDocument document)
        {
            Proxy.SaveClientReport(document);
        }
        public void DeleteClientReport(ClientReportDocument document)
        {
            Proxy.DeleteClientReport(document);
        }
        #endregion
        #region cash transaction
        public IEnumerable<CashTransactionListItem> GetCashTransactions()
        {
            return Proxy.GetCashTransactions();
        }
        public CashTransactionDto GetCashTransaction(int id)
        {
            return Proxy.GetCashTransaction(id);
        }
        public KeyValue<int,int> SaveCashTransaction(CashTransactionDocument document)
        {
            return Proxy.SaveCashTransaction(document);
        }
        public void DeleteCashTransaction(CashTransactionDocument document)
        {
            Proxy.DeleteCashTransaction(document);
        }
        #endregion
        #region commission
        public IEnumerable<CommissionListItem> GetCommissions()
        {
            return Proxy.GetCommissions();
        }
        public CommissionDto GetCommission(int id)
        {
            return Proxy.GetCommission(id);
        }
        public KeyValue<int, int> SaveCommission(CommissionDocument document)
        {
            return Proxy.SaveCommission(document);
        }
        public void DeleteCommission(CommissionDocument document)
        {
            Proxy.DeleteCommission(document);
        }
        public IEnumerable<SourceTrancport> GetSourceTrancport()
        {
            return Proxy.GetSourceTrancport();       }
        #endregion
        #region credit transaction
        public IEnumerable<CreditTransactionListItem> GetCreditTransactions()
        {
            return Proxy.GetCreditTransactions();
        }
        public CreditTransactionDocument GetCreditTransaction(int id)
        {
            return Proxy.GetCreditTransaction(id);
        }
        public KeyValue<int, int> SaveCreditTransaction(CreditTransactionDocument document)
        {
            return Proxy.SaveCreditTransaction(document);
        }
        public void DeleteCreditTransaction(CreditTransactionDocument document)
        {
            Proxy.DeleteCreditTransaction(document);
        }
        #endregion
        public TrancportInfo GetTrancportInfo()
        {
            return Proxy.GetTrancportInfo();
        }
        public ContractorInfo GetContractorInfo()
        {
            return Proxy.GetContractorInfo();
        }
        public IEnumerable<Contractor> SearchContractor(TypeSearchContractor type, string text)
        {
            return Proxy.SearchContractors(type, text);
        }
        public IEnumerable<Trancport> SearchTranports(TypeSearchTrancport type, string text)
        {
            return Proxy.SearchTrancports(type, text);
        }
        public UserFile GetUserFile(int id)
        {
            return Proxy.GetUserFile(id);
        }
        public int SaveContractor(Contractor contractor)
        {
            return Proxy.SaveContractor(contractor);
        }
        public int SaveTrancport(Trancport trancport)
        {
            return Proxy.SaveTrancport(trancport);
        }
        public IEnumerable<UserRight> GetUserRights(int id)
        {
            return Proxy.GetUserRights(id);
        }
        public IEnumerable<User> GetUsers()
        {
            return Proxy.GetUsers();
        }

        public int SaveUser(User user, IEnumerable<string> rightIds)
        {
            return Proxy.SaveUser(user, rightIds);
        }

        public void DeleteUser(User user)
        {
            Proxy.DeleteUser(user);
        }

        public WordPrintedDocument GetPrintedDocument(DocumentType type, string name,int id)
        {
            return Proxy.GetPrintedDocument(type, name,id);
        }

        public IEnumerable<EntityName> GetPrintedList(DocumentType type)
        {
            return Proxy.GetPrintedList(type);
        }
        public PrintedDocumentTemplate GetPrintDocTemplate(int id)
        {
           return  Proxy.GetPrintedDocTemplate(id);
        }
        public IEnumerable<PrinDocTempListItem> GetListPrintDocTemplateDto()
        {
            return Proxy.GetPrintedDocTemplatesList();
        }
        public void SavePrintDocTemplate(PrintedDocumentTemplate template)
        {
            Proxy.SavePrintedDocTemplate(template);
        }
        public void DeletePrintedDocTemplate(PrintedDocumentTemplate template)
        {
            Proxy.DeletePrintedDocTemplate(template);
        }
        public IEnumerable<Row> GetDictionary(string tableName, IEnumerable<string> columns)
        {
            return Proxy.GetDictionary(tableName, columns);
        }

        public void SaveRowDictionary(string tableName, string value,int id)
        {
            Proxy.SaveRowDictionary(tableName, value,id);
        }
        public void DeleteRowDictionary(string tableName, int id)
        {
            Proxy.DeleteRowDictionary(tableName, id);
        }
        public void SaveRowValuesDictionary(string tableName, IDictionary<string, string> columnValues, int id)
        {
            Proxy.SaveRowValuesDictionary(tableName, columnValues, id);
        }
        public ExcelPrintedDocument GetClientReportList(ClientReports reports)
        {
            return Proxy.GetClientReportPrintedDocument(reports);
        }
        public CreditTransactionInfoDto GetCreditInfo()
        {
            return Proxy.GetCreditTransactionInfo();
        }
        #region card trancport
        public CardTrancportsDto GetCardsTrancport()
        {
            return Proxy.GetCardsTrancport();
        }
        public CardTrancportDocument GetCardTrancport(int id)
        {
            return Proxy.GetCardTrancport(id);
        }
        public int AddCardTrancport(int idCommission,DateTime dateStart)
        {
            return Proxy.AddCardTrancport(idCommission, dateStart);
        }
        public int SaveCardTrancport(CardTrancportDocument document)
        {
            return Proxy.SaveCardTrancport(document);
        }
        public void DeleteCardTrancport(int idCommission)
        {
            return Proxy.DeleteCardTrancport(idCommission);
        }

        public IEnumerable<StatusCardTrancport> GetStatusesCard()
        {
            return Proxy.GetStatusesCardTrancport();
        }
        #endregion
    }
}
