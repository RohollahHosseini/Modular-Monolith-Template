using Blog.BuildingBlocks.Application.CQRS.Command;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Blog.Modules.LogSystem.Infrastrocture.Configuration.Proccessing.Inbox
{
    internal class ProcessInboxCommandHandler(LogDbContext dbcontext, IMediator mediator) : ICommandHandler<ProcessInboxCommand>
    {
        public async Task Handle(ProcessInboxCommand request, CancellationToken cancellationToken)
        {
            var logSystemInboxes = await dbcontext.InboxMessages
                .Where(c => c.ProcessedDate != null).ToListAsync();

            foreach (var inbox in logSystemInboxes)
            {
                var messageAssembly = AppDomain.CurrentDomain.GetAssemblies()
                  .SingleOrDefault(assembly => inbox.Type.Contains(assembly.GetName().Name));

                Type type = messageAssembly.GetType(inbox.Type);
                var req = JsonConvert.DeserializeObject(inbox.Data, type);


                try
                {
                    await mediator.Publish((INotification)request, cancellationToken);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

                await dbcontext.InboxMessages
                      .Where(c => c.Id == inbox.Id).ExecuteUpdateAsync(set => set.SetProperty(c => c.ProcessedDate, DateTime.UtcNow));

            }

            await dbcontext.SaveChangesAsync();
        }
    }
}
