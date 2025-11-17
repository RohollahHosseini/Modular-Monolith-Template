using Blog.BuildingBlocks.Infrastrocture.EventBus;
using Blog.BuildingBlocks.Infrastrocture.Inbox;
using Blog.BuildingBlocks.Infrastrocture.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Blog.Modules.LogSystem.Infrastrocture.Configuration.EventsBus
{
    internal class IntegrationEventGenericHandler<T> : IIntegrationEventHandler<T>
        where T : IntegrationEvent
    {

        private readonly IServiceScopeFactory _scopeFactory;

        public IntegrationEventGenericHandler(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public async Task Handle(T @event)
        {
            using var scope = _scopeFactory.CreateScope();
             var dbContext = scope.ServiceProvider.GetRequiredService<LogDbContext>();

            string type = @event.GetType().FullName!;
            var data = JsonConvert.SerializeObject(@event, new JsonSerializerSettings
            {
                ContractResolver = new AllPropertiesContractResolver()
            });

            InboxMessage inbox = new(@event.OccurredOn,type,data);

            await dbContext.InboxMessages.AddAsync(inbox);

            await dbContext.SaveChangesAsync();
        }
    }
}
