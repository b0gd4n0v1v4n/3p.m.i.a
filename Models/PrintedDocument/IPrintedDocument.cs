namespace Models.PrintedDocument
{
    public interface IPrintedDocument
    {
        string FileName { get; }
        byte[] File { get; set; }
    }
}
