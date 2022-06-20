using FunTrophy.Web.Contracts.Services;
using Microsoft.AspNetCore.Components;

namespace FunTrophy.Web.Shared
{
    public partial class EditorNavMenu: IDisposable
    {
        [Inject]
        private IAppStateService AppStateService { get; set; } = default!;

        private bool IsCurrentRaceSelected { get; set; } = false;

        private Action OnEditorStateChanged => async () => await RefreshView();

        protected override async Task OnInitializedAsync()
        {
            AppStateService.OnEditorStateChanged += OnEditorStateChanged;
            await RefreshView();
        }

        public void Dispose()
        {
            AppStateService.OnEditorStateChanged -= OnEditorStateChanged;
        }

        private async Task RefreshView()
        {
            var currentRace = await AppStateService.GetEditorSelectedRace();
            IsCurrentRaceSelected = currentRace is not null;
            StateHasChanged();
        }
    }
}