using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Data.SqlClient;
using Aimp.DataAccess.Ef;
using Aimp.DataAccess.Interfaces;
using Aimp.Reports.Templates;
using Entities;
using System.Collections.Generic;

namespace Migration.Test
{
    [TestClass]
    public class ReportTemplates
    {
        private class RowReader : IDisposable
        {
            SqlConnection _connection;
            SqlDataReader reader;
            public RowReader(string query)
            {

                var connectionString = @"data source=STRH-SRVPK\SQLEXPRESS;initial catalog=ampspb;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
                _connection = new SqlConnection(connectionString);
                _connection.Open();
                reader = new SqlCommand(query, _connection).ExecuteReader();
                reader.Read();
            }
            public int Length
            {
                get { return reader.FieldCount; }
            }
            public string this[string column]
            {
                get
                {
                    return reader[column].ToString();
                }
            }
            public IEnumerable<string> GetColumns()
            {
                return Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToList();
            }
            public void Dispose()
            {
                _connection.Close();
            }
        }
        
        private IDataContext _context = new EfDataContext();


        [TestMethod]
        public void CommisionTemplateTest()
        {
            using (ampspbEntities oldDb = new ampspbEntities())
            {
                List<byte> types = new List<byte> { 3, 5 };
                var oldCommission = oldDb.СДЕЛКИ
                                   .First(x => types.Contains(x.тип.Value) && x.дата_доверенность.HasValue
                                          && (x.КОНТРАГЕНТЫ.юр == 1 || x.КОНТРАГЕНТЫ1.юр == 1 || x.КОНТРАГЕНТЫ2.юр == 1));

                var template = oldDb.ТИПЫ_ШАБЛОНОВ.Where(x => x.наименование == "Комиссия").First();
                var query = template.запрос + oldCommission.код;

                int oldNumber = Convert.ToInt32(oldCommission.номер);

                var newCommissionTransaction = _context.CommissionTransactions.All(x => x.Seller.City,
                    x => x.Seller.Region,
                    x => x.Seller.LegalPerson,
                    x => x.Buyer.City,
                    x => x.Buyer.Region,
                    x => x.Buyer.LegalPerson,
                    x => x.Owner.City,
                    x => x.Owner.Region,
                    x => x.Owner.LegalPerson,
                    x => x.Trancport.Model,
                    x => x.Trancport.Make,
                    x => x.Trancport.Category,
                    x => x.Trancport.EngineType,
                    x => x.Trancport.Type,
                    x => x.User)
                    .First(x => x.Number == oldNumber && x.Date.Year == oldCommission.дата.Value.Year);
                var newTempalte = new CommissionTransactionPrintedDocumentTemplate(newCommissionTransaction, null, null);

                using (var row = new RowReader(query))
                {
                    var excepts = row.GetColumns().Except(newTempalte.LabelValues.Select(x => x.Key));
                    newTempalte.LabelValues.Add("дата_да", null);
                    newTempalte.LabelValues.Add("месяц_ад", null);
                    Assert.AreEqual(row.Length, newTempalte.LabelValues.Count);

                    foreach (var iLabel in newTempalte.LabelValues)
                    {
                        string oldValue = string.Empty;

                        if (iLabel.Key == "дата_ад" || iLabel.Key == "дата_ад")
                            oldValue = null;
                        else
                            oldValue = row[iLabel.Key];

                        Assert.IsTrue(oldValue == iLabel.Value || oldValue + ",00" == iLabel.Value
                                          || (string.IsNullOrEmpty(oldValue) && string.IsNullOrEmpty(iLabel.Value))
                                          || oldValue.Length == iLabel.Value.Length);
                    }
                }
            }
        }

