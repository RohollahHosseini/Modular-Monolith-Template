using Blog.BuildingBlocks.Application.Events.Result;
using Mediator;

namespace Blog.BuildingBlocks.Application.CQRS.Command
{
    public interface ICommandHandler<in TCommand,TResponse> :IRequestHandler<TCommand,OperationResult<TResponse>>
        where TCommand: ICommand<TResponse>;
}
