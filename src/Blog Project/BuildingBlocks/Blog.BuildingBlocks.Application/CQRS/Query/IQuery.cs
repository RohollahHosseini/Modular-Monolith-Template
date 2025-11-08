using Blog.BuildingBlocks.Application.Events.Result;
using MediatR;

namespace Blog.BuildingBlocks.Application.CQRS.Query
{
    public interface IQuery<TResponse>:IRequest<OperationResult<TResponse>>;
    //public interface IQuery<TResponse>:Mediator.IQuery<OperationResult<TResponse>>;
}
