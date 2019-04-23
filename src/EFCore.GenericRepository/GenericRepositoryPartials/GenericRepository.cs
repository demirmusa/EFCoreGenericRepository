using EFCore.GenericRepository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EFCore.GenericRepository
{
    public partial class GenericRepository<TContext, TEntity> : IGenericRepository<TContext, TEntity>, IDisposable
        where TContext : DbContext
        where TEntity : class, IBaseDbEntity
    {
        TContext _context;
        protected DbSet<TEntity> DbSet;
        protected bool IsSoftDeletableEntity { get; }
        protected bool IsSoftUpdatableEntity { get; }

        public GenericRepository(TContext context)
        {
            _context = context;
            DbSet = _context.Set<TEntity>();

            IsSoftDeletableEntity = typeof(ISoftDeletableEntity).IsAssignableFrom(typeof(TEntity));
            IsSoftUpdatableEntity = typeof(ISoftUpdatableEntity).IsAssignableFrom(typeof(TEntity));
        }
        public virtual IQueryable<TEntity> AsQueryable(bool getDeleted = false)
        {
            if (getDeleted)
                return DbSet;
            else
            {
                if (IsSoftDeletableEntity)
                {
                    return ((IQueryable<ISoftDeletableEntity>)(this.DbSet))
                        .Where(q => q.Deleted == false)
                        .Cast<TEntity>();
                }
                return DbSet;
            }
        }
  
        public virtual void Dispose()
        {
            _context.Dispose();
        }
    }
}
