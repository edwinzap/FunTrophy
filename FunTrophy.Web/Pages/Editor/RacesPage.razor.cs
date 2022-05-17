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

        private Confirm DeleteConfirmation { get; set; }

        private List<RaceDto> Races { get; set; } = new();

        private int? DeleteRaceId { get; set; }

        private AddOrUpdateRaceDto addRace = new() { Name = "Fun Trophy", Date = DateTime.Now };

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

        private void ConfirmDeleteRace(RaceDto race)
        {
            DeleteRaceId = race.Id;
            var message = $"Es-tu sûr de vouloir supprimer '{race.Name}'?";
            DeleteConfirmation.Show(message);
        }

        private async Task AddRace()
        {
            await RaceService.Add(addRace);
            await LoadRaces();
        }

        private void EditRace(int raceId)
        {

        }

        private async Task DeleteRace(bool confirmDelete)
        {
            if (confirmDelete && DeleteRaceId.HasValue)
            {
                await RaceService.Remove(DeleteRaceId.Value);
                await LoadRaces();
            }
        }


    }
}