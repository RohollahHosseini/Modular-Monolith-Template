using Blog.BuildingBlocks.Infrastrocture.EventBus;

namespace Blog.Modules.Content.IntegrationEvents.CreateBlog
{
    public class CreateBlogIntegrationEvent(Guid Id,DateTime OccurredOn,Guid BlogId,string BlogTitle) :IntegrationEvent(Id, OccurredOn)
    {

        public Guid BlogId { get; set; }= BlogId;
        public string BlogTitle { get; set; }= BlogTitle;
    }
}
