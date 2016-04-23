namespace Models
{
    public class SaveEntityResult : IResponse
    {
        public bool Error { get; set; }
        public string Message { get; set; }
        public int Id { get; set; }
        public int Number { get; set; }
    }
}
