namespace Blog.BuildingBlocks.Model
{
    public class InternalCommandBaseEntity:BaseEntity<Guid>
    {
        public DateTime EnqueueDate { get; set; }
        public string Type { get; set; } = null!;
        public string Data { get; set; } = null!;
        public DateTime? ProcessedDate { get; set; }
        public string? Error { get; set; }
    }
}
