using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aimp.Model.PrintDocumentTemplate
{
    public class PrintedDocumentTemplatesListDto : IResponse
    {
        public bool Error { get; set; }
        public string Message { get; set; }
        public IEnumerable<PrinDocTempListItem> Items { get; set; }
    }
}
