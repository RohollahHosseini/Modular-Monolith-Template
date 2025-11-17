using Blog.BuildingBlocks.Application.CQRS.Command;
using Blog.BuildingBlocks.Application.Proccessing.InternalCommand;

namespace Blog.Modules.LogSystem.Application.Features
{
    public record CreateLogCommand(string Description,Guid id)
        : InternalCommandBase<bool>(id)
    {
    }
}
