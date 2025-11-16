using Blog.BuildingBlocks.Application.Events;
using Blog.Modules.Content.Model.Blog.Events;

namespace Blog.Modules.Content.Application.EventNotification.Content
{
    public class CreateBlogNotification
        (BlogCreatedDomainEvent blogCreatedEvent, Guid Id) : DomainNotificationBase<BlogCreatedDomainEvent>(blogCreatedEvent,Id)
    {
        public BlogCreatedDomainEvent DomainEvent { get; set; } = blogCreatedEvent;
        public Guid Id { get;set; }=Id;
    }
}
