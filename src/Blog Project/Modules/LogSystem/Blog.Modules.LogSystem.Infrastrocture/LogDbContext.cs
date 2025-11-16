using Blog.BuildingBlocks.Infrastrocture.Inbox;
using Blog.Modules.LogSystem.Domain.Log;
using Blog.Modules.LogSystem.Infrastrocture.EntityConfiguration.InboxConfig;
using Blog.Modules.LogSystem.Infrastrocture.EntityConfiguration.LogInternalCommandConfig;
using Microsoft.EntityFrameworkCore;

namespace Blog.Modules.LogSystem.Infrastrocture
{
    public class LogDbContext(DbContextOptions<LogDbContext> option):DbContext(option)
    {
        public DbSet<LogEntity> Logs { get; set; }
        public DbSet<LogInternalCommandEntity> InternalCommands { get; set; }
        public DbSet<InboxMessage> InboxMessages{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LogEntity>().HasKey(c=>c.Id);

            modelBuilder.ApplyConfiguration(new LogInternalCommandConfiguration());
            modelBuilder.ApplyConfiguration(new LogInboxConfiguration());

        }

    }
}
