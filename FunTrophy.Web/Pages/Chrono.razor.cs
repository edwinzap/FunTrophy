using FunTrophy.Shared.Model;
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

        private List<ColorDto>? Colors { get; set; }

        private List<TeamLapInfoDto>? Laps { get; set; }

        private DateTime CurrentDateTime { get; set; } = DateTime.Now;

        public int? CurrentColorId { get; set; }

        private Timer? timer;

        #endregion

        protected override async Task OnInitializedAsync()
        {
            await LoadColors();
            await LoadLaps();
            StartTime();
        }

        private void StartTime()
        {
            timer = new Timer(_ =>
            {
                if (Laps?.Any() == true)
                {
                    CurrentDateTime = DateTime.Now;
                }
                StateHasChanged();
            }, new AutoResetEvent(false), 1000, 1000); // fire every 2000 milliseconds
        }

        public async Task LoadColors()
        {
            if (AppState.Race != null)
            {
                Colors = await ColorService.GetColors(AppState.Race.Id);
            }
        }

        public async Task LoadLaps()
        {
            Laps = new List<TeamLapInfoDto>
            {
                new TeamLapInfoDto
                {
                    CurrentTrack = null,
                    CurrentTrackStartTime = null,
                    Team = new TeamDto
                    {
                        Id = 1,
                        Number = 1,
                        Name = "Mon équipe 1",
                    },
                    NextTrack = new TrackDto
                    {
                        Id = 1,
                        Name = "Le Parcours 1"
                    }
                },
                new TeamLapInfoDto
                {
                    CurrentTrack = new TrackDto
                    {
                        Id = 1,
                        Name = "Le Parcours 1"
                    },
                    CurrentTrackStartTime = DateTime.Now,
                    Team = new TeamDto
                    {
                        Id = 2,
                        Number = 2,
                        Name = "Mon équipe 2",
                    },
                    NextTrack = new TrackDto
                    {
                        Id = 1,
                        Name = "Le Parcours 2"
                    }
                }
            };
            await Task.CompletedTask;
        }

        private async Task OnCurrentColorChanged(int colorId)
        {
            CurrentColorId = colorId;
            await LoadLaps();
        }

        private async Task OnStopClicked(int teamId)
        {

        }
    }
}
