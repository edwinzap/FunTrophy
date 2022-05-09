using Blazored.SessionStorage;
using FunTrophy.Shared.Model;
using FunTrophy.Web.Contracts.Services;
using Microsoft.AspNetCore.Components;

namespace FunTrophy.Web.Pages.Editor.Race
{
    public partial class Index
    {
        [Inject]
        ISessionStorageService SessionStorageService { get; set; } = default!;

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
            await SessionStorageService.SetItemAsync("race", race);
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
