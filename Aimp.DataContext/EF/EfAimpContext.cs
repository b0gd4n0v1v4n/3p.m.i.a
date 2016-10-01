using Aimp.DataContext.EF.Repository;
using Aimp.DataContext.Repository;
using Aimp.Entities;
using System.Collections.Generic;

namespace Aimp.DataContext.EF
{
    public class EfAimpContext : IAimpContext
    {
        private IAimpEFContext _context;

        public EfAimpContext()
        {
            _context = new SqlContext();
        }

        public IRepository<IBankReportClient> BankReportClients
        {
            get
            {
                return null;
                    //new EFRepository<IBankReportClient>(_context,_context.BankReportClients as DbSet<IEntity>);
            }
        }

        public IRepository<IBank> Banks
        {
            get
            {
                return null;
                    //new EFRepository<IBank>(_context, _context.Banks);
            }
        }

        public IRepository<IBankStatus> BankStatuses
        {
            get
            {
                return null;
                    //new EFRepository<IBankStatus>(_context, _context.BankStatuses);
            }
        }

        public IRepository<ICashTransaction> CashTransactions
        {
            get
            {
                return null;
                    //new EFRepository<ICashTransaction>(_context, _context.CashTransactions);
            }
        }

        public IRepository<ICity> Cities
        {
            get
            {
                return null;
                    //new EFRepository<ICity>(_context, _context.Cities);
            }
        }

        public IRepository<IClientReport> ClientReports
        {
            get
            {
                return null;
                //new EFRepository<IClientReport>(_context, _context.ClientReports);
            }
        }

        public IRepository<IClientStatus> ClientStatuses
        {
            get
            {
                return null;
                //new EFRepository<IClientStatus>(_context, _context.ClientStatuses);
            }
        }

        public IRepository<IContractor> Contractors
        {
            get
            {
                return null;
                //new EFRepository<IContractor>(_context, _context.Contractors);
            }
        }

        public IRepository<ICreditProgramm> CreditProgramms
        {
            get
            {
                return null;
                //new EFRepository<ICreditProgramm>(_context, _context.CreditProgramms);
            }
        }

        public IRepository<ICreditTransaction> CreditTransactions
        {
            get
            {
                return null;
                //new EFRepository<ICreditTransaction>(_context, _context.CreditTransactions);
            }
        }

        public IRepository<IEngineType> EngineTypes
        {
            get
            {
                return null;
                //new EFRepository<IEngineType>(_context, _context.EngineTypes);
            }
        }

        public IRepository<ILegalPerson> LegalPersons
        {
            get
            {
                return null;
                //new EFRepository<ILegalPerson>(_context, _context.LegalPersons);
            }
        }

        public IRepository<IMakeTrancport> MakesTrancport
        {
            get
            {
                return null;
                //new EFRepository<IMakeTrancport>(_context, _context.MakesTrancport);
            }
        }

        public IRepository<IModelTrancport> ModelsTrancport
        {
            get
            {
                return null;
                //new EFRepository<IModelTrancport>(_context, _context.ModelsTrancport);
            }
        }

        public IRepository<IRegion> Regions
        {
            get
            {
                return null;
                //new EFRepository<IRegion>(_context, _context.Regions);
            }
        }

        public IRepository<IPrintedDocumentTemplate> PrintedDocumentTemplates
        {
            get
            {
                return null;
                //new EFRepository<IPrintedDocumentTemplate>(_context, _context.PrintedDocumentTemplates);
            }
        }

        public IRepository<ITrancportCategory> TrancportCategories
        {
            get
            {
                return null;
                //new EFRepository<ITrancportCategory>(_context, _context.TrancportCategories);
            }
        }

        public IRepository<ITrancport> Trancports
        {
            get
            {
                return null;
                //new EFRepository<ITrancport>(_context, _context.Trancports);
            }
        }

        public IRepository<ITrancportType> TrancportTypes
        {
            get
            {
                return null;
                //new EFRepository<ITrancportType>(_context, _context.TrancportTypes);
            }
        }

        public IRepository<IUserFile> UserFiles
        {
            get
            {
                return null;
                //new EFRepository<IUserFile>(_context, _context.UserFiles);
            }
        }

        public IRepository<IUserRight> UserRights
        {
            get
            {
                return new EFRepository<IUserRight>(_context, _context.UserRights);
            }
        }

        public IRepository<IUser> Users
        {
            get
            {
                return null;
                //new EFRepository<IUser>(_context, _context.Users);
            }
        }

        public IRepository<ICreditor> Creditors
        {
            get
            {
                return null;
                //new EFRepository<ICreditor>(_context, _context.Creditors);
            }
        }

        public IRepository<IRequisit> Requisits
        {
            get
            {
                return null;
                //new EFRepository<IRequisit>(_context, _context.Requisits);
            }
        }

        public IRepository<ICommissionTransaction> CommissionTransactions
        {
            get
            {
                return null;// new EFRepository<ICommissionTransaction>(_context, _context.CommissionTransactions);
            }
        }

        public IRepository<ICardTrancport> CardsTrancport
        {
            get
            {
                return null;//new EFRepository<ICardTrancport>(_context, _context.CardsTrancport);
            }
        }
        public IRepository<IStatusCardTrancport> StatusesCardTrancport
        {
            get
            {
                return null;//new EFRepository<IStatusCardTrancport>(_context, _context.StatusesCardTrancport);
            }
        }
        public IRepository<IPreCheckCardTrancport> PreChecksCardTrancport
        {
            get
            {
                return null;//new EFRepository<IPreCheckCardTrancport>(_context, _context.PreChecksCardTrancport);
            }
        }

        public IRepository<ISourceTrancport> SourcesTrancport
        {
            get
            {
                return null;//new EFRepository<ISourceTrancport>(_context, _context.SourcesTrancport);
            }
        }

        public IRepository<IStatusTrancport> StatusesTrancport
        {
            get
            {
                return null;//new EFRepository<IStatusTrancport>(_context, _context.StatusesTrancport);
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
