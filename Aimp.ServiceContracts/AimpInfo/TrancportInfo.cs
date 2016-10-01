using Aimp.Entities;
using System.Collections.Generic;

namespace Aimp.ServiceContracts.AimpInfo
{
    public class TrancportInfo
    {
        public IEnumerable<IModelTrancport> Models {get;set;}
        public IEnumerable<IMakeTrancport> Makes { get; set; }
        public IEnumerable<ITrancportCategory> Categories { get; set; }
        public IEnumerable<ITrancportType> Types { get; set; }
        public IEnumerable<IEngineType> EngineTypes { get; set; }
    }
}
