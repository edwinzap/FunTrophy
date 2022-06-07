using FunTrophy.Shared.Model;
using FunTrophy.Web.Contracts.Services;
using Microsoft.AspNetCore.Components;

namespace FunTrophy.Web.Pages
{
    public partial class Index
    {
        [Inject]
        private IRaceService RaceService { get; set; } = default!;

        [Inject]
        private AppState AppState { get; set; } = default!;

        private List<RaceDto>? Races { get; set; }

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
        }
    }
}