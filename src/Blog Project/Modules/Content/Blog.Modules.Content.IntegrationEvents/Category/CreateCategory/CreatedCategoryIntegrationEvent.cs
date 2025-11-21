using Blog.BuildingBlocks.Infrastrocture.EventBus;

namespace Blog.Modules.Content.IntegrationEvents.Category.CreateCategory
{
    public class CreatedCategoryIntegrationEvent(Guid Id, DateTime OccurredOn,Guid CategoryId,string CategoryName): IntegrationEvent(Id,OccurredOn)
    {
        public Guid CategoryId { get; } = CategoryId;
        public string CategoryName { get; } = CategoryName;

    }
}
