using FunTrophy.Shared.Model;
using FunTrophy.Web.Contracts.Helpers;
using FunTrophy.Web.Contracts.Services;
using Microsoft.AspNetCore.Components;

namespace FunTrophy.Web.Pages
{
    public partial class TeamResultsPage
    {
        #region Properties

        [Inject]
        public IAppStateService AppStateService { get; set; } = default!;

        [Inject]
        public ITeamService TeamService { get; set; } = default!;

        [Inject]
        public IResultService ResultService { get; set; } = default!;

        [Inject]
        public INotificationHubHelper NotificationHubHelper { get; set; } = default!;

        [Inject]
        public ITimeAdjustmentService TimeAdjustmentService { get; set; } = default!;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        private List<TeamDto>? _teams { get; set; }
        private List<TeamDto>? Teams { get; set; }

        private List<TeamResultDto>? Results { get; set; }

        private List<TimeAdjustmentDto>? TimeAdjustments { get; set; }

        private int? SelectedTeamId { get; set; }

        [Parameter]
        public string? SelectedTeamIdParam { get; set; }

        private TeamDto? SelectedTeam => _teams?.FirstOrDefault(x => x.Id == SelectedTeamId);

        private string? _searchText;
        private int? _selectedRaceId;

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
            var state = await AppStateService.GetState();
            _selectedRaceId = state?.Race?.Id;
            await LoadTeams();
            await NotificationHubHelper.ConnectToServer();
            NotificationHubHelper.TimeAdjustmentChanged += OnTimeAdjustmentChanged;
            NotificationHubHelper.TrackTimeChanged += OnTrackTimeChanged;
        }

        private async Task OnTrackTimeChanged(int trackId, int teamId)
        {
            if (SelectedTeamId == teamId)
            {
                await LoadResults();
                StateHasChanged();
            }
        }

        private async Task OnTimeAdjustmentChanged(int teamId)
        {
            if (SelectedTeamId == teamId)
            {
                await LoadTimeAdjustments();
                StateHasChanged();
            }
        }

        private async Task LoadTeams()
        {
            if (_selectedRaceId.HasValue)
            {
                _teams = (await TeamService.GetTeamsByRace(_selectedRaceId.Value))
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
            if (int.TryParse(SelectedTeamIdParam, out int selectedTeamId))
            {
                SelectedTeamId = selectedTeamId;
            }

            if (SelectedTeamId.HasValue)
            {
                Results = null;
                Results = (await ResultService.GetTeamResults(SelectedTeamId.Value))
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
            StateHasChanged();
        }

        private async Task OnSelectedTeamChanged(ChangeEventArgs args)
        {
            var teamId = int.Parse(args.Value!.ToString()!);
            OnSelectTeam(teamId);
        }

        private async void OnSelectTeam(int teamId)
        {
            SelectedTeamId = teamId;
            NavigationManager.NavigateTo("resultats/equipe/" + teamId);
            SelectedTeamIdParam = null;
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
                Teams = _teams?.FindAll(x => x.Name.ToLower().Contains(SearchText.ToLower()) || x.Number.ToString().StartsWith(SearchText));
            }
        }
    }
}