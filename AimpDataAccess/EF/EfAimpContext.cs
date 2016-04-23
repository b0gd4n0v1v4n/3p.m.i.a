using System;
using AimpDataAccess.Context;
using AimpDataAccess.Repository;
using AimpDataAccess.EF.Repository;
using Models.Entities;
using System.Collections.Generic;

namespace AimpDataAccess.EF
{
    public class EfAimpContext : IAimpContext
    {
        private IAimpEFContext _context;

        public EfAimpContext()
        {
            _context = new SqlContext();
        }

        public IRepository<BankReportClient> BankReportClients
        {
            get
            {
                return new EFRepository<BankReportClient>(_context,_context.BankReportClients);
            }
        }

        public IRepository<Bank> Banks
        {
            get
            {
                return new EFRepository<Bank>(_context, _context.Banks);
            }
        }

        public IRepository<BankStatus> BankStatuses
        {
            get
            {
                return new EFRepository<BankStatus>(_context, _context.BankStatuses);
            }
        }

        public IRepository<CashTransaction> CashTransactions
        {
            get
            {
                return new EFRepository<CashTransaction>(_context, _context.CashTransactions);
            }
        }

        public IRepository<City> Cities
        {
            get
            {
                return new EFRepository<City>(_context, _context.Cities);
            }
        }

        public IRepository<ClientReport> ClientReports
        {
            get
            {
                return new EFRepository<ClientReport>(_context, _context.ClientReports);
            }
        }

        public IRepository<ClientStatus> ClientStatuses
        {
            get
            {
                return new EFRepository<ClientStatus>(_context, _context.ClientStatuses);
            }
        }

        public IRepository<Contractor> Contractors
        {
            get
            {
                return new EFRepository<Contractor>(_context, _context.Contractors);
            }
        }

        public IRepository<CreditProgramm> CreditProgramms
        {
            get
            {
                return new EFRepository<CreditProgramm>(_context, _context.CreditProgramms);
            }
        }

        public IRepository<CreditTransaction> CreditTransactions
        {
            get
            {
                return new EFRepository<CreditTransaction>(_context, _context.CreditTransactions);
            }
        }

        public IRepository<EngineType> EngineTypes
        {
            get
            {
                return new EFRepository<EngineType>(_context, _context.EngineTypes);
            }
        }

        public IRepository<LegalPerson> LegalPersons
        {
            get
            {
                return new EFRepository<LegalPerson>(_context, _context.LegalPersons);
            }
        }

        public IRepository<MakeTrancport> MakesTrancport
        {
            get
            {
                return new EFRepository<MakeTrancport>(_context, _context.MakesTrancport);
            }
        }

        public IRepository<ModelTrancport> ModelsTrancport
        {
            get
            {
                return new EFRepository<ModelTrancport>(_context, _context.ModelsTrancport);
            }
        }

        public IRepository<Region> Regions
        {
            get
            {
                return new EFRepository<Region>(_context, _context.Regions);
            }
        }

        public IRepository<PrintedDocumentTemplate> PrintedDocumentTemplates
        {
            get
            {
                return new EFRepository<PrintedDocumentTemplate>(_context, _context.PrintedDocumentTemplates);
            }
        }

        public IRepository<TrancportCategory> TrancportCategories
        {
            get
            {
                return new EFRepository<TrancportCategory>(_context, _context.TrancportCategories);
            }
        }

        public IRepository<Trancport> Trancports
        {
            get
            {
                return new EFRepository<Trancport>(_context, _context.Trancports);
            }
        }

        public IRepository<TrancportType> TrancportTypes
        {
            get
            {
                return new EFRepository<TrancportType>(_context, _context.TrancportTypes);
            }
        }

        public IRepository<UserFile> UserFiles
        {
            get
            {
                return new EFRepository<UserFile>(_context, _context.UserFiles);
            }
        }

        public IRepository<UserRight> UserRights
        {
            get
            {
                return new EFRepository<UserRight>(_context, _context.UserRights);
            }
        }

        public IRepository<User> Users
        {
            get
            {
                return new EFRepository<User>(_context, _context.Users);
            }
        }

        public IRepository<Creditor> Creditors
        {
            get
            {
                return new EFRepository<Creditor>(_context, _context.Creditors);
            }
        }

        public IRepository<Requisit> Requisits
        {
            get
            {
                return new EFRepository<Requisit>(_context, _context.Requisits);
            }
        }

        public IRepository<CommissionTransaction> CommissionTransactions
        {
            get
            {
                return new EFRepository<CommissionTransaction>(_context, _context.CommissionTransactions);
            }
        }

        public IRepository<CardTrancport> CardsTrancport
        {
            get
            {
                return new EFRepository<CardTrancport>(_context, _context.CardsTrancport);
            }
        }
        public IRepository<StatusCardTrancport> StatusesCardTrancport
        {
            get
            {
                return new EFRepository<StatusCardTrancport>(_context, _context.StatusesCardTrancport);
            }
        }
        public IRepository<PreCheckCardTrancport> PreChecksCardTrancport
        {
            get
            {
                return new EFRepository<PreCheckCardTrancport>(_context, _context.PreChecksCardTrancport);
            }
        }

        public IRepository<SourceTrancport> SourcesTrancport
        {
            get
            {
                return new EFRepository<SourceTrancport>(_context, _context.SourcesTrancport);
            }
        }

        public IRepository<StatusTrancport> StatusesTrancport
        {
            get
            {
                return new EFRepository<StatusTrancport>(_context, _context.StatusesTrancport);
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public IEnumerable<T> Query<T>(string query) where T : class
        {
            return _context.Query<T>(query);
        }

        public void Command(string query)
        {
            _context.Query(query);
        }
    }
}
