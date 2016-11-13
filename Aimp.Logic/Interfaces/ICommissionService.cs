using System.Collections.Generic;
using Aimp.Model;
using Aimp.Model.Documents;
using Aimp.Model.PrintedDocument;
using Entities;
using System.Linq.Expressions;
using System;

namespace Aimp.Logic.Interfaces
{
    public interface ICommissionService
    {
        void SaveDocument(CommissionDocument document);
        CommissionDocument GetDocument(int id);
        void DeleteDocument(CommissionDocument document);
        IEnumerable<CommissionTransaction> GetCommissions(User user, params Expression<Func<CommissionTransaction, object>>[] includes);
        IEnumerable<PrintedDocumentTemplate> GetPrintedDocumentTemplates();
        WordPrintedDocument GetPrintedDocument(int idCommission, string name);
        IEnumerable<EntityName> GetPrintedList();
        IEnumerable<SourceTrancport> GetSourcesTrancport();
    }
}