using MelodiaxGuitarsAPI.Models;
using MelodiaxGuitarsAPI.Repositories.Base;

namespace MelodiaxGuitarsAPI.Repositories.Users
{
    public interface IUserRepository : IEntityBaseRepository<User>
    {
        Task UpdateUserAsync(int id, User user);
    }
}
