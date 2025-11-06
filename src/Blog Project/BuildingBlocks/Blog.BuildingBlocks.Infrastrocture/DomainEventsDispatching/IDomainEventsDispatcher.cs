namespace Blog.BuildingBlocks.Infrastrocture.DomainEventsDispatching
{
    public interface IDomainEventsDispatcher
    {
        Task DispatchEventsAsync();
    }
}
