using Blazorcrud.Server.Authorization;
using Microsoft.Net.Http.Headers;
using Blazorcrud.Server.Models;
using Blazorcrud.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Blazorcrud.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly AppSettings _appSettings;

        public UserController(IUserRepository userRepository, IOptions<AppSettings> appSettings)
        {
            _userRepository = userRepository;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Authenticates a user and returns a JWT token and user details
        /// </summary>
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public ActionResult Authenticate(AuthenticateRequest request)
        {
            return Ok(_userRepository.Authenticate(request));
        }

        /// <summary>
        /// Log out the user.
        /// </summary>
        [AllowAnonymous]
        [HttpPost("logout")]
        public async Task<ActionResult> LogOut(int id)
        {
            var user = await _userRepository.GetUser(id);
            return Ok(_userRepository.Logout(user!));
        }


        /// <summary>
        /// Returns a list of paginated users with a default page size of 5.
        /// </summary>
        [AllowAnonymous]
        [HttpGet]
        public ActionResult GetUsers([FromQuery] string? name, int page)
        {
            return Ok(_userRepository.GetUsers(name, page));
        }

        /// <summary>
        /// Gets a specific user by Id.
        /// </summary>
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetUser(int id)
        {
            return Ok(await _userRepository.GetUser(id));
        }

        /// <summary>
        /// Creates a user and hashes password.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> AddUser(User user)
        {
            string userAgent = Request.Headers[HeaderNames.UserAgent]!;
            user.OS = _userRepository.GetOperatingSystem(userAgent);
            user.Browser = _userRepository.GetBrowser(userAgent);
            return Ok(await _userRepository.AddUser(user));
        }

        /// <summary>
        /// Updates a user with a specific Id, hashing password if updated.
        /// </summary>
        [HttpPut]
        public async Task<ActionResult> UpdateUser(User user)
        {
            return Ok(await _userRepository.UpdateUser(user));
        }

        /// <summary>
        /// Deletes a user with a specific Id.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            return Ok(await _userRepository.DeleteUser(id));
        }
    }
}
