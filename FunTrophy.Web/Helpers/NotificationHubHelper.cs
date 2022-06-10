using FunTrophy.Shared;
using FunTrophy.Web.Contracts.Helpers;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Options;

namespace FunTrophy.Web.Helpers
{
    public class NotificationHubHelper : INotificationHubHelper
    {
        private HubConnection? _hubConnection;
        private readonly AppSettings _appSettings;

        public event Func<int, bool, Task>? RaceStatusChanged;

        public event Func<int, Task>? TimeAdjustmentChanged;
        
        public event Func<int, int, Task>? TrackTimeChanged;

        public NotificationHubHelper(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public async Task ConnectToServer()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(_appSettings.HubUrl)
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

            _hubConnection.On<int, bool>(HubConstants.RaceStatusChanged, (raceId, isEnded) =>
            {
                RaceStatusChanged?.Invoke(raceId, isEnded);
            });
        }
    }
}
