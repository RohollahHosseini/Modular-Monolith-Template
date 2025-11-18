using Blog.BuildingBlocks.Infrastrocture;
using Blog.BuildingBlocks.Infrastrocture.DomainEventsDispatching;
using Blog.BuildingBlocks.Infrastrocture.InternalCommands;
using Blog.BuildingBlocks.Peresentation.Endpoints;
using Blog.Modules.LogSystem.Application.Contracts.UnitOfWork;
using Blog.Modules.LogSystem.Application.EventHandler.Content.Blog;
using Blog.Modules.LogSystem.Application.Features;
using Blog.Modules.LogSystem.Application.Features.Category;
using Blog.Modules.LogSystem.Application.Proccessing.InternalCommand;
using Blog.Modules.LogSystem.Domain.Log.Repository;
using Blog.Modules.LogSystem.Infrastrocture.Configuration.EventsBus;
using Blog.Modules.LogSystem.Infrastrocture.Configuration.Proccessing;
using Blog.Modules.LogSystem.Infrastrocture.Configuration.Proccessing.Inbox;
using Blog.Modules.LogSystem.Infrastrocture.Configuration.Proccessing.InternalCommands;
using Blog.Modules.LogSystem.Infrastrocture.Configuration.Proccessing.Quartz;
using Blog.Modules.LogSystem.Infrastrocture.Repository;
using Blog.Modules.LogSystem.Infrastrocture.UnitOfWork;
using Blog.Modules.LogSystem.Peresentation;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Blog.Modules.LogSystem.Infrastrocture.ServiceConfiguration
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection LogSystemServiceCollactionExtensions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddInfrastrocture(configuration);
            services.AddEndpoints(AssemblyReference.Assembly);

            //quartz
            services.AddQuartz(c =>
            {
            });

            services.AddQuartzHostedService(options =>
            {
                options.WaitForJobsToComplete = true;
            });



            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(ProcessInboxCommandHandler).Assembly);
            });


            return services;
        }

        private static void AddInfrastrocture(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<LogDbContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("ModularMonolithConnectionString"));
            });
            services.AddScoped<IDomainEventsAccessor, DomainEventsAccessor<LogDbContext>>();
            services.AddScoped<ILogUnitOfWork, LogUnitOfWork>();
            services.AddScoped<ILogRepository, LogRepository>();
            services.AddScoped<ILogCommandsScheduler, LogCommandsScheduler>();
            services.AddScoped<EventsBusStartup>();

            BiDictionary<string, Type> internalCommandsMap = new BiDictionary<string, Type>();
            internalCommandsMap.Add("CreateLog", typeof(CreateLogCommand));
            internalCommandsMap.Add("CreateLogForCategoryCommand", typeof(CreateLogForCategoryCommand));
            services.AddSingleton<IInternalCommandsMapper>(new InternalCommandsMapper(internalCommandsMap));

        }


        public static void LogSystemServiceScopExtensions(this WebApplication app, IServiceScopeFactory services, IConfiguration configuration)
        {

            using var scope = app.Services.CreateScope();

            var startup = scope.ServiceProvider.GetRequiredService<EventsBusStartup>();

            startup.Initialize();


            CommandsExecutor.Configure(services);


            var quartzIntervalTime = configuration.GetSection("QuartzConfig:LogSystemModule").Value.ToString();
            QuartzStartup.Initialize(long.Parse(quartzIntervalTime!));

        }

    }
}
