using Aimp.Entities;

namespace Aimp.Model.Entities
{
    public class ClientStatus : Entity, IClientStatus
    {
        public string Name { get; set; }
        public bool UsedFilter { get; set; }
    }
}
