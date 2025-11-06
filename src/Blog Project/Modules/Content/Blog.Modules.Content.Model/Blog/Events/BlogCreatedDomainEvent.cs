using Blog.BuildingBlocks.Model;

namespace Blog.Modules.Content.Model.Blog.Events
{
    public class BlogCreatedDomainEvent(Guid blogId,string blogTitle) : DomainEventBase
    {

        public Guid BlogId{ get; init; }=blogId;
        public string BlogTitle { get; init; }= blogTitle;
    }
}
