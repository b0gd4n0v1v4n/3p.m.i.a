using Aimp.DataAccess.Interfaces;
using Aimp.Domain;
using Entities;
using System.Linq;

namespace Aimp.Logic.Extensions
{
    public static class UserExtensions
    {
        public static bool IsAdmin(this User user)
        {
            using (var context = IoC.Resolve<IDataContext>())
            {
                return context.Users.All().Any(x => x == SecurityRigths.UserRightsCollection.Admin.Id);
            }
        }
    }
}
