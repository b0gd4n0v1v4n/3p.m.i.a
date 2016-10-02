using Aimp.Rights;
using System.Collections.Generic;

namespace Aimp.Model.Rights
{
    public static class RightsCollection
    {
        public static IRight Add { get; }
        public static IRight Delete { get; }
        public static IRight View { get; }
        public static IRight Admin { get; }

        private static List<IRight> _items;
        public static IEnumerable<IRight> Items { get { return _items; } }

        static RightsCollection()
        {
            _items = new List<IRight>();
            Add = new RightAdd();
            _items.Add(Add);
            Delete = new RightDelete();
            _items.Add(Delete);
            Admin = new RightAdmin();
            _items.Add(Admin);
            View = new RightView();
            _items.Add(View);
        }
    }
}
