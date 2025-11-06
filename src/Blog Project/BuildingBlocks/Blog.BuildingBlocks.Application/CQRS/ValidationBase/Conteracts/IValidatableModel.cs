using FluentValidation;

namespace Blog.BuildingBlocks.Application.CQRS.ValidationBase.Conteracts
{
    public interface IValidatableModel<TApplicationModel>where TApplicationModel : class
    {
        IValidator<TApplicationModel> ValidateApplicationModel(ApplicationBaseValidationModelProvider<TApplicationModel> validator);
    }
}
