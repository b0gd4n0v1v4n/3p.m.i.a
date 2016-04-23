using Models.Entities;

namespace Models.TrancportInfo
{
    public class TrancportDto : IResponse
    {
        public bool Error { get; set; }
        public string Message { get; set; }
        public Trancport Trancport { get; set; }
    }
}
