using System.Collections.Generic;

namespace Models.PrintedDocument
{
    public class PrintedDocumentsListDto : IResponse
    {
        public bool Error { get; set; }
        public string Message { get; set; }
        public IEnumerable<EntityName> List { get; set; }
    }
}
