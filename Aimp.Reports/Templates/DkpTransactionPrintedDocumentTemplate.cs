using System.Collections.Generic;
using Aimp.Model.PrintedDocument.Templates;
using Entities;

namespace Aimp.Reports.Templates
{
    public class DkpTransactionPrintedDocumentTemplate : CreditTransactionPrintedDocumentTemplateBase, IPrintedDocumentTemplate
    {
        private Dictionary<string, string> _labelValues;
        public DkpTransactionPrintedDocumentTemplate(CreditTransaction transaction, byte[] templateFile, string fileName)
            : base(transaction)
        {
            FileName = fileName;
            TemplateFile = templateFile;
            _labelValues = new Dictionary<string, string>();

            TransactionDataFill();

            BuyerFill();
            SellerFill();
            TrancportFill();

            ProxyDataFill();
            OwnerFill();

            CreditDataFill();
        }


        public Dictionary<string, string> LabelValues => _labelValues;
        public PrintedDocumentTemplateType Type => PrintedDocumentTemplateType.Дкп;
        public byte[] TemplateFile { get; }

        public string FileName
        {
            get;
        }
    }
}
