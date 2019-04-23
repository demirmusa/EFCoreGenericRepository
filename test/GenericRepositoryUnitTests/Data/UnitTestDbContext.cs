using GenericRepositoryUnitTests.Data.DbEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenericRepositoryUnitTests.Data
{
    public class UnitTestDbContext : DbContext
    {
        public UnitTestDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Person_SoftUpdatableDbEntity> Person_SoftUpdatableDbEntity { get; set; }
    }
}
