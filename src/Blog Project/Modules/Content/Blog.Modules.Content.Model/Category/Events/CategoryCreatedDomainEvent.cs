using Blog.BuildingBlocks.Model;

namespace Blog.Modules.Content.Model.Category.Events
{
    public class CategoryCreatedDomainEvent(Guid categoryId,string categoryTitle):DomainEventBase
    {
        public Guid categoryId { get; } = categoryId;
        public string categoryTitle { get; } = categoryTitle;
    }
}
