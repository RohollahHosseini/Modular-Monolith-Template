using Blog.Modules.LogSystem.Domain.Log;
using Microsoft.EntityFrameworkCore;

namespace Blog.Modules.LogSystem.Infrastrocture
{
    public class LogDbContext(DbContextOptions<LogDbContext> option):DbContext(option)
    {
        public DbSet<LogEntity> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LogEntity>().HasKey(c=>c.Id);
        }

    }
}
