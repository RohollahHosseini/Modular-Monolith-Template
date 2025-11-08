using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Blog.Modules.LogSystem.Infrastrocture
{
    public class LogDbContextFactory : IDesignTimeDbContextFactory<LogDbContext>
    {
        public LogDbContext CreateDbContext(string[] args)
        {

            var configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();

            var connectionString = configuration.GetConnectionString("ModularMonolithConnectionString");

            var builder = new DbContextOptionsBuilder<LogDbContext>();
            builder.UseSqlServer(connectionString);

            return new LogDbContext(builder.Options);

        }
    }
}
