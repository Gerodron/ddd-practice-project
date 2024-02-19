using App.Domain.Alquileres.Services;
using Microsoft.Extensions.DependencyInjection;

namespace App.Application
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(config => {
                config.RegisterServicesFromAssembly(typeof(DependecyInjection).Assembly);
            });

            services.AddTransient<PrecioDomainService>();


            return services;
        }
    }
}
