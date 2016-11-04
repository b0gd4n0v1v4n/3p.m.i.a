using Aimp.Model.Documents;

namespace AIMP_v3._0.PrintControl
{
    public class PrintItem
    {
        public IDocument Document { get; set; }
        public string Name { get; set; }
        public DocumentType Type { get; set; }
    }
}
