using EFCore.GenericRepository.interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.GenericRepository
{
    public class GenericRepository<TContext, TEntity> : IGenericRepository<TContext, TEntity>, IDisposable
        where TContext : DbContext
        where TEntity : BaseDbEntity
    {
        TContext _context;
        protected DbSet<TEntity> DbSet;

        public GenericRepository(TContext context)
        {
            _context = context;
            DbSet = _context.Set<TEntity>();
        }
        public virtual TEntity Find(int id)
        {
            return DbSet.Find(id);
        }
        public virtual async Task<TEntity> FindAsync(int id)
        {
            return await DbSet.FindAsync(id);
        }
        public virtual IQueryable<TEntity> AsQueryable(bool getDeleted = false)
        {
            if (getDeleted)
                return DbSet;
            else
            {
                if (typeof(ISoftDeletableEntity).IsAssignableFrom(typeof(TEntity)))
                {
                    return ((IQueryable<ISoftDeletableEntity>)(this.DbSet))
                        .Where(q => q.Deleted == false)
                        .Cast<TEntity>();
                }
                return DbSet;
            }
        }
        public virtual TEntity Delete(int id)
        {
            var entity = DbSet.Find(id);
            if (entity == null)
                return null;

            if (entity is ISoftDeletableEntity)
            {
                entity.LastUpdateTime = DateTime.Now;
                (entity as ISoftDeletableEntity).Deleted = true;
            }
            else
                DbSet.Remove(entity);

            Commit();
            return entity;
        }
        public virtual async Task<TEntity> DeleteAsync(int id)
        {
            var entity = await DbSet.FindAsync(id);
            if (entity == null)
                return null;

            if (entity is ISoftDeletableEntity)
            {
                entity.LastUpdateTime = DateTime.Now;
                (entity as ISoftDeletableEntity).Deleted = true;
            }
            else
                DbSet.Remove(entity);

            await CommitAsync();
            return entity;
        }
        public virtual TEntity AddOrUpdate(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Entity is null!");

            if (entity.ID != 0)
                return Update(entity);
            else
                return Insert(entity);
        }
        public virtual async Task<TEntity> AddOrUpdateAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Entity is null!");

            if (entity.ID != 0)
                return await UpdateAsync(entity);
            else
                return await InsertAsync(entity);
        }

        public virtual TEntity Insert(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Entity is null!");

            entity.CreationTime = DateTime.Now;

            DbSet.Add(entity);
            Commit();

            return entity;
        }
        public virtual async Task<TEntity> InsertAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Entity is null!");

            entity.CreationTime = DateTime.Now;

            await DbSet.AddAsync(entity);
            await CommitAsync();

            return entity;
        }

        public virtual TEntity Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Entity is null!");

            entity.LastUpdateTime = DateTime.Now;
            //if its ISoftUpdatable , get deep copy of entity and insert it as a soft deleted with FKPreviousVersionID=entity.ID 
            if (typeof(ISoftUpdatableEntity).IsAssignableFrom(typeof(TEntity)))
            {
                var dbResult = DbSet.AsNoTracking().FirstOrDefault(x => x.ID == entity.ID);
                if (dbResult == null)
                    throw new ArgumentNullException($"There is no object in db whose ID is {entity.ID}. Check your object's ID");


                dbResult.ID = 0;
                (dbResult as ISoftUpdatableEntity).FKPreviousVersionID = entity.ID;
                (dbResult as ISoftUpdatableEntity).Deleted = true;
                (dbResult as ISoftUpdatableEntity).LastUpdateTime = null;

                return Insert(dbResult);
            }

            Commit();
            return entity;
        }
        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Entity is null!");

            entity.LastUpdateTime = DateTime.Now;
            //if its ISoftUpdatable , get deep copy of entity and insert it as a soft deleted with FKPreviousVersionID=entity.ID 
            if (typeof(ISoftUpdatableEntity).IsAssignableFrom(typeof(TEntity)))
            {
                var dbResult = await DbSet.AsNoTracking().FirstOrDefaultAsync(x => x.ID == entity.ID);
                if (dbResult == null)
                    throw new ArgumentNullException($"There is no object in db whose ID is {entity.ID}. Check your object's ID");

                dbResult.ID = 0;
                (dbResult as ISoftUpdatableEntity).FKPreviousVersionID = entity.ID;
                (dbResult as ISoftUpdatableEntity).Deleted = true;
                (dbResult as ISoftUpdatableEntity).LastUpdateTime = null;

                return await InsertAsync(dbResult);
            }
            await CommitAsync();
            return entity;
        }

        protected virtual void Commit()
        {
            _context.SaveChanges();
        }
        protected virtual async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
        public virtual void Dispose()
        {
            _context.Dispose();
        }
    }
}
