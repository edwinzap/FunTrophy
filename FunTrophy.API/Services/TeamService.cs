﻿using FunTrophy.API.Mappers;
using FunTrophy.API.Contracts.Services;
using FunTrophy.Infrastructure.Contracts.Repositories;
using FunTrophy.Shared.Model;
using FunTrophy.API.Contracts.Mappers;

namespace FunTrophy.API.Services
{
    public class TeamService : ServiceBase, ITeamService
    {
        private readonly ITeamRepository _repository;
        private readonly ITeamMapper _mapper;
        private readonly ITeamTypeMapper _typeMapper;

        public TeamService(ITeamRepository repository, ITeamMapper mapper, ITeamTypeMapper typeMapper)
        {
            _repository = repository;
            _mapper = mapper;
            _typeMapper = typeMapper;
        }

        public Task<int> Create(AddTeamDto team)
        {
            var dbTeam = _mapper.Map(team);
            return _repository.Add(dbTeam);
        }

        public async Task<List<TeamDto>> GetAll(int raceId)
        {
            var dbTeams = await _repository.GetOfColor(raceId);
            return _mapper.Map(dbTeams);
        }

        public async Task Remove(int teamId)
        {
            await _repository.Remove(teamId);
        }

        public async Task Update(int teamId, UpdateTeamDto team)
        {
            var dbTeam = await _repository.Get(teamId);
            dbTeam.Name = team.Name;
            dbTeam.Number = team.Number;
            dbTeam.ColorId = team.ColorId;
            dbTeam.Type = _typeMapper.Map(team.Type);
            await _repository.Update(dbTeam);
        }
    }
}