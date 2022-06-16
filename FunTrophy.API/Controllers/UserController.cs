using FunTrophy.API.Authentication;
using FunTrophy.API.Contracts.Services;
using FunTrophy.Shared.Model;
using FunTrophy.Shared.Model.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FunTrophy.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : AdminControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IJWTManagerRepository _jwtRepository;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IJWTManagerRepository jwtRepository, IUserService userService)
        {
            _logger = logger;
            _jwtRepository = jwtRepository;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        [ProducesResponseType(typeof(Token), 200)]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticationUser user)
        {
            var token = await _jwtRepository.Authenticate(user);

            if (token is null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }

        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="user">The user</param>
        /// <returns>The new user Id</returns>
        [HttpPost("")]
        [ProducesResponseType(typeof(int), 200)]
        public async Task<IActionResult> CreateUser([FromBody] AddUserDto user)
        {
            var userId = await _userService.Create(user);
            return Ok(userId);
        }

        [HttpPost("{userId}/changepassword")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> ChangeUserPassword(int userId, [FromBody] string password)
        {
            await _userService.ChangePassword(userId, password);
            return Ok();
        }

        // <summary>
        /// Remove the user with the given Id
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns></returns>
        [HttpDelete("{userId}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> RemoveUser(int userId)
        {
            await _userService.Remove(userId);
            return Ok();
        }

        /// <summary>
        /// Update the user with the given Id
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="user">User object</param>
        /// <returns></returns>
        [HttpPut("{userId}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] UpdateUserDto user)
        {
            await _userService.Update(userId, user);
            return Ok();
        }

        /// <summary>
        /// Get a list of all users
        /// </summary>
        /// <returns>List of users</returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(List<UserDto>), 200)]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAll();
            return Ok(users);
        }
    }
}