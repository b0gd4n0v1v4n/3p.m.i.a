using Models;
using Models.Documents;
using Models.Entities;
using Models.PrintedDocument;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AimpLogic.Commission
{
    public interface ICommissionService : IDisposable
    {
        void SaveDocument(CommissionDocument document);
        CommissionDocument GetDocument(int id);
        void DeleteDocument(CommissionDocument document);
        IQueryable<CommissionTransaction> GetCommissions();
        IQueryable<PrintedDocumentTemplate> GetPrintedDocumentTemplates();
        WordPrintedDocument GetPrintedDocument(int idCommission, string name);
        IEnumerable<EntityName> GetPrintedList();
        IQueryable<SourceTrancport> GetSourcesTrancport();
    }
}
