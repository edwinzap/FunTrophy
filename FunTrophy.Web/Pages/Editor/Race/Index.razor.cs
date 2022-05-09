using FunTrophy.Shared.Model;
using FunTrophy.Web.Contracts.Services;
using Microsoft.AspNetCore.Components;

namespace FunTrophy.Web.Pages.Editor.Race
{
    public partial class Index
    {
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
    }
}
