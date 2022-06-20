namespace FunTrophy.Web.Contracts.Helpers
{
    public interface INotificationHubHelper
    {
        event Func<int, Task>? TimeAdjustmentChanged;

        event Func<int, int, Task>? TrackTimeChanged;

        event Func<int, bool, Task>? RaceStatusChanged;

        Task ConnectToServer();

        Task DisconnectFromServer();
    }
}