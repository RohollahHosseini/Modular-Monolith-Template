using Blog.BuildingBlocks.Application.CQRS.Command;
using Blog.BuildingBlocks.Application.CQRS.ValidationBase;
using Blog.BuildingBlocks.Application.CQRS.ValidationBase.Contracts;
using FluentValidation;

namespace Blog.Modules.Content.Application.Features.Category.Commands
{
    public record CreateCategoryCommand(string Title, string? Description, Guid? parentCategoryId = null) : ICommand<bool>,IValidatableModel<CreateCategoryCommand>
    {
        public Guid Id { get; } = Guid.NewGuid();

        public IValidator<CreateCategoryCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateCategoryCommand> validator)
        {
            validator.RuleFor(c => c.Title)
                .NotEmpty().WithMessage("Title is required.");
           
            return validator;
        }
    }
}
