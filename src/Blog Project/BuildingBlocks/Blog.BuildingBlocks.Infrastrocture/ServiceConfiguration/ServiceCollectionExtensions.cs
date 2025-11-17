using Blog.BuildingBlocks.Infrastrocture.DomainEventsDispatching;
using Blog.BuildingBlocks.Infrastrocture.EventBus;
using Blog.BuildingBlocks.Infrastrocture.InternalCommands;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.BuildingBlocks.Infrastrocture.ServiceConfiguration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ServiceCollectionExtensionsBuildingBlock(this IServiceCollection services)
        {
            services.AddScoped<IDomainEventsDispatcher, DomainEventsDispatcher>();
            services.AddScoped<IEventsBus, InMemoryEventBusClient>();
            services.AddScoped<IDomainNotificationsMapper, DomainNotificationsMapper>();
            services.AddScoped<IInternalCommandsMapper, InternalCommandsMapper>();

            return services;
        }

    }
}
