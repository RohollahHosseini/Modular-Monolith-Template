using Blog.BuildingBlocks.Application.Events.Result;
using Blog.BuildingBlocks.Infrastrocture;
using Blog.Modules.Content.Application.Contracts.UnitOfWork;
using Blog.Modules.Content.Model.Blog;
using Blog.Modules.Content.Model.Contracts.Blog;
using MediatR;

namespace Blog.Modules.Content.Application.Features.Blog.Commands
{
    public class CreateBlogCommandHandler(IBlogRepository blogRepository,IContentUnitOfWork unitOfWork): BuildingBlocks.Application.CQRS.Command.ICommandHandler<CreateBlogCommand, bool>
    {
        async Task<OperationResult<bool>> IRequestHandler<CreateBlogCommand, OperationResult<bool>>.Handle(CreateBlogCommand request, CancellationToken cancellationToken)
        {
            BlogEntity blog = new();
            var newBlog= blog.Create(request.BlogTitle, request.BlogContent, request.CategoryId);

            await blogRepository.AddBlogAsync(newBlog);

            await unitOfWork.CommitAsync(cancellationToken);

            return OperationResult<bool>.SuccessResult(true);
        }
    }
}
