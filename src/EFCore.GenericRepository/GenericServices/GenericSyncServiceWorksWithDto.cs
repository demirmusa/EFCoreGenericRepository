using AutoMapper;
using EFCore.GenericRepository.interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFCore.GenericRepository.GenericServices
{
    /// <summary>
    /// This use AutoMapper to map entity to dto and dto to entity.
    /// To use this you should add AutoMapper to your services and add create map from dto to entity, from entity to dto
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TEntityDto">input and output dto, it will map to Tentity</typeparam>
    public class GenericSyncService<TContext, TEntity, TEntityDto>
         where TContext : DbContext
         where TEntity : class, IBaseDbEntity
         where TEntityDto : class
    {
        protected readonly IGenericRepository<TContext, TEntity> _genericRepo;
        private readonly IMapper _mapper;

        public GenericSyncService(IGenericRepository<TContext, TEntity> genericRepo, IMapper mapper)
        {
            _genericRepo = genericRepo;
            this._mapper = mapper;
        }
        public virtual TEntityDto Get(int id)
        {
            var result = _genericRepo.Find(id);
            return _mapper.Map<TEntityDto>(result);
        }
        public virtual List<TEntityDto> Get()
        {
            var resultList = _genericRepo.AsQueryable().ToList();
            return _mapper.Map<List<TEntityDto>>(resultList);
        }
        public virtual TEntityDto Insert(TEntityDto entityDto)
        {
            var entity = _mapper.Map<TEntity>(entityDto);
            var result = _genericRepo.Insert(entity);
            return _mapper.Map<TEntityDto>(result);
        }
        public virtual TEntityDto Update(TEntityDto entityDto)
        {
            var entity = _mapper.Map<TEntity>(entityDto);
            var result = _genericRepo.Update(entity);
            return _mapper.Map<TEntityDto>(result);
        }
        public virtual TEntityDto Delete(TEntityDto entityDto)
        {
            var entity = _mapper.Map<TEntity>(entityDto);
            var result = _genericRepo.Delete(entity);

            return _mapper.Map<TEntityDto>(result);

        }
        public virtual TEntityDto Delete(int id)
        {
            var result = _genericRepo.Delete(id);
            return _mapper.Map<TEntityDto>(result);
        }
    }
}
