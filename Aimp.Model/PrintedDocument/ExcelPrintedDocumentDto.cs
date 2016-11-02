using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aimp.Model.PrintedDocument
{
    public class ExcelPrintedDocumentDto : IResponse
    {
        public bool Error { get; set; }
        public string Message { get; set; }
        public ExcelPrintedDocument Document { get; set; }
    }
}
