using EFCore.GenericRepository;
using EFCore.GenericRepository.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Example.Data.Interfaces
{
    public interface IExampleDbContextGenericRepository<TEntity> : IGenericRepository<ExampleDbContext, TEntity>
       where TEntity : BaseDbEntity
    {

    }
}
