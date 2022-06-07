using FunTrophy.Shared.Model.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FunTrophy.API.Authentication
{
    public class UserController : ControllerBase
    {
        private readonly IJWTManagerRepository _jwtRepository;

        public UserController(IJWTManagerRepository jwtRepository)
        {
            _jwtRepository = jwtRepository;
        }
		
		
		[AllowAnonymous]
		[HttpPost]
		[Route("authenticate")]
		public IActionResult Authenticate([FromBody]AuthenticationUser user)
		{
			var token = _jwtRepository.Authenticate(user);

			if (token == null)
			{
				return Unauthorized();
			}

			return Ok(token);
		}

	}
}
