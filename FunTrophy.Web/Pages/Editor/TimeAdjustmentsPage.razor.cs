using FunTrophy.Shared.Model;
using FunTrophy.Web.Components;
using FunTrophy.Web.Contracts.Services;
using FunTrophy.Web.Helpers;
using Microsoft.AspNetCore.Components;

namespace FunTrophy.Web.Pages.Editor
{
    public partial class TimeAdjustmentsPage
    {
        #region Properties

        [Inject]
        private AppState AppState { get; set; } = default!;

        [Inject]
        private ITimeAdjustmentService TimeAdjustmentService { get; set; } = default!;

        [Inject]
        private ITeamService TeamService { get; set; } = default!;

        [Inject]
        private ITimeAdjustmentCategoryService CategoryService { get; set; } = default!;

        [Inject]
        private IColorService ColorService { get; set; } = default!;

        private ConfirmDialog DeleteDialog { get; set; } = default!;

        private List<TimeAdjustmentDto>? TimeAdjustments { get; set; }

        public List<ColorDto> Colors { get; set; }

        private List<TeamDto>? Teams { get; set; }

        private List<TimeAdjustmentCategoryDto>? Categories { get; set; }

        private AddTimeAdjustmentDto addTimeAdjustment = new();

        private int? SelectedCategoryId { get; set; }
        private int addMinutes = 0;
        private int addSeconds = 0;
        private bool isPositive = true;

        public int? CurrentColorId { get; set; }

        public int? CurrentTeamId { get; set; }

        private int? DeleteTimeAdjustmentId { get; set; }

        #endregion Properties

        protected override async Task OnInitializedAsync()
        {
            var task = await LoadColors().ContinueWith(_ => LoadTeams());
            await Task.WhenAll(task, LoadCategories());
        }

        private async Task LoadColors()
        {
            if (AppState.Race?.Id == null)
                return;

            Colors = await ColorService.GetColors(AppState.Race.Id);
            if (Colors.Any())
            {
                CurrentColorId = Colors.First().Id;
            }
        }

        private async Task LoadTeams()
        {
            if (!CurrentColorId.HasValue)
                return;

            Teams = null;
            Teams = await TeamService.GetTeams(CurrentColorId.Value);
            if (Teams.Any())
            {
                CurrentTeamId = Teams.First().Id;
               await LoadTimeAdjustments();
            }
        }

        private async Task LoadCategories()
        {
            if (AppState.Race?.Id == null)
                return;

            Categories = null;
            Categories = await CategoryService.GetCategories(AppState.Race.Id);
            SelectedCategoryId = Categories.FirstOrDefault()?.Id;
        }

        private async Task LoadTimeAdjustments()
        {
            if (CurrentTeamId.HasValue)
            {
                TimeAdjustments = null;
                TimeAdjustments = await TimeAdjustmentService.GetTimeAdjustments(CurrentTeamId.Value);
            }
        }

        private async Task OnCurrentTeamChanged(ChangeEventArgs args)
        {
            CurrentTeamId = int.Parse(args.Value!.ToString()!);
            await LoadTimeAdjustments();
        }

        private async Task AddTimeAdjustment()
        {
            var totalSeconds = addMinutes * 60 + addSeconds;
            if (!CurrentTeamId.HasValue || !SelectedCategoryId.HasValue || totalSeconds == 0)
                return;

            if (!isPositive)
                totalSeconds = -totalSeconds;

            addTimeAdjustment.TeamId = CurrentTeamId.Value;
            addTimeAdjustment.Seconds = totalSeconds;
            addTimeAdjustment.CategoryId = SelectedCategoryId.Value;

            await TimeAdjustmentService.Add(addTimeAdjustment);
            await LoadTimeAdjustments();

            addTimeAdjustment.Seconds = 0;
            addMinutes = 0;
            addSeconds = 0;
        }

        private void ConfirmDeleteTimeAdjustment(TimeAdjustmentDto timeAdjustment)
        {
            DeleteTimeAdjustmentId = timeAdjustment.Id;
            var message = $"Es-tu sûr de vouloir supprimer '{timeAdjustment.CategoryName}: {TimeSpan.FromSeconds(timeAdjustment.Seconds).ToMinutesAndSecondsString()}'?";
            DeleteDialog.Show(message);
        }

        private async Task RemoveTimeAdjustment(bool confirm)
        {
            if (confirm && DeleteTimeAdjustmentId.HasValue)
            {
                await TimeAdjustmentService.Remove(DeleteTimeAdjustmentId.Value);
                await LoadTimeAdjustments();
            }
        }

        private async Task OnCurrentColorChanged(int colorId)
        {
            CurrentColorId = colorId;
            await LoadTeams();
        }
    }
}