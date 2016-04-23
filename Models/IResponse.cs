namespace Models
{
    public interface IResponse
    {
        bool Error { get; set; }

        string Message { get; set; }
    }
}
