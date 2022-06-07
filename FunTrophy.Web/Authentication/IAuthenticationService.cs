using FunTrophy.Web.Models;

namespace FunTrophy.Web.Authentication
{
    public interface IAuthenticationService
    {
        Task<AuthenticatedUserModel?> Login(AuthenticationUserModel userForAuthentication);
        Task Logout();
    }
}