using EFCore.GenericRepository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace EFCore.GenericRepository
{
    public partial class GenericRepository<TContext, TEntity> : IGenericRepository<TContext, TEntity>, IDisposable
        where TContext : DbContext
        where TEntity : class, IBaseDbEntity
    {
        private TContext _context;
        private DbSet<TEntity> _dbSet;
        protected bool IsSoftDeletableEntity { get; }
        protected bool IsSoftUpdatableEntity { get; }

        public GenericRepository(TContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();

            IsSoftDeletableEntity = typeof(ISoftDeletableEntity).IsAssignableFrom(typeof(TEntity));
            IsSoftUpdatableEntity = typeof(ISoftUpdatableEntity).IsAssignableFrom(typeof(TEntity));
        }
        public virtual IQueryable<TEntity> AsQueryable(bool getDeleted = false)
        {
            if (getDeleted)
                return _dbSet;
            else
            {
                if (IsSoftDeletableEntity)
                {
                    return ((IQueryable<ISoftDeletableEntity>)(this._dbSet))
                        .Where(q => q.Deleted == false)
                        .Cast<TEntity>();
                }
                return _dbSet;
            }
        }

        public virtual void Dispose()
        {
            _context.Dispose();
        }
    }
}
