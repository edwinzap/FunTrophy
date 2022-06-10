using Blazored.LocalStorage;
using FunTrophy.Shared.Model;
using FunTrophy.Web.Contracts.Services;

namespace FunTrophy.Web.Services
{
    public class AppStateService : IAppStateService
    {
        private readonly ILocalStorageService _localStorage;
        private static readonly string AppStateKey = "AppState";
        private static readonly string EditorSelectedRaceKey = "EditorSelectedRace";

        public event Action? OnAppStateChanged;

        public event Action? OnEditorStateChanged;

        public AppStateService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task<AppState?> GetState()
        {
            var appState = await _localStorage.GetItemAsync<AppState>(AppStateKey);
            return appState;
        }

        public async Task SetAppSelectedRace(RaceDto? race)
        {
            var appState = await GetState() ?? new AppState();
            appState.Race = race;
            await SetState(appState);
        }

        public async Task<RaceDto?> GetEditorSelectedRace()
        {
            var race = await _localStorage.GetItemAsync<RaceDto>(EditorSelectedRaceKey);
            return race;
        }

        public async Task SetEditorSelectedRace(RaceDto? race)
        {
            await _localStorage.SetItemAsync(EditorSelectedRaceKey, race);
            NotifyEditorStateChanged();
        }

        #region Private methods

        private async Task SetState(AppState state)
        {
            await _localStorage.SetItemAsync(AppStateKey, state);
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnAppStateChanged?.Invoke();

        private void NotifyEditorStateChanged() => OnEditorStateChanged?.Invoke();

        #endregion Private methods
    }
}