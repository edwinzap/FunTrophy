using FunTrophy.Shared.Model.Authentication;

namespace FunTrophy.Web.Authentication
{
    public interface IAuthenticationService
    {
        Task<Token?> Login(AuthenticationUser userForAuthentication);

        Task Logout();

        Task<bool> IsConnected();
    }
}