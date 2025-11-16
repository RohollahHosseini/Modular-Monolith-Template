using Blog.BuildingBlocks.Application.CQRS.Command;
using Blog.BuildingBlocks.Application.Proccessing.InternalCommand;

namespace Blog.Modules.Content.Infrastrocture.Configuration.Proccessing.InternalCommands
{
    public class CommandsScheduler(ContentDbcontext dbcontext) : ICommandsScheduler
    {
        public Task EnqueueAsync(ICommand command)
        {
            throw new NotImplementedException();
        }

        public Task EnqueueAsync<T>(ICommand<T> command)
        {
            throw new NotImplementedException();
        }
    }
}
