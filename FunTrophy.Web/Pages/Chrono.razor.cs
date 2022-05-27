﻿using FunTrophy.Shared.Model;
using FunTrophy.Web.Contracts.Services;
using Microsoft.AspNetCore.Components;

namespace FunTrophy.Web.Pages
{
    public partial class Chrono
    {
        #region Properties
        [Inject]
        private AppState AppState { get; set; } = default!;

        [Inject]
        private IColorService ColorService { get; set; } = default!;

        [Inject]
        private ITrackTimeService TrackTimeService { get; set; } = default!;

        private List<ColorDto>? Colors { get; set; }

        private List<TeamLapInfoDto>? Laps { get; set; }

        private DateTime CurrentDateTime { get; set; } = DateTime.Now;

        public int? CurrentColorId { get; set; }

        private Timer? _timer;

        #endregion

        protected override async Task OnInitializedAsync()
        {
            await LoadColors();
            //await LoadLaps();
            StartTime();
        }

        private void StartTime()
        {
            _timer = new Timer(_ =>
            {
                if (Laps?.Any() == true)
                {
                    CurrentDateTime = DateTime.Now;
                }
                StateHasChanged();
            }, new AutoResetEvent(false), 1000, 1000);
        }

        public async Task LoadColors()
        {
            if (AppState.Race != null)
            {
                Colors = await ColorService.GetColors(AppState.Race.Id);
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
                Laps = null;
                Laps = await TrackTimeService.GetLaps(CurrentColorId.Value);
            }
        }

        private async Task OnCurrentColorChanged(int colorId)
        {
            CurrentColorId = colorId;
            await LoadLaps();
        }

        private async Task OnStopClicked(int teamId)
        {
            throw new NotImplementedException();
        }
    }
}
