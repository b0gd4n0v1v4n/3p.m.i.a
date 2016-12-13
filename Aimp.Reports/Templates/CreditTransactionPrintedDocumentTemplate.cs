using Aimp.Model.PrintedDocument.Templates;
using Entities;

namespace Aimp.Reports.Templates
{
    public class CreditTransactionPrintedDocumentTemplate : CreditTransactionPrintedDocumentTemplateBase, IPrintedDocumentTemplate
    {
        public const string DKP_REPORT_NAME = "ДКП Банк";
        public CreditTransactionPrintedDocumentTemplate(CreditTransaction transaction, byte[] templateFile, string fileName)
            : base(transaction)
        {
            FileName = fileName;
            TemplateFile = templateFile;

            TransactionDataFill();

            SellerFill();
            BuyerFill();
            TrancportFill();

            ProxyDataFill();
            OwnerFill();

            CreditDataFill();
    }
        public PrintedDocumentTemplateType Type => PrintedDocumentTemplateType.Кредит;
        public byte[] TemplateFile { get; }

        public string FileName
        {
            get;
        }
    }
}

