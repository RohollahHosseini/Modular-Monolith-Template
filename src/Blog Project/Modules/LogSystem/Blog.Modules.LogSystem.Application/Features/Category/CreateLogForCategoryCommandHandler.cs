using Blog.BuildingBlocks.Application.CQRS.Command;
using Blog.BuildingBlocks.Application.Events.Result;
using Blog.Modules.LogSystem.Application.Contracts.UnitOfWork;
using Blog.Modules.LogSystem.Domain.Log;
using Blog.Modules.LogSystem.Domain.Log.Repository;

namespace Blog.Modules.LogSystem.Application.Features.Category
{
    internal class CreateLogForCategoryCommandHandler(ILogRepository logRepository, ILogUnitOfWork unitOfWork) : ICommandHandler<CreateLogForCategoryCommand, bool>
    {
        public async Task<OperationResult<bool>> Handle(CreateLogForCategoryCommand request, CancellationToken cancellationToken)
        {
            LogEntity logEntity = new()
            {
                LogDescription = request.Description
            };

            await logRepository.AddLogAsync(logEntity);

            var resultTransaction = await unitOfWork.CommitAsync(cancellationToken);

            return OperationResult<bool>.SuccessResult(resultTransaction is >0 ?true:false);
        }
    }
}
