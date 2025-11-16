using Blog.BuildingBlocks.Application.CQRS.Command;
using Blog.BuildingBlocks.Application.Events.Result;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Modules.Content.Infrastrocture.Configuration.Proccessing
{
    public static class CommandsExecutor
    {
        private static IServiceScopeFactory _serviceScopeFactory;

        // این متد را باید در Startup یا Program.cs یک‌بار مقداردهی کنیم
        public static void Configure(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public static async Task Execute(ICommand command)
        {

            //if (_serviceScopeFactory is null)
            //    throw new InvalidOperationException("CommandsExecutor is not configured. Call Configure() at startup.");

            using var scope = _serviceScopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            await mediator.Send(command);
        }

        public static async Task<OperationResult<TResult>> Execute<TResult>(ICommand<TResult> command)
        {
            if (_serviceScopeFactory is null)
                throw new InvalidOperationException("CommandsExecutor is not configured. Call Configure() at startup.");

            using var scope = _serviceScopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            return await mediator.Send(command);
        }

    }
}
