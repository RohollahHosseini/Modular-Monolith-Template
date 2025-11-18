using Blog.BuildingBlocks.Model;
using Blog.BuildingBlocks.Model.BusinessRule.PublicRules;
using Blog.Modules.Content.Model.Blog;
using Blog.Modules.Content.Model.Category.Events;

namespace Blog.Modules.Content.Model.Category
{
    public sealed class CategoryEntity:BaseEntity<Guid>
    {
        public string CategoryTitle { get; private set; }
        public string? Description { get; private set; }
        public Guid? ParentCategoryId { get; private set; }

        #region NavigationProperty
        public CategoryEntity? ParentCategory { get; set; }
        public ICollection<CategoryEntity>? SubCategories { get; set; }
        public ICollection<BlogEntity>? Blogs { get; set; }
        #endregion


        public CategoryEntity CreateCategory(string title,string? description ,Guid? parentCategoryId )
        {

            CheckRule(new StrignIsNullOrEmptyRule(title));

            var @categoryEntity= new CategoryEntity()
            {
                    Id = Guid.NewGuid(),
                    CategoryTitle=title,
                    Description=description,
                    ParentCategoryId=parentCategoryId,
            };

            //rais event
            @categoryEntity.Raise(new CategoryCreatedDomainEvent(
                                       @categoryEntity.Id,                                                           @categoryEntity.CategoryTitle));

            return @categoryEntity;
        }

        public void Edit(string title, string? description, Guid? parentCategoryId)
        {
            CategoryTitle = title;
            Description = description;
            ParentCategoryId=parentCategoryId;
        }

    }
}
