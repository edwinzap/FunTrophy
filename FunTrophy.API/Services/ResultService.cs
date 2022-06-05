using FunTrophy.API.Contracts.Mappers;
using FunTrophy.API.Contracts.Services;
using FunTrophy.Infrastructure.Contracts.Repositories;
using FunTrophy.Shared.Model;

namespace FunTrophy.API.Services
{
    public class ResultService : ServiceBase, IResultService
    {
        private readonly ITrackTimeRepository _trackTimeRepository;
        private readonly ITimeAdjustmentRepository _timeAdjustmentRepository;
        private readonly ITrackRepository _trackRepository;
        private readonly IResultMapper _mapper;

        public ResultService(
            ITrackTimeRepository trackTimeRepository,
            ITimeAdjustmentRepository timeAdjustmentRepository,
            ITrackRepository trackRepository,
            IResultMapper mapper)
        {
            _trackTimeRepository = trackTimeRepository;
            _timeAdjustmentRepository = timeAdjustmentRepository;
            _trackRepository = trackRepository;
            _mapper = mapper;
        }

        public async Task<List<FinalResultDto>> GetFinalResults(int raceId)
        {
            var tracks = await _trackRepository.GetAll(raceId);
            var times = await _trackTimeRepository.GetOfRace(raceId);
            var filteredTimes = times.Where(x => x.StartTime.HasValue && x.EndTime.HasValue).ToList();

            var adjustments = await _timeAdjustmentRepository.GetOfRace(raceId);
            var teams = filteredTimes.Select(x => x.Team).Distinct().ToList();

            var results = new List<FinalResultDto>();
            foreach (var team in teams)
            {
                var teamTimes = filteredTimes.Where(x => x.TeamId == team.Id).ToList();
                if (teamTimes.Count < tracks.Count) //keep only teams that has done every tracks
                    continue;
                
                var teamAdjustments = adjustments.Where(x => x.TeamId == team.Id).ToList();
                var teamResult = _mapper.MapFinal(team, teamTimes, teamAdjustments);
                results.Add(teamResult);
            }
            results = results.OrderBy(x => x.TotalDuration).ToList();
            return results;
        }

        public async Task<List<TeamResultDto>> GetTeamResults(int teamId)
        {
            var times = (await _trackTimeRepository.GetOfTeam(teamId))
                .Where(x => x.StartTime.HasValue)
                .OrderBy(x => x.StartTime)
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
