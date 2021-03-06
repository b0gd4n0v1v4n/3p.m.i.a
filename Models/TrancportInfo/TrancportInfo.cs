﻿using Models.Entities;
using System.Collections.Generic;

namespace Models.TrancportInfo
{
    public class TrancportInfo : IResponse
    {
        public bool Error { get; set; }
        public string Message { get; set; }
        public IEnumerable<ModelTrancport> Models {get;set;}
        public IEnumerable<MakeTrancport> Makes { get; set; }
        public IEnumerable<TrancportCategory> Categories { get; set; }
        public IEnumerable<TrancportType> Types { get; set; }
        public IEnumerable<EngineType> EngineTypes { get; set; }
    }
}
