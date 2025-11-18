using Blog.BuildingBlocks.Application.Events;
using Blog.Modules.Content.Model.Blog.Events;

namespace Blog.Modules.Content.Application.EventNotification.Content.Blog
{
    public class CreateBlogNotification
        (BlogCreatedDomainEvent DomainEvent, Guid Id) : DomainNotificationBase<BlogCreatedDomainEvent>(DomainEvent, Id)
    {
    }
}
