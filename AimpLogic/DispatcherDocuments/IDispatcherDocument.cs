using Models.Documents;

namespace AimpLogic.DispatcherDocuments
{
    public interface IDispatcherDocument
    {
        string NewDocument(int id, DocumentType documentType);

        void UpdateTimeDocument(string guid);
    }
}
