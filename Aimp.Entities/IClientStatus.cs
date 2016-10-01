namespace Aimp.Entities
{
    public interface IClientStatus : IEntity
    {
        string Name { get; set; }
        bool UsedFilter { get; set; }
    }
}
