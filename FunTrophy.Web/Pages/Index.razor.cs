using FunTrophy.Shared.Model;
using FunTrophy.Web.Contracts.Services;
using Microsoft.AspNetCore.Components;

namespace FunTrophy.Web.Pages
{
    public partial class Index: IAsyncDisposable
    {
        #region Properties

        [Inject]
        private IRaceService RaceService { get; set; } = default!;

        [Inject]
        private IAppStateService AppStateService { get; set; } = default!;

        private List<RaceDto>? Races { get; set; }

        private RaceDto? SelectedRace { get; set; }

        private Action OnAppStateChanged => async () => await UpdateSelectedRace();

        #endregion Properties

        protected override async Task OnInitializedAsync()
        {
            AppStateService.OnAppStateChanged += OnAppStateChanged;
            await AppStateService.StartListeningToChange();
            await UpdateSelectedRace();
            await LoadRaces();
        }

        private async Task UpdateSelectedRace()
        {
            var state = await AppStateService.GetState();
            SelectedRace = state?.Race;
            await LoadRaces();
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

        public async ValueTask DisposeAsync()
        {
            AppStateService.OnAppStateChanged -= OnAppStateChanged;
            await AppStateService.StopListeningToChange();
            GC.SuppressFinalize(this);
        }
    }
}