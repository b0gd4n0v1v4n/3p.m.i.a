using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aimp.Model
{
    public class AimpUserDto : IResponse
    {
        public int Id { get; set; }
        public IEnumerable<string> UserRights { get; set; }
        public bool Error { get; set; }
        public string Message { get; set; }
    }
}
