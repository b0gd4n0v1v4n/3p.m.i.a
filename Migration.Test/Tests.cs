using AimpDataAccess.Context;
using AimpDataAccess.EF;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;

namespace Migration.Test
{
    [TestClass]
    public class Tests
    {
        private string _pathLog = @"D:\AimpFiles\Logs\migration";
        private IAimpContext _context = new EfAimpContext();

        //public Tests()
        //{
        //    if (File.Exists(_pathLog))
        //        File.Delete(_pathLog);

        //    File.Create(_pathLog);
        //}

        [TestMethod]
        public void Banks()
        {
            var newBanks = _context.Banks.All();
            using (ampspbEntities oldDb = new ampspbEntities())
            {
                Assert.AreEqual(newBanks.Count(), oldDb.спр_БАНКИ_ОТЧЁТЫ_КЛИЕНТОВ.Count());

                foreach (var nBank in newBanks)
                {
                    var search = oldDb.спр_БАНКИ_ОТЧЁТЫ_КЛИЕНТОВ
                        .Any(x => x.наименование == nBank.Name);
                    Assert.AreEqual(true, search);
                }
            }
        }

        [TestMethod]
        public void BankStatuses()
        {
            var newBanks = _context.BankStatuses.All();
            using (ampspbEntities oldDb = new ampspbEntities())
            {
                Assert.AreEqual(newBanks.Count(), oldDb.спр_СТАТУСЫ_БАНКА.Count());

                foreach (var nBank in newBanks)
                {
                    var search = oldDb.спр_СТАТУСЫ_БАНКА
                        .Any(x => x.наименование == nBank.Name);
                    Assert.AreEqual(true, search);
                }
            }
        }

        [TestMethod]
        public void BankReportClients()
        {
            var newBanks = _context.BankReportClients.All();
            using (ampspbEntities oldDb = new ampspbEntities())
            {
                Assert.AreEqual(newBanks.Count(), oldDb.БАНКИ_ДЛЯ_ОТЧЁТЫ_КЛИЕНТОВ.Count());
            }
        }

        [TestMethod]
        public void CreditTransactions()
        {
            var credits = _context.CreditTransactions.All();
            using (ampspbEntities oldDb = new ampspbEntities())
            {
                var oldCredits = oldDb.СДЕЛКИ.Where(x => x.тип == 2);
                Assert.AreEqual(credits.Count(), oldCredits.Count());
                foreach (var o in oldCredits)
                {
                    var result = credits.Any(x =>
                    x.Number.ToString() == o.номер &&
                    x.Date == o.дата &&
                    x.DateAgent == o.дата_ад &&
                    x.DateProxy == o.дата_доверенность &&
                    x.NumberProxy == o.номер_доверенность);

                    Assert.AreEqual(true, result);
                }
            }
        }

        [TestMethod]
        public void CashTransactions()
        {
            var credits = _context.CashTransactions.All();
            using (ampspbEntities oldDb = new ampspbEntities())
            {
                var oldCredits = oldDb.СДЕЛКИ.Where(x => x.тип == 1);
                Assert.AreEqual(credits.Count(), oldCredits.Count());
                foreach (var o in oldCredits)
                {
                    var result = credits.Any(x =>
                    x.Number.ToString() == o.номер &&
                    x.Date == o.дата &&
                    x.DateProxy == o.дата_доверенность &&
                    x.NumberProxy == o.номер_доверенность);

                    Assert.AreEqual(true, result);
                }
            }
        }
        [TestMethod]
        public void CommissionTransactions()
        {
            var credits = _context.CommissionTransactions.All();
            using (ampspbEntities oldDb = new ampspbEntities())
            {
                var oldCredits = oldDb.СДЕЛКИ.Where(x => x.тип == 3 || x.тип == 5);
                Assert.AreEqual(credits.Count(), oldCredits.Count());
                foreach (var o in oldCredits)
                {
                    var result = credits.Any(x =>
                    x.Number.ToString() == o.номер &&
                    x.Date == o.дата &&
                    x.DateProxy == o.дата_доверенность &&
                    x.NumberProxy == o.номер_доверенность);

                    Assert.AreEqual(true, result);
                }
            }
        }
        [TestMethod]
        public void Cities()
        {
            var newCities = _context.Cities.All();
            using (ampspbEntities oldDb = new ampspbEntities())
            {
                //Assert.AreEqual(newCities.Count(), oldDb.ГОРОДА.Count());

                foreach (var nItem in newCities)
                {
                    var search = oldDb.ГОРОДА
                        .Any(x => x.наименование == nItem.Name);
                    //if (!search)
                    //    File.AppendAllText(_pathLog, $"[City: new=({nItem.Name}]");
                    Assert.AreEqual(true, search);
                }
            }
        }

