using MelodiaxGuitarsAPI.Models;
using MelodiaxGuitarsAPI.Repositories.Base;

namespace MelodiaxGuitarsAPI.Repositories.Users
{
    public interface IUserRepository : IEntityBaseRepository<User>
    {
        Task<User> GetUserById(string id);
        Task<User> GetUserByEmail(string email);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(string id, User user);
        Task UpdateUserOrdersAsync(string userId, string orderId);
    }
}
