using Entities;
using Aimp.Model.PrintedDocument.Templates;

namespace Aimp.Reports.Templates
{
    public class CashTransactionPrintedDocumentTemplate : ProxyTransactionPrintedDocumentTemplateBase,  IPrintedDocumentTemplate
    { 
        public CashTransactionPrintedDocumentTemplate(CashTransaction transaction,byte[] templateFile, string fileName)
            :base(transaction)
        {
            FileName = fileName;
            TemplateFile = templateFile;

            TransactionDataFill();
            
            BuyerFill();
            SellerFill();
            TrancportFill();
           
            ProxyDataFill();
            OwnerFill();
        }
        
        public PrintedDocumentTemplateType Type => PrintedDocumentTemplateType.Сделка;
        public byte[] TemplateFile { get; }

        public string FileName
        {
            get;
        }
    }
}
