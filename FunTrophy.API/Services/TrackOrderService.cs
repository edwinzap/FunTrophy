using FunTrophy.API.Mappers;
using FunTrophy.API.Services.Contracts;
using FunTrophy.Infrastructure;
using FunTrophy.Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace FunTrophy.API.Services
{
    public class TrackOrderService : ServiceBase, ITrackOrderService
    {
        private readonly ITrackOrderMapper _mapper;

        public TrackOrderService(FunTrophyContext dbContext, ITrackOrderMapper mapper) : base(dbContext)
        {
            _mapper = mapper;
        }

        public async Task<int> AddTrackOrder(AddTrackOrderDto trackOrder)
        {
            var dbTrackOrder = _mapper.Map(trackOrder);
            _dbContext.TrackOrders.Add(dbTrackOrder);
            await _dbContext.SaveChangesAsync();

            return dbTrackOrder.Id;
        }

        public Task<List<TrackOrderDto>> GetAll(int colorId)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateTrackOrder(int trackId, int sortOrder)
        {
            var dbTrackOrder = await _dbContext.TrackOrders.FindAsync(trackId);
            if (dbTrackOrder == null)
            {
                throw new KeyNotFoundException();
            }

            if (dbTrackOrder.SortOrder == sortOrder)
            {
                return;
            }

            var dbTrackOrders = await _dbContext.TrackOrders.Where(x => x.ColorId == dbTrackOrder.ColorId).ToListAsync();
        }
    }
}