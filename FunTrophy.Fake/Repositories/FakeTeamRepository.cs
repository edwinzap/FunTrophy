using FunTrophy.Infrastructure.Contracts.Repositories;
using FunTrophy.Infrastructure.Model;

namespace FunTrophy.Fake.Repositories
{
    public class FakeTeamRepository : FakeRepositoryBase<Team>, ITeamRepository
    {
        public FakeTeamRepository(FakeDbContext dbContext) : base(dbContext)
        {
        }

        public Task<List<Team>> GetAll(int raceId)
        {
            return GetAll(x => x.Color.RaceId == raceId);
        }
    }
}