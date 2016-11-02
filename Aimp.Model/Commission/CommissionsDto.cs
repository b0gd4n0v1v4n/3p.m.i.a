using System.Collections.Generic;

namespace Aimp.Model.Commission
{
    public class CommissionsDto : IResponse
    {
        public bool Error
        {
            get; set;
        }

        public IEnumerable<CommissionListItem> Items { get; set; }

        public string Message
        {
            get; set;
        }
    }
}
