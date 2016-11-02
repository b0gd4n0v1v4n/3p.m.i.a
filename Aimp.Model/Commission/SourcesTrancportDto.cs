using Aimp.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aimp.Model.Commission
{
    public class SourcesTrancportDto : IResponse
    {
        public bool Error { get; set; }
        public string Message { get; set; }
        public IEnumerable<SourceTrancport> Items { get; set; }
    }
}