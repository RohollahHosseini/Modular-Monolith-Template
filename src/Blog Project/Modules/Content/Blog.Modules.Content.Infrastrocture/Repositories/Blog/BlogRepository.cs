using Blog.BuildingBlocks.Infrastrocture.BaseRespository;
using Blog.Modules.Content.Model.Blog;
using Blog.Modules.Content.Model.Contracts.Blog;

namespace Blog.Modules.Content.Infrastrocture.Repositories.Blog
{
    internal class BlogRepository(ContentDbcontext dbcontext) : BaseAsyncRepository<BlogEntity, ContentDbcontext>(dbcontext), IBlogRepository
    {
        public async Task AddBlogAsync(BlogEntity blogEntity)
        {
            await AddAsync(blogEntity);
        }
    }
}
