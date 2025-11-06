using Blog.BuildingBlocks.Infrastrocture.Outbox;

namespace Blog.Modules.Content.Infrastrocture.Outbox
{
    internal class OutboxAccessor(ContentDbcontext context) : IOutbox
    {
        public void Add(OutboxMessage message)
        {
            context.OutboxMessages.Add(message);
        }

        public Task Save()
        {
            // Save is done automatically using EF Core Change Tracking mechanism during SaveChanges.
            return Task.CompletedTask;
        }
    }
}
