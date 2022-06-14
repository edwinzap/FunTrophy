using FunTrophy.Shared.Model;
using FunTrophy.Web.Contracts.Services;
using Microsoft.AspNetCore.Components;

namespace FunTrophy.Web.Pages
{
    public partial class Index
    {
        #region Properties

        [Inject]
        private IRaceService RaceService { get; set; } = default!;

        [Inject]
        private IAppStateService AppStateService { get; set; } = default!;

        private List<RaceDto>? Races { get; set; }
        public RaceDto? SelectedRace { get; private set; }

        #endregion Properties

        protected override async Task OnInitializedAsync()
        {
            AppStateService.OnAppStateChanged += async () => await UpdateSelectedRace();
            await UpdateSelectedRace();
            await LoadRaces();
        }

        public void Dispose()
        {
            AppStateService.OnAppStateChanged += async () => await UpdateSelectedRace();
        }

        private async Task UpdateSelectedRace()
        {
            var state = await AppStateService.GetState();
            SelectedRace = state?.Race;
            if (SelectedRace is null && Races?.Any() != true)
            {
                await LoadRaces();
            }
            StateHasChanged();
        }

        private Task ClearSelectedRace()
        {
            return AppStateService.SetAppSelectedRace(null);
        }

        private async Task LoadRaces()
        {
            if (SelectedRace is null)
            {
                Races = (await RaceService.GetRaces()).OrderByDescending(x => x.Date).ToList();
            }
        }

        private Task SelectRace(RaceDto race)
        {
            return AppStateService.SetAppSelectedRace(race);
        }
    }
}