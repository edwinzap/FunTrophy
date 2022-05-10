using Blazored.SessionStorage;
using FunTrophy.Shared.Model;
using FunTrophy.Web.Contracts.Services;
using Microsoft.AspNetCore.Components;

namespace FunTrophy.Web.Pages.Editor.Race
{
    public partial class Index
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        AppState AppState { get; set; }

        [Inject]
        IRaceService RaceService { get; set; } = default!;

        public List<RaceDto> Races { get; set; }

        public Index()
        {
            //Races = FakeModel.Races;
        }

        protected override async Task OnInitializedAsync()
        {
            await GetRaces();
        }

        private async Task GetRaces()
        {
            Races = await RaceService.GetRaces();
        }

        private async Task SelectRace(RaceDto race)
        {
            AppState.Race = race;
            NavigationManager.NavigateTo("editeur/couleurs");
        }

        private async Task EditRace(int raceId)
        {
        }

        private async Task RemoveRace(int raceId)
        {
            await RaceService.Remove(raceId);
        }
    }
}
