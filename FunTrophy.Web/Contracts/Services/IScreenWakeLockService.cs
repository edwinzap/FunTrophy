using FunTrophy.Web.Models;

namespace FunTrophy.Web.Contracts.Services
{
    public interface IScreenWakeLockService
    {
        // Requests a screen wake lock and returns a sentinel object
        Task<WakeLockSentinel> RequestWakeLockAsync();

        // Releases a screen wake lock given a sentinel object
        Task ReleaseWakeLockAsync(WakeLockSentinel sentinel);

        // Checks if the browser supports the screen wake lock API
        Task<bool> IsSupportedAsync();
    }

}
