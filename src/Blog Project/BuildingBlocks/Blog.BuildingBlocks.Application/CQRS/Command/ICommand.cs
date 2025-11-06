using Blog.BuildingBlocks.Application.Events.Result;
using Mediator;

namespace Blog.BuildingBlocks.Application.CQRS.Command
{
    public interface ICommand<TResponse>:IRequest<OperationResult<TResponse>>;
}
