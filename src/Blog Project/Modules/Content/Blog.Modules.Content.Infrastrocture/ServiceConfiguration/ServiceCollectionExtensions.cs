using Blog.Modules.Content.Infrastrocture.Repositories.Blog;
using Blog.Modules.Content.Model.Blog.Contracts.Blog;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Blog.BuildingBlocks.Peresentation.Endpoints;
using Blog.Modules.Content.Peresentation;
using Blog.BuildingBlocks.Infrastrocture;
using Blog.BuildingBlocks.Infrastrocture.Outbox;
using Blog.Modules.Content.Infrastrocture.Outbox;
using Blog.BuildingBlocks.Infrastrocture.DomainEventsDispatching;
using Blog.Modules.Content.Infrastrocture.UnitOfWork;
using Blog.Modules.Content.Application.Conteracts.UnitOfWork;
using Blog.Modules.Content.Model.Blog.Events;
using Blog.Modules.Content.Application.EventNotification.Content;

namespace Blog.Modules.Content.Infrastrocture.ServiceConfiguration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection BlogServiceCollactionExtensions(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddInfrastrocture(configuration);

            services.AddEndpoints(AssemblyReference.Assembly);

            var domainNotificationsMap = new BiDictionary<string, Type>();
            domainNotificationsMap.Add(nameof(BlogCreatedDomainEvent), typeof(CreateBlogNotification));

            services.AddSingleton(domainNotificationsMap);

            return services;
        }


        private static void AddInfrastrocture(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<ContentDbcontext>(option => 
            {
                option.UseSqlServer(configuration.GetConnectionString("ModularMonolithConnectionString"));
            });
            services.AddSingleton(new BiDictionary<string, Type>());
            services.AddScoped<IDomainNotificationsMapper, DomainNotificationsMapper>();
            services.AddScoped<IDomainEventsAccessor,DomainEventsAccessor<ContentDbcontext>>();

            services.AddScoped<IOutbox, OutboxAccessor>();
            services.AddScoped<IContentUnitOfWork, ContentUnitOfWork>();

            services.AddScoped<IBlogRepository, BlogRepository>();
        }

    }
}
