using System.Collections.Generic;

namespace Aimp.Model.PrintedDocument
{
    public interface ILabelValue
    {
        Dictionary<string,string> LabelValues { get; } 
    }
}
