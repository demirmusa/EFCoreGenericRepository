using EFCore.GenericRepository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace EFCore.GenericRepository
{

    public partial class GenericRepository<TContext, TEntity>
    {
        public virtual TEntity Find(int id)
        {
            return DbSet.Find(id);
        }
        /// <summary>
        ///  Determines whether any element of a sequence satisfies a condition.
        ///  Does not query deleted signed entities
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            return AsQueryable().Any(predicate);
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
        public virtual TEntity Insert(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Entity is null!");

            entity.CreationTime = DateTime.Now;

            DbSet.Add(entity);
            Commit();

            return entity;
        }
        public virtual TEntity Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Entity is null!");

            entity.LastUpdateTime = DateTime.Now;
            //if its ISoftUpdatable , get deep copy of entity and insert it as a soft deleted with FKPreviousVersionID=entity.ID 
            if (IsSoftUpdatableEntity)
            {
                var dbResult = DbSet.AsNoTracking().FirstOrDefault(x => x.ID == entity.ID);
                if (dbResult == null)
                    throw new ArgumentNullException($"There is no object in db whose ID is {entity.ID}. Check your object's ID");


                dbResult.ID = 0;
                (dbResult as ISoftUpdatableEntity).FKPreviousVersionID = entity.ID;
                (dbResult as ISoftUpdatableEntity).Deleted = true;
                (dbResult as ISoftUpdatableEntity).LastUpdateTime = null;

                Insert(dbResult);
                return entity;
            }

            Commit();
            return entity;
        }
        public virtual TEntity Delete(TEntity entity)
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

            Commit();
            return entity;
        }
        public virtual TEntity Delete(int id)
        {
            var entity = DbSet.Find(id);
            if (entity == null)
                return null;

            if (IsSoftDeletableEntity)
            {
                entity.LastUpdateTime = DateTime.Now;
                (entity as ISoftDeletableEntity).Deleted = true;
            }
            else
                DbSet.Remove(entity);

            Commit();
            return entity;
        }
        public virtual IEnumerable<TEntity> Delete(IEnumerable<TEntity> entities)
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

            Commit();
            return entities;
        }
        public virtual IEnumerable<TEntity> Delete(Expression<Func<TEntity, bool>> predicate)
        {

            var entities = AsQueryable().Where(predicate).ToList();
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

            Commit();
            return entities;
        }
        private void Commit()
        {
            _context.SaveChanges();
        }
    }
}