        [TestMethod]
        public void CreditTemplateTest()
        {
            using (ampspbEntities oldDb = new ampspbEntities())
            {
                var oldCredit = oldDb.СДЕЛКИ
                                   .First(x => x.тип == 2 && x.дата_доверенность.HasValue
                                          //&& (x.КОНТРАГЕНТЫ.юр == 1 || x.КОНТРАГЕНТЫ1.юр == 1 || x.КОНТРАГЕНТЫ2.юр == 1)
                                          );

                var template = oldDb.ТИПЫ_ШАБЛОНОВ.Where(x => x.наименование == "Сделка в кредит").First();
                var query = template.запрос + oldCredit.код;

                int oldNumber = Convert.ToInt32(oldCredit.номер);

                var newCreditTransaction = _context.CreditTransactions
                                        .All(x => x.Seller.City,
                                        x => x.Seller.Region,
                                        x => x.Buyer.City,
                                        x => x.Buyer.Region,
                                        x => x.Owner.City,
                                        x => x.Owner.Region,
                                        x => x.Trancport.Model,
                                        x => x.Trancport.Make,
                                        x => x.Trancport.Category,
                                        x => x.Trancport.EngineType,
                                        x => x.Trancport.Type,
                                        x => x.Requisit,
                                        x => x.Creditor,
                                        x => x.User)
                    .First(x => x.Number == oldNumber && x.Date.Year == oldCredit.дата.Value.Year);
                var newTempalte = new CreditTransactionPrintedDocumentTemplate(newCreditTransaction, null, null);

                using (var row = new RowReader(query))
                {
                    var excepts = row.GetColumns().Except(newTempalte.LabelValues.Select(x => x.Key));
                    newTempalte.LabelValues.Add("дата_да", newTempalte.LabelValues["дата_ад"]);
                    Assert.AreEqual(row.Length+1, newTempalte.LabelValues.Count);

                    foreach (var iLabel in newTempalte.LabelValues)
                    {
                        string oldValue = string.Empty;

                        if (iLabel.Key == "дата_ад")
                            oldValue = row["дата_да"];
                        else
                            oldValue = row[iLabel.Key];

                        Assert.IsTrue(oldValue == iLabel.Value || oldValue + ",00" == iLabel.Value
                                          || (string.IsNullOrEmpty(oldValue) && string.IsNullOrEmpty(iLabel.Value)) 
                                          || oldValue.Length == iLabel.Value.Length);
                    }
                }
            }
        }
        [TestMethod]
        public void CashTemplateTest()
        {
            using (ampspbEntities oldDb = new ampspbEntities())
            {

                var oldCash = oldDb.СДЕЛКИ
                                   .First(x => x.тип == 1 && x.дата_доверенность.HasValue 
                                          && (x.КОНТРАГЕНТЫ.юр == 1 || x.КОНТРАГЕНТЫ1.юр == 1 || x.КОНТРАГЕНТЫ2.юр == 1));

                var template = oldDb.ТИПЫ_ШАБЛОНОВ.Where(x => x.наименование == "Сделка").First();
                var query = template.запрос + oldCash.код;

                int oldNumber = Convert.ToInt32(oldCash.номер);

                var newTransaction = _context.CashTransactions.All(x => x.Seller.City,
                    x => x.Seller.Region,
                    x => x.Seller.LegalPerson,
                    x => x.Buyer.City,
                    x => x.Buyer.Region,
                    x => x.Buyer.LegalPerson,
                    x => x.Owner.City,
                    x => x.Owner.Region,
                    x => x.Owner.LegalPerson,
                    x => x.Trancport.Model,
                    x => x.Trancport.Make,
                    x => x.Trancport.Category,
                    x => x.Trancport.EngineType,
                    x => x.Trancport.Type,
                    x => x.User)
                    .First(x => x.Number == oldNumber && x.Date.Year == oldCash.дата.Value.Year);
                var newTempalte = new CashTransactionPrintedDocumentTemplate(newTransaction, null, null);

                using (var row = new RowReader(query))
                {
                    Assert.AreEqual(row.Length, newTempalte.LabelValues.Count);

                    foreach (var iLabel in newTempalte.LabelValues)
                    {
                        var oldValue = row[iLabel.Key];
                        
                        Assert.IsTrue(oldValue == iLabel.Value || oldValue + ",00" == iLabel.Value
                                          || (string.IsNullOrEmpty(oldValue) && string.IsNullOrEmpty(iLabel.Value)) 
                                          || oldValue.Length == iLabel.Value.Length);
                    }
                }
            }
        }
    }
}