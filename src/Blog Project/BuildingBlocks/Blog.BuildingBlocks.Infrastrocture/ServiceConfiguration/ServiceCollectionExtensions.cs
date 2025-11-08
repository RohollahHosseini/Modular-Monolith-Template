using Blog.BuildingBlocks.Infrastrocture.DomainEventsDispatching;
using Blog.BuildingBlocks.Infrastrocture.Outbox;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.BuildingBlocks.Infrastrocture.ServiceConfiguration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ServiceCollectionExtensionsBuildingBlock(this IServiceCollection services)
        {
            services.AddScoped<IDomainEventsDispatcher, DomainEventsDispatcher>();

            return services;
        }

    }
}
