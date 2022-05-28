using FunTrophy.Shared.Model;

namespace FunTrophy.Web.Contracts.Services
{
    public interface ITrackTimeService
    {
        Task<List<TeamLapInfoDto>> GetLaps(int colorId);

        Task SaveLap(int teamId);
    }
}