namespace Blog.BuildingBlocks.Infrastrocture.Configuration.Proccessing.Inbox
{
    public class InboxMessageDto
    {
        public Guid Id { get; set; }

        public string Type { get; set; }

        public string Data { get; set; }
    }
}
