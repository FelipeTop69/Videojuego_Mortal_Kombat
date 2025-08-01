using Entity.Context;

namespace Web.Extensions
{
    public static class  EntitiesServiceExtensions
    {
        public static IServiceCollection AddEntitiesServices(this IServiceCollection services)
        {
            // Aquí solo necesitas registrar el DbContext
            services.AddDbContextFactory<AppDbContext>(); // o AddDbContext<AppDbContext>()

            return services;
        }
    }
}
