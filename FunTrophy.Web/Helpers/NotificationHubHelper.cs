using FunTrophy.Shared;
using FunTrophy.Web.Contracts.Helpers;
using Microsoft.AspNetCore.SignalR.Client;

namespace FunTrophy.Web.Helpers
{
    public class NotificationHubHelper : INotificationHubHelper
    {
        private HubConnection? _hubConnection;

        public event Func<int, Task>? TimeAdjustmentChanged;
        
        public event Func<int, int, Task>? TrackTimeChanged;

        public async Task ConnectToServer()
        {
            var url = "https://localhost:7183/notificationhub";
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(url)
                .Build();

            await _hubConnection.StartAsync();

            _hubConnection.Closed += async (e) =>
            {
                await _hubConnection.StartAsync();
            };

            _hubConnection.On<int, int>(HubConstants.TimeAdjustmentChanged, (teamId, categoryId) =>
            {
                TimeAdjustmentChanged?.Invoke(teamId);
            });

            _hubConnection.On<int, int>(HubConstants.TrackTimeChanged, (trackId, teamId) =>
            {
                TrackTimeChanged?.Invoke(trackId, teamId);
            });
        }
    }
}
