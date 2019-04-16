using Example.Data.DbEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Example.Data
{
    public class ExampleDbContext : DbContext
    {
        public ExampleDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Person_SoftDeletable> Person_SoftDeletable { get; set; }
        public DbSet<Person_BaseDbEntity> Person_BaseDbEntity { get; set; }
        public DbSet<Person_SoftUpdatable> Person_SoftUpdatable { get; set; }
    }
}
