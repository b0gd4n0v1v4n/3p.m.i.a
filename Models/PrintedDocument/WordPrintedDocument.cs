using System;

namespace Models.PrintedDocument
{
    public class WordPrintedDocument : IPrintedDocument
    {
        public byte[] File { get; set; }

        public string FileName
        {
            get;

            set;
        }
    }
}
