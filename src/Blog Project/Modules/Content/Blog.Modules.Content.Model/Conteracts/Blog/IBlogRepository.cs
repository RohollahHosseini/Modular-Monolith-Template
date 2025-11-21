using Blog.Modules.Content.Model.Blog;

namespace Blog.Modules.Content.Model.Contracts.Blog
{
    public interface IBlogRepository
    {
        Task AddBlogAsync(BlogEntity blogEntity);
    }
}
