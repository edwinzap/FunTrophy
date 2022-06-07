using FunTrophy.Shared.Model.Authentication;
using FunTrophy.Web.Authentication;
using Microsoft.AspNetCore.Components;

namespace FunTrophy.Web.Pages
{
    public partial class LoginPage
    {
        [Inject]
        private IAuthenticationService AuthenticationService { get; set; } = default!;

        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;

        private bool ShowAuthenticationErrorMessage { get; set; } = false;

        private AuthenticationUser user = new();

        private async Task Login()
        {
            var token = await AuthenticationService.Login(user);
            if (token is null)
            {
                ShowAuthenticationErrorMessage = true;
            }
            else
            {
                NavigationManager.NavigateTo("/");
            }
        }
    }
}