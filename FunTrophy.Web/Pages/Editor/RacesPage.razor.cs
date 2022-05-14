using FunTrophy.Shared.Model;
using FunTrophy.Web.Components;
using FunTrophy.Web.Contracts.Services;
using Microsoft.AspNetCore.Components;

namespace FunTrophy.Web.Pages.Editor
{
    public partial class RacesPage
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        private AppState AppState { get; set; }

        [Inject]
        private IRaceService RaceService { get; set; } = default!;

        public Confirm DeleteConfirmation { get; set; }

        public List<RaceDto> Races { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            await LoadRaces();
        }

        private async Task LoadRaces()
        {
            Races = await RaceService.GetRaces();
        }

        private void SelectRace(RaceDto race)
        {
            AppState.Race = race;
            NavigationManager.NavigateTo("editeur/couleurs");
        }

        private void ClearSelectedRace()
        {
            AppState.Race = null;
        }

        private async Task EditRace(int raceId)
        {
        }

        private void ConfirmDeleteRace()
        {
            DeleteConfirmation.Show();
        }

        private async Task DeleteRace(bool confirmDelete, int raceId)
        {
            if (confirmDelete)
            {
                await RaceService.Remove(raceId);
                await LoadRaces();
            }
        }
    }
}