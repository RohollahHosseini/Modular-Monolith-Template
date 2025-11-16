using Blog.Modules.Content.Infrastrocture.EntityConfiguration.Schema;
using Blog.Modules.Content.Model.Category;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Modules.Content.Infrastrocture.EntityConfiguration.CategoryConfig
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<CategoryEntity>
    {
        public void Configure(EntityTypeBuilder<CategoryEntity> builder)
        {
            builder.ToTable("Categories", BaseSchema.ContentSchema).Property(c => c.Id).HasColumnName("CategoryId");
        }
    }
}
