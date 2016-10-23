using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models.PrintedDocument
{
    public class ExcelPrintedDocument : IPrintedDocument
    {
        public byte[] File
        {
            get;

            set;
        }

        public string FileName
        {
            get;

            set;
        }
    }
}
