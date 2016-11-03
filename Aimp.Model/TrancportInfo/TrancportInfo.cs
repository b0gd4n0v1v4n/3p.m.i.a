using Entities;
using System.Collections.Generic;

namespace Aimp.Model.TrancportInfo
{
    public class TrancportInfo 
    {
        public IEnumerable<ModelTrancport> Models {get;set;}
        public IEnumerable<MakeTrancport> Makes { get; set; }
        public IEnumerable<TrancportCategory> Categories { get; set; }
        public IEnumerable<TrancportType> Types { get; set; }
        public IEnumerable<EngineType> EngineTypes { get; set; }
    }
}
