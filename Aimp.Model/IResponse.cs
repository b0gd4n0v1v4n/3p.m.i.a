namespace Aimp.Model
{
    public interface IResponse
    {
        bool Error { get; set; }

        string Message { get; set; }
    }
}
