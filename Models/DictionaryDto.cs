using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class DictionaryDto : IResponse
    {
        public bool Error { get; set; }
        public string Message { get; set; }
        public IEnumerable<EntityName> Items { get; set; }
    }
}
