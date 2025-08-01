using Entity.Context;
using Microsoft.EntityFrameworkCore;
using Utilities.Enums;

namespace Web.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<AppDbContext>(provider =>
            {
                var httpContext = provider.GetService<IHttpContextAccessor>()?.HttpContext;
                var configuration = provider.GetRequiredService<IConfiguration>();

                var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

                var dbProvider = DatabaseProviderType.SqlServer;

                if (httpContext != null &&
                    httpContext.Items.TryGetValue("DatabaseProvider", out var providerObj) &&
                    providerObj is DatabaseProviderType providerType)
                {
                    dbProvider = providerType;
                }

                string connectionName = dbProvider switch
                {
                    DatabaseProviderType.SqlServer => "DefaultConnection",
                    _ => throw new InvalidOperationException($"Proveedor de base de datos no soportado: {dbProvider}")
                };

                string? connectionString = configuration.GetConnectionString(connectionName);
                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    throw new InvalidOperationException($"No se encontró la cadena de conexión '{connectionName}'.");
                }

                switch (dbProvider)
                {
                    case DatabaseProviderType.SqlServer:
                        optionsBuilder.UseSqlServer(connectionString,
                            b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName));
                        break;
                }

                return new AppDbContext(optionsBuilder.Options, configuration);
            });

            return services;
        }
    }
}