using FunTrophy.Infrastructure.Contracts.Repositories;
using FunTrophy.Infrastructure.Model;

namespace FunTrophy.Fake.Repositories
{
    public class FakeTeamRepository : FakeRepositoryBase<Team>, ITeamRepository
    {
        public FakeTeamRepository(FakeDbContext dbContext) : base(dbContext)
        {
        }

        public Task<List<Team>> GetAll(int colorId)
        {
            return GetAll(x => x.ColorId == colorId);
        }

        public override Task Update(Team entity)
        {
            entity.Color = _dbContext.Colors.First(x => x.Id == entity.ColorId);
            return base.Update(entity);
        }
    }
}