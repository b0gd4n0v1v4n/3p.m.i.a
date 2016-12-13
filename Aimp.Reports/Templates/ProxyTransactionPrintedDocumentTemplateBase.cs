using Aimp.Model;
using Aimp.Reports.Helpers;
using Entities;

namespace Aimp.Reports.Templates
{
    public abstract class ProxyTransactionPrintedDocumentTemplateBase : TransactionPrintedDocumentTemplateBase
    {
        private TransactionProxy _transactionProxy;

        public ProxyTransactionPrintedDocumentTemplateBase(TransactionProxy transaction)
            : base(transaction)
        {
            _transactionProxy = transaction;
        }

        protected void ProxyDataFill()
        {
            LabelValues.Add("месяц_доверенность", _transactionProxy.DateProxy?.Month.ToString());
            LabelValues.Add("дата_доверенность", _transactionProxy.DateProxy?.ToString(DataFormats.DateFormat));
            LabelValues.Add("номер_доверенность", _transactionProxy.NumberProxy);
            LabelValues.Add("номер_реестр", _transactionProxy.NumberRegistry);
        }

        protected void OwnerFill()
        {
            if (_transactionProxy.Owner != null)
            {
                LabelValues.Add("соб_фио", ReportHelper.GetFullName(_transactionProxy.Owner));
                LabelValues.Add("соб_фио_сокр", ReportHelper.GetShortName(_transactionProxy.Owner));
                LabelValues.Add("соб_контрагент_и", ReportHelper.GetContractorName(_transactionProxy.Owner));
                LabelValues.Add("соб_контрагент_р", ReportHelper.GetContractorNameGenitive(_transactionProxy.Owner));
                LabelValues.Add("соб_подпись", ReportHelper.GetContractorSignature(_transactionProxy.Owner));
                LabelValues.Add("соб_данные", ReportHelper.GetOwnerData(_transactionProxy.Owner));
            }
            else
            {
                LabelValues.Add("соб_фио", null);
                LabelValues.Add("соб_фио_сокр", null);
                LabelValues.Add("соб_контрагент_и", null);
                LabelValues.Add("соб_контрагент_р", null);
                LabelValues.Add("соб_подпись", null);
                LabelValues.Add("соб_данные", null);
            }
        }
    }
}