        [TestMethod]
        public void ClientReports()
        {
            var news = _context.ClientReports.All();
            using (ampspbEntities oldDb = new ampspbEntities())
            {
                foreach (var nReport in news)
                {
                    var search = oldDb.ОТЧЁТЫ_КЛИЕНТОВ
                        .Any(x => x.дата == nReport.Date &&
                        x.фио == nReport.FullName &&
                        x.комментарий == nReport.Comment);

                    //if (!search)
                    //    File.AppendAllText(_pathLog, $"[ClientReport: ({nReport.Date}]");
                    Assert.AreEqual(true, search);
                }

                Assert.AreEqual(news.Count(), oldDb.ОТЧЁТЫ_КЛИЕНТОВ.Count());
            }
        }
        
        [TestMethod]
        public void ClientStatuses()
        {
            var news = _context.ClientStatuses.All();
            using (ampspbEntities oldDb = new ampspbEntities())
            {
                Assert.AreEqual(news.Count(), oldDb.спр_СТАТУСЫ_КЛИЕНТОВ.Count());

                foreach (var nItem in news)
                {
                    var search = oldDb.спр_СТАТУСЫ_КЛИЕНТОВ
                        .Any(x => x.наименование == nItem.Name);
                    //if (!search)
                    //    File.AppendAllText(_pathLog, $"[ClientStatuses: new=({nItem.Name}]");
                    Assert.AreEqual(true, search);
                }
            }
        }

        [TestMethod]
        public void Contractors()
        {
            var news = _context.Contractors.All();
            using (ampspbEntities oldDb = new ampspbEntities())
            {
                Assert.AreEqual(news.Count(), oldDb.КОНТРАГЕНТЫ.Count());

                foreach (var nItem in news)
                {
                    var search = oldDb.КОНТРАГЕНТЫ
                        .Any(x => x.имя == nItem.FirstName &&
                        x.фамилия == nItem.LastName && 
                        x.отчество == nItem.MiddleName &&
                        x.дата == nItem.DateBirth &&
                        x.номер_док == nItem.NumberDocument &&
                        x.серия_док == nItem.SerialDocument);

                    //if (!search)
                    //    File.AppendAllText(_pathLog, $"[Contractors: new=({nItem.LastName}]");
                    Assert.AreEqual(true, search);
                }
            }
        }
        [TestMethod]
        public void LegalPersons()
        {
            var news = _context.LegalPersons.All();
            using (ampspbEntities oldDb = new ampspbEntities())
            {
                Assert.AreEqual(news.Count(), oldDb.КОНТРАГЕНТЫ.Where(x=>x.юр == 1).Count());

                foreach (var nItem in news)
                {
                    var search = oldDb.КОНТРАГЕНТЫ.Where(x => x.юр == 1)
                        .Any(x => x.наименование == nItem.Name &&
                        x.бик == nItem.Bik &&
                        x.инн == nItem.Inn &&
                        x.кор_счет == nItem.Kor_schet &&
                        x.рас_счет == nItem.Ras_schet &&
                        x.кпп == nItem.Kpp);

                    //if (!search)
                    //    File.AppendAllText(_pathLog, $"[LegalPersons: new=({nItem.Name}]");
                    Assert.AreEqual(true, search);
                }
            }
        }
        [TestMethod]
        public void Creditors()
        {
            var news = _context.Creditors.All();
            using (ampspbEntities oldDb = new ampspbEntities())
            {
                Assert.AreEqual(news.Count(), oldDb.КРЕДИТОРЫ.Count());

                foreach (var nItem in news)
                {
                    var search = oldDb.КРЕДИТОРЫ
                        .Any(x => x.наименование == nItem.Name);
                    //if (!search)
                    //    File.AppendAllText(_pathLog, $"[Creditors: new=({nItem.Name}]");
                    Assert.AreEqual(true, search);
                }
            }
        }

