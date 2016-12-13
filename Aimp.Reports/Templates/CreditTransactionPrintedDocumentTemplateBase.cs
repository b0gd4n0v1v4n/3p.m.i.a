using Aimp.Model;
using Aimp.Reports.Helpers;
using Entities;

namespace Aimp.Reports.Templates
{
    public abstract class CreditTransactionPrintedDocumentTemplateBase : ProxyTransactionPrintedDocumentTemplateBase
    {
        private CreditTransaction _transaction;

        public CreditTransactionPrintedDocumentTemplateBase(CreditTransaction transaction) 
            : base(transaction)
        {
            _transaction = transaction;
        }

        protected void CreditDataFill()
        {
            LabelValues.Add("банк_кредитор", _transaction.Creditor.Name);
            LabelValues.Add("пропись_итого_комиссия",
                MoneyToText.Convert(((_transaction.CreditSumm -
                                      (_transaction.RealPrice - _transaction.DownPaymentCashbox)) -
                                     _transaction.ReportInsurance - _transaction.Rollback +
                                     _transaction.CommissionCashbox)));
            LabelValues.Add("итого_комиссия",
                ((_transaction.CreditSumm - (_transaction.RealPrice - _transaction.DownPaymentCashbox)) -
                 _transaction.ReportInsurance - _transaction.Rollback + _transaction.CommissionCashbox).ToString());
            LabelValues.Add("остаток_в_сумме",
                (_transaction.CreditSumm - (_transaction.RealPrice - _transaction.DownPaymentCashbox)).ToString());
            LabelValues.Add("пропись_остаток_в_сумме",
                MoneyToText.Convert(_transaction.CreditSumm -
                                    (_transaction.RealPrice - _transaction.DownPaymentCashbox)));
            LabelValues.Add("к_выдаче_продавцу", (_transaction.RealPrice - _transaction.DownPaymentCashbox).ToString());
            LabelValues.Add("пропись_к_выдаче_продавцу",
                MoneyToText.Convert(_transaction.RealPrice - _transaction.DownPaymentCashbox));
            LabelValues.Add("пропись_комиссия_касса", MoneyToText.Convert(_transaction.CommissionCashbox));
            LabelValues.Add("пропись_сумма_кредит", MoneyToText.Convert(_transaction.CreditSumm));
            LabelValues.Add("пропись_первый_взнос", MoneyToText.Convert(_transaction.DownPayment));
            LabelValues.Add("пропись_стоимость_банк", MoneyToText.Convert(_transaction.PriceBank));
            LabelValues.Add("реквизит", _transaction.Requisit.Name);
            LabelValues.Add("бик_реквизит", _transaction.Requisit.Bik);
            LabelValues.Add("рс_реквизит", _transaction.Requisit.Ros_schet);
            LabelValues.Add("кс_реквизит", _transaction.Requisit.Kor_schet);
            LabelValues.Add("в_банке_реквизит", _transaction.Requisit.InBank);

            LabelValues.Add("стоимость_банк", _transaction.PriceBank.ToString());
            LabelValues.Add("первый_взнос", _transaction.DownPayment.ToString());
            LabelValues.Add("сумма_кредит", _transaction.CreditSumm.ToString());
            LabelValues.Add("стоимость_реальная", _transaction.RealPrice.ToString());
            LabelValues.Add("первый_взнос_касса", _transaction.DownPaymentCashbox.ToString());
            LabelValues.Add("отчёт_по_страховым", _transaction.ReportInsurance.ToString());
            LabelValues.Add("откат", _transaction.Rollback.ToString());
            LabelValues.Add("источник", _transaction.Source);
            LabelValues.Add("комиссия_Касса", _transaction.CommissionCashbox.ToString());

            LabelValues.Add("дата_ад", _transaction.DateAgent?.ToString(DataFormats.DateFormat));
            LabelValues.Add("месяц_ад", _transaction.DateAgent?.Date.Month.ToString());
        }
    }
}
