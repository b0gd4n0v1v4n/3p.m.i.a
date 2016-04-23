using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models.PrintDocumentTemplate
{
    public class PrintedDocumentTemplateDto : IResponse
    {
        public bool Error { get; set; }
        public string Message { get; set; }
        public PrintedDocumentTemplate Template { get; set; }
    }
}
