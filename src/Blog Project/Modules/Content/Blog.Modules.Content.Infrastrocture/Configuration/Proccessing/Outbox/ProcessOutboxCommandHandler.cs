using Blog.BuildingBlocks.Application.CQRS.Command;
using Blog.BuildingBlocks.Application.Events;
using Blog.BuildingBlocks.Infrastrocture.DomainEventsDispatching;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Blog.Modules.Content.Infrastrocture.Configuration.Proccessing.Outbox
{
    public class ProcessOutboxCommandHandler(ContentDbcontext dbcontext,IMediator _mediator, IDomainNotificationsMapper _domainNotificationsMapper) : ICommandHandler<ProcessOutboxCommand>
    {
        public async Task Handle(ProcessOutboxCommand request, CancellationToken cancellationToken)
        {
            var outboxResults = await dbcontext.OutboxMessages
                .Where(c => c.ProcessedDate == null)
                .ToListAsync(cancellationToken);

            if (outboxResults.Count > 0)
            {
                foreach (var outbox in outboxResults)
                {
                    var type = _domainNotificationsMapper.GetType(outbox.Type);
                    var @event = JsonConvert.DeserializeObject(outbox.Data, type) as IDomainEventNotification;

                    //using (LogContext.Push(new OutboxMessageContextEnricher(@event)))
                    //{
                     await _mediator.Publish(@event, cancellationToken);

                    await dbcontext.OutboxMessages
                        .Where(c => c.Id == outbox.Id)
                        .ExecuteUpdateAsync(set => set.SetProperty(c => c.ProcessedDate, DateTime.UtcNow),cancellationToken);


                    //}
                }
                await dbcontext.SaveChangesAsync(cancellationToken);
            }
        }

        //private class OutboxMessageContextEnricher : ILogEventEnricher
        //{
        //    private readonly IDomainEventNotification _notification;

        //    public OutboxMessageContextEnricher(IDomainEventNotification notification)
        //    {
        //        _notification = notification;
        //    }

        //    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        //    {
        //        logEvent.AddOrUpdateProperty(new LogEventProperty("Context", new ScalarValue($"OutboxMessage:{_notification.Id.ToString()}")));
        //    }
        //}
    }
}
