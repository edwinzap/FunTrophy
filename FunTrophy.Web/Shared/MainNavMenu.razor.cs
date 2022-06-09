using FunTrophy.Web.Models;
using Microsoft.AspNetCore.Components;

namespace FunTrophy.Web.Shared
{
    public partial class MainNavMenu
    {
        #region Properties

        [Inject]
        private AppState AppState { get; set; } = default!;

        private bool expandDropdown = false;
        private bool collapseNavMenu = true;

        private string? NavMenuCssClass => collapseNavMenu ? "collapse" : null;
        private string? ResultsMenuCssClass => expandDropdown ? "show" : null;
        private List<DropDownMenuItem>? UserMenuItems { get; set; }
        private List<DropDownMenuItem>? ResultMenuItems { get; set; }

        #endregion Properties

        protected override void OnInitialized()
        {
            UserMenuItems = new List<DropDownMenuItem>()
            {
                new DropDownMenuItem("Se déconnecter", "fun-logout")
            };

            var finalMenu = new DropDownMenuItem("Classement final", "resultats/fin", AppState.Race?.IsEnded == true);
            ResultMenuItems = new List<DropDownMenuItem>()
            {
                new DropDownMenuItem("Par parcours", "resultats/parcours"),
                new DropDownMenuItem("Par équipe", "resultats/equipe"),
                finalMenu,
            };

            AppState.OnChange += () =>
            {
                finalMenu.IsVisible = AppState.Race?.IsEnded == true;
                StateHasChanged();
            };
        }

        private void CloseResultsMenu()
        {
            collapseNavMenu = true;
            expandDropdown = false;
        }

        private void ToggleNavMenu()
        {
            collapseNavMenu = !collapseNavMenu;
        }

        public void Dispose()
        {
            AppState.OnChange -= StateHasChanged;
        }

        private void ToggleResults()
        {
            expandDropdown = !expandDropdown;
        }
    }
}