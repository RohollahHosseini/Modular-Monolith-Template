using Blog.BuildingBlocks.Application.CQRS.Command;

namespace Blog.Modules.Content.Infrastrocture.Configuration.Proccessing.Inbox
{
    public class ProcessInboxCommand : CommandBase, IRecurringCommand
    {
    }
}
