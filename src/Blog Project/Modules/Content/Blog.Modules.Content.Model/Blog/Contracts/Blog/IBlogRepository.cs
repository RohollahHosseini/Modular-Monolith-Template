namespace Blog.Modules.Content.Model.Blog.Contracts.Blog
{
    public  interface IBlogRepository
    {
        Task AddBlogAsync(BlogEntity blogEntity); 
    }
}
