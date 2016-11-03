﻿namespace Aimp.Model.Documents
{
    public interface IDocument
    {
        int Id { get; set; }
        string Identity { get; set; }
        DocumentType DocumentType { get;} 
        bool IsNew { get;}
        int UserId { get; }
    }
}