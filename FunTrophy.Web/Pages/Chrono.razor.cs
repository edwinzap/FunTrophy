using FunTrophy.Shared.Model;
using FunTrophy.Web.Contracts.Services;
using Microsoft.AspNetCore.Components;

namespace FunTrophy.Web.Pages
{
    public partial class Chrono
    {
        #region Properties
        [Inject]
        private AppState AppState { get; set; } = default!;

        [Inject]
        private IColorService ColorService { get; set; } = default!;

        private List<ColorDto>? Colors { get; set; }

        #endregion

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
    }
}
