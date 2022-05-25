using FunTrophy.Shared.Model;
using FunTrophy.Web.Components;
using FunTrophy.Web.Contracts.Services;
using Microsoft.AspNetCore.Components;

namespace FunTrophy.Web.Pages.Editor
{
    public partial class RacesPage
    {
        #region Properties

        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        private AppState AppState { get; set; } = default!;

        [Inject]
        private IRaceService RaceService { get; set; } = default!;

        private ConfirmDialog DeleteDialog { get; set; } = default!;

        private EditDialog EditDialog { get; set; } = default!;

        private List<RaceDto>? Races { get; set; }

        private int? DeleteRaceId { get; set; }

        private AddOrUpdateRaceDto addRace = new() { Name = "Fun Trophy", Date = DateTime.Now };

        private AddOrUpdateRaceDto updateRace = new();

        private int? updateRaceId;

        #endregion Properties

        protected override async Task OnInitializedAsync()
        {
            await LoadRaces();
        }

        private async Task LoadRaces()
        {
            Races = (await RaceService.GetRaces()).OrderByDescending(x => x.Date).ToList();
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
            DeleteDialog.Show(message);
        }

        private async Task AddRace()
        {
            await RaceService.Add(addRace);
            await LoadRaces();
        }

        private void ConfirmEditRace(RaceDto race)
        {
            updateRace.Name = race.Name;
            updateRace.Date = race.Date;
            updateRaceId = race.Id;
            EditDialog.Show();
        }

        private async Task DeleteRace(bool confirm)
        {
            if (confirm && DeleteRaceId.HasValue)
            {
                await RaceService.Remove(DeleteRaceId.Value);
                await LoadRaces();
            }
        }

        private async Task UpdateRace(bool confirm)
        {
            if (confirm && updateRaceId.HasValue)
            {
                await RaceService.Update(updateRaceId.Value, updateRace);
                await LoadRaces();
            }
        }
    }
}