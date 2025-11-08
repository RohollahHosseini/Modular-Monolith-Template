using Blog.BuildingBlocks.Infrastrocture.DomainEventsDispatching;
using Blog.Modules.LogSystem.Application.Contracts.UnitOfWork;

namespace Blog.Modules.LogSystem.Infrastrocture.UnitOfWork
{
    internal class LogUnitOfWork(LogDbContext context, IDomainEventsDispatcher domainEventsDispatcher) : ILogUnitOfWork
    {
        public async Task<int> CommitAsync(CancellationToken cancellationToken = default, Guid? internalCommandId = null)
        {
            await domainEventsDispatcher.DispatchEventsAsync();
            return await context.SaveChangesAsync(cancellationToken);
        }
    }
}
