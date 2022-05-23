using FunTrophy.Infrastructure.Contracts.Repositories;
using FunTrophy.Infrastructure.Model;

namespace FunTrophy.Fake.Repositories
{
    public class FakeTrackOrderRepository : FakeRepositoryBase<TrackOrder>, ITrackOrderRepository
    {
        public FakeTrackOrderRepository(FakeDbContext dbContext) : base(dbContext)
        {
        }

        public Task<List<TrackOrder>> GetAll(int colorId)
        {
            return GetAll(x => x.ColorId == colorId);
        }

        public Task RemoveAll(int colorId)
        {
            _dbContext.TrackOrders.RemoveAll(x => x.ColorId == colorId);
            return Task.CompletedTask;
        }
    }
}