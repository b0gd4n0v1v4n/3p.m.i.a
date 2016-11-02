﻿using Aimp.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aimp.Model.Documents
{
    public class CardTrancportDocument
: IDocument
    {
        public CardTrancport CardTrancport { get; set; }
        public IEnumerable<PreCheckCardTrancport> PreChecks { get; set; }
        public DocumentType DocumentType
        {
            get
            {
                return DocumentType.CardTrancport;
            }
        }

        public int Id { get; set; }

        public string Identity { get; set; }

        public bool IsNew { get { return Id != 0;  } }
    }
}
