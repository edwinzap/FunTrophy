using FunTrophy.Shared.Model;
using FunTrophy.Web.Contracts.Services;
using Microsoft.AspNetCore.Components;
using static FunTrophy.Web.Models.Filters;

namespace FunTrophy.Web.Pages
{
    public partial class FinalResultsPage
    {
        #region Properties

        [Inject]
        private AppState AppState { get; set; } = default!;

        [Inject]
        private IResultService ResultService { get; set; } = default!;

        private List<FinalResultDto>? _results;
        private List<FinalResultDto>? Results { get; set; }

        private TeamTypeFilter TeamTypeFilter { get; set; }

        #endregion Properties

        protected override async Task OnInitializedAsync()
        {
            await LoadResults();
        }

        private async Task LoadResults()
        {
            if (AppState.Race?.Id is not null)
            {
                _results = await ResultService.GetFinalResults(AppState.Race.Id);
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