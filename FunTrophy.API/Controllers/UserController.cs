using FunTrophy.API.Contracts.Services;

namespace FunTrophy.API.Controllers
{
    public class UserController: AdminControllerBase
    {
        public UserController(ILogger<UserController> logger, IUserService userService)
        {
        }
}
}
