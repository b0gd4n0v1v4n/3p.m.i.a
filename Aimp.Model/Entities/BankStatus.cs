using Aimp.Entities;

namespace Aimp.Model.Entities
{
    public class BankStatus : Entity, IBankStatus
    {
        public string Name { get; set; }
        public string MiddleName { get; set; }
    }
}
