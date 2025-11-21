using Blog.BuildingBlocks.Infrastrocture.EventBus;
using Blog.Modules.Content.IntegrationEvents.Category.CreateCategory;
using MediatR;

namespace Blog.Modules.Content.Application.EventNotification.Content.Category
{
    public class CreateCategoryNotificationHandler(IEventsBus eventsBus) : INotificationHandler<CreateCategoryNotification>
    {
        public async Task Handle(CreateCategoryNotification notification, CancellationToken cancellationToken)
        {
            await eventsBus.Publish(new CreatedCategoryIntegrationEvent(
                                            notification.Id,
                                            DateTime.UtcNow,
                                            notification.DomainEvent.categoryId,
                                            notification.DomainEvent.categoryTitle));
        }
    }
}
