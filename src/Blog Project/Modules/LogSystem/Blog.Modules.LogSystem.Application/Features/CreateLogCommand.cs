using Blog.BuildingBlocks.Application.CQRS.Command;

namespace Blog.Modules.LogSystem.Application.Features
{
    public record CreateLogCommand(string Description) : ICommand<bool>
    {
        public Guid Id { get; }=Guid.NewGuid();
    }
}
