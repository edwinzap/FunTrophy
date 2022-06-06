
namespace FunTrophy.Web.Contracts.Helpers
{
    public interface INotificationHubHelper
    {
        event Func<int, Task>? TimeAdjustmentChanged;

        Task ConnectToServer();
    }
}