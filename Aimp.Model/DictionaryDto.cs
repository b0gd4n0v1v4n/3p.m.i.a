using Aimp.Model.Dictionar;
using System.Collections.Generic;

namespace Aimp.Model
{
    public class DictionaryDto : IResponse
    {
        public bool Error { get; set; }
        public string Message { get; set; }
        public IEnumerable<Row> Rows { get; set; }
    }
}
