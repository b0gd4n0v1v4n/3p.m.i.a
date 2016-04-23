namespace Models.PrintedDocument.Templates
{
    public interface IPrintedDocumentTemplate : ILabelValue
    {
        PrintedDocumentTemplateType Type { get; }
        byte[] TemplateFile { get; }
        string FileName { get; }
    }
}
