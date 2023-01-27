using BlazorApp.Areas.Identity.Data;
using BlazorApp.Data.Enums;
using Microsoft.AspNetCore.Identity;

namespace BlazorApp.Data.Services
{
    public class UserService :IUserService
    {
        private readonly MainContext _context;
        private readonly UserManager<AppUser> _userManager;

        public UserService(MainContext taskContext, UserManager<AppUser> userManager)
        {
            _context = taskContext;
            _userManager = userManager;
        }

        public IEnumerable<AppUser> GetAppUsers()
        {
            return _context.Users.ToList();
        }

        public async Task<int> BlockUsers(IEnumerable<AppUser> users)
        {
            foreach (var user in users)
            {
                user.Status = Status.Blocked;
                await _userManager.UpdateSecurityStampAsync(user);
            }

            _context.UpdateRange(users);
            var result =  _context.SaveChanges();

            return result;
        }

        public int DeleteUsers(IEnumerable<AppUser> users)
        {
             _context.RemoveRange(users);
            var result = _context.SaveChanges();
            return result;
        }

        public int UnblockUsers(IEnumerable<AppUser> users)
        {
            foreach (var user in users)
            {
                user.Status = Status.Active;
            }

            _context.UpdateRange(users);
            var result = _context.SaveChanges();

            return result;
        }
    }
}
