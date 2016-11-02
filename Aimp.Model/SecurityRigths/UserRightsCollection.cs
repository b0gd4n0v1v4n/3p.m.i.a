using System.Collections.Generic;
using Aimp.Model.SecurityRigths.Operations;

namespace Aimp.Model.SecurityRigths
{
    public static class UserRightsCollection
    {
        public static AddOperation Add { get; }
        public static DeleteOperation Delete { get; }
        public static ViewOperation View { get; }
        public static AdminRight Admin { get; }
        public static List<IRight> Rights { get; } 
        static UserRightsCollection()
        {
            Rights = new List<IRight>();
            Add = new AddOperation();
            Rights.Add(Add);
            Delete = new DeleteOperation();
            Rights.Add(Delete);
            View = new ViewOperation();
            Rights.Add(View);
            Admin = new AdminRight();
            Rights.Add(Admin);
        }
    }
}
