namespace Aimp.Entities
{
    public interface ICity : IEntity
    {
        string Name { get; set; }
        int RegionId { get; set; }
        IRegion Region { get; set; }
    }
}
