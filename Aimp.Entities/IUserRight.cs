namespace Aimp.Entities
{
    public interface IUserRight : IEntity
    {
        int UserId { get; set; }
        IUser User { get; set; }
        string RightId { get; set; }
    }
}
