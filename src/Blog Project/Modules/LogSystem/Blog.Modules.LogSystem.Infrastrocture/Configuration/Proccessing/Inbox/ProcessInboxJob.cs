using Quartz;

namespace Blog.Modules.LogSystem.Infrastrocture.Configuration.Proccessing.Inbox
{
    [DisallowConcurrentExecution]

    public class ProcessInboxJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await CommandsExecutor.Execute(new ProcessInboxCommand());
        }
    }
}
