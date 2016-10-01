using Aimp.Entities;

namespace Aimp.Model.Entities
{
    public class PrintedDocumentTemplate : Entity, IPrintedDocumentTemplate
    {
        public string Name { get; set; }
        public byte[] File { get; set; }
        public string FileName { get; set; }
        public string Type { get; set; }
    }
}
