using FunTrophy.Infrastructure.Contracts.Repositories;
using FunTrophy.Infrastructure.Model;
using System.Linq.Expressions;

namespace FunTrophy.Fake.Repositories
{
    public class FakeRepositoryBase<T> : IRepositoryBase<T> where T : EntityBase
    {
        protected readonly FakeDbContext _dbContext;
        private readonly List<T> _dbSet;

        public FakeRepositoryBase(FakeDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Get<T>();
        }

        public virtual Task<int> Add(T entity)
        {
            var lastId = _dbSet.OrderBy(x => x.Id).Select(x => x.Id).LastOrDefault();
            entity.Id = ++lastId;
            _dbSet.Add(entity);
            return Task.FromResult(lastId);
        }

        public async Task Add(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                await Add(entity);
            }
        }

        public Task<T> Get(int id)
        {
            var value = _dbSet.First(x => x.Id == id);
            return Task.FromResult(value);
        }

        public Task<List<T>> GetAll(Expression<Func<T, bool>>? filter = null)
        {
            Thread.Sleep(200);
            var result = _dbSet;
            if (filter != null)
            {
                result = result.AsQueryable().Where(filter).ToList();
            }
            return Task.FromResult(result);
        }

        public Task Remove(int id)
        {
            _dbSet.RemoveAll(x => x.Id == id);
            return Task.CompletedTask;
        }

        public virtual Task Update(T entity)
        {
            //throw new NotImplementedException();
            return Task.CompletedTask;
        }

        public virtual Task Update(IEnumerable<T> entities)
        {
            //throw new NotImplementedException();
            return Task.CompletedTask;
        }
    }
}