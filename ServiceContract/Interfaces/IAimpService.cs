using System.Collections.Generic;
using Models;
using Models.CashTransact;
using Models.ContractorInfo;
using Models.CreditTransact;
using Models.Documents;
using Models.ReportOfClient;
using Models.TrancportInfo;
using System.ServiceModel;
using System.ServiceModel.Web;
using Models.Entities;
using Models.PrintedDocument;
using Models.PrintedDocument.Templates;
using Models.SecurityRigths;
using Models.UserFiles;
using ClientReport = Models.ReportOfClient.ClientReport;
using Models.PrintDocumentTemplate;
using Models.Commission;
using Models.CardTrancports;
using System;

namespace ServiceContract.Interfaces
{
    [ServiceContract(ConfigurationName = "Service")]
    [ServiceKnownType(typeof(DocumentType))]
    public interface IAimpService
    {
        #region client reports
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        ClientReports GetClientReports();

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        ClientReport GetNewClientReport();

        [OperationContract]
        [WebInvoke(Method = "POST",RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        ClientReport GetClientReport(int id);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json)]
        Response SaveClientReport(ClientReportDocument document);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json)]
        Response DeleteClientReport(ClientReportDocument document);
        #endregion
        #region cash tranasction
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        CashTransactionsDto GetCashTransactions();

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        CashTransactionDto GetCashTransaction(int id);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json)]
        SaveEntityResult SaveCashTransaction(CashTransactionDocument document);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json)]
        Response DeleteCashTransaction(CashTransactionDocument document);
        #endregion
        #region commission
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        CommissionsDto GetCommissions();
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        SourcesTrancportDto GetSourceTrancport();
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        CommissionDto GetCommission(int id);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json)]
        SaveEntityResult SaveCommission(CommissionDocument document);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json)]
        Response DeleteCommission(CommissionDocument document);
        #endregion
        #region credit tranasction
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        CreditTransactions GetCreditTransactions();

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        CreditTransactionDto GetCreditTransaction(int id);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json)]
        SaveEntityResult SaveCreditTransaction(CreditTransactionDocument document);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json)]
        Response DeleteCreditTransaction(CreditTransactionDocument document);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json)]
        CreditTransactionInfoDto GetCreditTransactionInfo();
        #endregion
        #region card trancport
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        CardTrancportsDto GetCardsTrancport();

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        CardTrancportDto GetCardTrancport(int id);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        SaveEntityResult AddCardTrancport(int idCommission,DateTime dateStart);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json)]
        SaveEntityResult SaveCardTrancport(CardTrancportDocument document);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json)]
        Response DeleteCardTrancport(int idCommission);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        StatusesCardTrancportDto GetStatusesCardTrancport();
        #endregion

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        TrancportDto GetTrancport(int id);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        TrancportInfo GetTrancportInfo();

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        ContractorInfo GetContractorInfo();

        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        SearchContractorResult SearchContractors(TypeSearchContractor type, string text);

        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        SearchTrancportResult SearchTrancports(TypeSearchTrancport type, string text);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        UserFileDto GetUserFile(int id);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        SaveEntityResult SaveContractor(Contractor contractor);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        SaveEntityResult SaveTrancport(Trancport trancport);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        UserRightsDto GetUserRights(int id);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        AimpUserDto Auth();

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        UsersDto GetUsers();

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,BodyStyle = WebMessageBodyStyle.Wrapped)]
        SaveEntityResult SaveUser(User user,IEnumerable<string> rightIds);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Response DeleteUser(User user);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        WordPrintedDocumentDto GetPrintedDocument(DocumentType type,string name,int id);

        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        PrintedDocumentsListDto GetPrintedList(DocumentType name);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        PrintedDocumentTemplateDto GetPrintedDocTemplate(int id);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        PrintedDocumentTemplatesListDto GetPrintedDocTemplatesList();

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        SaveEntityResult SavePrintedDocTemplate(PrintedDocumentTemplate template);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Response DeletePrintedDocTemplate(PrintedDocumentTemplate template);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        DictionaryDto GetDictionary(string tableName, IEnumerable<string> columns);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        Response SaveRowDictionary(string tableName,string value,int id);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        Response DeleteRowDictionary(string tableName, int id);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        Response SaveRowValuesDictionary(string tableName, IDictionary<string,string> columnValues,int id);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ExcelPrintedDocumentDto GetClientReportPrintedDocument(ClientReports reports);
    }
}
