using Blog.Modules.LogSystem.Domain.Log;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Modules.LogSystem.Infrastrocture.Configuration.LogInternalCommandConfig
{
    internal class LogInternalCommandConfiguration : IEntityTypeConfiguration<LogInternalCommandEntity>
    {
        public void Configure(EntityTypeBuilder<LogInternalCommandEntity> builder)
        {
            builder.ToTable("IntenalCommand","LogSystem").HasKey(t => t.Id);

            builder.Property(c => c.Id).IsRequired();
            builder.Property(c => c.EnqueueDate).IsRequired();
            builder.Property(c => c.Type).IsRequired()
                .HasMaxLength(255);
            builder.Property(c => c.Data).IsRequired();
        }
    }
}
