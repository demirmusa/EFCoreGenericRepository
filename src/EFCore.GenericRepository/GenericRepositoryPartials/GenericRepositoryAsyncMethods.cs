using EFCore.GenericRepository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.GenericRepository
{
    // Async part of GenericRepository
    public partial class GenericRepository<TContext, TEntity>
    {
        public virtual async Task<TEntity> FindAsync(int id)
        {
            return await DbSet.FindAsync(id);
        }
        /// <summary>
        ///  Determines whether any element of a sequence satisfies a condition.
        ///  Does not query deleted signed entities
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await AsQueryable().AnyAsync(predicate);
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
        public virtual async Task<TEntity> InsertAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Entity is null!");

            entity.CreationTime = DateTime.Now;

            await DbSet.AddAsync(entity);
            await CommitAsync();

            return entity;
        }
        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Entity is null!");

            entity.LastUpdateTime = DateTime.Now;
            //if its ISoftUpdatable , get deep copy of entity and insert it as a soft deleted with FKPreviousVersionID=entity.ID 
            if (IsSoftUpdatableEntity)
            {
                var dbResult = await DbSet.AsNoTracking().FirstOrDefaultAsync(x => x.ID == entity.ID);
                if (dbResult == null)
                    throw new ArgumentNullException($"There is no object in db whose ID is {entity.ID}. Check your object's ID");

                dbResult.ID = 0;
                (dbResult as ISoftUpdatableEntity).FKPreviousVersionID = entity.ID;
                (dbResult as ISoftUpdatableEntity).Deleted = true;
                (dbResult as ISoftUpdatableEntity).LastUpdateTime = null;

                await InsertAsync(dbResult);
                return entity;
            }
            await CommitAsync();
            return entity;
        }
        public virtual async Task<TEntity> DeleteAsync(TEntity entity)
        {
            if (entity == null)
                return null;

            if (IsSoftDeletableEntity)
            {
                entity.LastUpdateTime = DateTime.Now;
                (entity as ISoftDeletableEntity).Deleted = true;
            }
            else
                DbSet.Remove(entity);

            await CommitAsync();
            return entity;
        }
        public virtual async Task<TEntity> DeleteAsync(int id)
        {
            var entity = await DbSet.FindAsync(id);
            if (entity == null)
                return null;

            if (IsSoftDeletableEntity)
            {
                entity.LastUpdateTime = DateTime.Now;
                (entity as ISoftDeletableEntity).Deleted = true;
            }
            else
                DbSet.Remove(entity);

            await CommitAsync();
            return entity;
        }
        public virtual async Task<IEnumerable<TEntity>> DeleteAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null || !entities.Any())
                return entities;

            if (IsSoftDeletableEntity)// if entities are soft deletable
            {
                foreach (var entity in entities)
                {
                    entity.LastUpdateTime = DateTime.Now;
                    (entity as ISoftDeletableEntity).Deleted = true;// sign them as deletable                 
                }
            }
            else// if they are not,  remove them
                DbSet.RemoveRange(entities);

            await CommitAsync();
            return entities;
        }
        public virtual async Task<IEnumerable<TEntity>> DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = await AsQueryable().Where(predicate).ToListAsync();
            if (!entities.Any())
                return entities;

            if (IsSoftDeletableEntity)// if entities are soft deletable
            {
                foreach (var entity in entities)
                {
                    entity.LastUpdateTime = DateTime.Now;
                    (entity as ISoftDeletableEntity).Deleted = true;// sign them as deletable                 
                }
            }
            else// if they are not,  remove them
                DbSet.RemoveRange(entities);

            await CommitAsync();
            return entities;
        }     
        private async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
