using FunTrophy.API.Contracts.Helpers;
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
        private readonly INotificationHelper _notificationHelper;

        private readonly TimeSpan TRACK_MIN_DURATION = TimeSpan.FromSeconds(60);
        private static readonly SemaphoreSlim mutex = new SemaphoreSlim(1, 1);

        public TrackTimeService(
            ITrackOrderRepository trackOrderRepository,
            ITrackTimeRepository trackTimeRepository,
            ITeamRepository teamRepository,
            ITrackTimeMapper mapper,
            INotificationHelper notificationHelper)
        {
            _trackOrderRepository = trackOrderRepository;
            _trackTimeRepository = trackTimeRepository;
            _teamRepository = teamRepository;
            _mapper = mapper;
            _notificationHelper = notificationHelper;
        }

        private async Task<TrackTime?> GetNextTrackTime(Team team, IEnumerable<TrackOrder> trackOrders, IEnumerable<TrackTime> teamTrackTimes)
        {
            trackOrders = trackOrders.OrderBy(x => x.SortOrder);
            var currentTrackTime = teamTrackTimes.Where(x => x.StartTime.HasValue).LastOrDefault();
            int? nextTrackId;
            if (currentTrackTime is null)
            {
                nextTrackId = trackOrders.FirstOrDefault()?.TrackId;
            }
            else
            {
                nextTrackId = trackOrders
                      .SkipWhile(p => p.TrackId != currentTrackTime.TrackId)
                      .ElementAtOrDefault(1)?.TrackId;
            }
            if (!nextTrackId.HasValue)
                throw new InvalidDataException();

            var nextTrackTime = teamTrackTimes.Where(x => x.TrackId == nextTrackId.Value).FirstOrDefault();

            if (nextTrackTime is null)
            {
                var addNextTrackTime = new TrackTime()
                {
                    TeamId = team.Id,
                    TrackId = nextTrackId.Value
                };
                var newTrackTimeId = await _trackTimeRepository.Add(addNextTrackTime);
                nextTrackTime = await _trackTimeRepository.Get(newTrackTimeId);
                await _notificationHelper.NotifyTrackTimeChanged(nextTrackTime.TrackId, nextTrackTime.TeamId);
            }
            return nextTrackTime;
        }

        public async Task<List<TeamLapInfoDto>> GetTeamLaps(int colorId)
        {
            await mutex.WaitAsync();
            try
            {
                var trackTimes = await _trackTimeRepository.GetOfColor(colorId);

                var teams = await _teamRepository.GetOfColor(colorId);
                if (!teams.Any())
                    return new List<TeamLapInfoDto>();

                var trackOrders = await _trackOrderRepository.GetOfColor(colorId);
                if (!trackOrders.Any())
                    return new List<TeamLapInfoDto>();

                var laps = new List<TeamLapInfoDto>();
                foreach (var team in teams)
                {
                    var teamTrackTimes = trackTimes.Where(x => x.TeamId == team.Id).OrderBy(x => x.StartTime);
                    var nextTrackTime = await GetNextTrackTime(team, trackOrders, teamTrackTimes);
                    var currentTrackTime = teamTrackTimes.Where(x => x.StartTime.HasValue && !x.EndTime.HasValue).LastOrDefault();
                    var lapInfo = _mapper.Map(currentTrackTime, nextTrackTime, team);
                    laps.Add(lapInfo);
                }
                var currentDate = DateTime.UtcNow;
                laps.ForEach(x => x.ServerTime = currentDate);
                return laps.OrderBy(x => x.Team.Number).ToList();
            }
            finally
            {
                mutex.Release();
            }
        }

        public async Task SaveTeamLap(int userId, int teamId)
        {
            var currentTime = DateTime.UtcNow;
            var team = await _teamRepository.Get(teamId);
            var trackOrders = (await _trackOrderRepository.GetOfColor(team.ColorId)).OrderBy(x => x.SortOrder);
            var teamTrackTimes = (await _trackTimeRepository.GetOfTeam(teamId)).OrderBy(x => x.StartTime);
            var nextTrackTime = await GetNextTrackTime(team, trackOrders, teamTrackTimes);
            var currentTrackTime = teamTrackTimes.Where(x => x.StartTime.HasValue && !x.EndTime.HasValue).LastOrDefault();

            if (currentTrackTime is not null)
            {
                // avoid errors by double clicking a track that had just started
                if (currentTrackTime.StartTime.HasValue && currentTrackTime.StartTime.Value.Add(TRACK_MIN_DURATION) > currentTime)
                {
                    return;
                }
                currentTrackTime.EndTime = currentTime;
                currentTrackTime.UpdatedBy = userId;
                await _trackTimeRepository.Update(currentTrackTime);
                await _notificationHelper.NotifyTrackTimeChanged(currentTrackTime.TrackId, currentTrackTime.TeamId);
            }

            if (nextTrackTime is not null)
            {
                nextTrackTime.StartTime = currentTime;
                nextTrackTime.UpdatedBy = userId;
                await _trackTimeRepository.Update(nextTrackTime);
                await _notificationHelper.NotifyTrackTimeChanged(nextTrackTime.TrackId, nextTrackTime.TeamId);
            }
        }

        public async Task Undo(int userId)
        {
            var trackTimes = (await _trackTimeRepository.GetAll(x => x.UpdatedBy == userId && x.StartTime != null))
                .OrderByDescending(x => x.StartTime);

            var lastChanged = trackTimes.FirstOrDefault();
            if (lastChanged != null)
            {
                var previousTracks = trackTimes.Where(x => x.TeamId == lastChanged.TeamId);
                var previousChanged = previousTracks.ElementAtOrDefault(1);
                if (previousChanged != null)
                {
                    previousChanged.EndTime = null;
                    await _trackTimeRepository.Update(previousChanged);
                }
                lastChanged.StartTime = null;
                await _trackTimeRepository.Update(lastChanged);
                await _notificationHelper.NotifyTrackTimeChanged(lastChanged.TrackId, lastChanged.TeamId);
            }
        }
    }
}