using System.Collections.Generic;

namespace Aimp.Model.Dictionar
{
    public class Row
    {
        public string Name { get; set; }
        public List<KeyValue<string,string>>  Cells { get; set; }
        public Row(string name, List<KeyValue<string, string>> cells = null)
        {
            Name = name;
            Cells = cells == null ? new List<KeyValue<string,string>>() : cells;
        }
        public Row()
        {

        }
    }
}
