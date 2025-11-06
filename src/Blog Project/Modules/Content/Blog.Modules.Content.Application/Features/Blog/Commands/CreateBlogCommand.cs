using Blog.BuildingBlocks.Application.CQRS.Command;
using Blog.BuildingBlocks.Application.Events.Result;

namespace Blog.Modules.Content.Application.Features.Blog.Commands
{
    public record CreateBlogCommand(string BlogTitle, string BlogContent, Guid CategoryId) :ICommand<bool>;
}
