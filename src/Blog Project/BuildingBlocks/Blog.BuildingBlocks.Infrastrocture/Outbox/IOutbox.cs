namespace Blog.BuildingBlocks.Infrastrocture.Outbox
{
    public  interface IOutbox
    {
        void Add(OutboxMessage message);

        Task Save();
    }
}
