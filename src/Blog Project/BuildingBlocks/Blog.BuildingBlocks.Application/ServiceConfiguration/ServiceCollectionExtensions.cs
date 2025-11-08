using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Blog.BuildingBlocks.Application.ServiceConfiguration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ServiceCollectionExtensionsBuildingBlockApplication(this IServiceCollection services, Assembly[] moduleAssemblies)
        {

            services.AddMediatR(config => 
            {
                config.RegisterServicesFromAssemblies(moduleAssemblies);
            });

            return services;
        }
    }
}
