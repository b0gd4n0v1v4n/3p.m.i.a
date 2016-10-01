using System.Collections.Generic;

namespace Aimp.PrintedDocument
{
    public interface ILabelValue
    {
        Dictionary<string,string> LabelValues { get; } 
    }
}
