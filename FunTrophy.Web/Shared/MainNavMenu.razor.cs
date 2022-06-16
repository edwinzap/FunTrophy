using FunTrophy.Web.Contracts.Services;
using FunTrophy.Web.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace FunTrophy.Web.Shared
{
    public partial class MainNavMenu
    {
        #region Properties

        [Inject]
        private IAppStateService AppStateService { get; set; } = default!;

        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;

        private bool collapseNavMenu = true;

        private string? NavMenuCssClass => collapseNavMenu ? "collapse" : null;
        private List<DropDownMenuItem>? UserMenuItems { get; set; }

        private DropDownMenuItem _finalMenu;

        private List<DropDownMenuItem> ResultMenuItems { get; set; }
        private bool IsCurrentRaceSelected { get; set; } = false;
        private bool IsCurrentRaceEnded { get; set; } = false;
        private bool ShowFinalResultMenu { get; set; } = false;


        #endregion Properties

        public MainNavMenu()
        {
            _finalMenu = new DropDownMenuItem("Classement final", "resultats/fin", true);
            ResultMenuItems = new List<DropDownMenuItem>()
            {
                new DropDownMenuItem("Par parcours", "resultats/parcours"),
                new DropDownMenuItem("Par équipe", "resultats/equipe"),
            };
        }

        protected override async Task OnInitializedAsync()
        {
            UserMenuItems = new List<DropDownMenuItem>()
            {
                new DropDownMenuItem("Se déconnecter", "fun-logout")
            };

            AppStateService.OnAppStateChanged += async () => await RefreshMenu();
            AuthenticationStateProvider.AuthenticationStateChanged += async (_) => await RefreshMenu();
            await AppStateService.StartListeningToChange();
            await RefreshMenu();

            NavigationManager.LocationChanged += (_, _) => CloseNavMenu();
        }

        private async Task RefreshMenu()
        {
            var appState = await AppStateService.GetState();
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            IsCurrentRaceEnded = appState?.Race?.IsEnded == true;
            IsCurrentRaceSelected = appState?.Race is not null;
            ShowFinalResultMenu = IsCurrentRaceEnded || authState.User.Identity?.IsAuthenticated == true && !ResultMenuItems.Contains(_finalMenu);
            StateHasChanged();
        }

        private void CloseNavMenu()
        {
            if (!collapseNavMenu)
            {
                collapseNavMenu = true;
                StateHasChanged();
            }
        }

        private void ToggleNavMenu()
        {
            collapseNavMenu = !collapseNavMenu;
        }

        public void Dispose()
        {
            AppStateService.OnAppStateChanged -= StateHasChanged;
        }
    }
}