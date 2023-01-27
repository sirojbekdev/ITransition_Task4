using Task4.Areas.Identity.Data;

namespace Task4.Services
{
    public interface IUserService
    {
        Task<int> BlockUsers(List<string> usersIDs, CancellationToken cancellationToken);
        Task<List<AppUser>> GetAppUsers(CancellationToken cancellationToken);
    }
}
