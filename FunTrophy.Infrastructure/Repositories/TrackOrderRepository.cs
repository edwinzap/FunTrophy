using FunTrophy.Infrastructure.Contracts.Repositories;
using FunTrophy.Infrastructure.Model;

namespace FunTrophy.Infrastructure.Repositories
{
    public class TrackOrderRepository : RepositoryBase<TrackOrder>, ITrackOrderRepository
    {
        public TrackOrderRepository(FunTrophyContext dbContext) : base(dbContext)
        {
        }
    }
}