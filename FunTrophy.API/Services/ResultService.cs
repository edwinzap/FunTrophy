using FunTrophy.API.Contracts.Mappers;
using FunTrophy.API.Contracts.Services;
using FunTrophy.Infrastructure.Contracts.Repositories;
using FunTrophy.Shared.Model;

namespace FunTrophy.API.Services
{
    public class ResultService : ServiceBase, IResultService
    {
        private readonly ITrackTimeRepository _trackTimeRepository;
        private readonly IResultMapper _mapper;

        public ResultService(ITrackTimeRepository trackTimeRepository, IResultMapper mapper)
        {
            _trackTimeRepository = trackTimeRepository;
            _mapper = mapper;
        }

        public async Task<List<TeamResultDto>> GetTeamResults(int teamId)
        {
            var times = (await _trackTimeRepository.GetOfTeam(teamId))
                .Where(x => x.StartTime.HasValue)
                .ToList();

            var results = _mapper.MapOfTeam(times);
            return results;
        }

        public async Task<List<TrackResultDto>> GetTrackResults(int trackId)
        {
            var times = (await _trackTimeRepository.GetOfTrack(trackId))
                .Where(x => x.StartTime.HasValue)
                .ToList();

            var results = _mapper.MapOfTrack(times);
            return results;
        }
    }
}
