using Aimp.Model;
using Aimp.Model.Documents;
using Aimp.Model.PrintedDocument;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aimp.Logic.Interfaces
{
    public interface ICommissionService 
    {
        void SaveDocument(CommissionDocument document);
        CommissionDocument GetDocument(int id);
        void DeleteDocument(CommissionDocument document);
        IQueryable<CommissionTransaction> GetCommissions(User user);
        IQueryable<PrintedDocumentTemplate> GetPrintedDocumentTemplates();
        WordPrintedDocument GetPrintedDocument(int idCommission, string name);
        IEnumerable<EntityName> GetPrintedList();
        IQueryable<SourceTrancport> GetSourcesTrancport();
    }
}
