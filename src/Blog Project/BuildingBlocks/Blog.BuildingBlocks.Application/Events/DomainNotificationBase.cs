using Blog.BuildingBlocks.Model;

namespace Blog.BuildingBlocks.Application.Events
{
    public class DomainNotificationBase<T> : IDomainEventNotification<T>
        where T : IDomainEvent
    {
        public T DomainEvent { get; }

        public Guid Id { get; }

        public DomainNotificationBase(T domainEvent, Guid id)
        {
            Id = id;
            DomainEvent = domainEvent;
        }
    }
}
