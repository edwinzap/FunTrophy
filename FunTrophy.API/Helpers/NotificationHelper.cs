using FunTrophy.API.Contracts.Helpers;
using FunTrophy.API.Services;
using FunTrophy.Shared;
using Microsoft.AspNetCore.SignalR;

namespace FunTrophy.API.Helpers
{
    public class NotificationHelper : INotificationHelper
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationHelper(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public Task NotifyTimeAdjustementChanged(int teamId, int categoryId)
        {
            return _hubContext.Clients.All.SendAsync(HubConstants.TimeAdjustmentChanged, teamId, categoryId);
        }

        public Task NotifyTrackTimeChanged(int trackId, int teamId)
        {
            return _hubContext.Clients.All.SendAsync(HubConstants.TrackTimeChanged, trackId, teamId);
        }

        public Task NotifyRaceStatusChanged(int raceId, bool isEnded)
        {
            return _hubContext.Clients.All.SendAsync(HubConstants.RaceStatusChanged, raceId, isEnded);
        }
    }
}
