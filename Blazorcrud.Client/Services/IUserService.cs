using Blazorcrud.Client.Shared;
using Blazorcrud.Shared.Data;
using Blazorcrud.Shared.Models;
#nullable enable
namespace Blazorcrud.Client.Services
{
    public interface IUserService
    {
        User User { get; }
        Task Initialize();
        Task Login(Login model);
        Task Logout();
        Task UpdateLogoutTime(int userId);
        Task SetToInactive(int id);
        Task<PagedResult<User>> GetUsers(string? name, string page);
        Task<User> GetUser(int id);
        Task DeleteUser(int id);
        Task AddUser(User user);
        Task UpdateUser(User user);
    }
}