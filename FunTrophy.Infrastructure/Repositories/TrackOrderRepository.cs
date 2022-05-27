using FunTrophy.Infrastructure.Contracts.Repositories;
using FunTrophy.Infrastructure.Model;

namespace FunTrophy.Infrastructure.Repositories
{
    public class TrackOrderRepository : RepositoryBase<TrackOrder>, ITrackOrderRepository
    {
        public TrackOrderRepository(FunTrophyContext dbContext) : base(dbContext)
        {
        }

        public Task<List<TrackOrder>> GetOfColor(int colorId)
        {
            return GetAll(x => x.ColorId == colorId);
        }

        public async Task RemoveAll(int colorId)
        {
            var trackOrders = await GetOfColor(colorId);
           _dbContext.RemoveRange(trackOrders);
        }
    }
}