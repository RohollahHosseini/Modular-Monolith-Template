using Blog.BuildingBlocks.Application.CQRS.Command;

namespace Blog.Modules.LogSystem.Infrastrocture.Configuration.Proccessing.Inbox
{
    public class ProcessInboxCommand : CommandBase, IRecurringCommand
    {
    }
}
