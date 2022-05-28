using FunTrophy.Shared.Model;

namespace FunTrophy.API.Contracts.Services
{
    public interface ITrackTimeService
    {
        Task<List<TeamLapInfoDto>> GetTeamLaps(int colorId);

        Task<TeamLapInfoDto> GetTeamLap(int teamId);

        Task SaveTeamLap(int teamId);
    }
}
