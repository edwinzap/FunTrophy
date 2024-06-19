using FunTrophy.Shared.Model;

namespace FunTrophy.API.Contracts.Services
{
    public interface ITrackTimeService
    {
        Task<List<TeamLapInfoDto>> GetTeamLaps(int colorId);

        Task SaveTeamLap(int userId, int teamId);

        Task Undo(int userId);
    }
}
