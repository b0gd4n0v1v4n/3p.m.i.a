using System;

namespace Aimp.Entities
{
    public interface ITransactionProxy : ITransaction
    {
        DateTime? DateProxy { get; set; }
        string NumberProxy { get; set; }
        string NumberRegistry { get; set; }
        int? OwnerId { get; set; }
        IContractor Owner { get; set; }
    }
}
