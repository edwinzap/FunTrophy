using FunTrophy.Shared.Model;
using FunTrophy.Web.Contracts.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace FunTrophy.Web.Pages.Editor
{
    public partial class NewTimeAdjustmentsPage
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
            await LoadTimeAdjustements();
        }

        private async Task LoadTimeAdjustements()
        {
            if (!SelectedCategoryId.HasValue)
                return;
            TimeAdjustments = null;
            TimeAdjustments = await TimeAdjustmentService.GetTimeAdjustmentForCategory(SelectedCategoryId.Value);
        }

        private async Task OnSelectedCategoryChanged(ChangeEventArgs args)
        {
            SelectedCategoryId = int.Parse(args.Value!.ToString()!);
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

        //private async Task AddTimeAdjustment()
        //{
        //    var totalSeconds = addMinutes * 60 + addSeconds;
        //    if (!CurrentTeamId.HasValue || !SelectedCategoryId.HasValue || totalSeconds == 0)
        //        return;

        //    if (!isPositive)
        //        totalSeconds = -totalSeconds;

        //    addTimeAdjustment.TeamId = CurrentTeamId.Value;
        //    addTimeAdjustment.Seconds = totalSeconds;
        //    addTimeAdjustment.CategoryId = SelectedCategoryId.Value;

        //    await TimeAdjustmentService.Add(addTimeAdjustment);
        //    await LoadTimeAdjustments();

        //    addTimeAdjustment.Seconds = 0;
        //    addMinutes = 0;
        //    addSeconds = 0;
        //}

        //private void ConfirmDeleteTimeAdjustment(TimeAdjustmentDto timeAdjustment)
        //{
        //    DeleteTimeAdjustmentId = timeAdjustment.Id;
        //    var message = $"Es-tu sûr de vouloir supprimer '{timeAdjustment.CategoryName}: {TimeSpan.FromSeconds(timeAdjustment.Seconds).ToTimeString()}'?";
        //    DeleteDialog.Show(message);
        //}

        //private async Task RemoveTimeAdjustment(bool confirm)
        //{
        //    if (confirm && DeleteTimeAdjustmentId.HasValue)
        //    {
        //        await TimeAdjustmentService.Remove(DeleteTimeAdjustmentId.Value);
        //        await LoadTimeAdjustments();
        //    }
        //}

        //private async Task OnCurrentColorChanged(int colorId)
        //{
        //    CurrentColorId = colorId;
        //    await LoadTeams();
        //}
    }
}