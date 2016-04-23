using Models.Entities;
using System.Collections.Generic;

namespace Models.CardTrancports
{
    public class StatusesCardTrancportDto : IResponse
    {
        public bool Error
        {
            get; set;
        }

        public IEnumerable<StatusCardTrancport> Items { get; set; }

        public string Message
        {
            get; set;
        }
    }
}