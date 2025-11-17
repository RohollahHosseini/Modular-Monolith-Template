using Blog.BuildingBlocks.Application.Events.Result;
using MediatR;

namespace Blog.BuildingBlocks.Application.CQRS.Command
{
    public interface ICommand : IRequest
    {
        Guid Id { get; }
    }
    public interface ICommand<TResponse> : IRequest<OperationResult<TResponse>>
    {
        Guid Id { get; }
    }
}
