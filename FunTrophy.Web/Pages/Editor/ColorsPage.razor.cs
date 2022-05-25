using FunTrophy.Shared.Model;
using FunTrophy.Web.Components;
using FunTrophy.Web.Contracts.Services;
using Microsoft.AspNetCore.Components;

namespace FunTrophy.Web.Pages.Editor
{
    public partial class ColorsPage
    {
        #region Properties

        [Inject]
        private AppState AppState { get; set; } = default!;

        [Inject]
        private IColorService ColorService { get; set; } = default!;

        private int? DeleteColorId { get; set; }

        private AddColorDto addColor = new() { Code = "#000" };

        private UpdateColorDto updateColor = new();
        private int? updateColorId;

        private List<ColorDto>? Colors { get; set; }

        private ConfirmDialog DeleteDialog { get; set; } = default!;

        private EditDialog EditDialog { get; set; } = default!;

        #endregion Properties

        protected override async Task OnInitializedAsync()
        {
            await LoadColors();
        }

        public async Task LoadColors()
        {
            if (AppState.Race != null)
            {
                Colors = await ColorService.GetColors(AppState.Race.Id);
            }
        }

        private async Task AddColor()
        {
            if (AppState.Race == null)
                return;

            addColor.RaceId = AppState.Race.Id;

            await ColorService.Add(addColor);
            await LoadColors();
        }

        private void ConfirmEditColor(ColorDto color)
        {
            updateColor.Code = color.Code;
            updateColorId = color.Id;
            EditDialog.Show();
        }

        private void ConfirmDeleteColor(ColorDto color)
        {
            DeleteColorId = color.Id;
            var message = $"Es-tu sûr de vouloir supprimer cette couleur?";
            DeleteDialog.Show(message);
        }

        private async Task RemoveColor(bool confirm)
        {
            if (confirm && DeleteColorId.HasValue)
            {
                await ColorService.Remove(DeleteColorId.Value);
                await LoadColors();
            }
        }

        private async Task UpdateColor(bool confirm)
        {
            if (confirm && updateColorId.HasValue)
            {
                await ColorService.Update(updateColorId.Value, updateColor);
                await LoadColors();
            }
        }
    }
}