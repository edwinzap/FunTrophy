using FunTrophy.Infrastructure.Model;

namespace FunTrophy.Infrastructure.Contracts.Repositories
{
    public interface IColorRepository : IRepositoryBase<Color>
    {
        Task<List<Color>> GetAll(int raceId);
    }
}