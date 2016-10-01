﻿using System.Collections.Generic;
using Aimp.Entities;
using Aimp.PrintedDocument.Helpers;

namespace Aimp.PrintedDocument.Templates
{
    public class CashTransactionPrintedDocumentTemplate : IPrintedDocumentTemplate
    {
        private Dictionary<string, string> _labelValues; 
        public CashTransactionPrintedDocumentTemplate(ICashTransaction transaction,byte[] templateFile)
        {
            TemplateFile = templateFile;
            _labelValues = new Dictionary<string, string>();
            _labelValues.Add("месяц_доверенность", transaction.DateProxy?.Month.ToString());
            _labelValues.Add("месяц_ад", transaction.DateProxy?.Month.ToString());
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
            _labelValues.Add("дата", transaction.Date.ToString("dd.MM.yyyy"));

            _labelValues.Add("дата_доверенность", transaction.DateProxy?.ToString("dd.MM.yyyy"));
            _labelValues.Add("номер_доверенность", transaction.NumberProxy);
            _labelValues.Add("номер_реестр", transaction.NumberRegistry);
            _labelValues.Add("пропись", MoneyToText.Convert(transaction.Price));
            _labelValues.Add("стоимость", transaction.Price.ToString());

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
        public PrintedDocumentTemplateType Type => PrintedDocumentTemplateType.Сделка;
        public byte[] TemplateFile { get; }

        public string FileName
        {
            get;
        }
    }
}
