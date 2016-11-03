using Aimp.Domain;
using Aimp.Logic.Interfaces;
using Entities;
using System.Linq;

namespace Aimp.Logic.Extensions
{
    public static class UserExtensions
    {
        public static bool IsAdmin(this User user)
        {
            var service = IoC.Resolve<IUserRightsService>();

            var userRights = service.GetUserRights(user.Id);

            return userRights.Any(x => x.RightId == Model.SecurityRigths.UserRightsCollection.Admin.Id);
        }
    }
}

