using AimpDataAccess.EF;
using Models.Entities;
using Models.SecurityRigths;
using System;
using System.Collections.Generic;
using System.Linq;
using Models.PrintedDocument.Templates;

namespace MigrationDb2Vto3V
{
    class Program
    {
        static bool nullBye(Nullable<byte> byt)
        {
            return byt == null ? false : (byt == 1 ? true : false);
        }
        static Contractor NewContractor(КОНТРАГЕНТЫ ct, SqlContext newDb)
        {
            Contractor contractor = new Contractor();

            contractor.FirstName = ct.имя;
            contractor.LastName = ct.фамилия;
            contractor.MiddleName = ct.отчество;
            contractor.FirstNameGenitive = ct.имя_р;
            contractor.LastNameGenitive = ct.фамилия_р;
            contractor.MiddleNameGenitive = ct.отчество_р;
            contractor.DateBirth = ct.дата ?? DateTime.Now;

            contractor.Region =
                newDb.Regions.Local.FirstOrDefault(x => x.Name == ct.РЕГИОНЫ1.наименование) ??
                new Region() { Name = ct.РЕГИОНЫ1.наименование };

            contractor.City =
                newDb.Cities.Local.FirstOrDefault(
                    x => x.Name == ct.ГОРОДА1.наименование && x.Region.Name == contractor.Region.Name) ??
                new City()
                {
                    Name = ct.ГОРОДА1.наименование,
                    Region = contractor.Region
                };

            contractor.Raion = ct.район;
            contractor.Street = ct.улица;
            contractor.House = ct.дом;
            contractor.Housing = ct.корпус;
            contractor.Apartment = ct.квартира;
            contractor.Telefon = ct.телефон;
            contractor.NumberDocument = ct.номер_док;
            contractor.SerialDocument = ct.серия_док;
            contractor.DateDocument = ct.дата_док ?? DateTime.Now;
            contractor.ByDocument = ct.кем_док;
            if (!string.IsNullOrWhiteSpace(ct.документ_наим))
            {
                contractor.Document = new UserFile()
                {
                    Name = ct.документ_наим,
                    File = ct.документ
                };
            }
            if (!string.IsNullOrWhiteSpace(ct.фото_наим))
            {
                contractor.Photo = new UserFile()
                {
                    Name = ct.фото_наим,
                    File = ct.фото
                };
            }
            if (!string.IsNullOrWhiteSpace(ct.наименование))
            {
                contractor.LegalPerson = new LegalPerson()
                {
                    Name = ct.наименование,
                    Bank = ct.банк,
                    Bik = ct.бик,
                    Inn = ct.инн,
                    Kor_schet = ct.кор_счет,
                    Kpp = ct.кпп,
                    Ogrn = ct.огрн,
                    Ras_schet = ct.рас_счет
                };
            }
            return
                newDb.Contractors.Local.FirstOrDefault(
                    x =>
                        x.FirstName == contractor.FirstName && x.LastName == contractor.LastName &&
                        x.MiddleName == contractor.MiddleName && x.DateBirth == contractor.DateBirth && x.Region.Name == contractor.Region.Name && x.Telefon == contractor.Telefon && x.House == contractor.House && x.Street == contractor.Street && x.NumberDocument == contractor.NumberDocument) 
                        ?? 
                        contractor;
        }
        static Trancport NewTrancport(ТРАНСПОРТ trancport, SqlContext newDb)
        {
            var tc = new Trancport();

            tc.EngineType =
                newDb.EngineTypes.Local.FirstOrDefault(
                    y => y.Name == trancport.ТИПЫ_ДВИГАТЕЛЕЙ1.наименование) ?? new EngineType()
                    {
                        Name = trancport.ТИПЫ_ДВИГАТЕЛЕЙ1.наименование
                    };

            tc.Make =
                newDb.MakesTrancport.Local.FirstOrDefault(
                    x => x.Name == trancport.МАРКИ1.наименование) ??
                new MakeTrancport()
                {
                    Name = trancport.МАРКИ1.наименование
                };

            tc.Model =
                newDb.ModelsTrancport.Local.FirstOrDefault(
                    x => x.Name == trancport.МОДЕЛИ1.наименование && x.Make.Name == tc.Make.Name) ??
                new ModelTrancport() {Name = trancport.МОДЕЛИ1.наименование, Make = tc.Make};

            tc.Category =
                newDb.TrancportCategories.Local.FirstOrDefault(
                    x => x.Name == trancport.КАТЕГОРИИ_ТРАНСПОРТА1.наименование) ??
                new TrancportCategory() {Name = trancport.КАТЕГОРИИ_ТРАНСПОРТА1.наименование};

            tc.Type =
                newDb.TrancportTypes.Local.FirstOrDefault(
                    x => x.Name == trancport.ВИДЫ_ТРАНСПОРТА1.наименование) ??
                new TrancportType() {Name = trancport.ВИДЫ_ТРАНСПОРТА1.наименование};

            tc.BodyNumber = trancport.кузов;
            tc.Maker = trancport.изготовитель;
            tc.Mass = trancport.масса;
            tc.MaxMass = trancport.макс_масса;
            tc.Number = trancport.гос_номер;
            tc.Strong = trancport.мощность;
            tc.ByPts = trancport.кем_птс;
            tc.BySts = trancport.кем_стс;
            tc.ChassisNumber = trancport.шасси;
            tc.Color = trancport.цвет;
            tc.DatePts = trancport.дата_птс;
            tc.DateSts = trancport.дата_стс;
            tc.NumberPts = trancport.номер_птс;
            tc.NumberSts = trancport.номер_стс;
            tc.Pa = trancport.па;
            tc.Volume = trancport.объем;
            tc.Vin = trancport.вин;
            tc.Year = (short) Convert.ToInt32(trancport.год);
            tc.SerialPts = trancport.серия_птс;
            tc.SerialSts = trancport.серия_стс;
            tc.EngineMake = trancport.марка_двиг;
            if (!string.IsNullOrWhiteSpace(trancport.копия_птс_наим))
            {
                tc.CopyPts = new UserFile()
                {
                    Name = trancport.копия_птс_наим,
                    File = trancport.копия_птс
                };
            }

            if (!string.IsNullOrWhiteSpace(trancport.копия_стс_наим))
            {
                tc.CopySts = new UserFile()
                {
                    Name = trancport.копия_стс_наим,
                    File = trancport.копия_стс
                };
            }

            return newDb.Trancports.Local.FirstOrDefault(x => x.Number == tc.Number) ?? tc;
            
        }

