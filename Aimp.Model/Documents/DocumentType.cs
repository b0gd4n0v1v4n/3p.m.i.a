
using System.Runtime.Serialization;

namespace Aimp.Model.Documents
{   [DataContract]
    public enum DocumentType
    {
        [EnumMember]
        CreditTransaction = 1,
        [EnumMember]
        CashTransaction = 0,
        [EnumMember]
        ClientOfReport = 3,
        [EnumMember]
        Commission =4,
        [EnumMember]
        CardTrancport = 5
    }
}
