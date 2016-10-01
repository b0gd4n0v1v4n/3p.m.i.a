namespace Aimp.PrintedDocument
{
    public interface IPrintedDocumentTemplate : ILabelValue
    {
        PrintedDocumentTemplateType Type { get; }
        byte[] TemplateFile { get; }
        string FileName { get; }
    }
}
