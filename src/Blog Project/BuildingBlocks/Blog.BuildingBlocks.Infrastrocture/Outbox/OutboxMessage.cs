namespace Blog.BuildingBlocks.Infrastrocture.Outbox
{
    public class OutboxMessage
    {
        public required Guid Id { get; set; }

        public required DateTime OccurredOn { get; set; }

        public required string Type { get; set; }

        public required string Data { get; set; }

        public DateTime? ProcessedDate { get; set; }

        //private OutboxMessage()
        //{
        //}
    }
}
