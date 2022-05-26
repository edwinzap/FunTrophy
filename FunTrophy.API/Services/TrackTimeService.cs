using FunTrophy.API.Contracts.Services;
using FunTrophy.Infrastructure.Contracts.Repositories;
using FunTrophy.Shared.Model;

namespace FunTrophy.API.Services
{
    public class TrackTimeService : ServiceBase, ITrackTimeService
    {
        public TrackTimeService(
            ITrackRepository trackRepository, 
            ITrackTimeRepository trackTimeRepository)
        {

        }

        public Task<List<TeamLapInfoDto>> GetTeamLaps(int colorId)
        {
            throw new NotImplementedException();
        }

        public Task<TeamLapInfoDto> SaveTeamLap(int teamId)
        {
            throw new NotImplementedException();
        }
    }
}
