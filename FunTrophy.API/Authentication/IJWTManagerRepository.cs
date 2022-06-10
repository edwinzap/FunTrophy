using FunTrophy.Shared.Model.Authentication;

namespace FunTrophy.API.Authentication
{
    public interface IJWTManagerRepository
    {
        Task<Token?> Authenticate(AuthenticationUser user);
    }
}
