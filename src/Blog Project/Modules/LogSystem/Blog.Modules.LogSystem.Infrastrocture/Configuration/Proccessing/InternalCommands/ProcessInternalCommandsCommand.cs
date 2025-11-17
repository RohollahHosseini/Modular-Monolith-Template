using Blog.BuildingBlocks.Application.CQRS.Command;

namespace Blog.Modules.LogSystem.Infrastrocture.Configuration.Proccessing.InternalCommands
{
    internal class ProcessInternalCommandsCommand: CommandBase, IRecurringCommand
    {
    }
}
