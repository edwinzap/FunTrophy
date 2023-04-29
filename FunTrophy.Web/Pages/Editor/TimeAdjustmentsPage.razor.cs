using FunTrophy.Shared.Model;
using FunTrophy.Web.Contracts.Services;
using Microsoft.AspNetCore.Components;

namespace FunTrophy.Web.Pages.Editor
{
    public partial class TimeAdjustmentsPage
    {
        #region Properties

        [Inject]
        private IAppStateService AppStateService { get; set; } = default!;

        [Inject]
        private ITimeAdjustmentService TimeAdjustmentService { get; set; } = default!;

        [Inject]
        private ITimeAdjustmentCategoryService CategoryService { get; set; } = default!;

        private List<TimeAdjustmentCategoryDto>? Categories { get; set; }

        private List<TeamTimeAdjustmentDto>? TimeAdjustments { get; set; }

        private int? _selectedRaceId;

        private int? SelectedCategoryId { get; set; }

        private string? SelectedCategoryDescription { get; set; }

        public int? CurrentColorId { get; set; }

        #endregion Properties

        protected override async Task OnInitializedAsync()
        {
            var selectedRace = await AppStateService.GetEditorSelectedRace();
            _selectedRaceId = selectedRace?.Id;
            await LoadCategories();
        }

        private async Task LoadCategories()
        {
            if (!_selectedRaceId.HasValue)
                return;

            Categories = null;
            Categories = await CategoryService.GetCategories(_selectedRaceId.Value);
            SelectedCategoryId = Categories.FirstOrDefault()?.Id;
            SelectedCategoryDescription = Categories?.FirstOrDefault(x => x.Id == SelectedCategoryId)?.Description;
            await LoadTimeAdjustements();
        }

        private async Task LoadTimeAdjustements()
        {
            if (!SelectedCategoryId.HasValue)
                return;
            TimeAdjustments = null;
            TimeAdjustments = (await TimeAdjustmentService.GetTimeAdjustmentForCategory(SelectedCategoryId.Value))
                .OrderBy(x => x.Team.Color.Id)
                .ThenBy(x => x.Team.Number)
                .ToList();
        }

        private async Task OnSelectedCategoryChanged(ChangeEventArgs args)
        {
            SelectedCategoryId = int.Parse(args.Value!.ToString()!);
            SelectedCategoryDescription = Categories?.FirstOrDefault(x => x.Id == SelectedCategoryId)?.Description;
            await LoadTimeAdjustements();
        }

        private async Task OnValidateTimeAdjustment(TeamTimeAdjustmentDto bonus)
        {
            if (!SelectedCategoryId.HasValue)
                return;

            var updateTimeAdjustment = new AddTimeAdjustmentDto
            {
                CategoryId = SelectedCategoryId.Value,
                TeamId = bonus.Team.Id,
                Seconds = bonus.Seconds ?? 0
            };

            await TimeAdjustmentService.Update(updateTimeAdjustment);
        }
    }
}