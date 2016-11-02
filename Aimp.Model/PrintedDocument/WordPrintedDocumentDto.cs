namespace Aimp.Model.PrintedDocument
{
    public class WordPrintedDocumentDto : IResponse
    {
        public bool Error { get; set; }
        public string Message { get; set; }
        public WordPrintedDocument Document { get; set; }
    }
}
