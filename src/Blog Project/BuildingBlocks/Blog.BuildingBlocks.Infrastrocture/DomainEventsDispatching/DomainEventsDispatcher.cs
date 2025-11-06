
using Autofac;
using Autofac.Core;
using Blog.BuildingBlocks.Application.Events;
using Blog.BuildingBlocks.Infrastrocture.Outbox;
using Blog.BuildingBlocks.Infrastrocture.Serialization;
using Blog.BuildingBlocks.Model;
using Mediator;
using Newtonsoft.Json;

namespace Blog.BuildingBlocks.Infrastrocture.DomainEventsDispatching
{
    public class DomainEventsDispatcher(IMediator mediator, IOutbox outbox, IDomainEventsAccessor domainEventsAccessor, ILifetimeScope scope, IDomainNotificationsMapper domainNotificationsMapper) : IDomainEventsDispatcher
    {
        public async Task DispatchEventsAsync()
        {
            var domainEvents=domainEventsAccessor.GetAllDomainEvents();

            List<IDomainEventNotification<IDomainEvent>> domainEventNotifications = [];
            foreach (var domainEvent in domainEvents)
            {
                Type domainEvenNotificationType = typeof(IDomainEventNotification<>);
                var domainNotificationWithGenericType = domainEvenNotificationType.MakeGenericType(domainEvent.GetType());
                var domainNotification = scope.ResolveOptional(domainNotificationWithGenericType, new List<Parameter>
                {
                    new NamedParameter("domainEvent", domainEvent),
                    new NamedParameter("id", domainEvent.Id)
                });

                if (domainNotification != null)
                {
                    domainEventNotifications.Add(domainNotification as IDomainEventNotification<IDomainEvent>);
                }
            }
            domainEventsAccessor.ClearAllDomainEvents();

            foreach (var domainEvent in domainEvents)
            {
                await mediator.Publish(domainEvent);
            }

            foreach (var domainEventNotification in domainEventNotifications)
            {
                var type = domainNotificationsMapper.GetName(domainEventNotification.GetType());
                var data = JsonConvert.SerializeObject(domainEventNotification, new JsonSerializerSettings
                {
                    ContractResolver = new AllPropertiesContractResolver()
                });

                var outboxMessage = new OutboxMessage
                {
                    Id = domainEventNotification.Id,
                    OccurredOn= domainEventNotification.DomainEvent.OccurredOn,
                    Type= type,
                    Data=data 
                };
                

                outbox.Add(outboxMessage);
            }
        }
    }
}
