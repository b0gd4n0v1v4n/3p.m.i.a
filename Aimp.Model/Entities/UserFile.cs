using Aimp.Entities;

namespace Aimp.Model.Entities
{
    public class UserFile : Entity, IUserFile
    {
        public string Name { get; set; }
        public byte[] File  { get; set; }
    }
}
