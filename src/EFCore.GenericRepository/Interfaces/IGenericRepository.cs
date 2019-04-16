


using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.GenericRepository.interfaces
{
    public interface IGenericRepository<TContext, TEntity>
        where TContext : DbContext
        where TEntity : BaseDbEntity
    {
        TEntity Find(int id);
        Task<TEntity> FindAsync(int id);
        IQueryable<TEntity> AsQueryable(bool getDeleted = false);

        TEntity Delete(int id);
        Task<TEntity> DeleteAsync(int id);

        TEntity AddOrUpdate(TEntity entity);
        Task<TEntity> AddOrUpdateAsync(TEntity entity);

        TEntity Insert(TEntity entity);
        Task<TEntity> InsertAsync(TEntity entity);

        TEntity Update(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
    }
}
