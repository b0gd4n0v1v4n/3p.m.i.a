using Aimp.DataContext.Repository;
using Aimp.Entities;
using System;
using System.Collections.Generic;

namespace Aimp.DataContext
{
    public interface IAimpContext : IDisposable
    {
        IRepository<IBank> Banks { get; }
        IRepository<IBankStatus> BankStatuses { get; }
        IRepository<IBankReportClient> BankReportClients { get;  }
        IRepository<ICreditTransaction> CreditTransactions { get;  }
        IRepository<ICommissionTransaction> CommissionTransactions { get;  }
        IRepository<ICity> Cities { get;  }
        IRepository<IClientReport> ClientReports { get;  }
        IRepository<IClientStatus> ClientStatuses { get;  }
        IRepository<IContractor> Contractors { get;  }
        IRepository<ICreditor> Creditors { get;  }
        IRepository<ICreditProgramm> CreditProgramms { get;  }
        IRepository<ICashTransaction> CashTransactions { get;  }
        IRepository<IEngineType> EngineTypes { get;  }
        IRepository<ILegalPerson> LegalPersons { get;  }
        IRepository<IMakeTrancport> MakesTrancport { get;  }
        IRepository<IModelTrancport> ModelsTrancport { get;  }
        IRepository<IRegion> Regions { get;  }
        IRepository<IPrintedDocumentTemplate> PrintedDocumentTemplates { get;  }
        IRepository<IRequisit> Requisits { get;  }
        IRepository<ITrancport> Trancports { get;  }
        IRepository<ITrancportCategory> TrancportCategories { get;  }
        IRepository<ITrancportType> TrancportTypes { get;  }
        IRepository<IUser> Users { get;  }
        IRepository<IUserFile> UserFiles { get;  }
        IRepository<ICardTrancport> CardsTrancport { get;  }
        IRepository<IStatusCardTrancport> StatusesCardTrancport { get; }
        IRepository<IPreCheckCardTrancport> PreChecksCardTrancport { get; }
        IRepository<ISourceTrancport> SourcesTrancport { get; }
        IRepository<IStatusTrancport> StatusesTrancport { get; }
        IRepository<IUserRight> UserRights { get; }
        IEnumerable<T> Query<T>(string query) 
            where T : class;
        void Command(string query);
        int SaveChanges();
    }
}
