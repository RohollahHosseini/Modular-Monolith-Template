using Blog.BuildingBlocks.Application.Events.Result;
using MediatR;

namespace Blog.BuildingBlocks.Application.CQRS.Command
{
    //public interface ICommand<TResponse>:IRequest<OperationResult<TResponse>>;
    public interface ICommand<TResponse>:IRequest<OperationResult<TResponse>>;
}
