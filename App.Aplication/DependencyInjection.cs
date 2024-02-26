using App.Domain.Alquileres.Services;
using Microsoft.Extensions.DependencyInjection;

namespace App.Application
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            });

            services.AddTransient<PrecioDomainService>();   

            return services;
        }
    }
}
