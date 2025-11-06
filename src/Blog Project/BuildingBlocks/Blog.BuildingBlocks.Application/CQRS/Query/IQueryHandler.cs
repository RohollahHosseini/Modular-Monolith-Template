using Blog.BuildingBlocks.Application.Events.Result;
using Mediator;

namespace Blog.BuildingBlocks.Application.CQRS.Query
{
    public interface IQueryHandler<in TQuery,TResponse>:IRequestHandler<TQuery, OperationResult<TResponse>>
        where TQuery : IQuery<TResponse>;
}
