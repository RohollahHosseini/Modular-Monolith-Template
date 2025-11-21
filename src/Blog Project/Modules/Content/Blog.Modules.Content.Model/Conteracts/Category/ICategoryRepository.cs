using Blog.Modules.Content.Model.Category;

namespace Blog.Modules.Content.Model.Contracts.Category
{
    public interface ICategoryRepository
    {
        Task AddCategoryAsync(CategoryEntity category, CancellationToken cancellationToken = default);
    }
}