        [TestMethod]
        public void CreditProgramms()
        {
            var news = _context.CreditProgramms.All();
            using (ampspbEntities oldDb = new ampspbEntities())
            {
                Assert.AreEqual(news.Count(), oldDb.спр_ПРОГРАММЫ_КРЕДИТОВАНИЯ.Count());

                foreach (var nItem in news)
                {
                    var search = oldDb.спр_ПРОГРАММЫ_КРЕДИТОВАНИЯ
                        .Any(x => x.наименование == nItem.Name);
                    //if (!search)
                    //    File.AppendAllText(_pathLog, $"[CreditProgramms: new=({nItem.Name}]");
                    Assert.AreEqual(true, search);
                }
            }
        }
        [TestMethod]
        public void Trancports()
        {
            var news = _context.Trancports.All();
            using (ampspbEntities oldDb = new ampspbEntities())
            {
                var count = oldDb.СДЕЛКИ.Select(x => x.ТРАНСПОРТ1).GroupBy(x=>x.код).Count();
                Assert.AreEqual(news.Count(), count);

                foreach (var nItem in news)
                {
                    var search = oldDb.ТРАНСПОРТ
                        .Any(x => x.вин == nItem.Vin &&
                        x.год.ToString() == nItem.Year.ToString() &&
                        x.дата_птс == nItem.DatePts);
                    //if (!search)
                    //    File.AppendAllText(_pathLog, $"[Trancports: new=({nItem.Vin}]");
                    Assert.AreEqual(true, search);
                }
            }
        }
        [TestMethod]
        public void TrancportCategories()
        {
            var news = _context.TrancportCategories.All();
            using (ampspbEntities oldDb = new ampspbEntities())
            {
                Assert.AreEqual(news.Count(), oldDb.КАТЕГОРИИ_ТРАНСПОРТА.Count());

                foreach (var nItem in news)
                {
                    var search = oldDb.КАТЕГОРИИ_ТРАНСПОРТА
                        .Any(x => x.наименование == nItem.Name);
                    //if (!search)
                    //    File.AppendAllText(_pathLog, $"[TrancportCategories: new=({nItem.Name}]");
                    Assert.AreEqual(true, search);
                }
            }
        }
        [TestMethod]
        public void TrancportTypes()
        {
            var news = _context.TrancportTypes.All();
            using (ampspbEntities oldDb = new ampspbEntities())
            {
                var count = oldDb.СДЕЛКИ
                    .Select(x=>x.ТРАНСПОРТ1)
                    .Select(x => x.виды_транспорта)
                    .GroupBy(x => x)
                    .Count();

                Assert.AreEqual(news.Count(), count);

                foreach (var nItem in news)
                {
                    var search = oldDb.ВИДЫ_ТРАНСПОРТА
                        .Any(x => x.наименование == nItem.Name);
                    //if (!search)
                    //    File.AppendAllText(_pathLog, $"[TrancportTypes: new=({nItem.Name}]");
                    Assert.AreEqual(true, search);
                }
            }
        }
        [TestMethod]
        public void EngineTypes()
        {
            var news = _context.EngineTypes.All();
            using (ampspbEntities oldDb = new ampspbEntities())
            {
                var count = oldDb.ТРАНСПОРТ
                    .Select(x => x.типы_двигателей)
                    .GroupBy(x=>x)
                    .Count();

                Assert.AreEqual(news.Count(), count);

                foreach (var nItem in news)
                {
                    var search = oldDb.ТИПЫ_ДВИГАТЕЛЕЙ
                        .Any(x => x.наименование == nItem.Name);
                    //if (!search)
                    //    File.AppendAllText(_pathLog, $"[EngineTypes: new=({nItem.Name}]");
                    Assert.AreEqual(true, search);
                }
            }
        }

