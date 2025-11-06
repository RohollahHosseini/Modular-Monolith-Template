using Blog.BuildingBlocks.Infrastrocture.DomainEventsDispatching;
using Microsoft.EntityFrameworkCore;

namespace Blog.BuildingBlocks.Infrastrocture
{
    public class UnitOfWork
        (DbContext context, IDomainEventsDispatcher domainEventsDispatcher) : IUnitOfWork
    {

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default, Guid? internalCommandId = null)
        {
            await domainEventsDispatcher.DispatchEventsAsync();

            return await context.SaveChangesAsync(cancellationToken);
        }
        
    }
}
