using FunTrophy.API.Mappers;
using FunTrophy.Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace FunTrophy.API.Services
{
    public class RaceService : ServiceBase, IRaceService
    {
        private readonly IMapper _mapper;

        public RaceService(FunTrophyContext dbContext, IMapper mapper) : base(dbContext)
        {
            _mapper = mapper;
        }

        public async Task<RaceDto> GetRace(int raceId)
        {
            var race = await _dbContext.Races.FindAsync(raceId);
            if (race == null)
            {
                throw new KeyNotFoundException();
            }
            return _mapper.Map(race);
        }

        public async Task<int> CreateRace(AddOrUpdateRaceDto race)
        {
            var dbRace = _mapper.Map(race);
            _dbContext.Races.Add(dbRace);
            await _dbContext.SaveChangesAsync();

            return dbRace.Id;
        }

        public Task<List<RaceDto>> GetAllRaces()
        {
            var task = _dbContext.Races.Select(x => _mapper.Map(x)).ToListAsync();
            return task;
        }

        public async Task RemoveRace(int raceId)
        {
            var race = await _dbContext.Races.FindAsync(raceId);
            if (race == null)
            {
                throw new KeyNotFoundException();
            }
            _dbContext.Races.Remove(race);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateRace(int raceId, AddOrUpdateRaceDto race)
        {
            var dbRace = await _dbContext.Races.FindAsync(raceId);
            if (dbRace == null)
            {
                throw new KeyNotFoundException();
            }
            dbRace.Name = race.Name;
            dbRace.Date = race.Date;
            
            _dbContext.Races.Update(dbRace);
            await _dbContext.SaveChangesAsync();
        }
    }
}
