using Blog.BuildingBlocks.Application.CQRS.Command;

namespace Blog.Modules.Content.Application.Features.Blog.Commands
{
    public record CreateBlogCommand(string BlogTitle, string BlogContent, Guid CategoryId) : ICommand<bool>
    {
        public Guid Id { get; }=Guid.NewGuid(); 
    }
}
