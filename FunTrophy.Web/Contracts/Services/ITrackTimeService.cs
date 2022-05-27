using FunTrophy.Shared.Model;

namespace FunTrophy.Web.Contracts.Services
{
    public interface ITrackTimeService
    {
        Task<List<TeamLapInfoDto>> GetLaps(int colorId);

        Task<TeamLapInfoDto> SaveLap(int teamId);
    }
}