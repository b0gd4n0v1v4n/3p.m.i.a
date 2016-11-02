using Entities;
using System;
using System.Collections.Generic;

namespace Aimp.DataAccess.Interfaces
{
    public interface IDataContext : IDisposable
    {
        IRepository<Bank> Banks { get; }
        IRepository<BankStatus> BankStatuses { get; }
        IRepository<BankReportClient> BankReportClients { get; }
        IRepository<CreditTransaction> CreditTransactions { get; }
        IRepository<CommissionTransaction> CommissionTransactions { get; }
        IRepository<City> Cities { get; }
        IRepository<ClientReport> ClientReports { get; }
        IRepository<ClientStatus> ClientStatuses { get; }
        IRepository<Contractor> Contractors { get; }
        IRepository<Creditor> Creditors { get; }
        IRepository<CreditProgramm> CreditProgramms { get; }
        IRepository<CashTransaction> CashTransactions { get; }
        IRepository<EngineType> EngineTypes { get; }
        IRepository<LegalPerson> LegalPersons { get; }
        IRepository<MakeTrancport> MakesTrancport { get; }
        IRepository<ModelTrancport> ModelsTrancport { get; }
        IRepository<Region> Regions { get; }
        IRepository<PrintedDocumentTemplate> PrintedDocumentTemplates { get; }
        IRepository<Requisit> Requisits { get; }
        IRepository<Trancport> Trancports { get; }
        IRepository<TrancportCategory> TrancportCategories { get; }
        IRepository<TrancportType> TrancportTypes { get; }
        IRepository<User> Users { get; }
        IRepository<UserFile> UserFiles { get; }
        IRepository<CardTrancport> CardsTrancport { get; }
        IRepository<StatusCardTrancport> StatusesCardTrancport { get; }
        IRepository<PreCheckCardTrancport> PreChecksCardTrancport { get; }
        IRepository<SourceTrancport> SourcesTrancport { get; }
        IRepository<StatusTrancport> StatusesTrancport { get; }
        IRepository<UserRight> UserRights { get; }
        IEnumerable<T> Query<T>(string query)
            where T : class;
        void Command(string query);
        int SaveChanges();
    }
}
