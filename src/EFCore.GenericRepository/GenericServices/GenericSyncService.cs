using AutoMapper;
using EFCore.GenericRepository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFCore.GenericRepository.GenericServices
{
    public abstract class GenericSyncService<TContext, TEntity>
          where TContext : DbContext
          where TEntity : class, IBaseDbEntity
    {
        protected readonly IGenericRepository<TContext, TEntity> _genericRepo;

        public GenericSyncService(IGenericRepository<TContext, TEntity> genericRepo)
        {
            _genericRepo = genericRepo;
        }
        public virtual TEntity Get(int id)
        {
            return _genericRepo.Find(id);
        }
        public virtual List<TEntity> Get()
        {
            return _genericRepo.AsQueryable().ToList();
        }
        public virtual TEntity Insert(TEntity entity)
        {
            return _genericRepo.Insert(entity);
        }
        public virtual TEntity Update(TEntity entity)
        {
            return _genericRepo.Update(entity);
        }
        public virtual TEntity Delete(TEntity entity)
        {
            return _genericRepo.Delete(entity);
        }
        public virtual TEntity Delete(int id)
        {
            return _genericRepo.Delete(id);
        }
    }
}
