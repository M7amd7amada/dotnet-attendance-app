using Blazorcrud.Server.Authorization;
using Blazorcrud.Server.Helpers;
using Blazorcrud.Shared.Data;
using Blazorcrud.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Blazorcrud.Server.Models
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _appDbContext;
        private IJwtUtils _jwtUtils;

        public UserRepository(AppDbContext appDbContext, IJwtUtils jwtUtils)
        {
            _appDbContext = appDbContext;
            _jwtUtils = jwtUtils;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest request)
        {
            var _user = _appDbContext.Users.SingleOrDefault(u => u.Username == request.Username);

            // validate
            if (_user == null || !BCrypt.Net.BCrypt.Verify(request.Password, _user.PasswordHash))
                throw new AppException("Username or password is incorrect");

            _user.LoginDate = DateTime.Now;

            // authentication successful
            AuthenticateResponse response = new AuthenticateResponse();
            response.Id = _user.Id;
            response.LastName = _user.LastName;
            response.FirstName = _user.FirstName;
            response.Username = _user.Username;
            response.Token = _jwtUtils.GenerateToken(_user);
            return response;
        }

        public User Logout(User user)
        {
            user.LogoutDate = DateTime.Now;
            user.LoginStatus = "Inactive";
            return user;
        }

        public PagedResult<User> GetUsers(string? name, int page)
        {
            int pageSize = 5;

            if (name != null)
            {
                return _appDbContext.Users
                    .AsNoTracking()
                    .Where(u => u.FirstName == name || u.LastName == name || u.Username == name)
                    .OrderBy(u => u.Username)
                    .GetPaged(page, pageSize);
            }
            else
            {
                return _appDbContext.Users
                    .OrderBy(u => u.Username)
                    .GetPaged(page, pageSize);
            }
        }

        public async Task<User?> GetUser(int Id)
        {
            if (Id <= 0) Id = 1;
            var result = await _appDbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == Id);
            return result;
        }

        public async Task<User> AddUser(User user)
        {
            // validate unique
            if (_appDbContext.Users.Any(u => u.Username == user.Username))
                throw new AppException("Username '" + user.Username + "' is already taken");

            // hash password
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);
            Console.WriteLine(user.Password + " ==> " + user.PasswordHash);
            user.Password = "**********";
            user.LoginDate = DateTime.Now;

            var result = await _appDbContext.Users.AddAsync(user);
            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<User?> UpdateUser(User user)
        {
            var result = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Id == user.Id);

            // cannot update admin
            if (result!.Username == "admin")
                throw new AppException("Admin may not be updated");

            // validate unique
            if (user.Username != result.Username && _appDbContext.Users.Any(u => u.Username == user.Username))
                throw new AppException("Username '" + user.Username + "' is already taken");

            // hash password if entered
            if (!string.IsNullOrEmpty(user.Password) && user.Password != result.Password)
            {
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);
                user.Password = "**********";
            }

            if (result != null)
            {
                // Update existing user
                _appDbContext.Entry(result).CurrentValues.SetValues(user);
                await _appDbContext.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("User not found " + nameof(UpdateUser));
            }
            return result;
        }

        public async Task<User?> DeleteUser(int Id)
        {
            var result = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Id == Id);

            // cannot delete admin
            if (result!.Username == "admin")
                throw new AppException("Admin may not be deleted");

            if (result != null)
            {
                _appDbContext.Users.Remove(result);
                await _appDbContext.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("User not found " + nameof(DeleteUser));
            }
            return result;
        }

        public string GetOperatingSystem(string userAgent)
        {

            if (userAgent.Contains("Windows"))
            {
                return "Windows 11";
            }
            else if (userAgent.Contains("Mac"))
            {
                return "Mac OS";
            }
            else if (userAgent.Contains("Linux"))
            {
                return "Linux";
            }
            else
            {
                return "Unknown";
            }
        }

        public string GetBrowser(string userAgent)
        {
            if (userAgent.Contains("MSIE") || userAgent.Contains("Trident"))
            {
                return "Internet Explorer";
            }
            else if (userAgent.Contains("Edge"))
            {
                return "Microsoft Edge";
            }
            else if (userAgent.Contains("Chrome"))
            {
                return "Google Chrome";
            }
            else if (userAgent.Contains("Firefox"))
            {
                return "Mozilla Firefox";
            }
            else if (userAgent.Contains("Safari"))
            {
                return "Safari";
            }
            else
            {
                return "Unknown";
            }
        }
    }
}