        [TestMethod]
        public void MakesTrancport()
        {
            var news = _context.MakesTrancport.All();
            using (ampspbEntities oldDb = new ampspbEntities())
            {
                Assert.AreEqual(news.Count(), oldDb.МАРКИ.Count());

                foreach (var nItem in news)
                {
                    var search = oldDb.МАРКИ
                        .Any(x => x.наименование == nItem.Name);
                    //if (!search)
                    //    File.AppendAllText(_pathLog, $"[MakesTrancport: new=({nItem.Name}]");
                    Assert.AreEqual(true, search);
                }
            }
        }

        [TestMethod]
        public void ModelsTrancport()
        {
            var news = _context.ModelsTrancport.All();
            using (ampspbEntities oldDb = new ampspbEntities())
            {
                var count = oldDb.СДЕЛКИ
                    .Select(x => x.ТРАНСПОРТ1.модели)
                    
                    .GroupBy(x => x)
                    
                    .Count();
                Assert.AreEqual(news.Count(), count);

                foreach (var nItem in news)
                {
                    var search = oldDb.МОДЕЛИ
                        .Any(x => x.наименование == nItem.Name);
                    //if (!search)
                    //    File.AppendAllText(_pathLog, $"[ModelsTrancport: new=({nItem.Name}]");
                    Assert.AreEqual(true, search);
                }
            }
        }

        [TestMethod]
        public void Regions()
        {
            var news = _context.Regions.All();
            using (ampspbEntities oldDb = new ampspbEntities())
            {
                var count = oldDb.СДЕЛКИ
                    .Select(x => x.КОНТРАГЕНТЫ.регионы)
                    .Union(oldDb.СДЕЛКИ.Select(x => x.КОНТРАГЕНТЫ1.регионы))
                    .Union(oldDb.СДЕЛКИ.Select(x => x.КОНТРАГЕНТЫ2.регионы))
                    .GroupBy(x=>x)
                    .Count();
                
                Assert.AreEqual(news.Count(), count);

                foreach (var nItem in news)
                {
                    var search = oldDb.РЕГИОНЫ
                        .Any(x => x.наименование == nItem.Name);
                    //if (!search)
                    //    File.AppendAllText(_pathLog, $"[Regions: new=({nItem.Name}]");
                    Assert.AreEqual(true, search);
                }
            }
        }

        [TestMethod]
        public void Requisits()
        {
            var news = _context.Requisits.All();
            using (ampspbEntities oldDb = new ampspbEntities())
            {
                Assert.AreEqual(news.Count(), oldDb.РЕКВИЗИТЫ.Count());

                foreach (var nItem in news)
                {
                    var search = oldDb.РЕКВИЗИТЫ
                        .Any(x => x.наименование == nItem.Name);
                    //if (!search)
                    //    File.AppendAllText(_pathLog, $"[Requisits: new=({nItem.Name}]");
                    Assert.AreEqual(true, search);
                }
            }
        }

        [TestMethod]
        public void Users()
        {
            var news = _context.Users.All();
            using (ampspbEntities oldDb = new ampspbEntities())
            {
                Assert.AreEqual(news.Count(), oldDb.ПОЛЬЗОВАТЕЛИ.Count());

                foreach (var nItem in news)
                {
                    var search = oldDb.ПОЛЬЗОВАТЕЛИ
                        .Any(x => x.имя == nItem.FirstName &&
                            x.отчёство == nItem.MiddleName &&
                            x.фамилия == nItem.LastName);
                    //if (!search)
                    //    File.AppendAllText(_pathLog, $"[Users: new=({nItem.LastName}]");
                    Assert.AreEqual(true, search);
                }
            }
        }

        [TestMethod]
        public void UserRights()
        {
            foreach(var iUser in _context.Users.All().ToList())
            {
                var rights = _context.UserRights.All().Count(x => x.UserId == iUser.Id);
                Console.WriteLine(rights);
                Assert.IsTrue(rights < 5);
            }
        }
    }
}