using Blog.BuildingBlocks.Model;

namespace Blog.BuildingBlocks.Infrastrocture.DomainEventsDispatching
{
    public interface IDomainEventsAccessor
    {
        IReadOnlyCollection<IDomainEvent> GetAllDomainEvents();

        void ClearAllDomainEvents();
    }
}
