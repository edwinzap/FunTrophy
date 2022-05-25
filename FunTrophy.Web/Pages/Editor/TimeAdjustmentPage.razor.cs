using FunTrophy.Shared.Model;
using FunTrophy.Web.Components;
using FunTrophy.Web.Contracts.Services;
using FunTrophy.Web.Helpers;
using Microsoft.AspNetCore.Components;

namespace FunTrophy.Web.Pages.Editor
{
    public partial class TimeAdjustmentPage
    {
        [Inject]
        private AppState AppState { get; set; } = default!;

        [Inject]
        private ITimeAdjustmentService TimeAdjustmentService { get; set; } = default!;

        [Inject]
        private ITeamService TeamService { get; set; } = default!;

        [Inject]
        private ITimeAdjustmentCategoryService CategoryService { get; set; } = default!;

        private ConfirmDialog DeleteDialog { get; set; } = default!;

        private List<TimeAdjustmentDto> TimeAdjustments { get; set; } = new();

        private List<TeamDto> Teams { get; set; } = new();

        private List<TimeAdjustmentCategoryDto> Categories { get; set; } = new();

        private AddTimeAdjustmentDto addTimeAdjustment = new();

        private int? SelectedCategoryId { get; set; }
        private int addMinutes = 0;
        private int addSeconds = 0;
        private bool isPositive = true;

        public int? CurrentTeamId { get; set; }

        private int? DeleteTimeAdjustmentId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await Task.WhenAll(LoadTeams(), LoadCategories());
        }

        private async Task LoadTeams()
        {
            if (AppState.Race?.Id == null)
                return;

            Teams = await TeamService.GetTeams(AppState.Race.Id);
            if (!CurrentTeamId.HasValue && Teams.Any())
            {
                CurrentTeamId = Teams.First().Id;
            }
        }

        private async Task LoadCategories()
        {
            if (AppState.Race?.Id == null)
                return;

            Categories = await CategoryService.GetCategories(AppState.Race.Id);
            SelectedCategoryId = Categories.FirstOrDefault()?.Id;
        }

        private async Task LoadTimeAdjustments()
        {
            if (CurrentTeamId.HasValue)
            {
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
            addTimeAdjustment.Time = TimeSpan.FromSeconds(totalSeconds);
            addTimeAdjustment.CategoryId = SelectedCategoryId.Value;

            await TimeAdjustmentService.Add(addTimeAdjustment);
            await LoadTimeAdjustments();

            addTimeAdjustment.Time = new TimeSpan(0);
            addMinutes = 0;
            addSeconds = 0;
        }

        private void ConfirmDeleteTimeAdjustment(TimeAdjustmentDto timeAdjustment)
        {
            DeleteTimeAdjustmentId = timeAdjustment.Id;
            var message = $"Es-tu sûr de vouloir supprimer '{timeAdjustment.CategoryName}: {timeAdjustment.Time.ToMinutesAndSecondsString()}'?";
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
    }
}