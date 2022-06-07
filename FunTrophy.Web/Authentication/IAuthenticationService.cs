using FunTrophy.Shared.Model.Authentication;

namespace FunTrophy.Web.Authentication
{
    public interface IAuthenticationService
    {
        Task<Token?> Login(User userForAuthentication);
        Task Logout();
    }
}