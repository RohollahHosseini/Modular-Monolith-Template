using Blog.BuildingBlocks.Model;
using Microsoft.EntityFrameworkCore;

namespace Blog.BuildingBlocks.Infrastrocture.DomainEventsDispatching
{
    public class DomainEventsAccessor(DbContext dbContext) : IDomainEventsAccessor
    {
        public IReadOnlyCollection<IDomainEvent> GetAllDomainEvents()
        {
            var domainEntities = dbContext.ChangeTracker
               .Entries<Entity>()
               .Where(x => x.Entity.DomainEventes != null && x.Entity.DomainEventes.Any()).ToList();

            return domainEntities
                .SelectMany(x => x.Entity.DomainEventes)
                .ToList();
        }
        public void ClearAllDomainEvents()
        {
            var domainEntities = dbContext.ChangeTracker
              .Entries<Entity>()
              .Where(x => x.Entity.DomainEventes != null && x.Entity.DomainEventes.Any()).ToList();

            domainEntities
                .ForEach(entity => entity.Entity.ClearDomainEvent());
        }
    }
}
