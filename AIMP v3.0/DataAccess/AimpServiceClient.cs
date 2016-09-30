using Models;
using Models.CashTransact;
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
using System;
using System.ServiceModel;

namespace AIMP_v3._0.DataAccess
{
    public class AimpServiceClient : ClientBase<IAimpService>, IAimpService
    {
        private void _SetHeaderRequestLoginAndPassword()
        {
            var headers = WebOperationContext.Current.OutgoingRequest.Headers;

            headers.Add("login", ConnectionSettings.Login);

            headers.Add("password", ConnectionSettings.Password);
        }
        public AimpServiceClient()
        {
            _SetHeaderRequestLoginAndPassword();
        }
        public AimpUserDto Auth()
        {
            return Channel.Auth();
        }
        #region client report
        public ClientReports GetClientReports()
        {
            return Channel.GetClientReports();
        }
        public Models.ReportOfClient.ClientReport GetClientReport(int? id)
        {
            if (id == null)
                return Channel.GetNewClientReport();
            else
                return Channel.GetClientReport((int)id);
        }
        public Response SaveClientReport(ClientReportDocument document)
        {
            return Channel.SaveClientReport(document);
        }
        public Response DeleteClientReport(ClientReportDocument document)
        {
            return Channel.DeleteClientReport(document);
        }
        #endregion
        #region cash transaction
        public CashTransactionsDto GetCashTransactions()
        {
            return Channel.GetCashTransactions();
        }
        public CashTransactionDto GetCashTransaction(int id)
        {
            return Channel.GetCashTransaction(id);
        }
        public SaveEntityResult SaveCashTransaction(CashTransactionDocument document)
        {
            return Channel.SaveCashTransaction(document);
        }
        public Response DeleteCashTransaction(CashTransactionDocument document)
        {
            return Channel.DeleteCashTransaction(document);
        }
        #endregion
        #region commission
        public CommissionsDto GetCommissions()
        {
            return Channel.GetCommissions();
        }
        public CommissionDto GetCommission(int id)
        {
            return Channel.GetCommission(id);
        }
        public SaveEntityResult SaveCommission(CommissionDocument document)
        {
            return Channel.SaveCommission(document);
        }
        public Response DeleteCommission(CommissionDocument document)
        {
            return Channel.DeleteCommission(document);
        }
        public SourcesTrancportDto GetSourceTrancport()
        {
            return Channel.GetSourceTrancport();
        }
        #endregion
        #region credit transaction
        public CreditTransactions GetCreditTransactions()
        {
            return Channel.GetCreditTransactions();
        }
        public CreditTransactionDto GetCreditTransaction(int id)
        {
            return Channel.GetCreditTransaction(id);
        }
        public SaveEntityResult SaveCreditTransaction(CreditTransactionDocument document)
        {
            return Channel.SaveCreditTransaction(document);
        }
        public Response DeleteCreditTransaction(CreditTransactionDocument document)
        {
            return Channel.DeleteCreditTransaction(document);
        }
        #endregion
        public TrancportInfo GetTrancportInfo()
        {
            return Channel.GetTrancportInfo();
        }
        public ContractorInfo GetContractorInfo()
        {
            return Channel.GetContractorInfo();
        }
        public SearchContractorResult SearchContractor(TypeSearchContractor type, string text)
        {
            return Channel.SearchContractors(type, text);
        }
        public SearchTrancportResult SearchTranports(TypeSearchTrancport type, string text)
        {
            return Channel.SearchTrancports(type, text);
        }
        public UserFileDto GetUserFile(int id)
        {
            return Channel.GetUserFile(id);
        }
        public SaveEntityResult SaveContractor(Contractor contractor)
        {
            return Channel.SaveContractor(contractor);
        }
        public SaveEntityResult SaveTrancport(Trancport trancport)
        {
            return Channel.SaveTrancport(trancport);
        }
        public UserRightsDto GetUserRights(int id)
        {
            return Channel.GetUserRights(id);
        }
        public UsersDto GetUsers()
        {
            return Channel.GetUsers();
        }

        public SaveEntityResult SaveUser(User user, IEnumerable<string> rightIds)
        {
            return Channel.SaveUser(user, rightIds);
        }

