using FunTrophy.Infrastructure.Contracts.Repositories;
using FunTrophy.Infrastructure.Model;

namespace FunTrophy.Fake.Repositories
{
    public class FakeRaceRepository : FakeRepositoryBase<Race>, IRaceRepository
    {
        public FakeRaceRepository(FakeDbContext dbContext) : base(dbContext)
        {
        }
    }
}