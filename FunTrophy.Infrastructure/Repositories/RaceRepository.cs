using FunTrophy.Infrastructure.Contracts.Repositories;
using FunTrophy.Infrastructure.Model;

namespace FunTrophy.Infrastructure.Repositories
{
    public class RaceRepository : RepositoryBase<Race>, IRaceRepository
    {
        public RaceRepository(FunTrophyContext dbContext) : base(dbContext)
        {
        }
    }
}