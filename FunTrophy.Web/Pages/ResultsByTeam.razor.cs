using FunTrophy.Shared.Model;
using FunTrophy.Web.Contracts.Services;
using Microsoft.AspNetCore.Components;

namespace FunTrophy.Web.Pages
{
    public partial class ResultsByTeam
    {
        #region Properties

        [Inject]
        public AppState AppState { get; set; } = default!;

        [Inject]
        public ITeamService TeamService { get; set; } = default!;

        [Inject]
        public IColorService ColorService { get; set; } = default!;

        [Inject]
        public IResultService ResultService { get; set; } = default!;

        private List<TeamDto>? Teams { get; set; }

        private List<TeamResultDto>? Results { get; set; }
        private List<ColorDto>? Colors { get; set; }

        private int? SelectedTeamId { get; set; }

        #endregion Properties

        protected override async Task OnInitializedAsync()
        {
            await LoadTeams();
            await LoadColors();
        }

        private async Task LoadTeams()
        {
            if (AppState.Race is not null)
            {
                Teams = (await TeamService.GetTeamsByRace(AppState.Race.Id))
                    .OrderBy(x => x.Name)
                    .ToList();
                if (Teams.Any())
                {
                    SelectedTeamId = Teams.First().Id;
                    await LoadResults();
                }
            }
        }

        public async Task LoadColors()
        {
            if (AppState.Race is not null)
            {
                Colors = await ColorService.GetColors(AppState.Race.Id);
            }
        }

        private async Task LoadResults()
        {
            if (SelectedTeamId.HasValue)
            {
                Results = null;
                Results = (await ResultService.GetTeamResults(SelectedTeamId.Value))
                    .OrderByDescending(x => x.LapDuration.HasValue)
                    .ThenBy(x => x.LapDuration)
                    .ToList();
            }
        }

        private async Task OnSelectedTeamChanged(ChangeEventArgs args)
        {
            var trackId = int.Parse(args.Value!.ToString()!);
            SelectedTeamId = trackId;
            await LoadResults();
        }
    }
}