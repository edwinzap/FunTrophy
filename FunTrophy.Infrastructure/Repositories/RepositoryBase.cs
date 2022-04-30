﻿using FunTrophy.Infrastructure.Contracts.Repositories;
using FunTrophy.Infrastructure.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FunTrophy.Infrastructure.Repositories
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : EntityBase
    {
        protected readonly FunTrophyContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public RepositoryBase(FunTrophyContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }

        public Task<TEntity> Get(int id)
        {
            return _dbSet.FirstAsync(x => x.Id == id);
        }

        public Task<List<TEntity>> GetAll(Expression<Func<TEntity, bool>>? filter = null)
        {
            IQueryable<TEntity> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return query.ToListAsync();
        }

        public async Task<int> Add(TEntity entity)
        {
            _dbSet.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public Task Update(TEntity entity)
        {
            _dbSet.Update(entity);
            return _dbContext.SaveChangesAsync();
        }

        public Task Update(IEnumerable<TEntity> entities)
        {
            _dbSet.UpdateRange(entities);
            return _dbContext.SaveChangesAsync();
        }

        public async Task Remove(int id)
        {
            var entity = await _dbSet.FirstAsync(x => x.Id == id);
            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}