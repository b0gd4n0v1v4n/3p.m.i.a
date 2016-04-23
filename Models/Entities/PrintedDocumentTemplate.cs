namespace Models.Entities
{
    public class PrintedDocumentTemplate : Entity
    {
        public string Name { get; set; }
        public byte[] File { get; set; }
        public string FileName { get; set; }
        public string Type { get; set; }
    }
}
