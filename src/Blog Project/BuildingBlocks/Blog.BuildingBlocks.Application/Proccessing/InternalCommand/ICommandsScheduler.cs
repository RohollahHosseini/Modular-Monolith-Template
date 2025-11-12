using Blog.BuildingBlocks.Application.CQRS.Command;

namespace Blog.BuildingBlocks.Application.Proccessing.InternalCommand
{
    public interface ICommandsScheduler
    {
        Task EnqueueAsync(ICommand command);

        Task EnqueueAsync<T>(ICommand<T> command);
    }
}
