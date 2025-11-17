using Blog.Modules.Content.Infrastrocture.EntityConfiguration.Schema;
using Blog.Modules.Content.Model.Blog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Blog.Modules.Content.Infrastrocture.EntityConfiguration.BlogConfig
{
    internal class BlogConfiguration : IEntityTypeConfiguration<BlogEntity>
    {
        public void Configure(EntityTypeBuilder<BlogEntity> builder)
        {
            builder.ToTable("Blogs", BaseSchema.ContentSchema).Property(c => c.Id).HasColumnName("BlogId");

            builder.Property(c => c.BlogTitle).HasMaxLength(100).IsRequired(true);

            builder.Property(c => c.BlogContent).IsRequired(true);

            builder.Property(c => c.Slug).HasMaxLength(100).IsRequired(true);

            builder.Property(c => c.CurrentState).HasConversion<EnumToStringConverter<BlogState>>();

            builder.HasIndex(c => c.BlogTitle).IsUnique();
            builder.HasIndex(c => c.Id);

            builder.HasQueryFilter(c => !c.IsDeleted);


            #region Navigation Property

            builder.HasOne(c => c.Category)
                .WithMany(c => c.Blogs)
                .HasForeignKey(c => c.CategoryId);



            builder.OwnsMany(c => c.ChangeLogs, navigationBuilder =>
            {
                navigationBuilder.ToJson("ChangeLogs");
            });
            #endregion
        }
    }
}
