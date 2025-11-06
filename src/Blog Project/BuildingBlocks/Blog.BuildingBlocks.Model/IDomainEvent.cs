using Mediator;

namespace Blog.BuildingBlocks.Model
{
    public interface IDomainEvent:INotification
    {
        Guid Id { get; }
        DateTime OccurredOn { get; }
    }
}
