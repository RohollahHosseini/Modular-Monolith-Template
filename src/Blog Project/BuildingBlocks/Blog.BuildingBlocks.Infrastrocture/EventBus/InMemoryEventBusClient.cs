using Microsoft.Extensions.Logging;

namespace Blog.BuildingBlocks.Infrastrocture.EventBus
{
    public class InMemoryEventBusClient : IEventsBus
    {
        //private readonly ILogger _logger;

        //public InMemoryEventBusClient(ILogger logger)
        //{
        //    _logger = logger;
        //}

        public void Dispose()
        {
        }

        public async Task Publish<T>(T @event) where T : IntegrationEvent
        {
           // throw new NotImplementedException();
            //_logger.Information("Publishing {Event}", @event.GetType().FullName);
            await InMemoryEventBus.Instance.Publish(@event);

        }

        public void StartConsuming()
        {
        }

        public void Subscribe<T>(IIntegrationEventHandler<T> handler) where T : IntegrationEvent
        {
            InMemoryEventBus.Instance.Subscribe(handler);
        }
    }
}
