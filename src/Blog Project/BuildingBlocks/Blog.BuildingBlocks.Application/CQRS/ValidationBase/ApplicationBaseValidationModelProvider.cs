using FluentValidation;

namespace Blog.BuildingBlocks.Application.CQRS.ValidationBase
{
    public class ApplicationBaseValidationModelProvider<TApplicationModel>:AbstractValidator<TApplicationModel>;
}
