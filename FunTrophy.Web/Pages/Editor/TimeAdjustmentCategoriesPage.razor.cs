using FunTrophy.Shared.Model;
using FunTrophy.Web.Components;
using FunTrophy.Web.Contracts.Services;
using Microsoft.AspNetCore.Components;

namespace FunTrophy.Web.Pages.Editor
{
    public partial class TimeAdjustmentCategoriesPage
    {
        #region Properties

        [Inject]
        private IAppStateService AppStateService { get; set; } = default!;

        [Inject]
        private ITimeAdjustmentCategoryService TimeAdjustmentCategoryService { get; set; } = default!;

        private ConfirmDialog DeleteDialog { get; set; } = default!;

        private EditDialog EditDialog { get; set; } = default!;

        private List<TimeAdjustmentCategoryDto>? Categories { get; set; }

        private AddTimeAdjustmentCategoryDto addCategory = new();

        private UpdateTimeAdjustmentCategoryDto updateCategory = new();

        private int? updateCategoryId;
        private int? _selectedRaceId;

        private int? DeleteCategoryId { get; set; }

        #endregion Properties

        protected override async Task OnInitializedAsync()
        {
            var selectedRace = await AppStateService.GetEditorSelectedRace();
            _selectedRaceId = selectedRace?.Id;
            await LoadCategories();
        }

        private async Task LoadCategories()
        {
            if (_selectedRaceId.HasValue)
            {
                Categories = await TimeAdjustmentCategoryService.GetCategories(_selectedRaceId.Value);
            }
        }

        private async Task AddTrack()
        {
            if (!_selectedRaceId.HasValue)
                return;

            addCategory.RaceId = _selectedRaceId.Value;

            await TimeAdjustmentCategoryService.Add(addCategory);
            await LoadCategories();

            addCategory.Name = string.Empty;
        }

        private async Task EditTrack(TimeAdjustmentCategoryDto category)
        {
            updateCategory.Name = category.Name;
            updateCategoryId = category.Id;
            updateCategory.Description = category.Description;
            await EditDialog.ShowAsync();
        }
        private async Task ConfirmEditTrack(bool confirm)
        {
            if (confirm && updateCategoryId.HasValue)
            {
                await TimeAdjustmentCategoryService.Update(updateCategoryId.Value, updateCategory);
                await LoadCategories();
            }
        }

        private void DeleteTrack(TimeAdjustmentCategoryDto category)
        {
            DeleteCategoryId = category.Id;
            var message = $"Es-tu sûr de vouloir supprimer '{category.Name}'?";
            DeleteDialog.Show(message);
        }

        private async Task ConfirmDeleteTrack(bool confirm)
        {
            if (confirm && DeleteCategoryId.HasValue)
            {
                await TimeAdjustmentCategoryService.Remove(DeleteCategoryId.Value);
                await LoadCategories();
            }
        }

    }
}