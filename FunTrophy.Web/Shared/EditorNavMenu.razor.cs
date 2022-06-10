using FunTrophy.Web.Contracts.Services;
using Microsoft.AspNetCore.Components;

namespace FunTrophy.Web.Shared
{
    public partial class EditorNavMenu
    {
        [Inject]
        private IAppStateService AppStateService { get; set; } = default!;

        private bool IsCurrentRaceSelected { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            AppStateService.OnEditorStateChanged += async () => await RefreshView();
            await RefreshView();
        }

        public void Dispose()
        {
            AppStateService.OnEditorStateChanged -= async () => await RefreshView();
        }

        private async Task RefreshView()
        {
            var currentRace = await AppStateService.GetEditorSelectedRace();
            IsCurrentRaceSelected = currentRace is not null;
            StateHasChanged();
        }
    }
}