using System.Collections.Generic;
using AIMP_v3._0.Enums;

namespace TestAccordDb2AndDb3Version
{
    public interface IReport
    {
        Dictionary<string, string> Get(TypeReport type, int idTransaction);
    }
}
