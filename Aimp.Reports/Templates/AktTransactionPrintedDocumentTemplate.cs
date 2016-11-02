using System;
using System.Collections.Generic;
using Aimp.Reports.Helpers;
using Aimp.Model.PrintedDocument.Templates;
using Entities;

namespace Aimp.Reports.Templates
{
    public class AktTransactionPrintedDocumentTemplate : IPrintedDocumentTemplate
    {
        private Dictionary<string, string> _labelValues;
        public AktTransactionPrintedDocumentTemplate(CreditTransaction transaction, byte[] templateFile, string fileName)
        {
            FileName = fileName;
            TemplateFile = templateFile;
            _labelValues = new Dictionary<string, string>();
            _labelValues.Add("банк_кредитор", transaction.Creditor.Name);
            _labelValues.Add("пропись_итого_комиссия",
                MoneyToText.Convert(((transaction.CreditSumm -
                                      (transaction.RealPrice - transaction.DownPaymentCashbox)) -
                                     transaction.ReportInsurance - transaction.Rollback +
                                     transaction.CommissionCashbox)));
            _labelValues.Add("итого_комиссия",
                ((transaction.CreditSumm - (transaction.RealPrice - transaction.DownPaymentCashbox)) -
                 transaction.ReportInsurance - transaction.Rollback + transaction.CommissionCashbox).ToString());
            _labelValues.Add("остаток_в_сумме",
                (transaction.CreditSumm - (transaction.RealPrice - transaction.DownPaymentCashbox)).ToString());
            _labelValues.Add("пропись_остаток_в_сумме",
                MoneyToText.Convert(transaction.CreditSumm -
                                    (transaction.RealPrice - transaction.DownPaymentCashbox)));
            _labelValues.Add("к_выдаче_продавцу", (transaction.RealPrice - transaction.DownPaymentCashbox).ToString());
            _labelValues.Add("пропись_к_выдаче_продавцу",
                MoneyToText.Convert(transaction.RealPrice - transaction.DownPaymentCashbox));
            _labelValues.Add("пропись_комиссия_касса", MoneyToText.Convert(transaction.CommissionCashbox));
            _labelValues.Add("пропись_сумма_кредит", MoneyToText.Convert(transaction.CreditSumm));
            _labelValues.Add("пропись_первый_взнос", MoneyToText.Convert(transaction.DownPayment));
            _labelValues.Add("пропись_стоимость_банк", MoneyToText.Convert(transaction.PriceBank));
            _labelValues.Add("реквизит", transaction.Requisit.Name);
            _labelValues.Add("бик_реквизит", transaction.Requisit.Bik);
            _labelValues.Add("рс_реквизит", transaction.Requisit.Ros_schet);
            _labelValues.Add("кс_реквизит", transaction.Requisit.Kor_schet);
            _labelValues.Add("в_банке_реквизит", transaction.Requisit.InBank);

            _labelValues.Add("месяц_доверенность", transaction.DateProxy?.Month.ToString());
            _labelValues.Add("месяц_ад", transaction.DateAgent?.Month.ToString());
            _labelValues.Add("месяц", transaction.Date.Month.ToString());
            _labelValues.Add("имя_мен", transaction.User.FirstName);
            _labelValues.Add("фамилия_мен", transaction.User.LastName);
            _labelValues.Add("отчество_мен", transaction.User.MiddleName);
            _labelValues.Add("имя_р__мен", transaction.User.FirstNameGenitive);
            _labelValues.Add("фамилия_р_мен", transaction.User.LastNameGenitive);
            _labelValues.Add("отчество_р__мен", transaction.User.MiddleNameGenitive);
            _labelValues.Add("номер_мен", transaction.User.Number);
            _labelValues.Add("дата_мен", transaction.User.Date.ToString("dd.MM.yyyy"));
            _labelValues.Add("номер", transaction.Number.ToString());

            _labelValues.Add("дата_да", transaction.DateAgent?.ToString("dd.MM.yyyy"));
            _labelValues.Add("дата", transaction.Date.ToString("dd.MM.yyyy"));
            _labelValues.Add("дата_доверенность", transaction.DateProxy?.ToString("dd.MM.yyyy"));
            _labelValues.Add("номер_доверенность", transaction.NumberProxy);
            _labelValues.Add("номер_реестр", transaction.NumberRegistry);
            _labelValues.Add("стоимость", transaction.Price.ToString());
            _labelValues.Add("пропись", MoneyToText.Convert(transaction.Price));

            _labelValues.Add("стоимость_банк", transaction.PriceBank.ToString());
            _labelValues.Add("первый_взнос", transaction.DownPayment.ToString());
            _labelValues.Add("сумма_кредит", transaction.CreditSumm.ToString());
            _labelValues.Add("стоимость_реальная", transaction.RealPrice.ToString());
            _labelValues.Add("первый_взнос_касса", transaction.DownPaymentCashbox.ToString());
            _labelValues.Add("отчёт_по_страховым", transaction.ReportInsurance.ToString());
            _labelValues.Add("откат", transaction.Rollback.ToString());
            _labelValues.Add("источник", transaction.Source);
            _labelValues.Add("комиссия_Касса", transaction.CommissionCashbox.ToString());

            if (transaction.Seller != null)
            {
                _labelValues.Add("п_фио", ReportHelper.GetFullName(transaction.Seller));
                _labelValues.Add("п_фио_сокр", ReportHelper.GetShortName(transaction.Seller));
                _labelValues.Add("п_контрагент_и", ReportHelper.GetContractorName(transaction.Seller));
                _labelValues.Add("п_контрагент_р", ReportHelper.GetContractorNameGenitive(transaction.Seller));
                _labelValues.Add("п_подпись", ReportHelper.GetContractorSignature(transaction.Seller));
            }
            else
            {
                _labelValues.Add("п_фио", null);
                _labelValues.Add("п_фио_сокр", null);
                _labelValues.Add("п_контрагент_и", null);
                _labelValues.Add("п_контрагент_р", null);
                _labelValues.Add("п_подпись", null);
            }

            if (transaction.Buyer != null)
            {
                _labelValues.Add("по_фио", ReportHelper.GetFullName(transaction.Buyer));
                _labelValues.Add("по_фио_сокр", ReportHelper.GetShortName(transaction.Buyer));
                _labelValues.Add("по_контрагент_и", ReportHelper.GetContractorName(transaction.Buyer));
                _labelValues.Add("по_контрагент_р", ReportHelper.GetContractorNameGenitive(transaction.Buyer));
                _labelValues.Add("по_подпись", ReportHelper.GetContractorSignature(transaction.Buyer));
            }
            else
            {
                _labelValues.Add("по_фио", null);
                _labelValues.Add("по_фио_сокр", null);
                _labelValues.Add("по_контрагент_и", null);
                _labelValues.Add("по_контрагент_р", null);
                _labelValues.Add("по_подпись", null);
            }

            if (transaction.Owner != null)
            {
                _labelValues.Add("соб_фио", ReportHelper.GetFullName(transaction.Owner));
                _labelValues.Add("соб_фио_сокр", ReportHelper.GetShortName(transaction.Owner));
                _labelValues.Add("соб_контрагент_и", ReportHelper.GetContractorName(transaction.Owner));
                _labelValues.Add("соб_контрагент_р", ReportHelper.GetContractorNameGenitive(transaction.Owner));
                _labelValues.Add("соб_подпись", ReportHelper.GetContractorSignature(transaction.Owner));
            }
            else
            {
                _labelValues.Add("соб_фио", null);
                _labelValues.Add("соб_фио_сокр", null);
                _labelValues.Add("соб_контрагент_и", null);
                _labelValues.Add("соб_контрагент_р", null);
                _labelValues.Add("соб_подпись", null);
            }

            _labelValues.Add("вид_тс", transaction.Trancport.Type.Name);
            _labelValues.Add("год_тс", transaction.Trancport.Year.ToString());
            _labelValues.Add("вин", transaction.Trancport.Vin);
            _labelValues.Add("гос_номер_тс", transaction.Trancport.Number);
            _labelValues.Add("дата_птс", transaction.Trancport.DatePts?.ToString("dd.MM.yyyy"));
            _labelValues.Add("дата_cтс", transaction.Trancport.DateSts?.ToString("dd.MM.yyyy"));
            _labelValues.Add("изготовитель_тс", transaction.Trancport.Maker);
            _labelValues.Add("категория_тc", transaction.Trancport.Category.Name);
            _labelValues.Add("кем_птс", transaction.Trancport.ByPts);
            _labelValues.Add("кем_стс", transaction.Trancport.BySts);
            _labelValues.Add("кузов", transaction.Trancport.BodyNumber);
            _labelValues.Add("макс_масса", transaction.Trancport.MaxMass);
            _labelValues.Add("марка_двиг", transaction.Trancport.EngineMake);
            _labelValues.Add("марка_тс", transaction.Trancport.Make.Name);
            _labelValues.Add("модель_тс", transaction.Trancport.Model.Name);
            _labelValues.Add("масса", transaction.Trancport.Mass);
            _labelValues.Add("мощность", transaction.Trancport.Strong);
            _labelValues.Add("номер_птс", transaction.Trancport.NumberPts);
            _labelValues.Add("номер_стс", transaction.Trancport.NumberSts);
            _labelValues.Add("объем", transaction.Trancport.Volume);
            _labelValues.Add("па", transaction.Trancport.Pa);
            _labelValues.Add("серия_птс", transaction.Trancport.SerialPts);
            _labelValues.Add("серия_стс", transaction.Trancport.SerialSts);
            _labelValues.Add("типы_двигателей_тс", transaction.Trancport.EngineType.Name);
            _labelValues.Add("цвет", transaction.Trancport.Color);
            _labelValues.Add("шасси", transaction.Trancport.ChassisNumber);
        }

        public Dictionary<string, string> LabelValues => _labelValues;
        public PrintedDocumentTemplateType Type => PrintedDocumentTemplateType.Акт;
        public byte[] TemplateFile { get; }

        public string FileName
        {
            get;
        }
    }
}

