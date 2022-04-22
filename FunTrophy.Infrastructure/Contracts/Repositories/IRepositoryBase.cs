using FunTrophy.Infrastructure.Model;
using System.Linq.Expressions;

namespace FunTrophy.Infrastructure.Contracts.Repositories
{
    public interface IRepositoryBase<TEntity> where TEntity : EntityBase
    {
        Task<int> Add(TEntity entity);

        Task<TEntity> Get(int id);

        Task<List<TEntity>> GetAll(Expression<Func<TEntity, bool>>? filter = null);

        Task Update(TEntity entity);

        Task Update(IEnumerable<TEntity> entities);

        Task Remove(int id);
    }
}