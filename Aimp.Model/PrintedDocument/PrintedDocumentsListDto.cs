using System.Collections.Generic;

namespace Aimp.Model.PrintedDocument
{
    public class PrintedDocumentsListDto : IResponse
    {
        public bool Error { get; set; }
        public string Message { get; set; }
        public IEnumerable<EntityName> List { get; set; }
    }
}
