using Blog.BuildingBlocks.Application.Events;
using Blog.Modules.Content.Model.Category.Events;

namespace Blog.Modules.Content.Application.EventNotification.Content.Category
{
    public class CreateCategoryNotification(CategoryCreatedDomainEvent DomainEvent,Guid Id): DomainNotificationBase<CategoryCreatedDomainEvent>(DomainEvent,Id)
    {
    }
}
