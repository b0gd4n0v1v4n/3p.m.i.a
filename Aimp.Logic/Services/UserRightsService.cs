using Aimp.DataAccess.Interfaces;
using Aimp.Domain;
using Aimp.Logic.Interfaces;
using Entities;
using System.Collections.Generic;
using System.Linq;

namespace Aimp.Logic.Services
{
    public class UserRightsService : IUserRightsService
    {
        private IDataContext _context;

        public UserRightsService()
        {
            _context = IoC.Resolve<IDataContext>();
        }

        public IQueryable<UserRight> GetUserRights(int id)
        {
            return _context.UserRights.All().Where(x => x.UserId == id);
        }
        public IQueryable<User> GetUsers()
        {
            return _context.Users.All();
        }
        public void SaveUser(IEnumerable<string> rightIds, User user)
        {
            var oldRights = _context.UserRights.All().Where(x => x.UserId == user.Id).ToList();
            if (user.Id != 0)
            {
                var deleteIds = oldRights.Where(x => !rightIds.Contains(x.RightId)).Select(x => x.Id).ToArray();
                _context.UserRights.DeleteRange(deleteIds);
            }

            _context.Users.AddOrUpdate(user);
            if (user.Id == 0)
                _context.SaveChanges();

            foreach (string iRight in rightIds)
            {
                if (!oldRights.Any(x => x.RightId == iRight))
                {
                    _context.UserRights.AddOrUpdate(new UserRight()
                    {
                        UserId = user.Id,
                        RightId = iRight
                    });
                }
            }
            _context.SaveChanges();
        }
        public void DeleteUser(User user)
        {
            var rigths = _context.UserRights.All().Where(x => x.UserId == user.Id).Select(x => x.Id).ToArray();
            _context.UserRights.DeleteRange(rigths);
            _context.Users.Delete(user);
            _context.SaveChanges();
        }
    }
}
