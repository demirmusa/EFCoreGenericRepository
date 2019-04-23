using AutoMapper;
using EFCore.GenericRepository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.GenericRepository.GenericServices
{
    /// <summary>
    /// This use AutoMapper to map entity to dto and dto to entity.
    /// To use this you should add AutoMapper to your services and add create map from dto to entity, from entity to dto
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TEntityDto">input and output dto, it will map to Tentity</typeparam>
    public abstract class GenericAsyncService<TContext, TEntity, TEntityDto>
          where TContext : DbContext
          where TEntity : class, IBaseDbEntity
          where TEntityDto : class
    {
        protected readonly IGenericRepository<TContext, TEntity> _genericRepo;
        protected readonly IMapper _mapper;

        public GenericAsyncService(IGenericRepository<TContext, TEntity> genericRepo, IMapper mapper)
        {
            _genericRepo = genericRepo;
            this._mapper = mapper;
        }
        public virtual async Task<TEntityDto> Get(int id)
        {
            var result = await _genericRepo.FindAsync(id);
            return _mapper.Map<TEntityDto>(result);
        }
        public virtual async Task<List<TEntityDto>> Get()
        {
            var resultList = await _genericRepo.AsQueryable().ToListAsync();
            return _mapper.Map<List<TEntityDto>>(resultList);
        }
        public virtual async Task<TEntityDto> Insert(TEntityDto entityDto)
        {
            var entity = _mapper.Map<TEntity>(entityDto);
            var result = await _genericRepo.InsertAsync(entity);
            return _mapper.Map<TEntityDto>(result);
        }
        public virtual async Task<TEntityDto> Update(TEntityDto entityDto)
        {
            var entity = _mapper.Map<TEntity>(entityDto);
            var result = await _genericRepo.UpdateAsync(entity);
            return _mapper.Map<TEntityDto>(result);
        }
        public virtual async Task<TEntityDto> Delete(TEntityDto entityDto)
        {
            var entity = _mapper.Map<TEntity>(entityDto);
            var result = await _genericRepo.DeleteAsync(entity);

            return _mapper.Map<TEntityDto>(result);

        }
        public virtual async Task<TEntityDto> Delete(int id)
        {
            var result = await _genericRepo.DeleteAsync(id);
            return _mapper.Map<TEntityDto>(result);
        }
    }
}
