using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Blog.Modules.Content.Infrastrocture
{
    public class ContentDbContextFactory : IDesignTimeDbContextFactory<ContentDbcontext>
    {
        public ContentDbcontext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();

            var connectionString = configuration.GetConnectionString("ModularMonolithConnectionString");

            var builder = new DbContextOptionsBuilder<ContentDbcontext>();
            builder.UseSqlServer(connectionString);

            return new ContentDbcontext(builder.Options);
        }
    }
}
