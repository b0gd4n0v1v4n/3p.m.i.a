using System;
using System.Collections.Generic;
using AimpReports.Helpers;
using Models.Entities;
using Models.PrintedDocument.Templates;

namespace AimpReports.Templates
{
    public class CommissionTransactionPrintedDocumentTemplate : IPrintedDocumentTemplate
    {
        private Dictionary<string, string> _labelValues;

        public CommissionTransactionPrintedDocumentTemplate(CommissionTransaction commission, byte[] templateFile)
        {
            TemplateFile = templateFile;
            _labelValues = new Dictionary<string, string>();

            _labelValues.Add("комиссия_пропись", MoneyToText.Convert(commission.Commission));
            _labelValues.Add("стоянка_пропись", MoneyToText.Convert(commission.Parking));
            _labelValues.Add("пропись_стоянка", MoneyToText.Convert(commission.Parking));
            _labelValues.Add("комиссия", commission.Commission.ToString());
            _labelValues.Add("стоянка", commission.Parking.ToString());
            _labelValues.Add("второй_месяц", commission.IsTwoMounth ? "со второго месяца" : string.Empty);

            _labelValues.Add("месяц_доверенность", commission.DateProxy?.Month.ToString());
            _labelValues.Add("месяц_ад", commission.DateProxy?.Month.ToString());
            _labelValues.Add("месяц", commission.Date.Month.ToString());
            _labelValues.Add("имя_мен", commission.User.FirstName);
            _labelValues.Add("фамилия_мен", commission.User.LastName);
            _labelValues.Add("отчество_мен", commission.User.MiddleName);
            _labelValues.Add("имя_р__мен", commission.User.FirstNameGenitive);
            _labelValues.Add("фамилия_р_мен", commission.User.LastNameGenitive);
            _labelValues.Add("отчество_р__мен", commission.User.MiddleNameGenitive);
            _labelValues.Add("номер_мен", commission.User.Number);
            _labelValues.Add("дата_мен", commission.User.Date.ToString("dd.MM.yyyy"));
            _labelValues.Add("номер", commission.Number.ToString());

            _labelValues.Add("дата", commission.Date.ToString("dd.MM.yyyy"));
            _labelValues.Add("дата_доверенность", commission.DateProxy?.ToString("dd.MM.yyyy"));
            _labelValues.Add("номер_доверенность", commission.NumberProxy);
            _labelValues.Add("номер_реестр", commission.NumberRegistry);
            _labelValues.Add("стоимость", commission.Price.ToString());
            _labelValues.Add("пропись", MoneyToText.Convert(commission.Price));

            if (commission.Seller != null)
            {
                _labelValues.Add("п_фио", ReportHelper.GetFullName(commission.Seller));
                _labelValues.Add("п_фио_сокр", ReportHelper.GetShortName(commission.Seller));
                _labelValues.Add("п_контрагент_и", ReportHelper.GetContractorName(commission.Seller));
                _labelValues.Add("п_контрагент_р", ReportHelper.GetContractorNameGenitive(commission.Seller));
                _labelValues.Add("п_подпись", ReportHelper.GetContractorSignature(commission.Seller));
            }
            else
            {
                _labelValues.Add("п_фио", null);
                _labelValues.Add("п_фио_сокр", null);
                _labelValues.Add("п_контрагент_и", null);
                _labelValues.Add("п_контрагент_р", null);
                _labelValues.Add("п_подпись", null);
            }

            //if (commission.Buyer != null)
            //{
            //    _labelValues.Add("по_фио", ReportHelper.GetFullName(commission.Buyer));
            //    _labelValues.Add("по_фио_сокр", ReportHelper.GetShortName(commission.Buyer));
            //    _labelValues.Add("по_контрагент_и", ReportHelper.GetContractorName(commission.Buyer));
            //    _labelValues.Add("по_контрагент_р", ReportHelper.GetContractorNameGenitive(commission.Buyer));
            //    _labelValues.Add("по_подпись", ReportHelper.GetContractorSignature(commission.Buyer));
            //}
            //else
            //{
            //    _labelValues.Add("по_фио", null);
            //    _labelValues.Add("по_фио_сокр", null);
            //    _labelValues.Add("по_контрагент_и", null);
            //    _labelValues.Add("по_контрагент_р", null);
            //    _labelValues.Add("по_подпись", null);
            //}

            if (commission.Owner != null)
            {
                _labelValues.Add("соб_фио", ReportHelper.GetFullName(commission.Owner));
                _labelValues.Add("соб_фио_сокр", ReportHelper.GetShortName(commission.Owner));
                _labelValues.Add("соб_контрагент_и", ReportHelper.GetContractorName(commission.Owner));
                _labelValues.Add("соб_контрагент_р", ReportHelper.GetContractorNameGenitive(commission.Owner));
                _labelValues.Add("соб_подпись", ReportHelper.GetContractorSignature(commission.Owner));
            }
            else
            {
                _labelValues.Add("соб_фио", null);
                _labelValues.Add("соб_фио_сокр", null);
                _labelValues.Add("соб_контрагент_и", null);
                _labelValues.Add("соб_контрагент_р", null);
                _labelValues.Add("соб_подпись", null);
            }

            _labelValues.Add("вид_тс", commission.Trancport.Type.Name);
            _labelValues.Add("год_тс", commission.Trancport.Year.ToString());
            _labelValues.Add("вин", commission.Trancport.Vin);
            _labelValues.Add("гос_номер_тс", commission.Trancport.Number);
            _labelValues.Add("дата_птс", commission.Trancport.DatePts?.ToString("dd.MM.yyyy"));
            _labelValues.Add("дата_cтс", commission.Trancport.DateSts?.ToString("dd.MM.yyyy"));
            _labelValues.Add("изготовитель_тс", commission.Trancport.Maker);
            _labelValues.Add("категория_тc", commission.Trancport.Category.Name);
            _labelValues.Add("кем_птс", commission.Trancport.ByPts);
            _labelValues.Add("кем_стс", commission.Trancport.BySts);
            _labelValues.Add("кузов", commission.Trancport.BodyNumber);
            _labelValues.Add("макс_масса", commission.Trancport.MaxMass);
            _labelValues.Add("марка_двиг", commission.Trancport.EngineMake);
            _labelValues.Add("марка_тс", commission.Trancport.Make.Name);
            _labelValues.Add("модель_тс", commission.Trancport.Model.Name);
            _labelValues.Add("масса", commission.Trancport.Mass);
            _labelValues.Add("мощность", commission.Trancport.Strong);
            _labelValues.Add("номер_птс", commission.Trancport.NumberPts);
            _labelValues.Add("номер_стс", commission.Trancport.NumberSts);
            _labelValues.Add("объем", commission.Trancport.Volume);
            _labelValues.Add("па", commission.Trancport.Pa);
            _labelValues.Add("серия_птс", commission.Trancport.SerialPts);
            _labelValues.Add("серия_стс", commission.Trancport.SerialSts);
            _labelValues.Add("типы_двигателей_тс", commission.Trancport.EngineType.Name);
            _labelValues.Add("цвет", commission.Trancport.Color);
            _labelValues.Add("шасси", commission.Trancport.ChassisNumber);
        }

        public Dictionary<string, string> LabelValues => _labelValues;
        public PrintedDocumentTemplateType Type => PrintedDocumentTemplateType.Комиссия;
        public byte[] TemplateFile { get; }

        public string FileName
        {
            get;
        }
    }
}

