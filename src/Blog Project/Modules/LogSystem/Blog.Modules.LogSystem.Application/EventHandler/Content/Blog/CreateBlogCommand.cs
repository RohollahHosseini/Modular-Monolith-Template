using Blog.BuildingBlocks.Application.Proccessing.InternalCommand;

namespace Blog.Modules.LogSystem.Application.EventHandler.Content.Blog
{
    public class CreateBlogCommand(Guid Id,Guid BlogId,string BlogTitle):InternalCommandBase<Guid>(Id)
    {
        public Guid BlogId { get; }=BlogId;
        public string BlogTitle { get; } = BlogTitle;

    }
}
