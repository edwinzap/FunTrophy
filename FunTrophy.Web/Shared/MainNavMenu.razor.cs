using FunTrophy.Web.Contracts.Services;
using FunTrophy.Web.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace FunTrophy.Web.Shared
{
    public partial class MainNavMenu: IAsyncDisposable
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
        private List<DropDownMenuItem> ResultMenuItems { get; set; }
        private bool IsCurrentRaceSelected { get; set; } = false;
        private bool IsCurrentRaceEnded { get; set; } = false;
        private bool ShowFinalResultMenu { get; set; } = false;
        private Action OnEditorStateChanged => async () => await RefreshMenu();
        private AuthenticationStateChangedHandler OnAuthenticationStateChanged => async (_) => await RefreshMenu();

        #endregion Properties

        public MainNavMenu()
        {
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

            AppStateService.OnAppStateChanged += OnEditorStateChanged;
            AuthenticationStateProvider.AuthenticationStateChanged += OnAuthenticationStateChanged;
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
            ShowFinalResultMenu = IsCurrentRaceEnded || authState.User.Identity?.IsAuthenticated == true;
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

        public async ValueTask DisposeAsync()
        {
            AppStateService.OnAppStateChanged -= OnEditorStateChanged;
            AuthenticationStateProvider.AuthenticationStateChanged -= OnAuthenticationStateChanged;
            await AppStateService.StopListeningToChange();
        }
    }
}