using Blog.BuildingBlocks.Application.Events.Result;
using MediatR;

namespace Blog.BuildingBlocks.Application.CQRS.Command
{
    public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, OperationResult<TResponse>>
        where TCommand : ICommand<TResponse>;

    public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand>
      where TCommand : ICommand
    {
    }
}
