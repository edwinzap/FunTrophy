using FunTrophy.Infrastructure.Contracts.Repositories;
using FunTrophy.Infrastructure.Model;

namespace FunTrophy.Infrastructure.Repositories
{
    public class ColorRepository : RepositoryBase<Color>, IColorRepository
    {
        public ColorRepository(FunTrophyContext dbContext) : base(dbContext)
        {
            
        }

        public Task<List<Color>> GetAll(int raceId)
        {
            return GetAll(x => x.RaceId == raceId);
        }
    }
}