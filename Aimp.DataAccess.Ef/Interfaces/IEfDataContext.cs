using Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Aimp.DataAccess.Ef.Interfaces
{
    public interface IEfDataContext : IDisposable
    {
        DbSet<Bank> Banks { get; set; }
        DbSet<BankStatus> BankStatuses { get; set; }
        DbSet<BankReportClient> BankReportClients { get; set; }
        DbSet<CreditTransaction> CreditTransactions { get; set; }
        DbSet<CommissionTransaction> CommissionTransactions { get; set; }
        DbSet<City> Cities { get; set; }
        DbSet<ClientReport> ClientReports { get; set; }
        DbSet<ClientStatus> ClientStatuses { get; set; }
        DbSet<Contractor> Contractors { get; set; }
        DbSet<Creditor> Creditors { get; set; }
        DbSet<CreditProgramm> CreditProgramms { get; set; }
        DbSet<CashTransaction> CashTransactions { get; set; }
        DbSet<EngineType> EngineTypes { get; set; }
        DbSet<LegalPerson> LegalPersons { get; set; }
        DbSet<MakeTrancport> MakesTrancport { get; set; }
        DbSet<ModelTrancport> ModelsTrancport { get; set; }
        DbSet<Region> Regions { get; set; }
        DbSet<PrintedDocumentTemplate> PrintedDocumentTemplates { get; set; }
        DbSet<Requisit> Requisits { get; set; }
        DbSet<Trancport> Trancports { get; set; }
        DbSet<TrancportCategory> TrancportCategories { get; set; }
        DbSet<TrancportType> TrancportTypes { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<UserFile> UserFiles { get; set; }
        DbSet<UserRight> UserRights { get; set; }
        DbSet<CardTrancport> CardsTrancport { get; set; }
        DbSet<StatusCardTrancport> StatusesCardTrancport { get; set; }
        DbSet<PreCheckCardTrancport> PreChecksCardTrancport { get; set; }
        DbSet<SourceTrancport> SourcesTrancport { get; set; }
        DbSet<StatusTrancport> StatusesTrancport { get; set; }
        DbEntityEntry Entry(object entity);
        IEnumerable<T> Query<T>(string query) where T : class;
        void Query(string query);
        int SaveChanges();
    }
}
