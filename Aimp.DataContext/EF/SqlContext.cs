using System.Data.Entity;
using Aimp.DataContext.EF.Repository;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Collections.Generic;
using System.Linq;
using Aimp.Model.Entities;

namespace Aimp.DataContext.EF
{
    public class SqlContext : DbContext, IAimpEFContext
    {
        public SqlContext() : 
            base("aimpConnectionString")
        {
            Configuration.ProxyCreationEnabled = false;
            ////Database.Log = Console.WriteLine;
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public IEnumerable<T> Query<T>(string query) where T : class
        {
            return Database.SqlQuery<T>(query).ToList();
        }

        public void Query(string query)
        {
            Database.ExecuteSqlCommand(query);
        }

        public DbSet<Bank> Banks { get; set; }
        public DbSet<BankStatus> BankStatuses { get; set; }
        public DbSet<BankReportClient> BankReportClients { get; set; }
        public DbSet<CreditTransaction> CreditTransactions { get; set; }
        public DbSet<CommissionTransaction> CommissionTransactions { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<ClientReport> ClientReports { get; set; }
        public DbSet<ClientStatus> ClientStatuses { get; set; }
        public DbSet<Contractor> Contractors { get; set; }
        public DbSet<Creditor> Creditors { get; set; }
        public DbSet<CreditProgramm> CreditProgramms { get; set; }
        public DbSet<CashTransaction> CashTransactions { get; set; }
        public DbSet<EngineType> EngineTypes { get; set; }
        public DbSet<LegalPerson> LegalPersons { get; set; }
        public DbSet<MakeTrancport> MakesTrancport { get; set; }
        public DbSet<ModelTrancport> ModelsTrancport { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<PrintedDocumentTemplate> PrintedDocumentTemplates { get; set; }
        public DbSet<Requisit> Requisits { get; set; }
        public DbSet<Trancport> Trancports { get; set; }
        public DbSet<TrancportCategory> TrancportCategories { get; set; }
        public DbSet<TrancportType> TrancportTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserFile> UserFiles { get; set; }
        public DbSet<UserRight> UserRights { get; set; }

        public DbSet<CardTrancport> CardsTrancport { get; set; }
        public DbSet<StatusCardTrancport> StatusesCardTrancport { get; set; }

        public DbSet<PreCheckCardTrancport> PreChecksCardTrancport { get; set; }

        public DbSet<SourceTrancport> SourcesTrancport { get; set; }

        public DbSet<StatusTrancport> StatusesTrancport { get; set; }
    }
}
