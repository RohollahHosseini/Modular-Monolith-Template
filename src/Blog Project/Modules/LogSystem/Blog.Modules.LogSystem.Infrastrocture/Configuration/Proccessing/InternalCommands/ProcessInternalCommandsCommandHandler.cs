using Blog.BuildingBlocks.Application.CQRS.Command;
using Blog.BuildingBlocks.Infrastrocture.Configuration.Proccessing.InternalCommand;
using Blog.BuildingBlocks.Infrastrocture.InternalCommands;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Polly;

namespace Blog.Modules.LogSystem.Infrastrocture.Configuration.Proccessing.InternalCommands
{
    internal class ProcessInternalCommandsCommandHandler(LogDbContext dbContext, IInternalCommandsMapper internalCommandsMapper) : ICommandHandler<ProcessInternalCommandsCommand>
    {
        public async Task Handle(ProcessInternalCommandsCommand request, CancellationToken cancellationToken)
        {
            var commands = await dbContext.InternalCommands
                .Where(c => c.ProcessedDate == null)
                .OrderBy(c => c.EnqueueDate)
                .ToListAsync(cancellationToken);

            //mapping: LogInternalCommandEntity->InternalCommandDto
            var inteternalCommands = commands.Select(c => new InternalCommandDto()
            {
                Id = c.Id,
                Data = c.Data,
                Type = c.Type,
            }).ToList();

            //Use Polly
            var policy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(new[]
                {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(2),
                    TimeSpan.FromSeconds(3)
                });


            foreach (var internalCommand in inteternalCommands)
            {
                var result = await policy.ExecuteAndCaptureAsync(() => ProcessCommand(
                   internalCommand));

                if (result.Outcome == OutcomeType.Failure)
                {
                    await dbContext.InternalCommands
                        .Where(c=>c.Id == internalCommand.Id)
                        .ExecuteUpdateAsync(set=>
                                set.SetProperty(c=>c.ProcessedDate,DateTime.UtcNow)
                                   .SetProperty(c=>c.Error, result.FinalException.ToString())
                                   .SetProperty(c=>c.Id,internalCommand.Id));
                }
            }

            await dbContext.SaveChangesAsync();
        }

        private async Task ProcessCommand(
    InternalCommandDto internalCommand)
        {
            var type = internalCommandsMapper.GetType(internalCommand.Type);
            dynamic commandToProcess = JsonConvert.DeserializeObject(internalCommand.Data, type);

            await CommandsExecutor.Execute(commandToProcess);
        }

    }
}
