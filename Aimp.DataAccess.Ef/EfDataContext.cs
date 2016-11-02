using Aimp.DataAccess.Ef.Interfaces;
using Aimp.DataAccess.EF;
using Aimp.DataAccess.EF.Repository;
using Aimp.DataAccess.Interfaces;
using Entities;
using System.Collections.Generic;

namespace Aimp.DataAccess.Ef
{
    public class EfDataContext : IDataContext
    {
        private IEfDataContext _context;

        public EfDataContext()
        {
            _context = new SqlContext();
        }

        public IRepository<BankReportClient> BankReportClients
        {
            get
            {
                return new EfRepository<BankReportClient>(_context, _context.BankReportClients);
            }
        }

        public IRepository<Bank> Banks
        {
            get
            {
                return new EfRepository<Bank>(_context, _context.Banks);
            }
        }

        public IRepository<BankStatus> BankStatuses
        {
            get
            {
                return new EfRepository<BankStatus>(_context, _context.BankStatuses);
            }
        }

        public IRepository<CashTransaction> CashTransactions
        {
            get
            {
                return new EfRepository<CashTransaction>(_context, _context.CashTransactions);
            }
        }

        public IRepository<City> Cities
        {
            get
            {
                return new EfRepository<City>(_context, _context.Cities);
            }
        }

        public IRepository<ClientReport> ClientReports
        {
            get
            {
                return new EfRepository<ClientReport>(_context, _context.ClientReports);
            }
        }

        public IRepository<ClientStatus> ClientStatuses
        {
            get
            {
                return new EfRepository<ClientStatus>(_context, _context.ClientStatuses);
            }
        }

        public IRepository<Contractor> Contractors
        {
            get
            {
                return new EfRepository<Contractor>(_context, _context.Contractors);
            }
        }

        public IRepository<CreditProgramm> CreditProgramms
        {
            get
            {
                return new EfRepository<CreditProgramm>(_context, _context.CreditProgramms);
            }
        }

        public IRepository<CreditTransaction> CreditTransactions
        {
            get
            {
                return new EfRepository<CreditTransaction>(_context, _context.CreditTransactions);
            }
        }

        public IRepository<EngineType> EngineTypes
        {
            get
            {
                return new EfRepository<EngineType>(_context, _context.EngineTypes);
            }
        }

        public IRepository<LegalPerson> LegalPersons
        {
            get
            {
                return new EfRepository<LegalPerson>(_context, _context.LegalPersons);
            }
        }

        public IRepository<MakeTrancport> MakesTrancport
        {
            get
            {
                return new EfRepository<MakeTrancport>(_context, _context.MakesTrancport);
            }
        }

        public IRepository<ModelTrancport> ModelsTrancport
        {
            get
            {
                return new EfRepository<ModelTrancport>(_context, _context.ModelsTrancport);
            }
        }

        public IRepository<Region> Regions
        {
            get
            {
                return new EfRepository<Region>(_context, _context.Regions);
            }
        }

        public IRepository<PrintedDocumentTemplate> PrintedDocumentTemplates
        {
            get
            {
                return new EfRepository<PrintedDocumentTemplate>(_context, _context.PrintedDocumentTemplates);
            }
        }

        public IRepository<TrancportCategory> TrancportCategories
        {
            get
            {
                return new EfRepository<TrancportCategory>(_context, _context.TrancportCategories);
            }
        }

        public IRepository<Trancport> Trancports
        {
            get
            {
                return new EfRepository<Trancport>(_context, _context.Trancports);
            }
        }

        public IRepository<TrancportType> TrancportTypes
        {
            get
            {
                return new EfRepository<TrancportType>(_context, _context.TrancportTypes);
            }
        }

        public IRepository<UserFile> UserFiles
        {
            get
            {
                return new EfRepository<UserFile>(_context, _context.UserFiles);
            }
        }

        public IRepository<UserRight> UserRights
        {
            get
            {
                return new EfRepository<UserRight>(_context, _context.UserRights);
            }
        }

        public IRepository<User> Users
        {
            get
            {
                return new EfRepository<User>(_context, _context.Users);
            }
        }

        public IRepository<Creditor> Creditors
        {
            get
            {
                return new EfRepository<Creditor>(_context, _context.Creditors);
            }
        }

        public IRepository<Requisit> Requisits
        {
            get
            {
                return new EfRepository<Requisit>(_context, _context.Requisits);
            }
        }

        public IRepository<CommissionTransaction> CommissionTransactions
        {
            get
            {
                return new EfRepository<CommissionTransaction>(_context, _context.CommissionTransactions);
            }
        }

        public IRepository<CardTrancport> CardsTrancport
        {
            get
            {
                return new EfRepository<CardTrancport>(_context, _context.CardsTrancport);
            }
        }
        public IRepository<StatusCardTrancport> StatusesCardTrancport
        {
            get
            {
                return new EfRepository<StatusCardTrancport>(_context, _context.StatusesCardTrancport);
            }
        }
        public IRepository<PreCheckCardTrancport> PreChecksCardTrancport
        {
            get
            {
                return new EfRepository<PreCheckCardTrancport>(_context, _context.PreChecksCardTrancport);
            }
        }

        public IRepository<SourceTrancport> SourcesTrancport
        {
            get
            {
                return new EfRepository<SourceTrancport>(_context, _context.SourcesTrancport);
            }
        }

        public IRepository<StatusTrancport> StatusesTrancport
        {
            get
            {
                return new EfRepository<StatusTrancport>(_context, _context.StatusesTrancport);
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
