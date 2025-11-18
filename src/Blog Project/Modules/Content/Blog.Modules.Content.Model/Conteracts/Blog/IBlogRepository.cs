using Blog.Modules.Content.Model.Blog;

namespace Blog.Modules.Content.Model.Conteracts.Blog
{
    public interface IBlogRepository
    {
        Task AddBlogAsync(BlogEntity blogEntity);
    }
}
