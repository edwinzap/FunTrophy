using FunTrophy.Shared.Model;

namespace FunTrophy.Web.Contracts.Services
{
    public interface IAppStateService
    {
        Task<AppState?> GetState();

        Task SetAppSelectedRace(RaceDto? race);

        Task<RaceDto?> GetEditorSelectedRace();

        Task SetEditorSelectedRace(RaceDto? race);

        Task StartListeningToChange();
        Task StopListeningToChange();

        event Action? OnAppStateChanged;

        event Action? OnEditorStateChanged;
    }
}