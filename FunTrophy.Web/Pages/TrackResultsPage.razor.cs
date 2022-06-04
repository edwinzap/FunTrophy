﻿using FunTrophy.Shared.Model;
using FunTrophy.Web.Components;
using FunTrophy.Web.Contracts.Services;
using FunTrophy.Web.Models;
using Microsoft.AspNetCore.Components;
using System.Timers;
using static FunTrophy.Web.Models.Filters;

namespace FunTrophy.Web.Pages
{
    public partial class TrackResultsPage
    {
        #region Properties

        [Inject]
        public AppState AppState { get; set; } = default!;

        [Inject]
        public ITrackService TrackService { get; set; } = default!;

        [Inject]
        public IResultService ResultService { get; set; } = default!;

        private List<TrackDto>? Tracks { get; set; }

        private List<TrackResultDto>? _results;
        private List<TrackResultDto>? Results { get; set; }

        private int? SelectedTrackId { get; set; }

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
            _timer.Elapsed += new ElapsedEventHandler(OnTimerTick);

            RefreshAutoRotate();
            await LoadTracks();
        }

        private void OnTimerTick(object? sender, ElapsedEventArgs e)
        {
            ChangeTrack(1);
        }

        private async Task LoadTracks()
        {
            if (AppState.Race != null)
            {
                Tracks = await TrackService.GetTracks(AppState.Race.Id);
                if (Tracks.Any())
                {
                    SelectedTrackId = Tracks.First().Id;
                    await LoadResults();
                }
            }
        }

        private async Task LoadResults()
        {
            if (SelectedTrackId.HasValue)
            {
                Results = null;
                _results = (await ResultService.GetTrackResults(SelectedTrackId.Value))
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

                case TeamTypeFilter.Family:
                    Results = _results?.Where(x => x.Team.Type == TeamType.Family).ToList();
                    break;

                case TeamTypeFilter.Warrior:
                    Results = _results?.Where(x => x.Team.Type == TeamType.Warrior).ToList();
                    break;

                default:
                    break;
            }
        }

        private async Task OnSelectedTrackChanged(ChangeEventArgs args)
        {
            var trackId = int.Parse(args.Value!.ToString()!);
            SelectedTrackId = trackId;
            await LoadResults();
        }

        private async void ChangeTrack(int value)
        {
            if (Tracks?.Any() != true || !SelectedTrackId.HasValue)
                return;

            var addValue = value > 0 ? 1 : -1;
            var currentTrack = Tracks.First(x => x.Id == SelectedTrackId);
            var currentIndex = Tracks.IndexOf(currentTrack);
            var newIndex = (currentIndex + addValue) % Tracks.Count;
            if (newIndex < 0)
                newIndex = Tracks.Count - 1;

            SelectedTrackId = Tracks[newIndex].Id;
            await LoadResults();
            StateHasChanged();
        }

        private void ShowSettingsDialog()
        {
            SettingsDialog.Show();
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