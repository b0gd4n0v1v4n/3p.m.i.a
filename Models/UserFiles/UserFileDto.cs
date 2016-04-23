using Models.Entities;

namespace Models.UserFiles
{
    public class UserFileDto : IResponse
    {
        public bool Error { get; set; }
        public string Message { get; set; }
        public UserFile UserFile { get; set; }
    }
}
