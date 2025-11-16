using Blog.BuildingBlocks.Application.CQRS.Command;
using MediatR;

namespace Blog.Modules.Content.Infrastrocture.Configuration.Proccessing.Inbox
{
    internal class ProcessInboxCommandHandler(ContentDbcontext dbcontext,IMediator mediator) : ICommandHandler<ProcessInboxCommand>
    {
        public Task Handle(ProcessInboxCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
