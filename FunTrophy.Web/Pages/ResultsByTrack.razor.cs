using FunTrophy.Shared.Model;
using FunTrophy.Web.Components;
using FunTrophy.Web.Contracts.Services;
using Microsoft.AspNetCore.Components;

namespace FunTrophy.Web.Pages
{
    public partial class ResultsByTrack
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

        private List<CheckBoxItem<TeamType>> TeamTypeFilter { get; set; } = new();

        #endregion

        protected override async Task OnInitializedAsync()
        {
            TeamTypeFilter = Enum.GetValues<TeamType>()
                .Select(value => new CheckBoxItem<TeamType>(true, value))
                .ToList();

            await LoadTracks();
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
            var filter = TeamTypeFilter.Where(x => x.IsChecked).Select(x => x.Value);
            Results = _results?.Where(x => filter.Contains(x.Team.Type)).ToList();
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

        private void OnTeamTypeFilterChanged(ChangeEventArgs args, CheckBoxItem<TeamType> item)
        {
            item.IsChecked = bool.Parse(args.Value!.ToString()!);
            FilterResults();
        }
    }
}
