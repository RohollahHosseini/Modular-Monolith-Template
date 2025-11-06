using Blog.BuildingBlocks.Application.Events.Result;
using Blog.BuildingBlocks.Infrastrocture;
using Blog.Modules.Content.Model.Blog;
using Blog.Modules.Content.Model.Blog.Contracts.Blog;

namespace Blog.Modules.Content.Application.Features.Blog.Commands
{
    public class CreateBlogCommandHandler(IBlogRepository blogRepository,IUnitOfWork unitOfWork): BuildingBlocks.Application.CQRS.Command.ICommandHandler<CreateBlogCommand, bool>
    {
        public async ValueTask<OperationResult<bool>> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
        {
            BlogEntity blog = new();
            blog.Create(request.BlogTitle, blog.BlogContent, Guid.NewGuid());

           await blogRepository.AddBlogAsync(blog);

            await unitOfWork.CommitAsync(cancellationToken);

            return OperationResult<bool>.SuccessResult(true);
        }
    }
}
