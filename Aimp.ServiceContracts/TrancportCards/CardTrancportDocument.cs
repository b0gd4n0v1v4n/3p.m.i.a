

using Aimp.Entities;
using System.Collections.Generic;

namespace Aimp.ServiceContracts.TrancportCards
{
    public class CardTrancportDocument
    {
        public ICardTrancport CardTrancport { get; set; }
        public IEnumerable<IPreCheckCardTrancport> PreChecks { get; set; }
    }
}
