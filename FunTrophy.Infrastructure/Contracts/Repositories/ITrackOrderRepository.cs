using FunTrophy.Infrastructure.Model;

namespace FunTrophy.Infrastructure.Contracts.Repositories
{
    public interface ITrackOrderRepository : IRepositoryBase<TrackOrder>
    {
        Task<List<TrackOrder>> GetAll(int colorId);
    }
}