        static User NewUser(ПОЛЬЗОВАТЕЛИ пользовател, SqlContext newDb)
        {
            DateTime date = Convert.ToDateTime(пользовател.дата);

            var userDb = newDb.Users.Local.FirstOrDefault(
                    x =>
                        x.FirstName == пользовател.имя && x.LastName == пользовател.фамилия &&
                        x.MiddleName == пользовател.отчёство && x.Date == date &&
                        x.Number == пользовател.номер && x.Login == пользовател.логин &&
                        x.Password == пользовател.пароль)
                        ;
            var user = userDb
                 ??
                new User()
                {
                    FirstName = пользовател.имя,
                    LastName = пользовател.фамилия,
                    MiddleName = пользовател.отчёство,
                    FirstNameGenitive = пользовател.имя_р,
                    LastNameGenitive = пользовател.фамилия_р,
                    MiddleNameGenitive = пользовател.отчество_р,
                    Number = пользовател.номер,
                    Date = date,
                    Login = пользовател.логин,
                    Password = пользовател.пароль
                };

            if(userDb == null)
            {
                if (пользовател.фамилия.ToLower() == "дмитриева")
                {
                    UserRight userAdmin = new UserRight()
                    {
                        RightId = UserRightsCollection.Admin.Id,
                        User = user
                    };
                    newDb.UserRights.Add(userAdmin);
                }
                else
                {
                    UserRight userRight = new UserRight()
                    {
                        RightId = UserRightsCollection.Add.Id,
                        User = user
                    };
                    newDb.UserRights.Add(userRight);
                    UserRight userRightView = new UserRight()
                    {
                        RightId = UserRightsCollection.View.Id,
                        User = user
                    };
                    newDb.UserRights.Add(userRightView);
                }
            }

            return user;
        } 
        static void Main(string[] args)
        {
#warning ПРИ ИЗМЕНЕНИИ БД НЕ ЗАБУДЬ СОЗДАТЬ ТРИГЕРЫ
            using (var aimp = new AIMPSPBEntities1())
            {
                using (var newDb = new SqlContext())
                {
                    newDb.StatusesCardTrancport.Add(new StatusCardTrancport() { Name = "В наличии" });
                    newDb.StatusesCardTrancport.Add(new StatusCardTrancport() { Name = "Продана" });
                    newDb.StatusesCardTrancport.Add(new StatusCardTrancport() { Name = "Зобрали с комиссии" });

                    foreach (ШАБЛОНЫ шаблоны in aimp.ШАБЛОНЫ)
                    {
                        string type = string.Empty;
                        switch (шаблоны.типы_шаблонов)
                        {
                            case 1:
                                type = PrintedDocumentTemplateType.Сделка.ToString();
                                break;
                            case 2:
                                type = PrintedDocumentTemplateType.Кредит.ToString();
                                break;
                            case 3:
                                type = PrintedDocumentTemplateType.Дкп.ToString();
                                break;
                            case 4:
                                type = PrintedDocumentTemplateType.Акт.ToString();
                                break;
                            case 5:
                                type = PrintedDocumentTemplateType.Комиссия.ToString();
                                break;
                        }
                        PrintedDocumentTemplate reportTemplate = new PrintedDocumentTemplate()
                        {
                            Name = шаблоны.наименование,
                            File = шаблоны.файл,
                            FileName = шаблоны.файл_наим,
                            Type = type
                        };
                        newDb.PrintedDocumentTemplates.Add(reportTemplate);
                        Console.WriteLine(type);
                    }
                    
                    int prevClientReport = 0;
                    ClientReport clientReport = null;

                    foreach (var bankReport in aimp.БАНКИ_ДЛЯ_ОТЧЁТЫ_КЛИЕНТОВ.OrderBy(X=>X.отчёты_клиентов))
                    {
                        if(bankReport.отчёты_клиентов != prevClientReport)
                        {
                            prevClientReport = bankReport.отчёты_клиентов.Value;

                            var отчётыКлиентов = aimp.ОТЧЁТЫ_КЛИЕНТОВ
                                .First(x => x.код == bankReport.отчёты_клиентов);

                            спр_СТАТУСЫ_КЛИЕНТОВ спрСтатусыКлиентов =
                                aimp.спр_СТАТУСЫ_КЛИЕНТОВ.First(
                                    x => x.код == отчётыКлиентов.спр_статусы_клиентов);

                            спр_ПРОГРАММЫ_КРЕДИТОВАНИЯ программыКредитования =
                                aimp.спр_ПРОГРАММЫ_КРЕДИТОВАНИЯ.First(
                                    x => x.код == отчётыКлиентов.спр_программы_кредитования);

                            clientReport = new ClientReport()
                            {
                                Date = отчётыКлиентов.дата ?? DateTime.Now,
                                Source = отчётыКлиентов.источник,
                                FullName = отчётыКлиентов.фио,
                                Price = Convert.ToDecimal(отчётыКлиентов.стоимость),
                                TotalContribution =
                                    Convert.ToDecimal(отчётыКлиентов.общий_взнос),
                                ClientStatus =
                                    newDb.ClientStatuses.Local.FirstOrDefault(
                                        x => x.Name == спрСтатусыКлиентов.наименование) ??
                                    new ClientStatus()
                                    {
                                        Name = спрСтатусыКлиентов.наименование,
                                        UsedFilter = nullBye(спрСтатусыКлиентов.фильтр)
                                    },
                                Telefon = отчётыКлиентов.телефон,
                                CreditProgramm =
                                    newDb.CreditProgramms.Local.FirstOrDefault(
                                        x => x.Name == программыКредитования.наименование) ??
                                    new CreditProgramm()
                                    {
                                        Name = программыКредитования.наименование
                                    },
                                CreditSum = Convert.ToDecimal(отчётыКлиентов.сумма_кредита),
                                CommissionKnow = nullBye(отчётыКлиентов.комиссии_знает),
                                CommissionRemoval =
                                    Convert.ToDecimal(отчётыКлиентов.комиссия_за_снятие),
                                CommissionCredit = nullBye(отчётыКлиентов.комиссии_в_кредите),
                                ActAssessment = Convert.ToDecimal(отчётыКлиентов.акт_оценки),
                                DKP_DK = отчётыКлиентов.дкп_дк,
                                Comment = отчётыКлиентов.комментарий,
                                CommissionSalon = отчётыКлиентов.комиссия_салона,
                                User = NewUser(отчётыКлиентов.ПОЛЬЗОВАТЕЛИ1, newDb),
                                Trancport = отчётыКлиентов.тс
                            };
                            newDb.ClientReports.Add(clientReport);
                        }
                        var bank = newDb.Banks.Local
                                    .FirstOrDefault(x => x.Name == bankReport.спр_БАНКИ_ОТЧЁТЫ_КЛИЕНТОВ1.наименование)
                                    ?? new Bank() { Name = bankReport.спр_БАНКИ_ОТЧЁТЫ_КЛИЕНТОВ1.наименование };
                        var bankStatus = newDb.BankStatuses.Local
                                        .FirstOrDefault(x => x.Name == bankReport.спр_СТАТУСЫ_БАНКА1.наименование)
                                        ?? new BankStatus() { Name = bankReport.спр_СТАТУСЫ_БАНКА1.наименование };

                        BankReportClient bankReportClient = new BankReportClient()
                        {
                            ClientReport = clientReport,
                            Bank = bank,
                            BankStatus = bankStatus,
                            Used = nullBye(bankReport.используется)
                        };

                        newDb.BankReportClients.Add(bankReportClient);
                    }

                    foreach (var statClient in aimp.спр_СТАТУСЫ_КЛИЕНТОВ)
                    {
                        if (!newDb.ClientStatuses.Local.Any(x => x.Name == statClient.наименование))
                            newDb.ClientStatuses.Add(new ClientStatus() { Name = statClient.наименование });
                    }

                    foreach (var bankStat in aimp.спр_СТАТУСЫ_БАНКА)
                    {
                        if(!newDb.BankStatuses.Local.Any(x=>x.Name == bankStat.наименование))
                            newDb.BankStatuses.Add(new BankStatus() { Name = bankStat.наименование, MiddleName = bankStat.наименование2 });
                    }
                    foreach (var progCredit in aimp.спр_ПРОГРАММЫ_КРЕДИТОВАНИЯ)
                    {
                        if (!newDb.CreditProgramms.Local.Any(x => x.Name == progCredit.наименование))
                            newDb.CreditProgramms.Add(new CreditProgramm() { Name = progCredit.наименование });
                    }

                    foreach(var oldClientReport in aimp.ОТЧЁТЫ_КЛИЕНТОВ)
                    {
                        var searchClientReport = newDb.ClientReports.Local.
                            FirstOrDefault(x => x.Date == oldClientReport.дата
                            && x.FullName == oldClientReport.фио
                            && x.Comment == oldClientReport.комментарий
                            && x.Source == oldClientReport.источник);

                        if(searchClientReport == null)
                        {
                            спр_СТАТУСЫ_КЛИЕНТОВ спрСтатусыКлиентов =
                                aimp.спр_СТАТУСЫ_КЛИЕНТОВ.First(
                                    x => x.код == oldClientReport.спр_статусы_клиентов);

                            спр_ПРОГРАММЫ_КРЕДИТОВАНИЯ программыКредитования =
                                aimp.спр_ПРОГРАММЫ_КРЕДИТОВАНИЯ.First(
                                    x => x.код == oldClientReport.спр_программы_кредитования);

                            var cr = new ClientReport()
                            {
                                Date = oldClientReport.дата ?? DateTime.Now,
                                Source = oldClientReport.источник,
                                FullName = oldClientReport.фио,
                                Price = Convert.ToDecimal(oldClientReport.стоимость),
                                TotalContribution =
                                    Convert.ToDecimal(oldClientReport.общий_взнос),
                                ClientStatus =
                                    newDb.ClientStatuses.Local.FirstOrDefault(
                                        x => x.Name == спрСтатусыКлиентов.наименование) ??
                                    new ClientStatus()
                                    {
                                        Name = спрСтатусыКлиентов.наименование,
                                        UsedFilter = nullBye(спрСтатусыКлиентов.фильтр)
                                    },
                                Telefon = oldClientReport.телефон,
                                CreditProgramm =
                                    newDb.CreditProgramms.Local.FirstOrDefault(
                                        x => x.Name == программыКредитования.наименование) ??
                                    new CreditProgramm()
                                    {
                                        Name = программыКредитования.наименование
                                    },
                                CreditSum = Convert.ToDecimal(oldClientReport.сумма_кредита),
                                CommissionKnow = nullBye(oldClientReport.комиссии_знает),
                                CommissionRemoval =
                                    Convert.ToDecimal(oldClientReport.комиссия_за_снятие),
                                CommissionCredit = nullBye(oldClientReport.комиссии_в_кредите),
                                ActAssessment = Convert.ToDecimal(oldClientReport.акт_оценки),
                                DKP_DK = oldClientReport.дкп_дк,
                                Comment = oldClientReport.комментарий,
                                CommissionSalon = oldClientReport.комиссия_салона,
                                User = NewUser(oldClientReport.ПОЛЬЗОВАТЕЛИ1, newDb),
                                Trancport = oldClientReport.тс
                            };
                            newDb.ClientReports.Add(cr);
                        }
                    }

                    foreach (var сделка in aimp.СДЕЛКИ)
                    {
                        Console.WriteLine(сделка.код);

                        CashTransaction cash = new CashTransaction();

                        cash.Date = сделка.дата ?? DateTime.Now;
                        cash.Number = Convert.ToInt32(сделка.номер);

                        if (сделка.КОНТРАГЕНТЫ1 != null)
                            cash.Buyer = NewContractor(сделка.КОНТРАГЕНТЫ1, newDb);

                        if (сделка.КОНТРАГЕНТЫ2 != null)
                            cash.Owner = NewContractor(сделка.КОНТРАГЕНТЫ2, newDb);

                        cash.Trancport = NewTrancport(сделка.ТРАНСПОРТ1, newDb);
                        cash.Price = Convert.ToDecimal(сделка.стоимость);
                        cash.User = NewUser(сделка.ПОЛЬЗОВАТЕЛИ1, newDb);

                        cash.DateProxy = сделка.дата_доверенность;
                        cash.NumberProxy = сделка.номер_доверенность;
                        cash.NumberRegistry = сделка.номер_реестр;

                        if (сделка.КОНТРАГЕНТЫ != null)
                            cash.Seller = NewContractor(сделка.КОНТРАГЕНТЫ, newDb);

                        switch (сделка.тип)
                        {
                            case 1:
                                newDb.CashTransactions.Add(cash);
                                break;
                            case 2:
                                CreditTransaction credit = new CreditTransaction()
                                {
                                    Date = cash.Date,
                                    Number = cash.Number,
                                    Seller = cash.Seller,
                                    Buyer = cash.Buyer,
                                    Owner = cash.Owner,
                                    Trancport = cash.Trancport,
                                    Price = cash.Price,
                                    User = cash.User,
                                    DateProxy = cash.DateProxy,
                                    NumberProxy = cash.NumberProxy,
                                    NumberRegistry = cash.NumberRegistry
                                };
                                credit.AgentDocument = new UserFile()
                                {
                                    Name = сделка.агенский_наим,
                                    File = сделка.агенский
                                };
                                credit.DkpDocument = new UserFile()
                                {
                                    Name = сделка.дкп_наим,
                                    File = сделка.дкп
                                };
                                credit.DateAgent = сделка.дата_ад ?? DateTime.Now;
                                credit.DateDkp = сделка.дата ?? DateTime.Now;
                                credit.PriceBank = Convert.ToDecimal(сделка.стоимость_банк);
                                credit.DownPayment = Convert.ToDecimal(сделка.первый_взнос);
                                credit.CreditSumm = Convert.ToDecimal(сделка.сумма_кредит);
                                credit.RealPrice = Convert.ToDecimal(сделка.стоимость_реальная);
                                credit.DownPaymentCashbox = Convert.ToDecimal(сделка.первый_взнос_касса);
                                string creditor = aimp.КРЕДИТОРЫ.FirstOrDefault(x => x.код == сделка.кредиторы)?.наименование;
                                if (!string.IsNullOrWhiteSpace(creditor))
                                {
                                    credit.Creditor = newDb.Creditors.Local.FirstOrDefault(x => x.Name == creditor) ??
                                                      new Creditor()
                                                      {
                                                          Name = creditor
                                                      };
                                }
                                РЕКВИЗИТЫ реквизит = aimp.РЕКВИЗИТЫ.First(x => x.код == сделка.реквизиты);

                                credit.Requisit =
                                    newDb.Requisits.Local.FirstOrDefault(
                                        x => x.Name == реквизит.наименование && x.Bik == реквизит.бик) ??
                                    new Requisit()
                                    {
                                        Name = реквизит.наименование,
                                        Bik = реквизит.бик,
                                        InBank = реквизит.в_банке,
                                        Kor_schet = реквизит.кор_счет,
                                        Ros_schet = реквизит.рос_счет
                                    };

                                credit.ReportInsurance = Convert.ToDecimal(сделка.отчёт_по_страховым);
                                credit.Rollback = Convert.ToDecimal(сделка.откат);
                                credit.Source = сделка.источник;
                                credit.IsCredit = сделка.кредит == 1 ? true : false;
                                credit.CommissionCashbox = Convert.ToDecimal(сделка.комиссия_Касса);

                                newDb.CreditTransactions.Add(credit);
                                break;
                            case 3:
                            case 5:
                                CommissionTransaction commission = new CommissionTransaction()
                                {
                                    Date = cash.Date,
                                    Number = cash.Number,
                                    Seller = cash.Seller,
                                    Buyer = cash.Buyer,
                                    Owner = cash.Owner,
                                    Trancport = cash.Trancport,
                                    Price = cash.Price,
                                    User = cash.User,
                                    DateProxy = cash.DateProxy,
                                    NumberProxy = cash.NumberProxy,
                                    NumberRegistry = cash.NumberRegistry
                                };

                                commission.Commission = Convert.ToDecimal(сделка.комиссия);
                                commission.Parking = Convert.ToDecimal(сделка.стоянка);
                                commission.IsTwoMounth = сделка.второй_месяц == null
                                    ? false
                                    : сделка.второй_месяц == 1 ? true : false;

                                newDb.CommissionTransactions.Add(commission);
                                break;
                        }
                    }
                    
                    newDb.SaveChanges();
                }
            }
        }
    }
}
