using Aimp.UserRights.Exeptions;
using Aimp.UserRights.Rights;
using System.Collections.Generic;
using System.Linq;

namespace Aimp.UserRights
{
    public static class AccountExtansions
    {
        private static IRight _add;
        private static IRight _delete;
        private static IRight _view;
        private static IRight _admin;
        static AccountExtansions()
        {
            _add = new RightAdd();
            _delete = new RightDelete();
            _admin = new RightAdmin());
            _view = new RightView(); 
        }
        public static bool IsAdmin(this IAccount user)
        {
            return user.RightIds.Any(x => x == _admin.Id);
        }
        public static void CheckViewRight(this IAccount user)
        {
            if (user.RightIds.Count(x => x == _view.Id || x == _admin.Id) == 0)
                throw new AccessDeniedException("У пользователя нет прав для выполнения операции");
        }
        public static void CheckAddRight(this IAccount user)
        {
            if (user.RightIds.Count(x => x == _add.Id || x == _admin.Id) == 0)
                throw new AccessDeniedException("У пользователя нет прав для выполнения операции");
        }
        public static void CheckDeleteRight(this IAccount user)
        {
            if (user.RightIds.Count(x => x == _delete.Id || x == _admin.Id) == 0)
                throw new AccessDeniedException("У пользователя нет прав для выполнения операции");
        }
        public static void CheckQueryRight(this IAccount user)
        {
            if (user.RightIds.Count(x => x == _add.Id || x == _delete.Id || x == _admin.Id) == 0)
                throw new AccessDeniedException("У пользователя нет прав для выполнения операции");
        }
    }
}
