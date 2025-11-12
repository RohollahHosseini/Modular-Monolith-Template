using Blog.BuildingBlocks.Infrastrocture;
using Blog.BuildingBlocks.Infrastrocture.DomainEventsDispatching;
using Blog.BuildingBlocks.Peresentation.Endpoints;
using Blog.Modules.LogSystem.Application.Contracts.UnitOfWork;
using Blog.Modules.LogSystem.Application.Proccessing.InternalCommand;
using Blog.Modules.LogSystem.Domain.Log.Repository;
using Blog.Modules.LogSystem.Infrastrocture.Proccessing.InternalCommand;
using Blog.Modules.LogSystem.Infrastrocture.Repository;
using Blog.Modules.LogSystem.Infrastrocture.UnitOfWork;
using Blog.Modules.LogSystem.Peresentation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Modules.LogSystem.Infrastrocture.ServiceConfiguration
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection LogSystemServiceCollactionExtensions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddInfrastrocture(configuration);

            services.AddEndpoints(AssemblyReference.Assembly);


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
        }

    }
}
