using EFCore.GenericRepository.interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EFCore.GenericRepository.BasicExample
{
    ///-------- generate your own generic repo
    public interface IExampleDbContextGenericRepository<TEntity> : IGenericRepository<BasicExampleDbContext, TEntity>
            where TEntity : BaseDbEntity
    {

    }
    public class ExampleDbContextGenericRepository<TEntity> : GenericRepository<BasicExampleDbContext, TEntity>, IExampleDbContextGenericRepository<TEntity>
            where TEntity : BaseDbEntity
    {
        public ExampleDbContextGenericRepository(BasicExampleDbContext context) : base(context)
        {
            context.Database.EnsureCreated();
        }
    }
    ///-------- generate your own generic repo


    ///-------- initialize db entity and context

    //when you delete this entity it will sing it as a deleted.
    //when you update this entity it will create one copy of it which previous id refer this updated one
    // You can also use ISoftUpdatableDbEntity interface and fill needed properties
    public class Person_SoftUpdatableDbEntity : SoftUpdatableDbEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
    public class BasicExampleDbContext : DbContext
    {
        public BasicExampleDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Person_SoftUpdatableDbEntity> Person_SoftUpdatableDbEntity { get; set; }
    }

    ///-------- initialize db entity and context
    
    class Program
    {
        static void Main(string[] args)
        {
            ///-------- dipendency injection stuffs
            
            var services = new ServiceCollection();

            services.AddDbContext<BasicExampleDbContext>(options =>
             options.UseSqlServer("Data Source=LAPTOP-3O58F4FN;database=efcoregenericrepobasictestdb;trusted_connection=yes;"));

            services.AddScoped(typeof(IExampleDbContextGenericRepository<>), typeof(ExampleDbContextGenericRepository<>));


            var provider = services.BuildServiceProvider();

            var _personRepo = provider.GetRequiredService<IExampleDbContextGenericRepository<Person_SoftUpdatableDbEntity>>();

            ///-------- dipendency injection stuffs


            var inserted = _personRepo.Insert(new Person_SoftUpdatableDbEntity { Name = "musa", Surname = "demir" });

            inserted.Surname = "DEMIR";

            var updated = _personRepo.Update(inserted);

            var deleted = _personRepo.Delete(updated.ID);

            Console.WriteLine("All Done.");
            Console.Read();
        }
    }
}
