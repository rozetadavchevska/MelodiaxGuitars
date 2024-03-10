using MelodiaxGuitarsAPI.Models;
using MelodiaxGuitarsAPI.Repositories.Base;

namespace MelodiaxGuitarsAPI.Repositories.Users
{
    public interface IUserRepository : IEntityBaseRepository<User>
    {
        Task<User> GetUserById(int id);
        Task<User> GetUserByEmail(string email);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(int id, User user);
        Task UpdateUserOrdersAsync(int userId, int orderId);
    }
}
