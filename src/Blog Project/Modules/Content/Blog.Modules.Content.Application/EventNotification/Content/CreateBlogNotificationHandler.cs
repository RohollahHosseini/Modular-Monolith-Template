using Blog.BuildingBlocks.Infrastrocture.EventBus;
using Blog.Modules.Content.IntegrationEvents.CreateBlog;
using MediatR;

namespace Blog.Modules.Content.Application.EventNotification.Content
{
    public class CreateBlogNotificationHandler(IEventsBus eventsBus) :
        INotificationHandler<CreateBlogNotification>
    {
        public async Task Handle(CreateBlogNotification notification, CancellationToken cancellationToken)
        {
            await eventsBus.Publish(new CreateBlogIntegrationEvent(
                notification.Id,
                notification.DomainEvent.OccurredOn, 
                notification.DomainEvent.BlogId, 
                notification.DomainEvent.BlogTitle));

        }
    }
}
