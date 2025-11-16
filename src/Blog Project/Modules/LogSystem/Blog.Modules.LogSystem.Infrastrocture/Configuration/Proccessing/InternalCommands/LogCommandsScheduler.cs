using Blog.BuildingBlocks.Application.CQRS.Command;
using Blog.BuildingBlocks.Application.Proccessing.InternalCommand;
using Blog.BuildingBlocks.Infrastrocture.InternalCommands;
using Blog.BuildingBlocks.Infrastrocture.Serialization;
using Blog.Modules.LogSystem.Application.Proccessing.InternalCommand;
using Blog.Modules.LogSystem.Domain.Log;
using Newtonsoft.Json;

namespace Blog.Modules.LogSystem.Infrastrocture.Configuration.Proccessing.InternalCommands
{
    public class LogCommandsScheduler(LogDbContext dbContext, IInternalCommandsMapper internalCommandsMapper) : ILogCommandsScheduler
    {
        public async Task EnqueueAsync(ICommand command)
        {

            var intenamCommand = LogInternalCommandEntity.CreateInternamCommand(
                Id: command.Id,
                EnqueueDate: DateTime.UtcNow,
                Type: internalCommandsMapper.GetName(command.GetType()),
                Data: JsonConvert.SerializeObject(command, new JsonSerializerSettings
                {
                    ContractResolver = new AllPropertiesContractResolver()
                }));

            await dbContext.InternalCommands.AddAsync(intenamCommand);

            //await dbContext.SaveChangesAsync();
        }

        public async Task EnqueueAsync<T>(ICommand<T> command)
        {

            var intenamCommand = LogInternalCommandEntity.CreateInternamCommand(
                Id: command.Id,
                EnqueueDate: DateTime.UtcNow,
                Type: internalCommandsMapper.GetName(command.GetType()),
                Data: JsonConvert.SerializeObject(command, new JsonSerializerSettings
                {
                    ContractResolver = new AllPropertiesContractResolver()
                }));

            await dbContext.InternalCommands.AddAsync(intenamCommand);
            //await dbContext.SaveChangesAsync();
        }
    }
}
