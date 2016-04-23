using Models;
using Models.CashTransact;
using Models.ContractorInfo;
using Models.CreditTransact;
using Models.Documents;
using Models.Entities;
using Models.ReportOfClient;
using Models.TrancportInfo;
using ServiceContract.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.ServiceModel.Web;
using Models.PrintedDocument;
using Models.PrintedDocument.Templates;
using Models.SecurityRigths;
using Models.UserFiles;
using Models.PrintDocumentTemplate;
using Models.Commission;
using Models.CardTrancports;

namespace AIMP_v3._0.DataAccess
{
    public class AimpService : BaseHttpServer<IAimpService>
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
        public Models.ReportOfClient.ClientReport GetClientReport(int? id)
        {
            if (id == null)
                return Proxy.GetNewClientReport();
            else
                return Proxy.GetClientReport((int)id);
        }
        public Response SaveClientReport(ClientReportDocument document)
        {
            return Proxy.SaveClientReport(document);
        }
        public Response DeleteClientReport(ClientReportDocument document)
        {
            return Proxy.DeleteClientReport(document);
        }
        #endregion
        #region cash transaction
        public CashTransactionsDto GetCashTransactions()
        {
            return Proxy.GetCashTransactions();
        }
        public CashTransactionDto GetCashTransaction(int id)
        {
            return Proxy.GetCashTransaction(id);
        }
        public SaveEntityResult SaveCashTransaction(CashTransactionDocument document)
        {
            return Proxy.SaveCashTransaction(document);
        }
        public Response DeleteCashTransaction(CashTransactionDocument document)
        {
            return Proxy.DeleteCashTransaction(document);
        }
        #endregion
        #region commission
        public CommissionsDto GetCommissions()
        {
            return Proxy.GetCommissions();
        }
        public CommissionDto GetCommission(int id)
        {
            return Proxy.GetCommission(id);
        }
        public SaveEntityResult SaveCommission(CommissionDocument document)
        {
            return Proxy.SaveCommission(document);
        }
        public Response DeleteCommission(CommissionDocument document)
        {
            return Proxy.DeleteCommission(document);
        }
        public SourcesTrancportDto GetSourceTrancport()
        {
            return Proxy.GetSourceTrancport();       }
        #endregion
        #region credit transaction
        public CreditTransactions GetCreditTransactions()
        {
            return Proxy.GetCreditTransactions();
        }
        public CreditTransactionDto GetCreditTransaction(int id)
        {
            return Proxy.GetCreditTransaction(id);
        }
        public SaveEntityResult SaveCreditTransaction(CreditTransactionDocument document)
        {
            return Proxy.SaveCreditTransaction(document);
        }
        public Response DeleteCreditTransaction(CreditTransactionDocument document)
        {
            return Proxy.DeleteCreditTransaction(document);
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
        public SearchContractorResult SearchContractor(TypeSearchContractor type, string text)
        {
            return Proxy.SearchContractors(type, text);
        }
        public SearchTrancportResult SearchTranports(TypeSearchTrancport type, string text)
        {
            return Proxy.SearchTrancports(type, text);
        }
        public UserFileDto GetUserFile(int id)
        {
            return Proxy.GetUserFile(id);
        }
        public SaveEntityResult SaveContractor(Contractor contractor)
        {
            return Proxy.SaveContractor(contractor);
        }
        public SaveEntityResult SaveTrancport(Trancport trancport)
        {
            return Proxy.SaveTrancport(trancport);
        }
        public UserRightsDto GetUserRights(int id)
        {
            return Proxy.GetUserRights(id);
        }
        public UsersDto GetUsers()
        {
            return Proxy.GetUsers();
        }

        public SaveEntityResult SaveUser(User user, IEnumerable<string> rightIds)
        {
            return Proxy.SaveUser(user, rightIds);
        }

        public Response DeleteUser(User user)
        {
            return Proxy.DeleteUser(user);
        }

        public WordPrintedDocumentDto GetPrintedDocument(DocumentType type, string name,int id)
        {
            return Proxy.GetPrintedDocument(type, name,id);
        }

        public PrintedDocumentsListDto GetPrintedList(DocumentType type)
        {
            return Proxy.GetPrintedList(type);
        }
        public PrintedDocumentTemplateDto GetPrintDocTemplate(int id)
        {
           return  Proxy.GetPrintedDocTemplate(id);
        }
        public PrintedDocumentTemplatesListDto GetListPrintDocTemplateDto()
        {
            return Proxy.GetPrintedDocTemplatesList();
        }
        public SaveEntityResult SavePrintDocTemplate(PrintedDocumentTemplate template)
        {
            return Proxy.SavePrintedDocTemplate(template);
        }
        public Response DeletePrintedDocTemplate(PrintedDocumentTemplate template)
        {
            return Proxy.DeletePrintedDocTemplate(template);
        }
        public DictionaryDto GetDictionary(string tableName)
        {
            return Proxy.GetDictionary(tableName);
        }

        public Response SaveRowDictionary(string tableName, string value,int id)
        {
            return Proxy.SaveRowDictionary(tableName, value,id);
        }
        public Response DeleteRowDictionary(string tableName, int id)
        {
            return Proxy.DeleteRowDictionary(tableName, id);
        }
        public Response SaveRowValuesDictionary(string tableName, IDictionary<string, string> columnValues, int id)
        {
            return Proxy.SaveRowValuesDictionary(tableName, columnValues, id);
        }
        public ExcelPrintedDocumentDto GetClientReportList(ClientReports reports)
        {
            return Proxy.GetClientReportPrintedDocument(reports);
        }public CreditTransactionInfoDto GetCreditInfo()
        {
            return Proxy.GetCreditTransactionInfo();
        }
        #region card trancport
        public CardTrancportsDto GetCardsTrancport()
        {
            return Proxy.GetCardsTrancport();
        }
        public CardTrancportDto GetCardTrancport(int id)
        {
            return Proxy.GetCardTrancport(id);
        }
        public SaveEntityResult AddCardTrancport(int idCommission)
        {
            return Proxy.AddCardTrancport(idCommission);
        }
        public SaveEntityResult SaveCardTrancport(CardTrancportDocument document)
        {
            return Proxy.SaveCardTrancport(document);
        }
        public Response DeleteCardTrancport(int idCommission)
        {
            return Proxy.DeleteCardTrancport(idCommission);
        }

        public StatusesCardTrancportDto GetStatusesCard()
        {
            return Proxy.GetStatusesCardTrancport();
        }
        #endregion
    }
}
