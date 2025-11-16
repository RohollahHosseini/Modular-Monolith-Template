using Blog.BuildingBlocks.Application.CQRS.Command;
using Blog.BuildingBlocks.Infrastrocture.Serialization;
using Blog.Modules.LogSystem.Application.Proccessing.InternalCommand;
using Blog.Modules.LogSystem.Domain.Log;
using Newtonsoft.Json;

namespace Blog.Modules.LogSystem.Infrastrocture.Proccessing.InternalCommand
{
    public class LogCommandsSchedulers(LogDbContext logDbContext) : ILogCommandsScheduler
    {
        public Task EnqueueAsync<T>(ICommand<T> command)
        {
            var data = JsonConvert.SerializeObject(command, new JsonSerializerSettings
            {
                ContractResolver = new AllPropertiesContractResolver()
            });

            var internalCommandEntity = LogInternalCommandEntity.CreateInternamCommand(command.Id,DateTime.UtcNow, command.GetType().FullName,data,DateTime.UtcNow);

            logDbContext.InternalCommands.Add(internalCommandEntity);

            logDbContext.SaveChangesAsync();
            
            return Task.CompletedTask;
        }

        public Task EnqueueAsync(ICommand command)
        {
            throw new NotImplementedException();
        }
    }
}
