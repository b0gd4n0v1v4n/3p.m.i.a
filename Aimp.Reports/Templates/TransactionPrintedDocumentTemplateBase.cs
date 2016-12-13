using Aimp.Model;
using Aimp.Reports.Helpers;
using Entities;
using System.Collections.Generic;

namespace Aimp.Reports.Templates
{
    public abstract class TransactionPrintedDocumentTemplateBase
    {
        private Transaction _transaction;

        public Dictionary<string, string> LabelValues { get; }

        public TransactionPrintedDocumentTemplateBase(Transaction transaction)
        {
            _transaction = transaction;
            LabelValues = new Dictionary<string, string>();
        }

        protected void TransactionDataFill()
        {
            LabelValues.Add("месяц", _transaction.Date.Month.ToString());
            LabelValues.Add("имя_мен", _transaction.User.FirstName);
            LabelValues.Add("фамилия_мен", _transaction.User.LastName);
            LabelValues.Add("отчество_мен", _transaction.User.MiddleName);
            LabelValues.Add("имя_р__мен", _transaction.User.FirstNameGenitive);
            LabelValues.Add("фамилия_р_мен", _transaction.User.LastNameGenitive);
            LabelValues.Add("отчество_р__мен", _transaction.User.MiddleNameGenitive);
            LabelValues.Add("номер_мен", _transaction.User.Number);

            LabelValues.Add("дата_мен", _transaction.User.Date.ToString(DataFormats.DateFormat));
            LabelValues.Add("номер", _transaction.Number.ToString());
            LabelValues.Add("дата", _transaction.Date.ToString(DataFormats.DateFormat));
            
            LabelValues.Add("пропись", MoneyToText.Convert(_transaction.Price));
            LabelValues.Add("стоимость", _transaction.Price.ToString());
        }
        protected void BuyerFill()
        {
            if (_transaction.Buyer != null)
            {
                LabelValues.Add("по_фио", ReportHelper.GetFullName(_transaction.Buyer));
                LabelValues.Add("по_фио_сокр", ReportHelper.GetShortName(_transaction.Buyer));
                LabelValues.Add("по_контрагент_и", ReportHelper.GetContractorName(_transaction.Buyer));
                LabelValues.Add("по_контрагент_р", ReportHelper.GetContractorNameGenitive(_transaction.Buyer));
                LabelValues.Add("по_подпись", ReportHelper.GetContractorSignature(_transaction.Buyer));
            }
            else
            {
                LabelValues.Add("по_фио", null);
                LabelValues.Add("по_фио_сокр", null);
                LabelValues.Add("по_контрагент_и", null);
                LabelValues.Add("по_контрагент_р", null);
                LabelValues.Add("по_подпись", null);
            }
        }
        protected void SellerFill()
        {
            if (_transaction.Seller != null)
            {
                LabelValues.Add("п_фио", ReportHelper.GetFullName(_transaction.Seller));
                LabelValues.Add("п_фио_сокр", ReportHelper.GetShortName(_transaction.Seller));
                LabelValues.Add("п_контрагент_и", ReportHelper.GetContractorName(_transaction.Seller));
                LabelValues.Add("п_контрагент_р", ReportHelper.GetContractorNameGenitive(_transaction.Seller));
                LabelValues.Add("п_подпись", ReportHelper.GetContractorSignature(_transaction.Seller));
            }
            else
            {
                LabelValues.Add("п_фио", null);
                LabelValues.Add("п_фио_сокр", null);
                LabelValues.Add("п_контрагент_и", null);
                LabelValues.Add("п_контрагент_р", null);
                LabelValues.Add("п_подпись", null);
            }
        }

        protected void TrancportFill()
        {
            LabelValues.Add("вид_тс", _transaction.Trancport.Type.Name);
            LabelValues.Add("год_тс", _transaction.Trancport.Year.ToString());
            LabelValues.Add("вин", _transaction.Trancport.Vin);
            LabelValues.Add("гос_номер_тс", _transaction.Trancport.Number);
            LabelValues.Add("дата_птс", _transaction.Trancport.DatePts?.ToString("dd.MM.yyyy"));
            LabelValues.Add("дата_cтс", _transaction.Trancport.DateSts?.ToString("dd.MM.yyyy"));
            LabelValues.Add("изготовитель_тс", _transaction.Trancport.Maker);
            LabelValues.Add("категория_тc", _transaction.Trancport.Category.Name);
            LabelValues.Add("кем_птс", _transaction.Trancport.ByPts);
            LabelValues.Add("кем_стс", _transaction.Trancport.BySts);
            LabelValues.Add("кузов", _transaction.Trancport.BodyNumber);
            LabelValues.Add("макс_масса", _transaction.Trancport.MaxMass);
            LabelValues.Add("марка_двиг", _transaction.Trancport.EngineMake);
            LabelValues.Add("марка_тс", _transaction.Trancport.Make.Name);
            LabelValues.Add("модель_тс", _transaction.Trancport.Model.Name);
            LabelValues.Add("масса", _transaction.Trancport.Mass);
            LabelValues.Add("мощность", _transaction.Trancport.Strong);
            LabelValues.Add("номер_птс", _transaction.Trancport.NumberPts);
            LabelValues.Add("номер_стс", _transaction.Trancport.NumberSts);
            LabelValues.Add("объем", _transaction.Trancport.Volume);
            LabelValues.Add("па", _transaction.Trancport.Pa);
            LabelValues.Add("серия_птс", _transaction.Trancport.SerialPts);
            LabelValues.Add("серия_стс", _transaction.Trancport.SerialSts);
            LabelValues.Add("типы_двигателей_тс", _transaction.Trancport.EngineType.Name);
            LabelValues.Add("цвет", _transaction.Trancport.Color);
            LabelValues.Add("шасси", _transaction.Trancport.ChassisNumber);
        }
    }
}
