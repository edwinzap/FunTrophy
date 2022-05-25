using FunTrophy.Shared.Model;
using FunTrophy.Web.Components;
using FunTrophy.Web.Contracts.Services;
using Microsoft.AspNetCore.Components;

namespace FunTrophy.Web.Pages.Editor
{
    public partial class TimeAdjustmentCategoriesPage
    {
        [Inject]
        private AppState AppState { get; set; } = default!;

        [Inject]
        private ITimeAdjustmentCategoryService TimeAdjustmentCategoryService { get; set; } = default!;

        private ConfirmDialog DeleteDialog { get; set; } = default!;

        private EditDialog EditDialog { get; set; } = default!;

        private List<TimeAdjustmentCategoryDto> Categories { get; set; } = new();

        private AddTimeAdjustmentCategoryDto addCategory = new();

        private UpdateTimeAdjustmentCategoryDto updateCategory = new();

        private int? updateCategoryId;

        private int? DeleteCategoryId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadCategories();
        }

        private async Task LoadCategories()
        {
            if (AppState.Race != null)
            {
                Categories = await TimeAdjustmentCategoryService.GetCategories(AppState.Race.Id);
            }
        }

        private async Task AddTrack()
        {
            if (AppState.Race == null)
                return;

            addCategory.RaceId = AppState.Race.Id;

            await TimeAdjustmentCategoryService.Add(addCategory);
            await LoadCategories();

            addCategory.Name = string.Empty;
        }

        private void ConfirmEditTrack(TimeAdjustmentCategoryDto category)
        {
            updateCategory.Name = category.Name;
            updateCategoryId = category.Id;
            EditDialog.Show();
        }

        private void ConfirmDeleteTrack(TimeAdjustmentCategoryDto category)
        {
            DeleteCategoryId = category.Id;
            var message = $"Es-tu sûr de vouloir supprimer '{category.Name}?";
            DeleteDialog.Show(message);
        }

        private async Task RemoveTrack(bool confirm)
        {
            if (confirm && DeleteCategoryId.HasValue)
            {
                await TimeAdjustmentCategoryService.Remove(DeleteCategoryId.Value);
                await LoadCategories();
            }
        }

        private async Task UpdateTrack(bool confirm)
        {
            if (confirm && updateCategoryId.HasValue)
            {
                await TimeAdjustmentCategoryService.Update(updateCategoryId.Value, updateCategory);
                await LoadCategories();
            }
        }
    }
}