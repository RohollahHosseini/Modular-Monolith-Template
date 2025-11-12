using Blog.Modules.Content.IntegrationEvents.CreateBlog;
using Blog.Modules.LogSystem.Application.Proccessing.InternalCommand;
using MediatR;

namespace Blog.Modules.LogSystem.Application.EventHandler.Content.Blog
{
    public class CreateBlogIntegrationEventHandler(ILogCommandsScheduler commandsScheduler) : INotificationHandler<CreateBlogIntegrationEvent>
    {

        public async Task Handle(CreateBlogIntegrationEvent notification, CancellationToken cancellationToken)
        {
            await commandsScheduler.EnqueueAsync(new CreateBlogCommand(Guid.NewGuid(),notification.BlogId,notification.BlogTitle));
        }
    }
}
