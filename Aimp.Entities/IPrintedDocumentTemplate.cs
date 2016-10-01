namespace Aimp.Entities
{
     public interface IPrintedDocumentTemplate : IEntity
    {
         string Name { get; set; }
         byte[] File { get; set; }
         string FileName { get; set; }
         string Type { get; set; }
    }
}
