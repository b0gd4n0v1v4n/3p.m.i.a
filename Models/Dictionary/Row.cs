using System.Collections.Generic;

namespace Models.Dictionar
{
    public class Row
    {
        public string Name { get; set; }
        public IDictionary<string,string>  Cells { get; set; }
        public Row(string name, IDictionary<string, string> cells = null)
        {
            Name = name;
            Cells = cells == null ? new Dictionary<string,string>() : cells;
        }
    }
}
