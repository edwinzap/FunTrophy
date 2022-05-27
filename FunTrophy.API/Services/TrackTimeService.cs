using FunTrophy.API.Contracts.Mappers;
using FunTrophy.API.Contracts.Services;
using FunTrophy.Infrastructure.Contracts.Repositories;
using FunTrophy.Infrastructure.Model;
using FunTrophy.Shared.Model;

namespace FunTrophy.API.Services
{
    public class TrackTimeService : ServiceBase, ITrackTimeService
    {
        private readonly ITrackOrderRepository _trackOrderRepository;
        private readonly ITrackTimeRepository _trackTimeRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly ITrackTimeMapper _mapper;

        public TrackTimeService(
            ITrackOrderRepository trackOrderRepository,
            ITrackTimeRepository trackTimeRepository,
            ITeamRepository teamRepository,
            ITrackTimeMapper mapper)
        {
            _trackOrderRepository = trackOrderRepository;
            _trackTimeRepository = trackTimeRepository;
            _teamRepository = teamRepository;
            _mapper = mapper;
        }

        public async Task<List<TeamLapInfoDto>> GetTeamLaps(int colorId)
        {
            var trackTimes = await _trackTimeRepository.GetOfColor(colorId);
            var trackOrders = (await _trackOrderRepository.GetOfColor(colorId)).OrderBy(x => x.SortOrder);
            var teams = await _teamRepository.GetOfColor(colorId);

            if (!teams.Any() || !trackOrders.Any())
            {
                return new List<TeamLapInfoDto>();
            }

            var laps = new List<TeamLapInfoDto>();
            foreach (var team in teams)
            {
                var teamTrackTimes = trackTimes.Where(x => x.TeamId == team.Id).OrderBy(x => x.StartTime);
                var nextTrackTime = teamTrackTimes.Where(x => !x.StartTime.HasValue && !x.EndTime.HasValue).LastOrDefault();
                var currentTrackTime = teamTrackTimes.Where(x => x.StartTime.HasValue).LastOrDefault();

                if (nextTrackTime == null) //create next track
                {
                    var addNextTrack = new TrackTime() { TeamId = team.Id, };
                    if (currentTrackTime == null)
                    {
                        var nextTrackId = trackOrders.First().TrackId;
                        addNextTrack.TrackId = nextTrackId;
                    }
                    else
                    {
                        addNextTrack.TrackId = currentTrackTime.TrackId;
                    }
                    var newTrackId = await _trackTimeRepository.Add(addNextTrack);
                    nextTrackTime = await _trackTimeRepository.Get(newTrackId);
                }
                var lapInfo = _mapper.Map(currentTrackTime, nextTrackTime, team);
                laps.Add(lapInfo);
            }

            return laps;
        }

        public Task<TeamLapInfoDto> SaveTeamLap(int teamId)
        {
            throw new NotImplementedException();
        }
    }
}