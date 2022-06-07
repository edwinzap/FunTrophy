using FunTrophy.Infrastructure.Contracts.Repositories;
using FunTrophy.Infrastructure.Model;

namespace FunTrophy.Fake.Repositories
{
    public class FakeTrackOrderRepository : FakeRepositoryBase<TrackOrder>, ITrackOrderRepository
    {
        public FakeTrackOrderRepository(FakeDbContext dbContext) : base(dbContext)
        {
        }

        public Task<List<TrackOrder>> GetOfColor(int colorId)
        {
            return GetAll(x => x.ColorId == colorId);
        }

        public Task RemoveAllOfColor(int colorId)
        {
            _dbContext.TrackOrders.RemoveAll(x => x.ColorId == colorId);
            return Task.CompletedTask;
        }
    }
}