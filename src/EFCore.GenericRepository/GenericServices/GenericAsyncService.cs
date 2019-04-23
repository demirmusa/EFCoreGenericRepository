using AutoMapper;
using EFCore.GenericRepository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.GenericRepository.GenericServices
{
    public abstract class GenericAsyncService<TContext, TEntity>
        where TContext : DbContext
        where TEntity : class, IBaseDbEntity
    {
        protected readonly IGenericRepository<TContext, TEntity> _genericRepo;

        public GenericAsyncService(IGenericRepository<TContext, TEntity> genericRepo)
        {
            _genericRepo = genericRepo;
        }
        public virtual async Task<TEntity> Get(int id)
        {
            return await _genericRepo.FindAsync(id);
        }

        public virtual async Task<List<TEntity>> Get()
        {
            return await _genericRepo.AsQueryable().ToListAsync();
        }

        public virtual async Task<TEntity> Insert(TEntity entity)
        {
            return await _genericRepo.InsertAsync(entity);
        }
        public virtual async Task<TEntity> Update(TEntity entity)
        {
            return await _genericRepo.UpdateAsync(entity);
        }
        public virtual async Task<TEntity> Delete(TEntity entity)
        {
            return await _genericRepo.DeleteAsync(entity);
        }
        public virtual async Task<TEntity> Delete(int id)
        {
            return await _genericRepo.DeleteAsync(id);
        }
    }
    
}
