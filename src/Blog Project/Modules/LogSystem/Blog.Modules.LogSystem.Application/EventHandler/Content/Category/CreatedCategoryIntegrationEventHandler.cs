using Blog.Modules.Content.IntegrationEvents.Category.CreateCategory;
using Blog.Modules.LogSystem.Application.Features;
using Blog.Modules.LogSystem.Application.Features.Category;
using Blog.Modules.LogSystem.Application.Proccessing.InternalCommand;
using MediatR;

namespace Blog.Modules.LogSystem.Application.EventHandler.Content.Category
{
    internal class CreatedCategoryIntegrationEventHandler(ILogCommandsScheduler  commandsScheduler) : INotificationHandler<CreatedCategoryIntegrationEvent>
    {
        public async Task Handle(CreatedCategoryIntegrationEvent notification, CancellationToken cancellationToken)
        {
            await commandsScheduler.EnqueueAsync(
                new CreateLogForCategoryCommand($"Content Module - Create New Category CategoryId={notification.CategoryId} , Category Title={notification.CategoryName}",notification.Id));

        }
    }
}
