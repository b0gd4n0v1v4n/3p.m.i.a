using Aimp.Model.Entities;

namespace Aimp.Model.UserFiles
{
    public class UserFileDto : IResponse
    {
        public bool Error { get; set; }
        public string Message { get; set; }
        public UserFile UserFile { get; set; }
    }
}
