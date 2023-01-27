using Microsoft.EntityFrameworkCore;
using Task4.Areas.Identity.Data;
using Task4.Models.Enums;

namespace Task4.Services
{
    public class UserService :IUserService
    {
        private readonly TaskContext _taskContext;

        public UserService(TaskContext taskContext)
        {
            _taskContext = taskContext;
        }

        public async Task<List<AppUser>> GetAppUsers(CancellationToken cancellationToken)
        {
            return await _taskContext.Users.ToListAsync(cancellationToken);
        }

        public async Task<int> BlockUsers(List<string> usersIDs, CancellationToken cancellationToken)
        {
            var users = await _taskContext.Users.Where(u=> usersIDs.Contains(u.Id)).ToListAsync(cancellationToken);

            foreach (var user in users)
            {
                user.Status = Status.Blocked;
            }

            _taskContext.UpdateRange(users);
            var result = await _taskContext.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
