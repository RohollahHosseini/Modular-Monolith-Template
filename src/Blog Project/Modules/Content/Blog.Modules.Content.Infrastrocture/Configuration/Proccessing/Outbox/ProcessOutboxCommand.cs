using Blog.BuildingBlocks.Application.CQRS.Command;

namespace Blog.Modules.Content.Infrastrocture.Configuration.Proccessing.Outbox
{
    public class ProcessOutboxCommand: CommandBase,IRecurringCommand
    {
    }
}
