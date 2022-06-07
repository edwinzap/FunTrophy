﻿using FunTrophy.API.Contracts.Helpers;
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
            var nextTrackTime = teamTrackTimes.Where(x => !x.StartTime.HasValue && !x.EndTime.HasValue).LastOrDefault();
            trackOrders = trackOrders.OrderBy(x => x.SortOrder);
            if (nextTrackTime is null) //create next track
            {
                var currentTrackTime = teamTrackTimes.Where(x => x.StartTime.HasValue).LastOrDefault();

                var addNextTrackTime = new TrackTime() { TeamId = team.Id, };
                if (currentTrackTime is null)
                {
                    var nextTrackId = trackOrders.First().TrackId;
                    addNextTrackTime.TrackId = nextTrackId;
                }
                else
                {
                    var nextTrackOrder = trackOrders
                        .SkipWhile(p => p.TrackId != currentTrackTime.TrackId)
                        .ElementAtOrDefault(1);

                    if (nextTrackOrder is not null)
                    {
                        addNextTrackTime.TrackId = nextTrackOrder.TrackId;
                    }
                    else return null;
                }
                var newTrackTimeId = await _trackTimeRepository.Add(addNextTrackTime);
                nextTrackTime = await _trackTimeRepository.Get(newTrackTimeId);
                await _notificationHelper.NotifyTrackTimeChanged(nextTrackTime.TrackId, nextTrackTime.TeamId);
            }
            return nextTrackTime;
        }

        public async Task<List<TeamLapInfoDto>> GetTeamLaps(int colorId)
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

            return laps;
        }

        public async Task SaveTeamLap(int teamId)
        {
            var currentTime = DateTime.Now;
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
                await _trackTimeRepository.Update(currentTrackTime);
                await _notificationHelper.NotifyTrackTimeChanged(currentTrackTime.TrackId, currentTrackTime.TeamId);
            }

            if (nextTrackTime is not null)
            {
                nextTrackTime.StartTime = currentTime;
                await _trackTimeRepository.Update(nextTrackTime);
                await _notificationHelper.NotifyTrackTimeChanged(nextTrackTime.TrackId, nextTrackTime.TeamId);
            }

        }

        public async Task<TeamLapInfoDto> GetTeamLap(int teamId)
        {
            var team = await _teamRepository.Get(teamId);
            var trackOrders = (await _trackOrderRepository.GetOfColor(team.ColorId)).OrderBy(x => x.SortOrder);
            var teamTrackTimes = (await _trackTimeRepository.GetOfTeam(teamId)).OrderBy(x => x.StartTime);
            var nextTrackTime = await GetNextTrackTime(team, trackOrders, teamTrackTimes);
            var currentTrackTime = teamTrackTimes.Where(x => x.StartTime.HasValue && !x.EndTime.HasValue).LastOrDefault();

            var lapInfo = _mapper.Map(currentTrackTime, nextTrackTime, team);
            return lapInfo;
        }
    }
}