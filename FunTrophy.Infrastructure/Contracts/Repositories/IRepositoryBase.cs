using FunTrophy.Infrastructure.Model;

namespace FunTrophy.Infrastructure.Contracts.Repositories
{
    public interface IRepositoryBase<TEntity> where TEntity : EntityBase
    {
        Task<int> Add(TEntity entity);
        Task<TEntity> Get(int id);
        Task<List<TEntity>> GetAll();
        Task Update(TEntity entity);

        Task Remove(int id);
    }
}