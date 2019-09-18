using AC.DAL.Common.Implementations;
using AC.DAL.EF;
using AC.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AC.DAL.Repositories.Implementations
{
    public class BaseRepository<TEntity, TDbContext> : DatabaseTransaction<TDbContext>, IBaseRepository<TEntity>
    where TEntity : class, IEntity
    where TDbContext : DbContext
    {

        public BaseRepository(ACDatabaseEntities context) : base(context) { }

        public virtual TEntity Add(TEntity entity)
        {
            if (entity is null)
            {
                throw new ArgumentException(nameof(entity));
            }

            return _dbContext.Set<TEntity>().Add(entity);
        }

        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            if (entities is null || entities.Any(e=> e is null))
            {
                throw new ArgumentException(nameof(entities));
            }

            _dbContext.Set<TEntity>().AddRange(entities);
        }

        public virtual Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate is null ? _dbContext.Set<TEntity>().AnyAsync() :
                                       _dbContext.Set<TEntity>().AnyAsync(predicate);
        }

        public virtual void Attach(TEntity entity)
        {
            if (entity is null)
            {
                throw new ArgumentException(nameof(entity));
            }

            _dbContext.Set<TEntity>().Attach(entity);
        }

        public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate is null ? _dbContext.Set<TEntity>().CountAsync() :
                                      _dbContext.Set<TEntity>().CountAsync(predicate);
        }

        public virtual async void Delete(int id)
        {
            var result = await _dbContext.Set<TEntity>().FindAsync(id);
            if(result != null)
            {
                DeleteSync(result);
            }
        }

        protected virtual void DeleteSync(TEntity entity)
        {
             _dbContext.Set<TEntity>().Remove(entity);
        }

        public void Edit(TEntity entity)
        {
            if (entity is null)
            {
                throw new ArgumentException(nameof(entity));
            }

            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate is null ? _dbContext.Set<TEntity>().FirstOrDefaultAsync() :
                                       _dbContext.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return filter is null ? await  _dbContext.Set<TEntity>().ToListAsync() :
                                    await _dbContext.Set<TEntity>().Where(filter).ToListAsync()  ;
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            if (entities is null || entities.Any(e => e is null))
            {
                throw new ArgumentException(nameof(entities));
            }

            _dbContext.Set<TEntity>().RemoveRange(entities);
        }
    }
}
