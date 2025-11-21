using Blog.BuildingBlocks.Infrastrocture.BaseRespository;
using Blog.Modules.Content.Model.Category;
using Blog.Modules.Content.Model.Contracts.Category;

namespace Blog.Modules.Content.Infrastrocture.Repositories.Category
{
    internal class CategoryRepository(ContentDbcontext dbcontext) : BaseAsyncRepository<CategoryEntity, ContentDbcontext>(dbcontext), ICategoryRepository
    {
        public async  Task AddCategoryAsync(CategoryEntity category, CancellationToken cancellationToken = default)
        {
            await AddAsync(category,cancellationToken);
        }
    }
}
