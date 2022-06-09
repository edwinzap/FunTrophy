using FunTrophy.Shared.Model.Authentication;

namespace FunTrophy.API.Authentication
{
    public interface IJWTManagerRepository
    {
        Token? Authenticate(AuthenticationUser user);
    }
}
