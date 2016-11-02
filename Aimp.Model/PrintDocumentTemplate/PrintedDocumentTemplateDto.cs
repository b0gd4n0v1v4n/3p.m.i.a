using Aimp.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aimp.Model.PrintDocumentTemplate
{
    public class PrintedDocumentTemplateDto : IResponse
    {
        public bool Error { get; set; }
        public string Message { get; set; }
        public PrintedDocumentTemplate Template { get; set; }
    }
}
