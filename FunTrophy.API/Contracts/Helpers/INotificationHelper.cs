namespace FunTrophy.API.Contracts.Helpers
{
    public interface INotificationHelper
    {
        /*
         * Update of model: Color (code), Team (name, number, color), Track (name)
         * Add of model: Color, Team, Track --not important
         * Add/Update of TrackTime or TimeAdjustment => priority
         */

        Task NotifyTrackTimeChanged(int trackId, int teamId);

        Task NotifyTimeAdjustementChanged(int teamId, int categoryId);
    }
}
