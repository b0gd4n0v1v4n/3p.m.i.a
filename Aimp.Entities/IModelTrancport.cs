
namespace Aimp.Entities
{
    public interface IModelTrancport : IEntity
    {
         string Name { get; set; }
         int MakeId { get; set; }
         IMakeTrancport Make { get; set; }
    }
}