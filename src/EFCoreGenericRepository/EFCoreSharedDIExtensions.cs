using EFCoreGenericRepository.interfaces;
using Microsoft.Extensions.DependencyInjection;
namespace EFCoreGenericRepository
{
    public static class EFCoreSharedDIExtensions
    {
        public static IServiceCollection AddGenericRepositoryScoped(this IServiceCollection collection)
        {
            collection.AddScoped(typeof(IGenericRepository<,>),
                typeof(GenericRepository<,>));
            return collection;
        }
        public static IServiceCollection AddGenericRepositoryTransient(this IServiceCollection collection)
        {
            collection.AddTransient(typeof(IGenericRepository<,>),
                typeof(GenericRepository<,>));
            return collection;
        }
        public static IServiceCollection AddGenericRepositorySingleton(this IServiceCollection collection)
        {
            collection.AddSingleton(typeof(IGenericRepository<,>),
                typeof(GenericRepository<,>));
            return collection;
        }
    }
}
