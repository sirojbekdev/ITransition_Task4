using BlazorApp.Areas.Identity.Data;

namespace BlazorApp.Data.Services
{
    public interface IUserService
    {
        Task<int> BlockUsers(IEnumerable<AppUser> usersIDs);
        IEnumerable<AppUser> GetAppUsers();
        int UnblockUsers(IEnumerable<AppUser> users);
        Task<int> DeleteUsers(IEnumerable<AppUser> users);
    }
}
