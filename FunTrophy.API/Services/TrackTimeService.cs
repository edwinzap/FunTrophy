﻿using FunTrophy.API.Contracts.Mappers;
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
            var team = await _teamRepository.Get(teamId);
            var trackOrders = (await _trackOrderRepository.GetOfColor(team.ColorId)).OrderBy(x => x.SortOrder);
            var teamTrackTimes = (await _trackTimeRepository.GetOfTeam(teamId)).OrderBy(x => x.StartTime);
            var nextTrackTime = await GetNextTrackTime(team, trackOrders, teamTrackTimes);
            var currentTrackTime = teamTrackTimes.Where(x => x.StartTime.HasValue && !x.EndTime.HasValue).LastOrDefault();

            if (currentTrackTime is not null)
            {
                currentTrackTime.EndTime = DateTime.Now;
            }

            if (nextTrackTime is not null)
            {
                nextTrackTime.StartTime = DateTime.Now;
                await _trackTimeRepository.Update(nextTrackTime);
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