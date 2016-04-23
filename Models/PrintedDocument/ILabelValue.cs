using System.Collections.Generic;

namespace Models.PrintedDocument
{
    public interface ILabelValue
    {
        Dictionary<string,string> LabelValues { get; } 
    }
}
