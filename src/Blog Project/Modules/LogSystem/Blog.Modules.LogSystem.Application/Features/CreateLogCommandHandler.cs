using Blog.BuildingBlocks.Application.Events.Result;
using Blog.BuildingBlocks.Infrastrocture;
using Blog.Modules.LogSystem.Domain.Log;
using Blog.Modules.LogSystem.Domain.Log.Repository;

namespace Blog.Modules.LogSystem.Application.Features
{
    internal class CreateLogCommandHandler(ILogRepository logRepository,IUnitOfWork  unitOfWork) : BuildingBlocks.Application.CQRS.Command.ICommandHandler<CreateLogCommand, bool>
    {
        public async ValueTask<OperationResult<bool>> Handle(CreateLogCommand request, CancellationToken cancellationToken)
        {

            LogEntity logEntity = new()
            {
                LogDescription = request.Description
            };

            await logRepository.AddLogAsync(logEntity);

            var resultTransaction= await unitOfWork.CommitAsync();

            return OperationResult<bool>.SuccessResult(true);
        }
    }
}
