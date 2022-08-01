using FunTrophy.Shared.Model;
using FunTrophy.Web.Contracts.Helpers;
using FunTrophy.Web.Contracts.Services;
using Microsoft.AspNetCore.Components;

namespace FunTrophy.Web.Pages
{
    public partial class Chrono
    {
        #region Properties

        [Inject]
        private IAppStateService AppStateService { get; set; } = default!;

        [Inject]
        private IColorService ColorService { get; set; } = default!;

        [Inject]
        private ITrackTimeService TrackTimeService { get; set; } = default!;

        [Inject]
        public INotificationHubHelper NotificationHubHelper { get; set; } = default!;

        private List<ColorDto>? Colors { get; set; }

        private List<TeamLapInfoDto>? Laps { get; set; }

        private DateTime CurrentDateTime { get; set; } = DateTime.UtcNow;

        public int? CurrentColorId { get; set; }

        private Timer? _timer;
        private TimeSpan serverTimeDiff = TimeSpan.Zero;
        private int? _selectedRaceId;

        #endregion Properties

        protected override async Task OnInitializedAsync()
        {
            var state = await AppStateService.GetState();
            _selectedRaceId = state?.Race?.Id;
            await LoadColors();
            StartTime();
            await NotificationHubHelper.ConnectToServer();
            NotificationHubHelper.TrackTimeChanged += OnTrackTimeChanged;
        }

        private async Task OnTrackTimeChanged(int trackId, int teamId)
        {
            if (Laps?.Any(x => x.Team.Id == teamId) == true)
            {
                await LoadLaps();
            }
        }

        private void StartTime()
        {
            _timer = new Timer(_ =>
            {
                if (Laps?.Any() == true)
                {
                    CurrentDateTime = DateTime.UtcNow - serverTimeDiff;
                    StateHasChanged();
                }
            }, new AutoResetEvent(false), 1000, 1000);
        }

        private async Task LoadColors()
        {
            if (_selectedRaceId.HasValue)
            {
                Colors = await ColorService.GetColors(_selectedRaceId.Value);
                if (Colors.Any())
                {
                    CurrentColorId = Colors.First().Id;
                    await LoadLaps();
                }
            }
        }

        public async Task LoadLaps()
        {
            if (CurrentColorId.HasValue)
            {
                Laps = await TrackTimeService.GetLaps(CurrentColorId.Value);
                var serverTime = Laps.FirstOrDefault()?.ServerTime;
                if (serverTime.HasValue)
                {
                    var currentDate = DateTime.UtcNow;
                    serverTimeDiff = currentDate.Subtract(serverTime.Value);
                }
            }
        }

        private async Task OnCurrentColorChanged(int colorId)
        {
            CurrentColorId = colorId;
            await LoadLaps();
        }

        private async Task OnStopClicked(int teamId)
        {
            await TrackTimeService.SaveLap(teamId);
            await LoadLaps();
        }
    }
}