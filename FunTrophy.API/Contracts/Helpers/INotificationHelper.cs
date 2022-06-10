namespace FunTrophy.API.Contracts.Helpers
{
    public interface INotificationHelper
    {
        Task NotifyTrackTimeChanged(int trackId, int teamId);

        Task NotifyTimeAdjustementChanged(int teamId, int categoryId);

        Task NotifyRaceStatusChanged(int raceId, bool isEnded);
    }
}