        public Response DeleteUser(User user)
        {
            return Channel.DeleteUser(user);
        }

        public WordPrintedDocumentDto GetPrintedDocument(DocumentType type, string name, int id)
        {
            return Channel.GetPrintedDocument(type, name, id);
        }

        public PrintedDocumentsListDto GetPrintedList(DocumentType type)
        {
            return Channel.GetPrintedList(type);
        }
        public PrintedDocumentTemplateDto GetPrintDocTemplate(int id)
        {
            return Channel.GetPrintedDocTemplate(id);
        }
        public PrintedDocumentTemplatesListDto GetListPrintDocTemplateDto()
        {
            return Channel.GetPrintedDocTemplatesList();
        }
        public SaveEntityResult SavePrintDocTemplate(PrintedDocumentTemplate template)
        {
            return Channel.SavePrintedDocTemplate(template);
        }
        public Response DeletePrintedDocTemplate(PrintedDocumentTemplate template)
        {
            return Channel.DeletePrintedDocTemplate(template);
        }
        public DictionaryDto GetDictionary(string tableName, IEnumerable<string> columns)
        {
            return Channel.GetDictionary(tableName, columns);
        }

        public Response SaveRowDictionary(string tableName, string value, int id)
        {
            return Channel.SaveRowDictionary(tableName, value, id);
        }
        public Response DeleteRowDictionary(string tableName, int id)
        {
            return Channel.DeleteRowDictionary(tableName, id);
        }
        public Response SaveRowValuesDictionary(string tableName, IDictionary<string, string> columnValues, int id)
        {
            return Channel.SaveRowValuesDictionary(tableName, columnValues, id);
        }
        public ExcelPrintedDocumentDto GetClientReportList(ClientReports reports)
        {
            return Channel.GetClientReportPrintedDocument(reports);
        }
        public CreditTransactionInfoDto GetCreditInfo()
        {
            return Channel.GetCreditTransactionInfo();
        }
        #region card trancport
        public CardTrancportsDto GetCardsTrancport()
        {
            return Channel.GetCardsTrancport();
        }
        public CardTrancportDto GetCardTrancport(int id)
        {
            return Channel.GetCardTrancport(id);
        }
        public SaveEntityResult AddCardTrancport(int idCommission, DateTime dateStart)
        {
            return Channel.AddCardTrancport(idCommission, dateStart);
        }
        public SaveEntityResult SaveCardTrancport(CardTrancportDocument document)
        {
            return Channel.SaveCardTrancport(document);
        }
        public Response DeleteCardTrancport(int idCommission)
        {
            return Channel.DeleteCardTrancport(idCommission);
        }

        public StatusesCardTrancportDto GetStatusesCard()
        {
            return Channel.GetStatusesCardTrancport();
        }

        public Models.ReportOfClient.ClientReport GetNewClientReport()
        {
            throw new NotImplementedException();
        }

        public Models.ReportOfClient.ClientReport GetClientReport(int id)
        {
            throw new NotImplementedException();
        }

        public CreditTransactionInfoDto GetCreditTransactionInfo()
        {
            throw new NotImplementedException();
        }

        public StatusesCardTrancportDto GetStatusesCardTrancport()
        {
            throw new NotImplementedException();
        }

        public TrancportDto GetTrancport(int id)
        {
            throw new NotImplementedException();
        }

        public SearchContractorResult SearchContractors(TypeSearchContractor type, string text)
        {
            throw new NotImplementedException();
        }

        public SearchTrancportResult SearchTrancports(TypeSearchTrancport type, string text)
        {
            throw new NotImplementedException();
        }

        public PrintedDocumentTemplateDto GetPrintedDocTemplate(int id)
        {
            throw new NotImplementedException();
        }

        public PrintedDocumentTemplatesListDto GetPrintedDocTemplatesList()
        {
            throw new NotImplementedException();
        }

        public SaveEntityResult SavePrintedDocTemplate(PrintedDocumentTemplate template)
        {
            throw new NotImplementedException();
        }

        public ExcelPrintedDocumentDto GetClientReportPrintedDocument(ClientReports reports)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
