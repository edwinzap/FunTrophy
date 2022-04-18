using FunTrophy.API.Mappers;
using FunTrophy.API.Services.Contracts;
using FunTrophy.Infrastructure;
using FunTrophy.Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace FunTrophy.API.Services
{
    public class TeamService : ServiceBase, ITeamService
    {
        private readonly ITeamMapper _mapper;
        private readonly ITeamTypeMapper _typeMapper;

        public TeamService(FunTrophyContext dbContext, ITeamMapper mapper, ITeamTypeMapper typeMapper) : base(dbContext)
        {
            _mapper = mapper;
            _typeMapper = typeMapper;
        }

        public async Task<int> Create(AddTeamDto team)
        {
            var dbTeam = _mapper.Map(team);
            _dbContext.Teams.Add(dbTeam);
            await _dbContext.SaveChangesAsync();

            return dbTeam.Id;
        }

        public Task<List<TeamDto>> GetAll()
        {
            var task = _dbContext.Teams.Select(x => _mapper.Map(x)).ToListAsync();
            return task;
        }

        public async Task Remove(int teamId)
        {
            var race = await _dbContext.Teams.FindAsync(teamId);
            if (race == null)
            {
                throw new KeyNotFoundException();
            }
            _dbContext.Teams.Remove(race);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(int teamId, UpdateTeamDto team)
        {
            var dbTeam = await _dbContext.Teams.FindAsync(teamId);
            if (dbTeam == null)
            {
                throw new KeyNotFoundException();
            }
            dbTeam.Name = team.Name;
            dbTeam.Number = team.Number;
            dbTeam.ColorId = team.ColorId;
            dbTeam.Type = _typeMapper.Map(team.Type);

            _dbContext.Teams.Update(dbTeam);
            await _dbContext.SaveChangesAsync();
        }
    }
}
