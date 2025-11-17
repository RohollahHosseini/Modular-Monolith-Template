using Blog.Modules.Content.Infrastrocture;
using Blog.Modules.LogSystem.Infrastrocture;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Web.WebFramwork
{
    public static class MigrationExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            ApplyMigration<ContentDbcontext>(scope);
            ApplyMigration<LogDbContext>(scope);
        }

        private static void ApplyMigration<TDbContext>(IServiceScope scope)
        where TDbContext : DbContext
        {
            using TDbContext context = scope.ServiceProvider.GetRequiredService<TDbContext>();

            context.Database.Migrate();
        }
    }
}
