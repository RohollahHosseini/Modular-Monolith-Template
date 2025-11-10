
using Autofac;
using Autofac.Core;
using Blog.BuildingBlocks.Application.Events;
using Blog.BuildingBlocks.Infrastrocture.Outbox;
using Blog.BuildingBlocks.Infrastrocture.Serialization;
using Blog.BuildingBlocks.Model;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Blog.BuildingBlocks.Infrastrocture.DomainEventsDispatching
{
    public class DomainEventsDispatcher(IMediator mediator, IOutbox outbox, IEnumerable<IDomainEventsAccessor> domainEventsAccessor, IServiceScopeFactory serviceScopeFactory, IDomainNotificationsMapper domainNotificationsMapper) : IDomainEventsDispatcher
    {
        public async Task DispatchEventsAsync()
        {
            //var domainEvents=domainEventsAccessor.GetAllDomainEvents();

            var domainEvents = domainEventsAccessor
      .SelectMany(a => a.GetAllDomainEvents())
      .ToList();

            List<IDomainEventNotification<IDomainEvent>> domainEventNotifications = [];

            using var scope2= serviceScopeFactory.CreateScope();

            var provider = scope2.ServiceProvider;

            foreach (var domainEvent in domainEvents)
            {
                Type domainEvenNotificationType = typeof(IDomainEventNotification<>);
                var domainNotificationWithGenericType = domainEvenNotificationType.MakeGenericType(domainEvent.GetType());

                var domainNotification = ActivatorUtilities.CreateInstance(
                    provider,
                    domainNotificationWithGenericType,
                    domainEvent,
                    domainEvent.Id
                ) as IDomainEventNotification<IDomainEvent>;

                if (domainNotification != null)
                    domainEventNotifications.Add(domainNotification);

                //var domainNotification = scope.ResolveOptional(domainNotificationWithGenericType, new List<Parameter>
                //{
                //    new NamedParameter("domainEvent", domainEvent),
                //    new NamedParameter("id", domainEvent.Id)
                //});

                //if (domainNotification != null)
                //{
                //    domainEventNotifications.Add(domainNotification as IDomainEventNotification<IDomainEvent>);
                //}
            }
            //  domainEventsAccessor.ClearAllDomainEvents();

            foreach (var accessor in domainEventsAccessor)
                accessor.ClearAllDomainEvents();

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
