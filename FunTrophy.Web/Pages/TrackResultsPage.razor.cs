using FunTrophy.Shared.Model;
using FunTrophy.Web.Components;
using FunTrophy.Web.Contracts.Helpers;
using FunTrophy.Web.Contracts.Services;
using Microsoft.AspNetCore.Components;
using System.Timers;
using static FunTrophy.Web.Models.Filters;

namespace FunTrophy.Web.Pages
{
    public partial class TrackResultsPage
    {
        #region Properties

        [Inject]
        public IAppStateService AppStateService { get; set; } = default!;

        [Inject]
        public ITrackService TrackService { get; set; } = default!;

        [Inject]
        public IResultService ResultService { get; set; } = default!;

        [Inject]
        public INotificationHubHelper NotificationHubHelper { get; set; } = default!;

        private List<TrackDto>? Tracks { get; set; }

        private List<TrackResultDto>? _results;
        private List<TrackResultDto>? Results { get; set; }

        private TrackDto? CurrentTrack { get; set; }

        private EditDialog SettingsDialog { get; set; } = default!;

        private TeamTypeFilter TeamTypeFilter { get; set; } = TeamTypeFilter.All;

        private bool _shouldAutoRotate = false;

        public bool ShouldAutoRotate
        {
            get => _shouldAutoRotate;
            set
            {
                _shouldAutoRotate = value;
                RefreshAutoRotate();
            }
        }

        private System.Timers.Timer _timer = new System.Timers.Timer();

        private int _rotationInterval = 10;
        private int? _selectedRaceId;

        public int RotationInterval
        {
            get => _rotationInterval;
            set
            {
                _rotationInterval = value;
                RefreshAutoRotate();
            }
        }

        #endregion Properties

        protected override async Task OnInitializedAsync()
        {
            var state = await AppStateService.GetState();
            _selectedRaceId = state?.Race?.Id;

            _timer.Elapsed += new ElapsedEventHandler(OnTimerTick);
            RefreshAutoRotate();
            await LoadTracks();
            await NotificationHubHelper.ConnectToServer();
            NotificationHubHelper.TrackTimeChanged += OnTrackTimeChanged;
        }

        private async Task OnTrackTimeChanged(int trackId, int teamId)
        {
            if (CurrentTrack?.Id == trackId)
            {
                await LoadResults();
                StateHasChanged();
            }
        }

        private void OnTimerTick(object? sender, ElapsedEventArgs e)
        {
            ChangeTrack(1);
        }

        private async Task LoadTracks()
        {
            if (_selectedRaceId.HasValue)
            {
                Tracks = await TrackService.GetTracks(_selectedRaceId.Value);
                if (Tracks.Any())
                {
                    CurrentTrack = Tracks.First();

                    await LoadResults();
                }
            }
        }

        private async Task LoadResults()
        {
            if (CurrentTrack is not null)
            {
                Results = null;
                _results = (await ResultService.GetTrackResults(CurrentTrack.Id))
                    .OrderByDescending(x => x.LapDuration.HasValue)
                    .ThenBy(x => x.LapDuration)
                    .ToList();

                FilterResults();
            }
        }

        private void FilterResults()
        {
            switch (TeamTypeFilter)
            {
                case TeamTypeFilter.All:
                    Results = _results;
                    break;

                case TeamTypeFilter.Fun:
                    Results = _results?.Where(x => x.Team.Type == TeamType.Fun).ToList();
                    break;

                case TeamTypeFilter.Warrior:
                    Results = _results?.Where(x => x.Team.Type == TeamType.Warrior).ToList();
                    break;

                default:
                    break;
            }
        }

        private async void ChangeTrack(int value)
        {
            if (Tracks?.Any() != true || CurrentTrack is null)
                return;

            var addValue = value > 0 ? 1 : -1;
            var currentTrack = Tracks.First(x => x.Id == CurrentTrack.Id);
            var currentIndex = Tracks.IndexOf(currentTrack);
            var newIndex = (currentIndex + addValue) % Tracks.Count;
            if (newIndex < 0)
                newIndex = Tracks.Count - 1;

            CurrentTrack = Tracks[newIndex];
            await LoadResults();
            StateHasChanged();
        }

        private async Task ShowSettingsDialog()
        {
            await SettingsDialog.ShowAsync();
        }

        private void OnTeamTypeFilterChanged(TeamTypeFilter filter)
        {
            TeamTypeFilter = filter;
            FilterResults();
        }

        private void RefreshAutoRotate()
        {
            _timer.Stop();
            if (ShouldAutoRotate)
            {
                _timer.Interval = RotationInterval * 1000;
                _timer.Start();
            }
        }
    }
}