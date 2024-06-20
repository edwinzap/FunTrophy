using FunTrophy.Infrastructure.Contracts.Repositories;
using FunTrophy.Infrastructure.Model;

namespace FunTrophy.Infrastructure.Repositories
{
    public class ColorRepository : RepositoryBase<Color>, IColorRepository
    {
        private readonly ITrackOrderRepository _trackOrderRepository;
        public ColorRepository(FunTrophyContext dbContext, ITrackOrderRepository trackOrderRepository) : base(dbContext)
        {
            _trackOrderRepository = trackOrderRepository;
        }

        public Task<List<Color>> GetAll(int raceId)
        {
            return GetAll(x => x.RaceId == raceId);
        }

        public override async Task Remove(int colorId)
        {
            await _trackOrderRepository.RemoveAllOfColor(colorId);
            await base.Remove(colorId);
        }
    }
}