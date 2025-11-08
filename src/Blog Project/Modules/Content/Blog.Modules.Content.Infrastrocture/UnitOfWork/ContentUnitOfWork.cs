using Blog.BuildingBlocks.Infrastrocture;
using Blog.BuildingBlocks.Infrastrocture.DomainEventsDispatching;
using Blog.Modules.Content.Application.Conteracts.UnitOfWork;

namespace Blog.Modules.Content.Infrastrocture.UnitOfWork
{
    public class ContentUnitOfWork(ContentDbcontext context, IDomainEventsDispatcher domainEventsDispatcher) : IContentUnitOfWork
    {
        public async Task<int> CommitAsync(CancellationToken cancellationToken = default, Guid? internalCommandId = null)
        {
            await domainEventsDispatcher.DispatchEventsAsync();
            return await context.SaveChangesAsync(cancellationToken);
        }
    }
}
