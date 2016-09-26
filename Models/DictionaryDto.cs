using Models.Dictionar;
using System.Collections.Generic;

namespace Models
{
    public class DictionaryDto : IResponse
    {
        public bool Error { get; set; }
        public string Message { get; set; }
        public IEnumerable<Row> Rows { get; set; }
    }
}
