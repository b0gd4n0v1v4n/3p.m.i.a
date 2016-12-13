using System;
using System.Collections.Generic;
using Entities;
using Aimp.Model;
using System.Text;

namespace Aimp.Reports.Helpers
{
    public static class ReportHelper
    {
        private static string _ShortName(string firstName, string lastName, string middleName)
        {
            string shortFirst = firstName?.Length > 0 ? $"{firstName?[0]}." : string.Empty;

            string shortMiddle = middleName?.Length > 0 ? $"{middleName?[0]}." : string.Empty;

            return $"{lastName} {shortFirst}{shortMiddle}";
        }

        private static string __comma(string val)
        {
            if (!string.IsNullOrEmpty(val))
                return $", {val}";
            else
                return String.Empty;
        }
        private static string __dash(string val)
        {
            if (!string.IsNullOrEmpty(val))
                return $"-{val}";
            else
                return String.Empty;
        }

        public static List<PrintedDocumentTemplate> ReportTemplates { get; set; }

        public static string GetContractorSignature(Contractor contractor)
        {
            string city = string.Empty;

            if (contractor.City.Name != contractor.Region.Name)
            {
                city = contractor.City.Name;
            }

            if (contractor.LegalPerson != null)
            {
                return $"{contractor.LegalPerson.Name}^p" +
                       $"{contractor.Region.Name}{__comma(contractor.Raion)}{__comma(city)}, " +
                       $"{contractor.Street}{__comma(contractor.House) + __dash(contractor.Housing) + __dash(contractor.Apartment)}^p" +
                       $"ИНН/КПП {contractor.LegalPerson.Inn}/{contractor.LegalPerson.Kpp}^pОГРН {contractor.LegalPerson.Ogrn}^p" +
                       $"р/с {contractor.LegalPerson.Ras_schet}^p" +
                       $"к/с {contractor.LegalPerson.Kor_schet}в {contractor.LegalPerson.Bank}^p" +
                       $"БИК {contractor.LegalPerson.Bik}";
            }
            else
            {
                string result = $"{GetFullName(contractor)}^p" +
                       $"Дата рождения: {contractor.DateBirth.ToString(DataFormats.DateFormat)}^p" +
                       $"Паспорт: {contractor.SerialDocument} № {contractor.NumberDocument}^p" +
                       $"Выдан: {contractor.ByDocument}^p" +
                       $"Дата выдачи: {contractor.DateDocument.Value.ToString(DataFormats.DateFormat)}^p" +
                       $"Зарегистрирован: {contractor.Region?.Name}";

                result += !string.IsNullOrEmpty(contractor.Raion) ? $", {contractor.Raion}" : String.Empty;

                result += contractor.City.Name == contractor.Region.Name
                    ? String.Empty
                    : $", {contractor.City.Name}";

                result +=
                    $", {contractor.Street}{__comma(contractor.House)}{__dash(contractor.Housing)}{__dash(contractor.Apartment)}";

                return result;
            }
        }
        public static string GetContractorName(Contractor contractor)
        {
            if (contractor.LegalPerson != null)
            {
                return $"{contractor.LegalPerson.Name} в лице генерального директора {contractor.LastNameGenitive} {contractor.FirstNameGenitive} {contractor.MiddleNameGenitive}";
            }
            else
            {
                return GetFullName(contractor);
            }
        }
        public static string GetContractorNameGenitive(Contractor contractor)
        {
            if (contractor.LegalPerson != null)
            {
                return $"{contractor.LegalPerson.Name} в лице генерального директора {contractor.LastNameGenitive} {contractor.FirstNameGenitive} {contractor.MiddleNameGenitive}";
            }
            else
            {
                return GetFullNameGenitive(contractor);
            }
        }
        public static string GetFullName(Contractor contractor)
        {
            return $"{contractor.LastName} {contractor.FirstName} {contractor.MiddleName}";
        }

        public static string GetFullName(User user)
        {
            return $"{user.FirstName} {user.LastName} {user.MiddleName}";
        }
        public static string GetFullNameGenitive(Contractor contractor)
        {
            return $"{contractor.LastNameGenitive} {contractor.FirstNameGenitive} {contractor.MiddleNameGenitive}";
        }

        public static string GetFullNameGenitive(User user)
        {
            return $"{user.FirstNameGenitive} {user.LastNameGenitive} {user.MiddleNameGenitive}";
        }
        public static string GetShortName(Contractor contractor)
        {
            return _ShortName(contractor.FirstName, contractor.LastName, contractor.MiddleName);
        }
        public static string GetShortName(User user)
        {
            return _ShortName(user.FirstName, user.LastName, user.MiddleName);
        }

        public static string GetOwnerData(Contractor owner)
        {
            if(owner.LegalPerson == null)
            {
                StringBuilder result = new StringBuilder();

                result.Append(GetFullNameGenitive(owner));
                result.Append($" (дата рождения: {owner.DateBirth.ToString(DataFormats.DateFormat)}");
                result.Append($" паспорт {owner.SerialDocument} № {owner.NumberDocument}, выдан: {owner.ByDocument}, {owner.DateDocument.Value.ToString(DataFormats.DateFormat)}");
                result.Append($", адрес регистрации: {owner.Region.Name}");

                if (!string.IsNullOrEmpty(owner.Raion))
                    result.Append($", {owner.Raion}");

                if (owner.City.Name != owner.Region.Name)
                    result.Append($", {owner.City.Name}");

                result.Append($", {owner.Street}");

                if (!string.IsNullOrEmpty(owner.House))
                    result.Append($", {owner.House}");

                if (!string.IsNullOrEmpty(owner.Housing))
                    result.Append($"-{owner.Housing}");

                if (!string.IsNullOrEmpty(owner.Apartment))
                    result.Append($"-{owner.Apartment}");

                result.Append(")");

                return result.ToString();
            }
            else
            {
                return $"{owner.LegalPerson.Name} в лице {GetFullNameGenitive(owner)}";
            }
        }
    }
}
