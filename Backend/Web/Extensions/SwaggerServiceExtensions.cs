using Microsoft.OpenApi.Models;

namespace Web.Extensions
{
    public static class SwaggerServiceExtensions
    {
        public static IServiceCollection AddSwaggerWithJwtSupport(this IServiceCollection services)
        {
            services.AddSwaggerGen();
            return services;
        }
    }
}
