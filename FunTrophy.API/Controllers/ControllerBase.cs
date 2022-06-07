using FunTrophy.API.Authentication;
using FunTrophy.Shared.Model.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FunTrophy.API.Controllers
{
    [AuthorizeRoles(UserRoles.Admin)]
    public class AdminControllerBase: ControllerBase
    {
    }

    [AuthorizeRoles(UserRoles.Admin, UserRoles.User)]
    public class UserControllerBase: ControllerBase
    {

    }

    public class AnonymousControllerBase: ControllerBase
    {

    }
}
