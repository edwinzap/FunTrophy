using FunTrophy.Infrastructure.Model;

namespace FunTrophy.Infrastructure.Contracts.Repositories
{
    public interface ITrackTimeRepository : IRepositoryBase<TrackTime>
    {
        Task<List<TrackTime>> GetOfColor(int colorId);

        Task<List<TrackTime>> GetOfTeam(int teamId);

        Task<List<TrackTime>> GetOfTrack(int trackId);

        Task<List<TrackTime>> GetOfRace(int raceId);

        Task RemoveAllOfRace(int raceId);
    }
}