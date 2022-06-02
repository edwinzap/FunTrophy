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
        public IResultService ResultService { get; set; } = default!;
        
        [Inject]
        public ITimeAdjustmentService TimeAdjustmentService { get; set; } = default!;

        private List<TeamDto>? _teams { get; set; }
        private List<TeamDto>? Teams { get; set; }

        private List<TeamResultDto>? Results { get; set; }

        private List<TimeAdjustmentDto>? TimeAdjustments { get; set; }

        private int? SelectedTeamId { get; set; }

        private TeamDto? SelectedTeam => _teams?.FirstOrDefault(x => x.Id == SelectedTeamId);

        private string? _searchText;

        private string? SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                FilterTeams();
            }
        }

        #endregion Properties

        protected override async Task OnInitializedAsync()
        {
            await LoadTeams();
        }

        private async Task LoadTeams()
        {
            if (AppState.Race is not null)
            {
                _teams = (await TeamService.GetTeamsByRace(AppState.Race.Id))
                    .OrderBy(x => x.Name)
                    .ToList();
                FilterTeams();

                if (_teams.Any())
                {
                    SelectedTeamId = _teams.First().Id;
                    await RefreshTeamResults();
                }
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

        private async Task LoadTimeAdjustments()
        {
            if (SelectedTeamId.HasValue)
            {
                TimeAdjustments = null;
                TimeAdjustments = (await TimeAdjustmentService.GetTimeAdjustments(SelectedTeamId.Value))
                    .OrderBy(x => x.CategoryName)
                    .ToList();
            }
        }

        private async Task RefreshTeamResults()
        {
            if (SelectedTeamId.HasValue)
            {
                await Task.WhenAll(LoadResults(), LoadTimeAdjustments());
            }
        }

        private async Task OnSelectedTeamChanged(ChangeEventArgs args)
        {
            var teamId = int.Parse(args.Value!.ToString()!);
            SelectedTeamId = teamId;
            await RefreshTeamResults();
        }

        private async Task OnSelectTeam(int teamId)
        {
            SelectedTeamId = teamId;
            await RefreshTeamResults();
        }

        private void FilterTeams()
        {
            if (string.IsNullOrEmpty(SearchText))
            {
                Teams = _teams;
            }
            else
            {
                Teams = _teams?.FindAll(x => x.Name.ToLower().Contains(SearchText.ToLower()));
            }
        }
    }
}