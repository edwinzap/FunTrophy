using FunTrophy.Shared.Model.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace FunTrophy.API.Authentication
{
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params UserRoles[] allowedRoles)
        {
            var allowedRolesAsStrings = allowedRoles.Select(x => Enum.GetName(typeof(UserRoles), x));
            Roles = string.Join(",", allowedRolesAsStrings);
        }
    }
}
