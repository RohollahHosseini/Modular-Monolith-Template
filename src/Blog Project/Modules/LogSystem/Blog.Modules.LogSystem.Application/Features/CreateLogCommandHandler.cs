using Blog.BuildingBlocks.Application.CQRS.Command;
using Blog.BuildingBlocks.Application.Events.Result;
using Blog.BuildingBlocks.Infrastrocture;
using Blog.Modules.LogSystem.Application.Contracts.UnitOfWork;
using Blog.Modules.LogSystem.Domain.Log;
using Blog.Modules.LogSystem.Domain.Log.Repository;

namespace Blog.Modules.LogSystem.Application.Features
{
    internal class CreateLogCommandHandler(ILogRepository logRepository, ILogUnitOfWork unitOfWork) : ICommandHandler<CreateLogCommand,bool>
    {
        public async Task<OperationResult<bool>> Handle(CreateLogCommand request, CancellationToken cancellationToken)
        {
            LogEntity logEntity = new()
            {
                LogDescription = request.Description
            };

            await logRepository.AddLogAsync(logEntity);

            var resultTransaction = await unitOfWork.CommitAsync();

            return OperationResult<bool>.SuccessResult(true);
        }
    }
}
