using Aimp.Entities;

namespace Aimp.Model.Entities
{
    public class Requisit : Entity, IRequisit
    {
        public string Name { get; set; }
        public string Ros_schet { get; set; }
        public string Kor_schet { get; set; }
        public string InBank { get; set; }
        public string Bik { get; set; }
    }
}
