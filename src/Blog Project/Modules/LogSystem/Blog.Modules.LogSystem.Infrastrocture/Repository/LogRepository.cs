using Blog.BuildingBlocks.Infrastrocture.BaseRespository;
using Blog.Modules.LogSystem.Domain.Log;
using Blog.Modules.LogSystem.Domain.Log.Repository;

namespace Blog.Modules.LogSystem.Infrastrocture.Repository
{
    internal class LogRepository(LogDbContext dbContext) : BaseAsyncRepository<LogEntity, LogDbContext>(dbContext), ILogRepository
    {
        public async Task AddLogAsync(LogEntity logEntity)
        {
            await base.AddAsync(logEntity);
        }
    }
}
