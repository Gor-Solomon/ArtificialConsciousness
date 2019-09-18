using AC.DAL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AC.DAL.Repositories.Interfaces
{
    public interface IBaseRepository<TEntity> : IDisposable where TEntity : class, IEntity
    {
        TEntity Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void RemoveRange(IEnumerable<TEntity> entities);
        void Edit(TEntity entity);
        void Delete(int entity);
        void Attach(TEntity entity);

        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate = null);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate = null);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null);
        Task<TEntity> GetByIdAsync(int id);
    }
}
