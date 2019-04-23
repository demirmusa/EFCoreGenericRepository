using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EFCore.GenericRepository.Interfaces
{
    public interface IGenericRepository<TContext, TEntity>
        where TContext : DbContext
        where TEntity : class, IBaseDbEntity
    {
        IQueryable<TEntity> AsQueryable(bool getDeleted = false);

        TEntity Find(int id);
        Task<TEntity> FindAsync(int id);

        TEntity AddOrUpdate(TEntity entity);
        Task<TEntity> AddOrUpdateAsync(TEntity entity);

        TEntity Insert(TEntity entity);
        Task<TEntity> InsertAsync(TEntity entity);

        TEntity Update(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);

        TEntity Delete(int id);
        Task<TEntity> DeleteAsync(int id);

        TEntity Delete(TEntity entity);
        Task<TEntity> DeleteAsync(TEntity entity);

        IEnumerable<TEntity> Delete(IEnumerable<TEntity> entities);
        Task<IEnumerable<TEntity>> DeleteAsync(IEnumerable<TEntity> entities);

        IEnumerable<TEntity> Delete(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> DeleteAsync(Expression<Func<TEntity, bool>> predicate);


        bool Any(Expression<Func<TEntity, bool>> predicate);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);



    }
}
