﻿using FunTrophy.Shared.Model;
using FunTrophy.Web.Components;
using FunTrophy.Web.Contracts.Services;
using Microsoft.AspNetCore.Components;

namespace FunTrophy.Web.Pages.Editor
{
    public partial class TracksPage
    {
        #region Properties

        [Inject]
        private IAppStateService AppStateService { get; set; } = default!;

        [Inject]
        private ITrackService TrackService { get; set; } = default!;

        [Inject]
        private IResultService ResultService { get; set; } = default!;

        private ConfirmDialog DeleteDialog { get; set; } = default!;

        private EditDialog EditDialog { get; set; } = default!;

        private List<TrackDto>? Tracks { get; set; }

        private AddTrackDto addTrack = new();

        private UpdateTrackDto updateTrack = new();

        private int? updateTrackId;
        private int? _selectedRaceId;

        private int? DeleteTrackId { get; set; }

        #endregion Properties

        protected override async Task OnInitializedAsync()
        {
            var selectedRace = await AppStateService.GetEditorSelectedRace();
            _selectedRaceId = selectedRace?.Id;
            await LoadTracks();
        }

        private async Task LoadTracks()
        {
            if (_selectedRaceId.HasValue)
            {
                Tracks = await TrackService.GetTracks(_selectedRaceId.Value);
            }
        }

        private async Task AddTrack()
        {
            if (!_selectedRaceId.HasValue)
                return;

            addTrack.RaceId = _selectedRaceId.Value;

            await TrackService.Add(addTrack);
            await LoadTracks();

            addTrack.Name = string.Empty;
            addTrack.Number = null;
        }

        private async Task EditTrack(TrackDto track)
        {
            updateTrack.Name = track.Name;
            updateTrack.Number = track.Number;
            updateTrackId = track.Id;
            await EditDialog.ShowAsync();
        }

        private void DeleteTrack(TrackDto track)
        {
            DeleteTrackId = track.Id;
            var message = $"Es-tu sûr de vouloir supprimer '{track.Name}'?";
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

        private async Task ConfirmEditTrack(bool confirm)
        {
            if (confirm && updateTrackId.HasValue)
            {
                await TrackService.Update(updateTrackId.Value, updateTrack);
                await LoadTracks();
            }
        }
    }
}