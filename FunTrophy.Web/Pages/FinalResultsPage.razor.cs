using FunTrophy.Shared.Model;
using FunTrophy.Web.Contracts.Services;
using Microsoft.AspNetCore.Components;

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

        private List<CheckBoxItem<TeamType>> TeamTypeFilter { get; set; } = new();
        #endregion Properties

        protected override async Task OnInitializedAsync()
        {
            TeamTypeFilter = Enum.GetValues<TeamType>()
                .Select(value => new CheckBoxItem<TeamType>(true, value, value.ToString()))
                .ToList();
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

        private void FilterResults()
        {
            var filter = TeamTypeFilter.Where(x => x.IsChecked).Select(x => x.Value);
            Results = _results?.Where(x => filter.Contains(x.Team.Type)).ToList();
        }
    }
}