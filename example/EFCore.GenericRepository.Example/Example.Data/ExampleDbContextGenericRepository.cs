using EFCore.GenericRepository;
using Example.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Example.Data
{
    public class ExampleDbContextGenericRepository<TEntity> : GenericRepository<ExampleDbContext, TEntity>, IExampleDbContextGenericRepository<TEntity>
      where TEntity : BaseDbEntity
    {
        public ExampleDbContextGenericRepository(ExampleDbContext context) : base(context)
        {
            context.Database.EnsureCreated();
        }
    }
}
