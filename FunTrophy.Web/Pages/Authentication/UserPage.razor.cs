using FunTrophy.Shared.Model.Authentication;
using FunTrophy.Web.Authentication;
using Microsoft.AspNetCore.Components;

namespace FunTrophy.Web.Pages.Authentication
{
    public partial class UserPage
    {
        [Inject]
        private IAuthenticationService AuthenticationService { get; set; } = default!;

        private AuthenticationUser user = new();

        private async Task Login()
        {
            var token = await AuthenticationService.Login(user);
        }
    }
}