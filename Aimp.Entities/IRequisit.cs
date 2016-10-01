namespace Aimp.Entities
{
     public interface IRequisit : IEntity
    {
         string Name { get; set; }
         string Ros_schet { get; set; }
         string Kor_schet { get; set; }
         string InBank { get; set; }
         string Bik { get; set; }
    }
}
