using Blazored.LocalStorage;
using FunTrophy.Shared.Model;
using FunTrophy.Web.Contracts.Helpers;
using FunTrophy.Web.Contracts.Services;

namespace FunTrophy.Web.Services
{
    public class AppStateService : IAppStateService, IAsyncDisposable
    {
        private readonly ILocalStorageService _localStorage;
        private readonly IRaceService _raceService;
        private readonly INotificationHubHelper _notificationHubHelper;
        private static readonly string AppStateKey = "AppState";
        private static readonly string EditorSelectedRaceKey = "EditorSelectedRace";

        public event Action? OnAppStateChanged;

        public event Action? OnEditorStateChanged;

        public AppStateService(
            ILocalStorageService localStorage,
            IRaceService raceService,
            INotificationHubHelper notificationHubHelper)
        {
            _localStorage = localStorage;
            _raceService = raceService;
            _notificationHubHelper = notificationHubHelper;
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

        public async Task StartListeningToChange()
        {
            await _notificationHubHelper.ConnectToServer();
            _notificationHubHelper.RaceStatusChanged += OnRaceChanged;
        }

        public async Task StopListeningToChange()
        {
            await _notificationHubHelper.DisconnectFromServer();
            _notificationHubHelper.RaceStatusChanged -= OnRaceChanged;
        }

        private async Task OnRaceChanged(int raceId, bool isEnded)
        {
            var state = await GetState();
            var currentRaceId = state?.Race?.Id;
            if (currentRaceId == raceId)
            {
                var race = await _raceService.GetRace(raceId);
                await SetAppSelectedRace(race);
            }

            var editorState = await GetEditorSelectedRace();
            if (editorState?.Id == raceId)
            {
                var race = await _raceService.GetRace(raceId);
                await SetEditorSelectedRace(race);
            }
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
            if (race == null)
                await _localStorage.RemoveItemAsync(EditorSelectedRaceKey);
            else
                await _localStorage.SetItemAsync(EditorSelectedRaceKey, race);
            NotifyEditorStateChanged();
        }

        public async ValueTask DisposeAsync()
        {
            await StopListeningToChange();
            GC.SuppressFinalize(this);
        }
    }
}