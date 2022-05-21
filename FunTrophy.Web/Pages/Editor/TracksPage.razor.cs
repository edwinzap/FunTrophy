using FunTrophy.Shared.Model;
using FunTrophy.Web.Components;
using FunTrophy.Web.Contracts.Services;
using Microsoft.AspNetCore.Components;

namespace FunTrophy.Web.Pages.Editor
{
    public partial class TracksPage
    {
        [Inject]
        private AppState AppState { get; set; }

        [Inject]
        private ITrackService TrackService { get; set; } = default!;

        private ConfirmDialog DeleteDialog { get; set; } = default!;

        private EditDialog EditDialog { get; set; } = default!;

        private List<TrackDto> Tracks { get; set; } = new();

        private AddTrackDto addTrack = new();

        private UpdateTrackDto updateTrack = new();

        private int? updateTrackId;

        private int? DeleteTrackId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadTracks();
        }

        private async Task LoadTracks()
        {
            if (AppState.Race != null)
            {
                Tracks = await TrackService.GetTracks(AppState.Race.Id);
            }
        }

        private async Task AddTrack()
        {
            if (AppState.Race == null)
                return;

            addTrack.RaceId = AppState.Race.Id;

            await TrackService.Add(addTrack);
            await LoadTracks();
        }

        private void ConfirmEditTrack(TrackDto track)
        {
            updateTrack.Name = track.Name;
            updateTrackId = track.Id;
            EditDialog.Show();
        }

        private void ConfirmDeleteTrack(TrackDto track)
        {
            DeleteTrackId = track.Id;
            var message = $"Es-tu sûr de vouloir supprimer '{track.Name}?";
            DeleteDialog.Show(message);
        }

        private async Task RemoveTrack(bool confirm)
        {
            if (confirm && DeleteTrackId.HasValue)
            {
                await TrackService.Remove(DeleteTrackId.Value);
                await LoadTracks();
            }
        }

        private async Task UpdateTrack(bool confirm)
        {
            if (confirm && updateTrackId.HasValue)
            {
                await TrackService.Update(updateTrackId.Value, updateTrack);
                await LoadTracks();
            }
        }
    }
}