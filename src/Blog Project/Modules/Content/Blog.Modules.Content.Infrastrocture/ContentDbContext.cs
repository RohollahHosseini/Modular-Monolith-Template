using Blog.BuildingBlocks.Infrastrocture.Outbox;
using Blog.Modules.Content.Infrastrocture.Configuration.BlogConfig;
using Blog.Modules.Content.Infrastrocture.Configuration.CategoryConfig;
using Blog.Modules.Content.Infrastrocture.Outbox;
using Microsoft.EntityFrameworkCore;

namespace Blog.Modules.Content.Infrastrocture
{
    public class ContentDbcontext(DbContextOptions<ContentDbcontext> option):DbContext(option)
    {
        public DbSet<OutboxMessage> OutboxMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BlogConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new OutboxMessageEntityTypeConfiguration());

        }


    }
}
