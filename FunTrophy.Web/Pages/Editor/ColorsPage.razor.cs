using FunTrophy.Shared.Model;
using FunTrophy.Web.Contracts.Services;
using Microsoft.AspNetCore.Components;

namespace FunTrophy.Web.Pages.Editor
{
    public partial class ColorsPage
    {
        [Inject]
        private AppState AppState { get; set; }

        [Inject]
        private IColorService ColorService { get; set; } = default!;

        private string ColorPickerValue { get; set; }

        public List<ColorDto> Colors { get; set; } = new();

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

            var color = new AddColorDto()
            {
                Code = ColorPickerValue,
                RaceId = AppState.Race.Id
            };

            await ColorService.Add(color);
            await LoadColors();
        }

        private async Task RemoveColor(int colorId)
        {
            await ColorService.Remove(colorId);
            await LoadColors();
        }
    }
}