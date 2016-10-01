namespace Aimp.Entities
{
    public interface IUserFile : IEntity
    {
        string Name { get; set; }
        byte[] File  { get; set; }
    }
}
