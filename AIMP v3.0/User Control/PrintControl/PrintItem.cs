using Models.Documents;
using Models.Entities;
using Models.PrintedDocument.Templates;

namespace AIMP_v3._0.User_Control
{
    public class PrintItem
    {
        public IDocument Document { get; set; }
        public string Name { get; set; }
        public DocumentType Type { get; set; }
    }
}
