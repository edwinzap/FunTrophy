using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FunTrophy.API.Controllers
{
    [Authorize]
    public class AuthenticatedControllerBase: ControllerBase
    {
    }

    public class AnonymousControllerBase: ControllerBase
    {

    }
}
