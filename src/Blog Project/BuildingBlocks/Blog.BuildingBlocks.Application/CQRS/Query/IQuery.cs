using Blog.BuildingBlocks.Application.Events.Result;
using Mediator;

namespace Blog.BuildingBlocks.Application.CQRS.Query
{
    public interface IQuery<TResponse>:IRequest<OperationResult<TResponse>>;
}
