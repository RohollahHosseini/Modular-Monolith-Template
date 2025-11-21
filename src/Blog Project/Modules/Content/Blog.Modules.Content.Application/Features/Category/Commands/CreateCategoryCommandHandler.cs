using Blog.BuildingBlocks.Application.CQRS.Command;
using Blog.BuildingBlocks.Application.Events.Result;
using Blog.Modules.Content.Application.Contracts.UnitOfWork;
using Blog.Modules.Content.Model.Category;
using Blog.Modules.Content.Model.Contracts.Category;

namespace Blog.Modules.Content.Application.Features.Category.Commands
{
    internal class CreateCategoryCommandHandler(ICategoryRepository categoryRepository,IContentUnitOfWork contentUnitOfWork) : ICommandHandler<CreateCategoryCommand, bool>
    {
        public async Task<OperationResult<bool>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            CategoryEntity categoryEntity = new();

            var newCategory=categoryEntity.CreateCategory(request.Title, request.Description, request.parentCategoryId);

            await categoryRepository.AddCategoryAsync(newCategory, cancellationToken);

            await contentUnitOfWork.CommitAsync(cancellationToken);

            return OperationResult<bool>.SuccessResult(true);

        }
    }
}
