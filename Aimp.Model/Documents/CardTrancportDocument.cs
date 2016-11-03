﻿using Entities;
using System.Collections.Generic;
using System;

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

        public int UserId
        {
            get
            {
                return CardTrancport.CommissionTransaction.UserId;
            }
        }
    }
}