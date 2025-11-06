namespace Blog.BuildingBlocks.Infrastrocture.DomainEventsDispatching
{
    public interface IDomainNotificationsMapper
    {
        string GetName(Type type);

        Type GetType(string name);
    }
}
