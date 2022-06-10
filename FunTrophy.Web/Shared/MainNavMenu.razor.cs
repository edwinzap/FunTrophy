using FunTrophy.Web.Contracts.Services;
using FunTrophy.Web.Models;
using Microsoft.AspNetCore.Components;

namespace FunTrophy.Web.Shared
{
    public partial class MainNavMenu
    {
        #region Properties

        [Inject]
        private IAppStateService AppStateService { get; set; } = default!;

        private bool expandDropdown = false;
        private bool collapseNavMenu = true;

        private string? NavMenuCssClass => collapseNavMenu ? "collapse" : null;
        private string? ResultsMenuCssClass => expandDropdown ? "show" : null;
        private List<DropDownMenuItem>? UserMenuItems { get; set; }
        private List<DropDownMenuItem>? ResultMenuItems { get; set; }
        private bool IsCurrentRaceSelected { get; set; } = false;
        private bool IsCurrentRaceEnded { get; set; } = false;

        #endregion Properties

        protected override async Task OnInitializedAsync()
        {
            UserMenuItems = new List<DropDownMenuItem>()
            {
                new DropDownMenuItem("Se déconnecter", "fun-logout")
            };

            var finalMenu = new DropDownMenuItem("Classement final", "resultats/fin", IsCurrentRaceSelected);
            ResultMenuItems = new List<DropDownMenuItem>()
            {
                new DropDownMenuItem("Par parcours", "resultats/parcours"),
                new DropDownMenuItem("Par équipe", "resultats/equipe"),
                finalMenu,
            };

            AppStateService.OnAppStateChanged += async () => await RefreshMenu();
            await RefreshMenu();
        }

        private async Task RefreshMenu()
        {
            var appState = await AppStateService.GetState();
            IsCurrentRaceEnded = appState?.Race?.IsEnded == true;
            IsCurrentRaceSelected = appState?.Race is not null;
            StateHasChanged();
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