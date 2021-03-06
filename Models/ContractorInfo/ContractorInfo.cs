﻿using Models.Entities;
using System.Collections.Generic;

namespace Models.ContractorInfo
{
    public class ContractorInfo : IResponse
    {
        public bool Error { get; set; }
        public string Message { get; set; }
        public IEnumerable<City> Cities { get; set; }
        public IEnumerable<Region> Regions { get; set; }
    }
}
