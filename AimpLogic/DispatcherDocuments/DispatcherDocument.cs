using Models.Documents;
using System;
using System.Collections.Generic;

namespace AimpLogic.DispatcherDocuments
{
    public class DispatcherDocument : IDispatcherDocument
    {
        private class Document
        {
            public Guid Guid { get; }

            public int Id { get; }
            
            public DocumentType Type { get;}
            
            public DateTime DateTime { get; set; }

            public Document(Guid guid, int id, DocumentType type)
            {
                Guid = guid;

                Id = id;

                Type = type;

                DateTime = DateTime.Now;
            }
        }
        
        private List<Document> _usingDocuments;

        public DispatcherDocument()
        {
            _usingDocuments = new List<Document>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="documentType"></param>
        /// <returns>GUID</returns>
        public string NewDocument(int id, DocumentType documentType)
        {
            var guid = Guid.NewGuid();

            //Document document = new Document(guid, id, documentType);

            return guid.ToString();
        }

        public void UpdateTimeDocument(string guid)
        {
            throw new NotImplementedException();
        }
    }
}
