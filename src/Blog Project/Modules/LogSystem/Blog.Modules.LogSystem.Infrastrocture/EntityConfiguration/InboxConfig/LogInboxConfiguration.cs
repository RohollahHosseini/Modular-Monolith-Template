using Blog.BuildingBlocks.Infrastrocture.Inbox;
using Blog.Modules.LogSystem.Infrastrocture.EntityConfiguration.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Modules.LogSystem.Infrastrocture.EntityConfiguration.InboxConfig
{
    internal class LogInboxConfiguration : IEntityTypeConfiguration<InboxMessage>
    {
        public void Configure(EntityTypeBuilder<InboxMessage> builder)
        {
            builder.ToTable("Inbox", BaseSchema.LogSchema).HasKey(c => c.Id);

            builder.Property(c => c.Id).IsRequired();
            builder.Property(c => c.OccurredOn).IsRequired();
            builder.Property(c => c.Type)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(c=>c.Data).IsRequired();
        }
    }
}
