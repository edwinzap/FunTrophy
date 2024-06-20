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

        public async Task RemoveAllOfColor(int colorId)
        {
            var trackOrders = await GetOfColor(colorId);
            _dbContext.TrackOrders.RemoveRange(trackOrders);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveAllOfTrack(int trackId)
        {
            var trackOrders = await GetAll(x => x.TrackId == trackId);
            _dbContext.TrackOrders.RemoveRange(trackOrders);
            await _dbContext.SaveChangesAsync();
        }
    }
}