using Blog.BuildingBlocks.Application.CQRS.Command;
using Blog.BuildingBlocks.Application.Events.Result;

namespace Blog.Modules.LogSystem.Application.Features
{
    public record CreateLogCommand(string Description):ICommand<bool>;
}
