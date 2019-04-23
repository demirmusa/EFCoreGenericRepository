using EFCore.GenericRepository.Interfaces;
using Microsoft.Extensions.DependencyInjection;
namespace EFCore.GenericRepository
{
    public static class EFCoreSharedDIExtensions
    {
        public static IServiceCollection AddGenericRepositoryScoped(this IServiceCollection services)
        {            
            services.AddScoped(typeof(IGenericRepository<,>),
                typeof(GenericRepository<,>));
            return services;
        }
        public static IServiceCollection AddGenericRepositoryTransient(this IServiceCollection services)
        {
            services.AddTransient(typeof(IGenericRepository<,>),
                typeof(GenericRepository<,>));
            return services;
        }
        public static IServiceCollection AddGenericRepositorySingleton(this IServiceCollection services)
        {
            services.AddSingleton(typeof(IGenericRepository<,>),
                typeof(GenericRepository<,>));
            return services;
        }
    }
}
