using FunTrophy.Shared.Model;
using FunTrophy.Web.Contracts.Helpers;
using FunTrophy.Web.Contracts.Services;
using Microsoft.AspNetCore.Components;
using static FunTrophy.Web.Models.Filters;

namespace FunTrophy.Web.Pages
{
    public partial class FinalResultsPage
    {
        #region Properties

        [Inject]
        private IAppStateService AppStateService { get; set; } = default!;

        [Inject]
        private IResultService ResultService { get; set; } = default!;

        [Inject]
        public INotificationHubHelper NotificationHubHelper { get; set; } = default!;

        private List<FinalResultDto>? _results;
        private int? _selectedRaceId;

        private List<FinalResultDto>? Results { get; set; }

        private TeamTypeFilter TeamTypeFilter { get; set; }

        #endregion Properties

        protected override async Task OnInitializedAsync()
        {
            var selectedRace = await AppStateService.GetEditorSelectedRace();
            _selectedRaceId = selectedRace?.Id;
            await LoadResults();
            await NotificationHubHelper.ConnectToServer();
            NotificationHubHelper.TimeAdjustmentChanged += OnTimeAdjustmentChanged;
            NotificationHubHelper.TrackTimeChanged += OnTrackTimeChanged;
        }

        private async Task OnTrackTimeChanged(int trackId, int teamId)
        {
            await LoadResults();
            StateHasChanged();
        }

        private async Task OnTimeAdjustmentChanged(int teamId)
        {
            await LoadResults();
            StateHasChanged();
        }

        private async Task LoadResults()
        {
            if (_selectedRaceId.HasValue)
            {
                _results = await ResultService.GetFinalResults(_selectedRaceId.Value);
                FilterResults();
            }
        }

        private void OnTeamTypeFilterChanged(TeamTypeFilter filter)
        {
            TeamTypeFilter = filter;
            FilterResults();
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
    }
}