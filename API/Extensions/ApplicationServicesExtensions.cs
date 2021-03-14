using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            // Adding the product repository , will be replaced by the Generic Repository 
            services.AddScoped<IProductRepository, ProductRepository>();

            // Adding the generic repo, the data type occurs at complie time 
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            return services;

        }

    }
}