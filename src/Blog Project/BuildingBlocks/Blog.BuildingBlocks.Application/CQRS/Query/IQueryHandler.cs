using Blog.BuildingBlocks.Application.Events.Result;
using MediatR;

namespace Blog.BuildingBlocks.Application.CQRS.Query
{
    public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, OperationResult<TResponse>>
        where TQuery : IQuery<TResponse>;
    //public interface IQueryHandlers<in TQuery,TResponse>:IQueryHandler<TQuery, OperationResult<TResponse>>
    //    where TQuery : IQuery<TResponse>; 
}
