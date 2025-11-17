using Blog.BuildingBlocks.Infrastrocture.EventBus;
using Blog.Modules.Content.IntegrationEvents.CreateBlog;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Blog.Modules.LogSystem.Infrastrocture.Configuration.EventsBus
{
    internal class EventsBusStartup
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        private readonly IEventsBus _eventBus;

        public EventsBusStartup(IServiceScopeFactory serviceScopeFactory,IEventsBus eventsBus)
        {
                _serviceScopeFactory = serviceScopeFactory;
            _eventBus = eventsBus;

        }


        internal  void Initialize()
        {
            SubscribeToIntegrationEvents();
        }

        private  void SubscribeToIntegrationEvents()
        {

            SubscribeToIntegrationEvent<CreateBlogIntegrationEvent>();
        }

        private  void SubscribeToIntegrationEvent<T>()
            where T : IntegrationEvent
        {
            //logger.Information("Subscribe to {@IntegrationEvent}", typeof(T).FullName);
            _eventBus.Subscribe(
                new IntegrationEventGenericHandler<T>(_serviceScopeFactory));
        }

    }
}
