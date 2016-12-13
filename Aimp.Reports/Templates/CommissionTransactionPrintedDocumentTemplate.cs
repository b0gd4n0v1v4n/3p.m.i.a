using System.Collections.Generic;
using Aimp.Reports.Helpers;
using Entities;
using Aimp.Model.PrintedDocument.Templates;

namespace Aimp.Reports.Templates
{
    public class CommissionTransactionPrintedDocumentTemplate : ProxyTransactionPrintedDocumentTemplateBase, IPrintedDocumentTemplate
    {
        public CommissionTransactionPrintedDocumentTemplate(CommissionTransaction commission, byte[] templateFile, string fileName)
            : base(commission)
        {
            FileName = fileName;
            TemplateFile = templateFile;

            TransactionDataFill();
            SellerFill();
            TrancportFill();

            ProxyDataFill();
            OwnerFill();

            LabelValues.Add("комиссия_пропись", MoneyToText.Convert(commission.Commission));
            LabelValues.Add("стоянка_пропись", MoneyToText.Convert(commission.Parking));
            LabelValues.Add("пропись_стоянка", MoneyToText.Convert(commission.Parking));
            LabelValues.Add("комиссия", commission.Commission.ToString());
            LabelValues.Add("стоянка", commission.Parking.ToString());
            LabelValues.Add("второй_месяц", commission.IsTwoMounth ? "со второго месяца" : string.Empty);
        }

        public PrintedDocumentTemplateType Type => PrintedDocumentTemplateType.Комиссия;
        public byte[] TemplateFile { get; }

        public string FileName
        {
            get;
        }
    }
}

