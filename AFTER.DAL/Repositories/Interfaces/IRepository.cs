using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AFTER.DAL.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(int id, params Expression<Func<TEntity, object>>[] includes);

        Task<TEntity> GetByIdMultiLevelAsync(int id, params Tuple<Expression<Func<TEntity, object>>, Expression<Func<object, object>>>[] includes);

        Task<IEnumerable<TEntity>> GetAllAsync(bool useIsDeleted = true);

        Task<IEnumerable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes);

        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

        Task<EntityEntry<TEntity>> AddAsync(TEntity entity);

        Task AddRangeAsync(IEnumerable<TEntity> entities);

        void DetachEntity(TEntity entity);

        void Remove(TEntity entity);

        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
